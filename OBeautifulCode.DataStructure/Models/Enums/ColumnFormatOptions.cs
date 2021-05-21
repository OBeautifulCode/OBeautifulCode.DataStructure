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
        /// Makes the column sortable and exposes this by adding a sort chevron on the header cell.
        /// </summary>
        Sortable = 2,

        /// <summary>
        /// In addition to <see cref="Sortable"/>, exercises the features to perform an ascending sort.
        /// </summary>
        /// <remarks>
        /// If two or more columns have this option or <see cref="SortedDescending"/>,
        /// then a double/triple/etc. sort is performed on all of those columns.
        /// </remarks>
        SortedAscending = 4,

        /// <summary>
        /// In addition to <see cref="Sortable"/>, exercises the feature to perform a descending sort.
        /// </summary>
        /// <remarks>
        /// If two or more columns have this option or <see cref="SortedDescending"/>,
        /// then a double/triple/etc. sort is performed on all of those columns.
        /// </remarks>
        SortedDescending = 8,
    }
}