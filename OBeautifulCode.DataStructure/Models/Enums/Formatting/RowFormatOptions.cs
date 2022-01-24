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
        /// Freeze the row (frozen row and all rows above it will not scroll).
        /// </summary>
        Freeze = 2,

        /// <summary>
        /// Disable the ability to collapse the row.
        /// </summary>
        DisableCollapsing = 4,

        /// <summary>
        /// When displaying the child rows, align them with their parent; do not indent.
        /// </summary>
        AlignChildRowsWithParent = 8,
    }
}