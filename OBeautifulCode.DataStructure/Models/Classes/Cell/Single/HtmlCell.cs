﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// A cell that contains an HTML value.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class HtmlCell : StandardCellBase, IHaveValueCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlCell"/> class.
        /// </summary>
        /// <param name="html">The cell's HTML value.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public HtmlCell(
            string html,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(format, hoverOver, link)
        {
            this.Html = html;
        }

        /// <summary>
        /// Gets the cell's HTML value.
        /// </summary>
        public string Html { get; private set; }

        /// <inheritdoc />
        public object GetValue() => this.Html;
    }
}