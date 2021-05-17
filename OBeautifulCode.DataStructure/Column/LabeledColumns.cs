// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabeledColumns.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Specifies the columns and how they are labeled.
    /// </summary>
    public partial class LabeledColumns : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabeledColumns"/> class.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <param name="headerRowFormat">OPTIONAL format to apply to the whole header row.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="preHeaderRow">OPTIONAL row just above the header row.</param>
        public LabeledColumns(
            IReadOnlyList<Column> columns,
            RowFormat headerRowFormat = null,
            PreHeaderRow preHeaderRow = null)
        {
            if (columns == null)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            if (!columns.Any())
            {
                throw new ArgumentException(Invariant($"{nameof(columns)} is empty."));
            }

            if (columns.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(columns)} contains a null object."));
            }

            if (preHeaderRow != null)
            {
                var numberOfColumns = columns.Count;

                var columnsSpannedInPreHeaderRow = preHeaderRow.Cells.GetNumberOfColumnsSpanned();

                if (columnsSpannedInPreHeaderRow != numberOfColumns)
                {
                    throw new ArgumentException(Invariant($"{nameof(preHeaderRow)} spans {columnsSpannedInPreHeaderRow} columns, but there are {numberOfColumns} columns in the table; those numbers must match."));
                }
            }

            this.Columns = columns;
            this.HeaderRowFormat = headerRowFormat;
            this.PreHeaderRow = preHeaderRow;
        }

        /// <summary>
        /// Gets the columns.
        /// </summary>
        public IReadOnlyList<Column> Columns { get; private set; }

        /// <summary>
        /// Gets the format to apply to the whole header row.
        /// </summary>
        public RowFormat HeaderRowFormat { get; private set; }

        /// <summary>
        /// Gets the row just above the header row.
        /// </summary>
        public PreHeaderRow PreHeaderRow { get; private set; }
    }
}