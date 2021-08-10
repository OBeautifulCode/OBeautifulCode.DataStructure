// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecalcPhase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// The phase of recalc.
    /// </summary>
    public enum RecalcPhase
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Executing the <see cref="IOperationOutputCell{TValue}"/>s' <see cref="IOperationOutputCell{TValue}.Operation"/>s.
        /// </summary>
        CellOpExecution,

        /// <summary>
        /// Performing validation.
        /// </summary>
        Validation,

        /// <summary>
        /// Performing availability checks.
        /// </summary>
        AvailabilityCheck,
    }
}