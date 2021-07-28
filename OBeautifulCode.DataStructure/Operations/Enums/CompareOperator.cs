// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompareOperator.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies the operator to use for comparing two values.
    /// </summary>
    public enum CompareOperator
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Perform a greater-than comparison.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Perform a greater-than-or-equal-to comparison.
        /// </summary>
        GreaterThanOrEqualTo,

        /// <summary>
        /// Perform a less-than operation.
        /// </summary>
        LessThan,

        /// <summary>
        /// Perform a less-than-or-equal-to
        /// </summary>
        LessThanOrEqualTo,
    }
}