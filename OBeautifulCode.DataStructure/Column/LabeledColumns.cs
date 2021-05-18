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
        /// <param name="preHeaderRows">OPTIONAL rows just above the header row.</param>
        public LabeledColumns(
            IReadOnlyList<Column> columns,
            RowFormat headerRowFormat = null,
            IReadOnlyList<FlatRow> preHeaderRows = null)
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

            if (preHeaderRows != null)
            {
                if (preHeaderRows.Any(_ => _ == null))
                {
                    throw new ArgumentException(Invariant($"{nameof(preHeaderRows)} contains a null object."));
                }

                var numberOfColumns = columns.Count;

                if (preHeaderRows.Any(_ => _.GetNumberOfColumnsSpanned() != numberOfColumns))
                {
                    throw new ArgumentException(Invariant($"{nameof(preHeaderRows)} contains a row that does not span all {numberOfColumns} of the defined columns."));
                }
            }

            this.Columns = columns;
            this.HeaderRowFormat = headerRowFormat;
            this.PreHeaderRows = preHeaderRows;
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
        /// Gets the rows just above the header row.
        /// </summary>
        public IReadOnlyList<FlatRow> PreHeaderRows { get; private set; }
    }
}