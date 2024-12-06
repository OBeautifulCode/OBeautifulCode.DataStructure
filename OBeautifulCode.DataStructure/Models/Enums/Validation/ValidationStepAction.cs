// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationStepAction.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Determines what to do within the context of executing a <see cref="ValidationStepBase"/>.
    /// </summary>
    public enum ValidationStepAction
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
        /// Stop; do not execute the next validation step.  The subject is valid.
        /// </summary>
        StopAsValid,

        /// <summary>
        /// Stop; do not execute the next validation step.  The subject is invalid.
        /// </summary>
        StopAsInvalid,

        /// <summary>
        /// Stop; do not execute the next validation step.  The validation should be aborted.
        /// </summary>
        StopToAbort,

        /// <summary>
        /// Stop; do not execute the next validation step.  The validation is not applicable to the subject.
        /// </summary>
        StopAsNotApplicable,
    }
}