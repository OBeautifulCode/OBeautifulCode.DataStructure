// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnSpanningHtmlCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// A cell that contains an HTML value and spans multiple columns.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ColumnSpanningHtmlCell : ColumnSpanningCellBase, IHaveValueCell, IFormattableCell, IHaveHoverOverCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSpanningHtmlCell"/> class.
        /// </summary>
        /// <param name="columnsSpanned">The number of columns spanned.</param>
        /// <param name="html">The cell's string value.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        public ColumnSpanningHtmlCell(
            int columnsSpanned,
            string html,
            CellFormat format = null,
            IHoverOver hoverOver = null)
            : base(columnsSpanned)
        {
            this.Html = html;
            this.Format = format;
            this.HoverOver = hoverOver;
        }

        /// <summary>
        /// Gets the cell's HTML value.
        /// </summary>
        public string Html { get; private set; }

        /// <inheritdoc />
        public CellFormat Format { get; private set; }

        /// <inheritdoc />
        public IHoverOver HoverOver { get; private set; }

        /// <inheritdoc />
        public object GetValue() => this.Html;
    }
}