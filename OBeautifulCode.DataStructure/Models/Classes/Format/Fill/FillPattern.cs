// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FillPattern.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Drawing;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A pattern to fill a cell with.
    /// </summary>
    public partial class FillPattern : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FillPattern"/> class.
        /// </summary>
        /// <param name="style">The style of the pattern.</param>
        /// <param name="color">The color of the pattern.</param>
        public FillPattern(
            FillPatternStyle style,
            Color color)
        {
            if (style == FillPatternStyle.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(style)} is {nameof(FillPatternStyle.Unknown)}."));
            }

            this.Style = style;
            this.Color = color;
        }

        /// <summary>
        /// Gets the style of the pattern.
        /// </summary>
        public FillPatternStyle Style { get; private set; }

        /// <summary>
        /// Gets color of the pattern.
        /// </summary>
        public Color Color { get; private set; }
    }
}
