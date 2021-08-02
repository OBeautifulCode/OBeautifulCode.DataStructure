// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumberFormatPercentDisplayKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies how to display percentages.
    /// </summary>
    public enum NumberFormatPercentDisplayKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Put the percent sign at the beginning of the number.
        /// </summary>
        /// <remarks>
        /// For example: %98
        /// </remarks>
        PercentSignAtBeginning,

        /// <summary>
        /// Put the percent sign at the end of the number.
        /// </summary>
        /// <remarks>
        /// For example: 98%
        /// </remarks>
        PercentSignAtEnd,
    }
}