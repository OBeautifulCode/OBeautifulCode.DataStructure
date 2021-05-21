// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnFormatOptions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    /// <summary>
    /// Some options for formatting a column in a tree table.
    /// </summary>
    [Flags]
    public enum ColumnFormatOptions
    {
        /// <summary>
        /// None (default).
        /// </summary>
        None = 0,

        /// <summary>
        /// Hide the column.
        /// </summary>
        Hide = 1,
    }
}