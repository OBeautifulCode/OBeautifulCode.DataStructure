// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AvailabilityCheckStatus.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// The status of an availability check performed on some subject.
    /// </summary>
    public enum AvailabilityCheckStatus
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// There is no availability check to perform.
        /// </summary>
        AvailabilityCheckMissing,

        /// <summary>
        /// The availability check has not been executed.
        /// </summary>
        Unchecked,

        /// <summary>
        /// The availability check on the subject failed.
        /// </summary>
        Failed,

        /// <summary>
        /// The availability check was run and it determined that the subject is enabled.
        /// </summary>
        DeterminedSubjectIsEnabled,

        /// <summary>
        /// The availability check was run and it determined that the subject is disabled.
        /// </summary>
        DeterminedSubjectIsDisabled,
    }
}