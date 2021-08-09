// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellOpExecutionStatus.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// The status of the execution of an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.
    /// </summary>
    public enum CellOpExecutionStatus
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The operation hasn't been executed.
        /// </summary>
        NotExecuted,

        /// <summary>
        /// The execution of the operation deemed itself not applicable.
        /// </summary>
        DeemedNotApplicable,

        /// <summary>
        /// The execution of the operation was aborted.
        /// </summary>
        Aborted,

        /// <summary>
        /// The execution of the operation failed.
        /// </summary>
        Failed,

        /// <summary>
        /// The execution of the operation completed.
        /// </summary>
        Completed,
    }
}