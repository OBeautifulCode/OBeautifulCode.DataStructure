// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableFormat.cs" company="OBeautifulCode">
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
    /// The format to apply to a tree table.
    /// </summary>
    public partial class TableFormat : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableFormat"/> class.
        /// </summary>
        /// <param name="cellsFormat">OPTIONAL format to apply to all cells in the table, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="outerBorders">OPTIONAL borders to apply to the outside of the table, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="innerBorders">OPTIONAL borders to apply to the cells inside the table, in the order that they should be applied.  DEFAULT is no border.</param>
        public TableFormat(
            CellFormat cellsFormat = null,
            IReadOnlyList<OuterBorder> outerBorders = null,
            IReadOnlyList<InnerBorder> innerBorders = null)
        {
            if ((outerBorders != null) && outerBorders.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(outerBorders)} contains a null element."));
            }

            if ((innerBorders != null) && innerBorders.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(innerBorders)} contains a null element."));
            }

            this.CellsFormat = cellsFormat;
            this.OuterBorders = outerBorders;
            this.InnerBorders = innerBorders;
        }

        /// <summary>
        /// Gets the format to apply to all cells in the table, individually.
        /// </summary>
        public CellFormat CellsFormat { get; private set; }

        /// <summary>
        /// Gets the borders to apply to the outside of the table, in the order that they should be applied.
        /// </summary>
        public IReadOnlyList<OuterBorder> OuterBorders { get; private set; }

        /// <summary>
        /// Gets the borders to apply to the cells inside the table, in the order that they should be applied.
        /// </summary>
        public IReadOnlyList<InnerBorder> InnerBorders { get; private set; }
    }
}
