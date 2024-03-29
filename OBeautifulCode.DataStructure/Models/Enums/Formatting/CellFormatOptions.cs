﻿// --------------------------------------------------------------------------------------------------------------------
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
        /// Wrap the text.
        /// </summary>
        WrapText = 1,
    }
}