// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AvailabilityCheckStepAction.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Determines what to do within the context of executing a <see cref="AvailabilityCheckStep"/>.
    /// </summary>
    public enum AvailabilityCheckStepAction
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Move on and evaluate the next step if there is one.
        /// </summary>
        NextStep,

        /// <summary>
        /// Stop; do not execute the next availability check step.  The subject is enabled.
        /// </summary>
        StopAsEnabled,

        /// <summary>
        /// Stop; do not execute the next availability check step.  The subject is disabled.
        /// </summary>
        StopAsDisabled,
    }
}