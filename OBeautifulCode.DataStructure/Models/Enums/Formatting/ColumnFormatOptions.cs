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

        /// <summary>
        /// Freeze the column (frozen column and all columns to the left of it will not scroll).
        /// </summary>
        Freeze = 2,

        /// <summary>
        /// Makes the column sortable and exposes this by adding a sort chevron on the header cell.
        /// </summary>
        Sortable = 4,

        /// <summary>
        /// Makes the column filterable.
        /// </summary>
        Filterable = 8,
    }
}