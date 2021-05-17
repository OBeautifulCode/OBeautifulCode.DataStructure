// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStringCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// A cell that contains a string value.
    /// </summary>
    public interface IStringCell : IHaveValueCell, ICell
    {
        /// <summary>
        /// Gets the cell's string value.
        /// </summary>
        string Value { get; }
    }
}
