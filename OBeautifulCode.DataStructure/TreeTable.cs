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
    /// A table (rows and columns) with parent-child relationships between rows
    /// along with various visualization (e.g. setting cell colors)
    /// and interaction (e.g. sorting) options.
    /// </summary>
    public partial class TreeTable : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeTable"/> class.
        /// </summary>
        /// <param name="columns">The columns of the table.</param>
        /// <param name="preHeaderRow">OPTIONAL row just above the header row.</param>
        /// <param name="dataRows">OPTIONAL data rows of the table.  DEFAULT is no data rows.</param>
        /// <param name="format">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        public TreeTable(
            TreeTableColumns columns,
            PreHeaderRow preHeaderRow = null,
            TreeTableDataRows dataRows = null,
            TableFormat format = null)
        {
            // ReSharper disable once JoinNullCheckWithUsage
            if (columns == null)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            if (preHeaderRow != null)
            {
                var numberOfColumns = columns.Columns.Count;

                var columnsSpannedInPreHeaderRow = preHeaderRow
                    .Cells
                    .Select(_ => _ is IColumnSpanningCell spanningCell ? spanningCell.ColumnsSpanned : 1)
                    .Sum();

                if (columnsSpannedInPreHeaderRow != numberOfColumns)
                {
                    throw new ArgumentException(Invariant($"{nameof(preHeaderRow)} spans {columnsSpannedInPreHeaderRow} columns, but there are {numberOfColumns} columns in the table; those numbers must match."));
                }
            }

            this.Columns = columns;
            this.PreHeaderRow = preHeaderRow;
            this.DataRows = dataRows;
            this.Format = format;
        }

        /// <summary>
        /// Gets the columns of the table.
        /// </summary>
        public TreeTableColumns Columns { get; private set; }

        /// <summary>
        /// Gets the row just above the header row.
        /// </summary>
        public PreHeaderRow PreHeaderRow { get; private set; }

        /// <summary>
        /// Gets the data rows of the table.
        /// </summary>
        public TreeTableDataRows DataRows { get; private set; }

        /// <summary>
        /// Gets the format to apply to the whole table.
        /// </summary>
        public TableFormat Format { get; private set; }
    }
}