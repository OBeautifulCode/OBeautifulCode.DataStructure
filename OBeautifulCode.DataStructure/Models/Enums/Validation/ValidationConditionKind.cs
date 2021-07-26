// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationConditionKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Determines how to interpret the result of executing a <see cref="ValidationCondition"/> operation.
    /// </summary>
    public enum ValidationConditionKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The condition passes (is met) when the operation returns true;
        /// it fails to be met when the operation returns false.
        /// </summary>
        PassWhenTrue,

        /// <summary>
        /// The condition passes (is met) when the operation returns false;
        /// it fails to be met when the operation returns true.
        /// </summary>
        PassWhenFalse,

        /// <summary>
        /// The condition fails to be met when the operation returns true;
        /// it passes (is met) when the operation returns false.
        /// </summary>
        FailWhenTrue,

        /// <summary>
        /// The condition fails to be met when the operation returns false;
        /// it passes (is met) when the operation returns true.
        /// </summary>
        FailWhenFalse,
    }
}