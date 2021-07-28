// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationStatus.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// The status of a validation.
    /// </summary>
    public enum ValidationStatus
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// There are no validation conditions for the subject.
        /// </summary>
        NoConditions,

        /// <summary>
        /// The validation was not run.
        /// </summary>
        Unvalidated,

        /// <summary>
        /// A validation condition deemed the validation not applicable.
        /// </summary>
        DeemedNotApplicable,

        /// <summary>
        /// A validation condition was aborted when executing.
        /// </summary>
        Aborted,

        /// <summary>
        /// A validation condition failed to be executed.
        /// </summary>
        Failed,

        /// <summary>
        /// The subject is valid (all conditions pass/are met).
        /// </summary>
        Valid,

        /// <summary>
        /// The subject is invalid (a condition failed to be met).
        /// </summary>
        Invalid,
    }
}