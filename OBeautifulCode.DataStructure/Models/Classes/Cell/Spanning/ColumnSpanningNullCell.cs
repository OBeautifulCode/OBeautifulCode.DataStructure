// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnSpanningNullCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// A cell that contains a string value and spans multiple columns.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ColumnSpanningNullCell : ColumnSpanningStandardCellBase, IHaveDisplayValueCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSpanningNullCell"/> class.
        /// </summary>
        /// <param name="columnsSpanned">The number of columns spanned.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="displayValue">OPTIONAL display value for the cell.  DEFAULT is no display value.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public ColumnSpanningNullCell(
            int columnsSpanned,
            string id = null,
            string displayValue = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(columnsSpanned, id, format, hoverOver, link)
        {
            this.DisplayValue = displayValue;
        }

        /// <inheritdoc />
        public string DisplayValue { get; private set; }

        /// <inheritdoc />
        public override object GetCellValue() => null;
    }
}