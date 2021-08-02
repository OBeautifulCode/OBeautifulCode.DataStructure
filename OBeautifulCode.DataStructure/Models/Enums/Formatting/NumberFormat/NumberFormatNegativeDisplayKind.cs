// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumberFormatNegativeDisplayKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies how to display negative numbers.
    /// </summary>
    public enum NumberFormatNegativeDisplayKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Put a negative sign at the beginning of the number.
        /// </summary>
        /// <remarks>
        /// For example: -527
        /// </remarks>
        NegativeSignAtBeginning,

        /// <summary>
        /// Put a negative sign at the end of the number.
        /// </summary>
        /// <remarks>
        /// For example: 527-
        /// </remarks>
        NegativeSignAtEnd,

        /// <summary>
        /// Wrap the number in parenthesis.
        /// </summary>
        /// <remarks>
        /// For example: (527)
        /// </remarks>
        WrappedInParenthesis,

        /// <summary>
        /// Wrap the number in square brackets.
        /// </summary>
        /// <remarks>
        /// For example: [527]
        /// </remarks>
        WrappedInSquareBrackets,
    }
}