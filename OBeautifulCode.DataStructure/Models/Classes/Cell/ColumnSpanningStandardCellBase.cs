// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnSpanningStandardCellBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// A cell that spans multiple columns.
    /// </summary>
    public abstract partial class ColumnSpanningStandardCellBase : StandardCellBase, IColumnSpanningCell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSpanningStandardCellBase"/> class.
        /// </summary>
        /// <param name="columnsSpanned">The number of columns spanned.</param>
        /// <param name="format">Format to apply to the cell.</param>
        /// <param name="hoverOver">Hover-over for the cell.</param>
        /// <param name="link">Link to some resource.</param>
        protected ColumnSpanningStandardCellBase(
            int columnsSpanned,
            CellFormat format,
            IHoverOver hoverOver,
            ILink link)
            : base(format, hoverOver, link)
        {
            if (columnsSpanned < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(columnsSpanned), Invariant($"{nameof(columnsSpanned)} is {columnsSpanned}; must be >= 2."));
            }

            this.ColumnsSpanned = columnsSpanned;
        }

        /// <inheritdoc />
        public int ColumnsSpanned { get; private set; }
    }
}
