// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnSpanningHtmlCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A cell that contains an HTML value and spans multiple columns.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ColumnSpanningHtmlCell : ColumnSpanningStandardCellBase, IHaveValueCell, IFormattableCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSpanningHtmlCell"/> class.
        /// </summary>
        /// <param name="html">The cell's string value.</param>
        /// <param name="columnsSpanned">The number of columns spanned.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public ColumnSpanningHtmlCell(
            string html,
            int columnsSpanned,
            string id = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(columnsSpanned, id, format, hoverOver, link)
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
        public object GetCellValue() => this.Html;
    }
}