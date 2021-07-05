// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DecimalCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// A cell that contains a decimal value.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class DecimalCell : StandardCellBase, IHaveDisplayValueCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalCell"/> class.
        /// </summary>
        /// <param name="value">The cell's decimal value.</param>
        /// <param name="displayValue">OPTIONAL display value for the cell.  DEFAULT is to use default function to convert <paramref name="value"/> into a display value.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public DecimalCell(
            decimal value,
            string displayValue = null,
            string id = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(id, format, hoverOver, link)
        {
            this.Value = value;
            this.DisplayValue = displayValue;
        }

        /// <summary>
        /// Gets the cell's decimal value.
        /// </summary>
        public decimal Value { get; private set; }

        /// <inheritdoc />
        public string DisplayValue { get; private set; }

        /// <inheritdoc />
        public override object GetCellValue() => this.Value;
    }
}