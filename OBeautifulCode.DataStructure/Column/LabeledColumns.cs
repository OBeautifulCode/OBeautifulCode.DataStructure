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
        /// <param name="displayHeaderRow">OPTIONAL value indicating whether to display the header row.  DEFAULT is to display the header row.</param>
        /// <param name="headerRowFormat">OPTIONAL format to apply to the whole header row.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="preHeaderRow">OPTIONAL row just above the header row.</param>
        public LabeledColumns(
            IReadOnlyList<Column> columns,
            bool displayHeaderRow = true,
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

            if (!displayHeaderRow)
            {
                if (headerRowFormat != null)
                {
                    throw new ArgumentException(Invariant($"{nameof(displayHeaderRow)} is false, but {nameof(headerRowFormat)} is not null; cannot format the header row because it does not exist."));
                }

                if (columns.Any(_ => _.HeaderCell != null))
                {
                    throw new ArgumentException(Invariant($"{nameof(displayHeaderRow)} is false, but {nameof(columns)} contains at least one {nameof(Column)} with a non-null {nameof(Column.HeaderCell)}; cannot specify a header cell because the header row does not exist."));
                }

                if (columns.Any(_ => _.HeaderCellOptions != null))
                {
                    throw new ArgumentException(Invariant($"{nameof(displayHeaderRow)} is false, but {nameof(columns)} contains at least one {nameof(Column)} with a non-null {nameof(Column.HeaderCellOptions)}; cannot specify header cell options because the header row does not exist."));
                }
            }

            this.Columns = columns;
            this.DisplayHeaderRow = displayHeaderRow;
            this.HeaderRowFormat = headerRowFormat;
            this.PreHeaderRow = preHeaderRow;
        }

        /// <summary>
        /// Gets the columns.
        /// </summary>
        public IReadOnlyList<Column> Columns { get; private set; }

        /// <summary>
        /// Gets a value indicating whether to display the header row.
        /// </summary>
        public bool DisplayHeaderRow { get; private set; }

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