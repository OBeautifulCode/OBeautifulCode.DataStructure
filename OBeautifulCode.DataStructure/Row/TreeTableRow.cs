// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeTableRow.cs" company="OBeautifulCode">
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
    /// A row in a tree table.
    /// </summary>
    public partial class TreeTableRow : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeTableRow"/> class.
        /// </summary>
        /// <param name="cells">OPTIONAL cells in the row.  DEFAULT is none.</param>
        /// <param name="childRows">OPTIONAL child rows.  DEFAULT is none.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        public TreeTableRow(
            IReadOnlyList<ICell> cells = null,
            IReadOnlyList<TreeTableRow> childRows = null,
            RowFormat format = null)
        {
            if ((cells != null) && cells.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(cells)} contains a null element."));
            }

            if ((childRows != null) && childRows.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(childRows)} contains a null element."));
            }

            this.Cells = cells;
            this.ChildRows = childRows;
            this.Format = format;
        }

        /// <summary>
        /// Gets the cells in the row.
        /// </summary>
        public IReadOnlyList<ICell> Cells { get; private set; }

        /// <summary>
        /// Gets the child rows.
        /// </summary>
        public IReadOnlyList<TreeTableRow> ChildRows { get; private set; }

        /// <summary>
        /// Gets the format to apply to the whole row.
        /// </summary>
        public RowFormat Format { get; private set; }
    }
}