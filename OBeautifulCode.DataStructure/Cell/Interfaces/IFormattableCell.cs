// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFormattableCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// A cell that can be formatted.
    /// </summary>
    public interface IFormattableCell : ICell
    {
        /// <summary>
        /// Gets the format to apply to the cell.
        /// </summary>
        CellFormat Format { get; }
    }
}
