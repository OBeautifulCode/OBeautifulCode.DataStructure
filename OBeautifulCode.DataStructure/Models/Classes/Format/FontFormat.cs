// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FontFormat.cs" company="OBeautifulCode">
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
    /// Specifies how to format text.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class FontFormat : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontFormat"/> class.
        /// </summary>
        /// <param name="fontColor">OPTIONAL font color.  DEFAULT is to leave the font color unchanged.</param>
        /// <param name="fontNamesInFallbackOrder">OPTIONAL font names, in fallback order, of the fonts to use.  DEFAULT is leave the font unchanged.</param>
        /// <param name="fontSizeInPoints">OPTIONAL font size, in points.  DEFAULT is to leave the font size unchanged.</param>
        /// <param name="options">OPTIONAL formatting options to apply to the font.  DEFAULT is to not apply any of the formatting options.</param>
        public FontFormat(
            Color? fontColor = null,
            IReadOnlyList<string> fontNamesInFallbackOrder = null,
            decimal? fontSizeInPoints = null,
            FontFormatOptions? options = null)
        {
            if (fontNamesInFallbackOrder != null)
            {
                if (!fontNamesInFallbackOrder.Any())
                {
                    throw new ArgumentException(Invariant($"{nameof(fontNamesInFallbackOrder)} is an empty enumerable."));
                }

                if (fontNamesInFallbackOrder.Any(_ => _ == null))
                {
                    throw new ArgumentException(Invariant($"{nameof(fontNamesInFallbackOrder)} contains at least one null element."));
                }

                if (fontNamesInFallbackOrder.Any(string.IsNullOrWhiteSpace))
                {
                    throw new ArgumentException(Invariant($"{nameof(fontNamesInFallbackOrder)} contains an element that is white space."));
                }
            }

            this.FontColor = fontColor;
            this.FontNamesInFallbackOrder = fontNamesInFallbackOrder;
            this.FontSizeInPoints = fontSizeInPoints;
            this.Options = options;
        }

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
        /// Gets the formatting options to apply to the font.
        /// </summary>
        public FontFormatOptions? Options { get; private set; }
    }
}
