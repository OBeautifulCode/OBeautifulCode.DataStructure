﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PercentCellValueFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A cell value format for a percentage value.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class PercentCellValueFormat : NumberCellFormatBase<decimal>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PercentCellValueFormat"/> class.
        /// </summary>
        /// <param name="percentDisplayKind">OPTIONAL value that specifies how to display a percentage.  DEFAULT is to leave the methodology unspecified.</param>
        /// <param name="numberOfDecimalPlaces">OPTIONAL number of digits after the decimal separator to display.  DEFAULT is to display all digits.</param>
        /// <param name="decimalSeparator">OPTIONAL character to use to separate whole numbers from fractional numbers.  Default is to leave the separator unspecified.</param>
        /// <param name="digitGroupKind">OPTIONAL kind of digit grouping to employ.  DEFAULT is to leave the methodology unspecified.</param>
        /// <param name="digitGroupSeparator">OPTIONAL separator character to use between digit groups.  DEFAULT is to leave the separator unspecified.</param>
        /// <param name="negativeNumberDisplayKind">OPTIONAL value that specifies how to display negative numbers.  DEFAULT is to leave the methodology unspecified.</param>
        /// <param name="missingValueText">OPTIONAL text to use when the cell is missing a value.  DEFAULT is to leave this text unspecified.</param>
        public PercentCellValueFormat(
            NumberFormatPercentDisplayKind? percentDisplayKind = null,
            int? numberOfDecimalPlaces = null,
            char? decimalSeparator = null,
            NumberFormatDigitGroupKind? digitGroupKind = null,
            char? digitGroupSeparator = null,
            NumberFormatNegativeDisplayKind? negativeNumberDisplayKind = null,
            string missingValueText = null)
            : base(numberOfDecimalPlaces, decimalSeparator, digitGroupKind, digitGroupSeparator, negativeNumberDisplayKind, missingValueText)
        {
            if ((percentDisplayKind != null) && (percentDisplayKind == NumberFormatPercentDisplayKind.Unknown))
            {
                throw new ArgumentOutOfRangeException(Invariant($"{percentDisplayKind} is {nameof(NumberFormatPercentDisplayKind)}.{nameof(NumberFormatPercentDisplayKind.Unknown)}."));
            }

            this.PercentDisplayKind = percentDisplayKind;
        }

        /// <summary>
        /// Gets a value that specifies how to display a percentage.
        /// </summary>
        public NumberFormatPercentDisplayKind? PercentDisplayKind { get; private set; }
    }
}