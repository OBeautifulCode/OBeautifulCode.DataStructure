// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnSpanningCellBase.cs" company="OBeautifulCode">
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
    public class ColumnSpanningCellBase : CellBase, IColumnSpanningCell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSpanningCellBase"/> class.
        /// </summary>
        /// <param name="columnsSpanned">The number of columns spanned.</param>
        protected ColumnSpanningCellBase(
            int columnsSpanned)
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
