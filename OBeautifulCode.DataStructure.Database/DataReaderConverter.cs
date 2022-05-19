// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataReaderConverter.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using OBeautifulCode.Reflection.Recipes;
    using OBeautifulCode.String.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Converts a <see cref="IDataReader"/> to a <see cref="TreeTable"/>.
    /// </summary>
    public static class DataReaderConverter
    {
        /// <summary>
        /// Converts the specified <see cref="IDataReader"/> into a <see cref="TreeTable"/>.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <param name="context">OPTIONAL context to use.  DEFAULT is no context.</param>
        /// <returns>
        /// A tree table representation of the specified data reader.
        /// </returns>
        public static TreeTable ToTreeTable(
            this IDataReader reader,
            DataReaderToTreeTableConversionContext context = null)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            context = context ?? new DataReaderToTreeTableConversionContext();

            if (reader.FieldCount == 0)
            {
                throw new InvalidOperationException(Invariant($"The {nameof(reader)} has no fields.  A {nameof(TreeTable)} requires at least one column."));
            }

            var columnNames = Enumerable.Range(0, reader.FieldCount)
                .Select(reader.GetName)
                .ToList();

            // todo: add per-column format to context (use field index or field name?  duplicate names?)
            var columns = columnNames.Select(_ => new Column()).ToList();

            var tableColumns = new TableColumns(columns, context.ColumnsFormat);

            // todo: add per header cell value and cell format.
            var headerCells = columnNames.Select(_ => Cell.CreateConst(_)).ToList();

            var headerRow = new FlatRow(headerCells, format: context.HeaderRowFormat);

            var headerRows = new HeaderRows(new[] { headerRow });

            var rows = new List<Row>();

            while (reader.Read())
            {
                var row = reader.BuildRow(columnNames, context);

                rows.Add(row);
            }

            var dataRows = new DataRows(rows, context.DataRowsFormat);

            var tableRows = new TableRows(headerRows, dataRows, rowsFormat: context.RowsFormat);

            var result = new TreeTable(tableColumns, tableRows, context.TableFormat);

            return result;
        }

        private static Row BuildRow(
            this IDataReader reader,
            IReadOnlyList<string> columnNames,
            DataReaderToTreeTableConversionContext context)
        {
            var cells = new List<ICell>();

            for (var x = 0; x < columnNames.Count; x++)
            {
                ICell cell;

                var columnName = columnNames[x];

                if (reader.IsDBNull(x))
                {
                    CellFormat cellFormat = null;

                    if (columnName != null)
                    {
                        if (context.ColumnNameToNullValueCellFormatMap?.ContainsKey(columnName) ?? false)
                        {
                            cellFormat = context.ColumnNameToNullValueCellFormatMap[columnName];
                        }
                    }

                    cell = new NullCell(format: cellFormat);
                }
                else
                {
                    var value = reader[x];

                    if (context.ConvertValuesToPreferredInvariantString)
                    {
                        value = value.ToStringInvariantPreferred();
                    }

                    CellFormat cellFormat = null;

                    if (columnName != null)
                    {
                        if (context.ColumnNameToValueToCellFormatMap?.ContainsKey(columnName) ?? false)
                        {
                            var valueToCellFormatMap = context.ColumnNameToValueToCellFormatMap[columnName];

                            valueToCellFormatMap.TryGetValue(value, out cellFormat);
                        }
                    }

                    cell = (ICell)typeof(ConstCell<>).MakeGenericType(value.GetType()).Construct(value, null, null, null, null, null, Availability.Enabled, null, null, null, cellFormat, null, null);
                }

                cells.Add(cell);
            }

            // todo: ability to specify format of a specific row.
            var result = new Row(cells);

            return result;
        }
    }
}