// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHoverOver.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// An HTML hover-over.
    /// </summary>
    public partial class HtmlHoverOver : HoverOverBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlHoverOver"/> class.
        /// </summary>
        /// <param name="html">The hover-over HTML value.</param>
        public HtmlHoverOver(
            string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException(nameof(html));
            }

            if (string.IsNullOrWhiteSpace(html))
            {
                throw new ArgumentException(Invariant($"{nameof(html)} is white space"));
            }

            this.Html = html;
        }

        /// <summary>
        /// Gets the hover-over HTML value.
        /// </summary>
        public string Html { get; private set; }
    }
}
