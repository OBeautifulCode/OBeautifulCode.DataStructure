// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationStatus.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// The status of a validation performed on some subject.
    /// </summary>
    public enum ValidationStatus
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// There is no validation to perform.
        /// </summary>
        ValidationMissing,

        /// <summary>
        /// The validation has not been executed.
        /// </summary>
        Unvalidated,

        /// <summary>
        /// The validation was run and the subject was deemed not applicable.
        /// </summary>
        DeemedNotApplicable,

        /// <summary>
        /// The validation of the subject was aborted.
        /// </summary>
        Aborted,

        /// <summary>
        /// The validation of the subject failed.
        /// </summary>
        Failed,

        /// <summary>
        /// The subject is valid.
        /// </summary>
        DeterminedSubjectIsValid,

        /// <summary>
        /// The subject is invalid.
        /// </summary>
        DeterminedSubjectIsInvalid,
    }
}