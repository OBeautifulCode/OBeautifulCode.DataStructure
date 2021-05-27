// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowFormat.cs" company="OBeautifulCode">
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
    /// The format to apply to a row in a tree table.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class RowFormat : RegionFormatBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RowFormat"/> class.
        /// </summary>
        /// <param name="cellsFormat">OPTIONAL format to apply to all cells in the row, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="heightInPixels">OPTIONAL height, in pixels, to apply to the row.  DEFAULT is to leave the height unchanged.</param>
        /// <param name="outerBorders">OPTIONAL borders to apply to the outside of the row, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="innerBorders">OPTIONAL borders to apply to the cells inside the row, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="options">OPTIONAL formatting options to apply to the row.  DEFAULT is to not apply any of the formatting options.</param>
        public RowFormat(
            CellFormat cellsFormat = null,
            int? heightInPixels = null,
            IReadOnlyList<OuterBorder> outerBorders = null,
            IReadOnlyList<InnerBorder> innerBorders = null,
            RowFormatOptions? options = null)
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
            this.HeightInPixels = heightInPixels;
            this.OuterBorders = outerBorders;
            this.InnerBorders = innerBorders;
            this.Options = options;
        }

        /// <summary>
        /// Gets the format to apply to all cells in the row, individually.
        /// </summary>
        public CellFormat CellsFormat { get; private set; }

        /// <summary>
        /// Gets the height, in pixels, to apply to the row.
        /// </summary>
        public int? HeightInPixels { get; private set; }

        /// <summary>
        /// Gets the borders to apply to the outside of the row, in the order that they should be applied.
        /// </summary>
        public IReadOnlyList<OuterBorder> OuterBorders { get; private set; }

        /// <summary>
        /// Gets the borders to apply to the cells inside the row, in the order that they should be applied.
        /// </summary>
        public IReadOnlyList<InnerBorder> InnerBorders { get; private set; }

        /// <summary>
        /// Gets the formatting options to apply to the row.
        /// </summary>
        public RowFormatOptions? Options { get; private set; }
    }
}
