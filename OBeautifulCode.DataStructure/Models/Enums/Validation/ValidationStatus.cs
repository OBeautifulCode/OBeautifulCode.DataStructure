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
        /// There is no validation specified for the subject.
        /// </summary>
        NoValidation,

        /// <summary>
        /// A validation condition was aborted when executing.
        /// </summary>
        ConditionExecutionAborted,

        /// <summary>
        /// A validation condition failed to be executed.
        /// </summary>
        ConditionExecutionFailed,

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