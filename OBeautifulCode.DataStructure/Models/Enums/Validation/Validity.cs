// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Validity.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// The validity of some subject.
    /// </summary>
    public enum Validity
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The subject is valid.
        /// </summary>
        Valid,

        /// <summary>
        /// The subject is invalid.
        /// </summary>
        Invalid,

        /// <summary>
        /// The validation is not applicable to the subject.
        /// </summary>
        NotApplicable,
    }
}