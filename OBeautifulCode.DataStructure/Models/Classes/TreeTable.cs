// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeTable.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Linq;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A table (rows and columns) where cells can have arbitrary data, with parent-child relationships between rows
    /// along with various visualization (e.g. setting cell colors) and interaction (e.g. sorting) options.
    /// </summary>
    public partial class TreeTable : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeTable"/> class.
        /// </summary>
        /// <param name="tableColumns">The columns of the table.</param>
        /// <param name="headerRows">OPTIONAL header rows of the table.  DEFAULT is no header rows.</param>
        /// <param name="dataRows">OPTIONAL data rows of the table.  DEFAULT is no data rows.</param>
        /// <param name="format">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        public TreeTable(
            TableColumns tableColumns,
            HeaderRows headerRows = null,
            DataRows dataRows = null,
            TableFormat format = null)
        {
            if (tableColumns == null)
            {
                throw new ArgumentNullException(nameof(tableColumns));
            }

            var numberOfColumns = tableColumns.Columns.Count;

            if (headerRows != null)
            {
                if (headerRows.Rows.Any(_ => _.GetNumberOfColumnsSpanned() != numberOfColumns))
                {
                    throw new ArgumentException(Invariant($"{nameof(headerRows)} contains a row that does not span all {numberOfColumns} of the defined columns."));
                }

                if (headerRows.Rows.Last().Cells.Count != numberOfColumns)
                {
                    throw new ArgumentException(Invariant($"The last row in {nameof(headerRows)} does not contain one cell for all {numberOfColumns} of the defined columns.  Spanning is disallowed for the last header row."));
                }
            }

            if (dataRows != null)
            {
                if (dataRows.GetAllDataRowsInOrder().Any(_ => _.GetNumberOfColumnsSpanned() != numberOfColumns))
                {
                    throw new ArgumentException(Invariant($"{nameof(dataRows)} contains a row or descendant row that does not span all {numberOfColumns} of the defined columns."));
                }
            }

            this.TableColumns = tableColumns;
            this.HeaderRows = headerRows;
            this.DataRows = dataRows;
            this.Format = format;
        }

        /// <summary>
        /// Gets the columns of the table.
        /// </summary>
        public TableColumns TableColumns { get; private set; }

        /// <summary>
        /// Gets the header rows of the table.
        /// </summary>
        public HeaderRows HeaderRows { get; private set; }

        /// <summary>
        /// Gets the data rows of the table.
        /// </summary>
        public DataRows DataRows { get; private set; }

        /// <summary>
        /// Gets the format to apply to the whole table.
        /// </summary>
        public TableFormat Format { get; private set; }
    }
}