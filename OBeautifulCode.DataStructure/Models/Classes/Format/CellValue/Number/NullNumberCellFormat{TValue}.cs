// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullNumberCellFormat{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Null object pattern implementation of a cell value format for a numeric value.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class NullNumberCellFormat<TValue> : NumberCellFormatBase<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullNumberCellFormat{TValue}"/> class.
        /// </summary>
        /// <param name="numberOfDecimalPlaces">The number of digits after the decimal separator to display.</param>
        /// <param name="roundingStrategy">The strategy to use to round the number.</param>
        /// <param name="decimalSeparator">The character to use to separate whole numbers from fractional numbers.</param>
        /// <param name="digitGroupKind">The kind of digit grouping to employ.</param>
        /// <param name="digitGroupSeparator">The separator character to use between digit groups.</param>
        /// <param name="negativeNumberDisplayKind">A value that specifies how to display negative numbers.</param>
        /// <param name="missingValueText">The text to use when the cell is missing a value.</param>
        public NullNumberCellFormat(
            int? numberOfDecimalPlaces,
            MidpointRounding? roundingStrategy,
            char? decimalSeparator,
            NumberFormatDigitGroupKind? digitGroupKind,
            char? digitGroupSeparator,
            NumberFormatNegativeDisplayKind? negativeNumberDisplayKind,
            string missingValueText)
            : base(numberOfDecimalPlaces, roundingStrategy, decimalSeparator, digitGroupKind, digitGroupSeparator, negativeNumberDisplayKind, missingValueText)
        {
        }
    }
}
