// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumberCellFormatBase{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Base class for a cell value format for a numeric value.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class NumberCellFormatBase<TValue> : StandardCellValueFormatBase<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberCellFormatBase{TValue}"/> class.
        /// </summary>
        /// <param name="numberOfDecimalPlaces">The number of digits after the decimal separator to display.</param>
        /// <param name="decimalSeparator">The character to use to separate whole numbers from fractional numbers.</param>
        /// <param name="digitGroupKind">The kind of digit grouping to employ.</param>
        /// <param name="digitGroupSeparator">The separator character to use between digit groups.</param>
        /// <param name="negativeNumberDisplayKind">A value that specifies how to display negative numbers.</param>
        /// <param name="missingValueText">The text to use when the cell is missing a value.</param>
        protected NumberCellFormatBase(
            int? numberOfDecimalPlaces,
            char? decimalSeparator,
            NumberFormatDigitGroupKind? digitGroupKind,
            char? digitGroupSeparator,
            NumberFormatNegativeDisplayKind? negativeNumberDisplayKind,
            string missingValueText)
            : base(missingValueText)
        {
            if ((digitGroupKind != null) && (digitGroupKind == NumberFormatDigitGroupKind.Unknown))
            {
                throw new ArgumentOutOfRangeException(Invariant($"{digitGroupKind} is {nameof(NumberFormatDigitGroupKind)}.{nameof(NumberFormatDigitGroupKind.Unknown)}."));
            }

            if ((negativeNumberDisplayKind != null) && (negativeNumberDisplayKind == NumberFormatNegativeDisplayKind.Unknown))
            {
                throw new ArgumentOutOfRangeException(Invariant($"{negativeNumberDisplayKind} is {nameof(NumberFormatNegativeDisplayKind)}.{nameof(NumberFormatNegativeDisplayKind.Unknown)}."));
            }

            this.NumberOfDecimalPlaces = numberOfDecimalPlaces;
            this.DecimalSeparator = decimalSeparator;
            this.DigitGroupKind = digitGroupKind;
            this.DigitGroupSeparator = digitGroupSeparator;
            this.NegativeNumberDisplayKind = negativeNumberDisplayKind;
        }

        /// <summary>
        /// Gets the number of digits after the decimal separator to display.
        /// </summary>
        public int? NumberOfDecimalPlaces { get; private set; }

        /// <summary>
        /// Gets the character to use to separate whole numbers from fractional numbers.
        /// </summary>
        public char? DecimalSeparator { get; private set; }

        /// <summary>
        /// Gets the kind of digit grouping to employ.
        /// </summary>
        public NumberFormatDigitGroupKind? DigitGroupKind { get; private set; }

        /// <summary>
        /// Gets the separator character to use between digit groups.
        /// </summary>
        public char? DigitGroupSeparator { get; private set; }

        /// <summary>
        /// Gets a value that specifies how to display negative numbers.
        /// </summary>
        public NumberFormatNegativeDisplayKind? NegativeNumberDisplayKind { get; private set; }
    }
}
