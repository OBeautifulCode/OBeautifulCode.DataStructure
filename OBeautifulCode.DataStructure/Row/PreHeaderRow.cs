// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PreHeaderRow.cs" company="OBeautifulCode">
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
    /// A row just above the header row in a tree table.
    /// </summary>
    public partial class PreHeaderRow : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreHeaderRow"/> class.
        /// </summary>
        /// <param name="cells">Cells in the row.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        public PreHeaderRow(
            IReadOnlyList<IStringCell> cells,
            RowFormat format = null)
        {
            if (cells == null)
            {
                throw new ArgumentNullException(nameof(cells));
            }

            if (cells.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(cells)} contains a null element."));
            }

            this.Cells = cells;
            this.Format = format;
        }

        /// <summary>
        /// Gets the cells in the row.
        /// </summary>
        public IReadOnlyList<ICell> Cells { get; private set; }

        /// <summary>
        /// Gets the format to apply to the whole row.
        /// </summary>
        public RowFormat Format { get; private set; }
    }
}