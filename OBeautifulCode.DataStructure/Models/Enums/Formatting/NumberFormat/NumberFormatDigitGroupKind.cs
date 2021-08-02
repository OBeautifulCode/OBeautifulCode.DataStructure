// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumberFormatDigitGroupKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies how digits are grouped within a numeric value.
    /// </summary>
    public enum NumberFormatDigitGroupKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// 3-digit grouping.
        /// </summary>
        /// <remarks>
        /// For example: 123,456,789.00
        /// </remarks>
        ThreeDigits,

        /// <summary>
        /// 2-digit grouping except for 3-digit grouping for denoting hundreds.
        /// </summary>
        /// <remarks>
        /// For example: 12,34,56,789.00
        /// </remarks>
        TwoDigitsExceptThreeDigitsForHundreds,
    }
}