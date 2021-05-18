// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnSpanningDecimalCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A cell that contains a string value and spans multiple columns.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ColumnSpanningDecimalCell : ColumnSpanningCellBase, IHaveValueCell, IHaveDisplayValueCell, IFormattableCell, IHaveHoverOverCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSpanningDecimalCell"/> class.
        /// </summary>
        /// <param name="columnsSpanned">The number of columns spanned.</param>
        /// <param name="value">The cell's string value.</param>
        /// <param name="displayValue">OPTIONAL display value for the cell.  DEFAULT is to use default function to convert <paramref name="value"/> into a display value.</param>        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        public ColumnSpanningDecimalCell(
            int columnsSpanned,
            decimal value,
            string displayValue = null,
            CellFormat format = null,
            IHoverOver hoverOver = null)
            : base(columnsSpanned)
        {
            this.Value = value;
            this.DisplayValue = displayValue;
            this.Format = format;
            this.HoverOver = hoverOver;
        }

        /// <summary>
        /// Gets the cell's decimal value.
        /// </summary>
        public decimal Value { get; private set; }

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