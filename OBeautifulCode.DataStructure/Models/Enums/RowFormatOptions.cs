// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowFormatOptions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    /// <summary>
    /// Some options for formatting a row in a tree table.
    /// </summary>
    [Flags]
    public enum RowFormatOptions
    {
        /// <summary>
        /// None (default).
        /// </summary>
        None = 0,

        /// <summary>
        /// Hide the row.
        /// </summary>
        Hide = 1,

        /// <summary>
        /// Disable the ability to collapse the row.
        /// </summary>
        DisableCollapsing = 2,
    }
}