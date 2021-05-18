// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveDisplayValueCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// A cell having a display value.
    /// </summary>
    public interface IHaveDisplayValueCell : ICell
    {
        /// <summary>
        /// Gets the cell's display value.
        /// </summary>
        string DisplayValue { get; }
    }
}
