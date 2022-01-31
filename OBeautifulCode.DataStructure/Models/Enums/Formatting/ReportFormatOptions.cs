// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportFormatOptions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    /// <summary>
    /// Some options for formatting a report.
    /// </summary>
    [Flags]
    public enum ReportFormatOptions
    {
        /// <summary>
        /// None (default).
        /// </summary>
        None = 0,

        /// <summary>
        /// Hide the <see cref="Report.DownloadLinks"/>.
        /// </summary>
        HideDownloadLinks = 1,
    }
}