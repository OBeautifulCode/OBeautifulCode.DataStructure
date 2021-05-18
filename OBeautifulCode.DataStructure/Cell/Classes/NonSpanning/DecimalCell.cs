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
    public partial class DecimalCell : CellBase, IDecimalCell, IFormattableCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalCell"/> class.
        /// </summary>
        /// <param name="value">The cell's decimal value.</param>
        /// <param name="displayValue">OPTIONAL display value for the cell.  DEFAULT is to use default function to convert <paramref name="value"/> into a display value.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        protected DecimalCell(
            decimal value,
            string displayValue = null,
            CellFormat format = null)
        {
            this.Value = value;
            this.DisplayValue = displayValue;
            this.Format = format;
        }

        /// <inheritdoc />
        public decimal Value { get; private set; }

        /// <inheritdoc />
        public string DisplayValue { get; private set; }

        /// <inheritdoc />
        public CellFormat Format { get; private set; }

        /// <inheritdoc />
        public object GetValue() => this.Value;
    }
}