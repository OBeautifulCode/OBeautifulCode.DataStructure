﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHoverOver.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

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
            this.Html = html;
        }

        /// <summary>
        /// Gets the hover-over HTML value.
        /// </summary>
        public string Html { get; private set; }
    }
}