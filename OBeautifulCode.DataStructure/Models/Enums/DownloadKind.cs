// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DownloadKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies a kind of download that is support when downloading a <see cref="Report"/>.
    /// </summary>
    public enum DownloadKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// A Microsoft Excel file.
        /// </summary>
        Excel,

        /// <summary>
        /// A CSV file.
        /// </summary>
        Csv,

        /// <summary>
        /// A PDF file.
        /// </summary>
        Pdf,
    }
}