// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SectionFormatOptions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    /// <summary>
    /// Some options for formatting a section.
    /// </summary>
    [Flags]
    public enum SectionFormatOptions
    {
        /// <summary>
        /// None (default).
        /// </summary>
        None = 0,

        /// <summary>
        /// Hide the section.
        /// </summary>
        Hide = 1,
    }
}