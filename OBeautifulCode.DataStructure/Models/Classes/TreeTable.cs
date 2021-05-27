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
        /// <param name="tableRows">OPTIONAL rows of the table.  DEFAULT is no rows.</param>
        /// <param name="format">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        public TreeTable(
            TableColumns tableColumns,
            TableRows tableRows = null,
            TableFormat format = null)
        {
            if (tableColumns == null)
            {
                throw new ArgumentNullException(nameof(tableColumns));
            }

            var numberOfColumns = tableColumns.Columns.Count;

            if (tableRows != null)
            {
                if (tableRows.GetAllRowsInOrder().Any(_ => _.GetNumberOfColumnsSpanned() != numberOfColumns))
                {
                    throw new ArgumentException(Invariant($"{nameof(tableRows)} contains a row or descendant row that does not span all {numberOfColumns} of the defined columns."));
                }

                if ((tableRows.HeaderRows != null) && tableRows.HeaderRows.Rows.Any())
                {
                    if (tableRows.HeaderRows.Rows.Last().Cells.Count != numberOfColumns)
                    {
                        throw new ArgumentException(Invariant($"The last row in {nameof(tableRows)}.{nameof(this.TableRows.HeaderRows)} does not contain one cell for all {numberOfColumns} of the defined columns.  Spanning is disallowed for the last header row."));
                    }
                }
            }

            this.TableColumns = tableColumns;
            this.TableRows = tableRows;
            this.Format = format;
        }

        /// <summary>
        /// Gets the columns of the table.
        /// </summary>
        public TableColumns TableColumns { get; private set; }

        /// <summary>
        /// Gets the rows of the table.
        /// </summary>
        public TableRows TableRows { get; private set; }

        /// <summary>
        /// Gets the format to apply to the whole table.
        /// </summary>
        public TableFormat Format { get; private set; }
    }
}