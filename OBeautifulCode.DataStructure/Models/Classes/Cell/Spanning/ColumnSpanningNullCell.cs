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
    public partial class ColumnSpanningNullCell : ColumnSpanningCellBase, IFormattableCell, IHaveHoverOverCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSpanningNullCell"/> class.
        /// </summary>
        /// <param name="columnsSpanned">The number of columns spanned.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        public ColumnSpanningNullCell(
            int columnsSpanned,
            CellFormat format = null,
            IHoverOver hoverOver = null)
            : base(columnsSpanned)
        {
            this.Format = format;
            this.HoverOver = hoverOver;
        }

        /// <inheritdoc />
        public CellFormat Format { get; private set; }

        /// <inheritdoc />
        public IHoverOver HoverOver { get; private set; }
    }
}