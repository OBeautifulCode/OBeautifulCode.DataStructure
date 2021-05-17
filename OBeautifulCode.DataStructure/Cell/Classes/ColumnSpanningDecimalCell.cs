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
    public partial class ColumnSpanningDecimalCell : CellBase, IDecimalCell, IColumnSpanningCell, IFormattableCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSpanningDecimalCell"/> class.
        /// </summary>
        /// <param name="value">The cell's string value.</param>
        /// <param name="columnsSpanned">The number of columns spanned.</param>
        /// <param name="displayValue">OPTIONAL display value for the cell.  DEFAULT is to use default function to convert <paramref name="value"/> into a display value.</param>        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        protected ColumnSpanningDecimalCell(
            decimal value,
            int columnsSpanned,
            string displayValue = null,
            CellFormat format = null)
        {
            if (columnsSpanned < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(columnsSpanned), Invariant($"{nameof(columnsSpanned)} is {columnsSpanned}; must be >= 2."));
            }

            this.Value = value;
            this.DisplayValue = displayValue;
            this.ColumnsSpanned = columnsSpanned;
            this.Format = format;
        }

        /// <inheritdoc />
        public decimal Value { get; private set; }

        /// <inheritdoc />
        public string DisplayValue { get; private set; }

        /// <inheritdoc />
        public int ColumnsSpanned { get; private set; }

        /// <inheritdoc />
        public CellFormat Format { get; private set; }

        /// <inheritdoc />
        public object GetValue() => this.Value;
    }
}