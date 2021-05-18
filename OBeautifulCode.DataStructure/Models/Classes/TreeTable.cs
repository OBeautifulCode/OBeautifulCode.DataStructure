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
        /// <param name="labeledColumns">The columns of the table and how they are labeled.</param>
        /// <param name="dataRows">OPTIONAL data rows of the table.  DEFAULT is no data rows.</param>
        /// <param name="format">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        public TreeTable(
            LabeledColumns labeledColumns,
            DataRows dataRows = null,
            TableFormat format = null)
        {
            if (labeledColumns == null)
            {
                throw new ArgumentNullException(nameof(labeledColumns));
            }

            var rows = dataRows.GetAllDataRowsInOrder();

            var numberOfColumns = labeledColumns.Columns.Count;

            if (rows.Any(_ => _.GetNumberOfColumnsSpanned() != numberOfColumns))
            {
                throw new ArgumentException(Invariant($"{nameof(dataRows)} contains a row or descendant row that does not span all {numberOfColumns} of the defined columns."));
            }

            this.LabeledColumns = labeledColumns;
            this.DataRows = dataRows;
            this.Format = format;
        }

        /// <summary>
        /// Gets the columns of the table.
        /// </summary>
        public LabeledColumns LabeledColumns { get; private set; }

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