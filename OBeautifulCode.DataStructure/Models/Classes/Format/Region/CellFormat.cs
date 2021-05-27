// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// The format to apply to a cell in a tree table.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellFormat : RegionFormatBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellFormat"/> class.
        /// </summary>
        /// <param name="backgroundColor">OPTIONAL background color of the cell.  DEFAULT is to leave the background color unchanged.</param>
        /// <param name="fontColor">OPTIONAL font color.  DEFAULT is to leave the font color unchanged.</param>
        /// <param name="fontNamesInFallbackOrder">OPTIONAL font names, in fallback order, of the fonts to use.  DEFAULT is leave the font unchanged.</param>
        /// <param name="fontSizeInPoints">OPTIONAL font size, in points.  DEFAULT is to leave the font size unchanged.</param>
        /// <param name="verticalAlignment">OPTIONAL vertical alignment.  DEFAULT is to leave the vertical alignment unchanged.</param>
        /// <param name="horizontalAlignment">OPTIONAL horizontal alignment.  DEFAULT is to leave the horizontal alignment unchanged.</param>
        /// <param name="fontRotationAngle">OPTIONAL font rotation angle, between +90 and -90.  Positive numbers cause the text to slope upward, negative numbers cause the text to slope downward.  DEFAULT is to leave the font rotation angle unchanged.</param>
        /// <param name="borders">OPTIONAL borders to apply to the cell, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="fillPattern">OPTIONAL pattern to fill the cell with.  DEFAULT is no pattern.</param>
        /// <param name="options">OPTIONAL formatting options to apply to the cell.  DEFAULT is to not apply any of the formatting options.</param>
        public CellFormat(
            Color? backgroundColor = null,
            Color? fontColor = null,
            IReadOnlyList<string> fontNamesInFallbackOrder = null,
            decimal? fontSizeInPoints = null,
            VerticalAlignment? verticalAlignment = null,
            HorizontalAlignment? horizontalAlignment = null,
            int? fontRotationAngle = null,
            IReadOnlyList<OuterBorder> borders = null,
            FillPattern fillPattern = null,
            CellFormatOptions? options = null)
        {
            if ((fontNamesInFallbackOrder != null) && fontNamesInFallbackOrder.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException(Invariant($"{nameof(fontNamesInFallbackOrder)} contains a null or white space element."));
            }

            if ((borders != null) && borders.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(borders)} contains a null element."));
            }

            this.BackgroundColor = backgroundColor;
            this.FontColor = fontColor;
            this.FontNamesInFallbackOrder = fontNamesInFallbackOrder;
            this.FontSizeInPoints = fontSizeInPoints;
            this.VerticalAlignment = verticalAlignment;
            this.HorizontalAlignment = horizontalAlignment;
            this.FontRotationAngle = fontRotationAngle;
            this.Borders = borders;
            this.FillPattern = fillPattern;
            this.Options = options;
        }

        /// <summary>
        /// Gets the background color.
        /// </summary>
        public Color? BackgroundColor { get; private set; }

        /// <summary>
        /// Gets the font color.
        /// </summary>
        public Color? FontColor { get; private set; }

        /// <summary>
        /// Gets the font names, in fallback order.
        /// </summary>
        public IReadOnlyList<string> FontNamesInFallbackOrder { get; private set; }

        /// <summary>
        /// Gets the font size, in points.
        /// </summary>
        public decimal? FontSizeInPoints { get; private set; }

        /// <summary>
        /// Gets the vertical alignment.
        /// </summary>
        public VerticalAlignment? VerticalAlignment { get; private set; }

        /// <summary>
        /// Gets the horizontal alignment.
        /// </summary>
        public HorizontalAlignment? HorizontalAlignment { get; private set; }

        /// <summary>
        /// Gets the font rotation angle, between +90 and -90.  Positive numbers cause the text to slope upward, negative numbers cause the text to slope downward.
        /// </summary>
        public int? FontRotationAngle { get; private set; }

        /// <summary>
        /// Gets the borders to apply to the cell, in the order that they should be applied.
        /// </summary>
        public IReadOnlyList<OuterBorder> Borders { get; private set; }

        /// <summary>
        /// Gets the pattern to fill the cell with.
        /// </summary>
        public FillPattern FillPattern { get; private set; }

        /// <summary>
        /// Gets the formatting options to apply to the cell.
        /// </summary>
        public CellFormatOptions? Options { get; private set; }
    }
}
