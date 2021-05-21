// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// A cell that contains a string value.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class StringCell : CellBase, IHaveValueCell, IHaveDisplayValueCell, IFormattableCell, IHaveHoverOverCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringCell"/> class.
        /// </summary>
        /// <param name="value">The cell's string value.</param>
        /// <param name="displayValue">OPTIONAL display value for the cell.  DEFAULT is to use default function to convert <paramref name="value"/> into a display value.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        public StringCell(
            string value,
            string displayValue = null,
            CellFormat format = null,
            IHoverOver hoverOver = null)
        {
            this.Value = value;
            this.DisplayValue = displayValue;
            this.Format = format;
            this.HoverOver = hoverOver;
        }

        /// <summary>
        /// Gets the cell's string value.
        /// </summary>
        public string Value { get; private set; }

        /// <inheritdoc />
        public string DisplayValue { get; private set; }

        /// <inheritdoc />
        public CellFormat Format { get; private set; }

        /// <inheritdoc />
        public IHoverOver HoverOver { get; private set; }

        /// <inheritdoc />
        public object GetValue() => this.Value;
    }
}