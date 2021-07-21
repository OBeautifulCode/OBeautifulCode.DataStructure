// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveCellFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies formatting for a cell.
    /// </summary>
    public interface IHaveCellFormat
    {
        /// <summary>
        /// Gets the format to apply to the cell.
        /// </summary>
        CellFormat Format { get; }
    }
}
