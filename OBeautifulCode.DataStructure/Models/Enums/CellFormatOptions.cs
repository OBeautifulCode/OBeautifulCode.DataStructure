// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellFormatOptions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    /// <summary>
    /// Some options for formatting a cell in a tree table.
    /// </summary>
    [Flags]
    public enum CellFormatOptions
    {
        /// <summary>
        /// None (default).
        /// </summary>
        None = 0,

        /// <summary>
        /// Bold the cell.
        /// </summary>
        Bold = 1,

        /// <summary>
        /// Italicize the cell.
        /// </summary>
        Italics = 2,

        /// <summary>
        /// Underline the cell.
        /// </summary>
        Underline = 4,

        /// <summary>
        /// Wrap the text.
        /// </summary>
        WrapText = 8,
    }
}