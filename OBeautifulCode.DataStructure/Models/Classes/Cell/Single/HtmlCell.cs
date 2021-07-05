// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A cell that contains an HTML value.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class HtmlCell : StandardCellBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlCell"/> class.
        /// </summary>
        /// <param name="html">The cell's HTML value.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public HtmlCell(
            string html,
            string id = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(id, format, hoverOver, link)
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
        /// Gets the cell's HTML value.
        /// </summary>
        public string Html { get; private set; }

        /// <inheritdoc />
        public override object GetCellValue() => this.Html;
    }
}