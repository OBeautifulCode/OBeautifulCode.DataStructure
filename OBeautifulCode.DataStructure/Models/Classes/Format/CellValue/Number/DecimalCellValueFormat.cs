// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DecimalCellValueFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.Type;

    /// <summary>
    /// A cell value format for a decimal value.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class DecimalCellValueFormat : NumberCellFormatBase<decimal>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalCellValueFormat"/> class.
        /// </summary>
        /// <param name="prefix">OPTIONAL string to display before the cell's value is displayed.  DEFAULT is to no prefix.</param>
        /// <param name="suffix">OPTIONAL string to display after the cell's value is displayed.  DEFAULT is no suffix.</param>
        /// <param name="numberOfDecimalPlaces">OPTIONAL number of digits after the decimal separator to display.  DEFAULT is to display all digits.</param>
        /// <param name="midpointRounding">OPTIONAL strategy to use to round the number.  DEFAULT is to leave the strategy unspecified.</param>
        /// <param name="decimalSeparator">OPTIONAL character to use to separate whole numbers from fractional numbers.  Default is to leave the separator unspecified.</param>
        /// <param name="digitGroupKind">OPTIONAL kind of digit grouping to employ.  DEFAULT is to leave the methodology unspecified.</param>
        /// <param name="digitGroupSeparator">OPTIONAL separator character to use between digit groups.  DEFAULT is to leave the separator unspecified.</param>
        /// <param name="negativeNumberDisplayKind">OPTIONAL value that specifies how to display negative numbers.  DEFAULT is to leave the methodology unspecified.</param>
        /// <param name="missingValueText">OPTIONAL text to use when the cell is missing a value.  DEFAULT is to leave this text unspecified.</param>
        public DecimalCellValueFormat(
            string prefix = null,
            string suffix = null,
            int? numberOfDecimalPlaces = null,
            MidpointRounding? midpointRounding = null,
            char? decimalSeparator = null,
            NumberFormatDigitGroupKind? digitGroupKind = null,
            char? digitGroupSeparator = null,
            NumberFormatNegativeDisplayKind? negativeNumberDisplayKind = null,
            string missingValueText = null)
            : base(numberOfDecimalPlaces, midpointRounding, decimalSeparator, digitGroupKind, digitGroupSeparator, negativeNumberDisplayKind, missingValueText)
        {
            this.Prefix = prefix;
            this.Suffix = suffix;
        }

        /// <summary>
        /// Gets a string to display before the cell's value is displayed.
        /// </summary>
        public string Prefix { get; private set; }

        /// <summary>
        /// Gets a string to display after the cell's value is displayed.
        /// </summary>
        public string Suffix { get; private set; }
    }
}
