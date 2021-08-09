// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellOpExecutionOutcome.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// The outcome of executing an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.
    /// </summary>
    public enum CellOpExecutionOutcome
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The operation it not applicable.
        /// </summary>
        NotApplicable,

        /// <summary>
        /// The operation was aborted.
        /// </summary>
        Aborted,

        /// <summary>
        /// The operation failed.
        /// </summary>
        Failed,

        /// <summary>
        /// The operation completed.
        /// </summary>
        Completed,
    }
}