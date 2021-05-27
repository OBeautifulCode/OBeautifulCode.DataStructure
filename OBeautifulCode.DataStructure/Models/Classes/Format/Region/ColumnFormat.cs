// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnFormat.cs" company="OBeautifulCode">
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
    /// The format to apply to a column in a tree table.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ColumnFormat : RegionFormatBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnFormat"/> class.
        /// </summary>
        /// <param name="cellsFormat">OPTIONAL format to apply to all cells in the column, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="widthInPixels">OPTIONAL width, in pixels, to apply to the column.  DEFAULT is to leave the width unchanged.</param>
        /// <param name="autoFitColumnWidth">OPTIONAL value indicating whether to auto-fit the width of the column.</param>
        /// <param name="outerBorders">OPTIONAL borders to apply to the outside of the column, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="innerBorders">OPTIONAL borders to apply to the cells inside the column, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="options">OPTIONAL formatting options to apply to the column.  DEFAULT is to not apply any of the formatting options.</param>
        public ColumnFormat(
            CellFormat cellsFormat = null,
            int? widthInPixels = null,
            bool? autoFitColumnWidth = null,
            IReadOnlyList<OuterBorder> outerBorders = null,
            IReadOnlyList<InnerBorder> innerBorders = null,
            ColumnFormatOptions? options = null)
        {
            if (outerBorders != null)
            {
                if (!outerBorders.Any())
                {
                    throw new ArgumentException(Invariant($"{nameof(outerBorders)} is an empty enumerable."));
                }

                if (outerBorders.Any(_ => _ == null))
                {
                    throw new ArgumentException(Invariant($"{nameof(outerBorders)} contains at least one null element."));
                }
            }

            if (innerBorders != null)
            {
                if (!innerBorders.Any())
                {
                    throw new ArgumentException(Invariant($"{nameof(innerBorders)} is an empty enumerable."));
                }

                if (innerBorders.Any(_ => _ == null))
                {
                    throw new ArgumentException(Invariant($"{nameof(innerBorders)} contains at least one null element."));
                }
            }

            this.CellsFormat = cellsFormat;
            this.WidthInPixels = widthInPixels;
            this.AutoFitColumnWidth = autoFitColumnWidth;
            this.OuterBorders = outerBorders;
            this.InnerBorders = innerBorders;
            this.Options = options;
        }

        /// <summary>
        /// Gets the format to apply to all cells in the column, individually.
        /// </summary>
        public CellFormat CellsFormat { get; private set; }

        /// <summary>
        /// Gets the width, in pixels, to apply to the column.
        /// </summary>
        public int? WidthInPixels { get; private set; }

        /// <summary>
        /// Gets a value indicating whether to auto-fit the width of the column.
        /// </summary>
        public bool? AutoFitColumnWidth { get; private set; }

        /// <summary>
        /// Gets the borders to apply to the outside of the column, in the order that they should be applied.
        /// </summary>
        public IReadOnlyList<OuterBorder> OuterBorders { get; private set; }

        /// <summary>
        /// Gets the borders to apply to the cells inside the column, in the order that they should be applied.
        /// </summary>
        public IReadOnlyList<InnerBorder> InnerBorders { get; private set; }

        /// <summary>
        /// Gets the formatting options to apply to the column.
        /// </summary>
        public ColumnFormatOptions? Options { get; private set; }
    }
}
