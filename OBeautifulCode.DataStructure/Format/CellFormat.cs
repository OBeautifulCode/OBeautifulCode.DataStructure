// --------------------------------------------------------------------------------------------------------------------
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
        /// <param name="backgroundColor">The background color.</param>
        /// <param name="fontColor">The font color.</param>
        /// <param name="fontNamesInFallbackOrder">The font names, in fallback order.</param>
        /// <param name="fontSizeInPoints">The font size, in points.</param>
        /// <param name="verticalAlignment">The vertical alignment.</param>
        /// <param name="horizontalAlignment">The horizontal alignment.</param>
        /// <param name="fontRotationAngle">The font rotation angle, between +90 and -90.  Positive numbers cause the text to slope upward, negative numbers cause the text to slope downward.</param>
        /// <param name="options">The formatting options to apply to the cell.</param>
        public CellFormat(
            Color? backgroundColor = null,
            Color? fontColor = null,
            IReadOnlyList<string> fontNamesInFallbackOrder = null,
            decimal? fontSizeInPoints = null,
            VerticalAlignment? verticalAlignment = null,
            HorizontalAlignment? horizontalAlignment = null,
            int? fontRotationAngle = null,
            CellFormatOptions? options = null)
        {
            this.BackgroundColor = backgroundColor;
            this.FontColor = fontColor;
            this.FontNamesInFallbackOrder = fontNamesInFallbackOrder;
            this.FontSizeInPoints = fontSizeInPoints;
            this.VerticalAlignment = verticalAlignment;
            this.HorizontalAlignment = horizontalAlignment;
            this.FontRotationAngle = fontRotationAngle;
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
        /// Gets the formatting options to apply to the cell.
        /// </summary>
        public CellFormatOptions? Options { get; private set; }
    }
}
