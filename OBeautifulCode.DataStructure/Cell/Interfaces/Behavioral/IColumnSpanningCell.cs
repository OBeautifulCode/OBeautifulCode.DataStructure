// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IColumnSpanningCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// A column spanning cell.
    /// </summary>
    public interface IColumnSpanningCell : ICell
    {
        /// <summary>
        /// Gets the number of columns spanned.
        /// </summary>
        int ColumnsSpanned { get; }
    }
}
