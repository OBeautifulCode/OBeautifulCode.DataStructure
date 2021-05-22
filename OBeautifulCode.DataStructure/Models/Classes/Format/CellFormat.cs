﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;
    using System.Drawing;

    using OBeautifulCode.Type;

    /// <summary>
    /// The format to apply to a cell in a tree table.
    /// </summary>
    public partial class CellFormat : IModelViaCodeGen
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
        /// <param name="border">OPTIONAL border to apply to the cell.  DEFAULT is no border.</param>
        /// <param name="options">OPTIONAL formatting options to apply to the cell.  DEFAULT is to not apply any of the formatting options.</param>
        public CellFormat(
            Color? backgroundColor = null,
            Color? fontColor = null,
            IReadOnlyList<string> fontNamesInFallbackOrder = null,
            decimal? fontSizeInPoints = null,
            VerticalAlignment? verticalAlignment = null,
            HorizontalAlignment? horizontalAlignment = null,
            int? fontRotationAngle = null,
            OuterBorder border = null,
            CellFormatOptions? options = null)
        {
            this.BackgroundColor = backgroundColor;
            this.FontColor = fontColor;
            this.FontNamesInFallbackOrder = fontNamesInFallbackOrder;
            this.FontSizeInPoints = fontSizeInPoints;
            this.VerticalAlignment = verticalAlignment;
            this.HorizontalAlignment = horizontalAlignment;
            this.FontRotationAngle = fontRotationAngle;
            this.Border = border;
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
        /// Gets the border to apply to the cell.
        /// </summary>
        public OuterBorder Border { get; private set; }

        /// <summary>
        /// Gets the formatting options to apply to the cell.
        /// </summary>
        public CellFormatOptions? Options { get; private set; }
    }
}
