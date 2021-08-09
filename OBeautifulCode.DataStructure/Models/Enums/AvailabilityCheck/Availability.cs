// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Availability.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// The availability of some subject.
    /// </summary>
    public enum Availability
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The subject is enabled.
        /// </summary>
        Enabled,

        /// <summary>
        /// The subject is disabled.
        /// </summary>
        Disabled,
    }
}