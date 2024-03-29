﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// The format to apply to a column in a tree table.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ColumnFormat : MultiCellRegionFormatBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnFormat"/> class.
        /// </summary>
        /// <param name="outerBorders">OPTIONAL borders to apply to the outside of the data rows in the column, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="innerBorders">OPTIONAL borders to apply to the cells inside the data rows in the column, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="cellsFormat">OPTIONAL format to apply to all data row cells in the column, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="widthInPixels">OPTIONAL width, in pixels, to apply to the whole column.  DEFAULT is to leave the width unchanged.</param>
        /// <param name="autofitColumnWidth">OPTIONAL value indicating whether to auto-fit the width of the whole column.</param>
        /// <param name="options">OPTIONAL formatting options to apply to the whole column.  DEFAULT is to not apply any of the formatting options.</param>
        public ColumnFormat(
            IReadOnlyList<OuterBorder> outerBorders = null,
            IReadOnlyList<InnerBorder> innerBorders = null,
            CellFormat cellsFormat = null,
            int? widthInPixels = null,
            bool? autofitColumnWidth = null,
            ColumnFormatOptions? options = null)
            : base(outerBorders, innerBorders)
        {
            this.CellsFormat = cellsFormat;
            this.WidthInPixels = widthInPixels;
            this.AutofitColumnWidth = autofitColumnWidth;
            this.Options = options;
        }

        /// <summary>
        /// Gets the format to apply to all data row cells in the column, individually.
        /// </summary>
        public CellFormat CellsFormat { get; private set; }

        /// <summary>
        /// Gets the width, in pixels, to apply to the whole column.
        /// </summary>
        public int? WidthInPixels { get; private set; }

        /// <summary>
        /// Gets a value indicating whether to auto-fit the width of the whole column.
        /// </summary>
        public bool? AutofitColumnWidth { get; private set; }

        /// <summary>
        /// Gets the formatting options to apply to the whole column.
        /// </summary>
        public ColumnFormatOptions? Options { get; private set; }
    }
}
