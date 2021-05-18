// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveValueCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// A cell having a value.
    /// </summary>
    public interface IHaveValueCell : ICell
    {
        /// <summary>
        /// Gets the cell's underlying value.
        /// </summary>
        /// <returns>
        /// The value of the cell.
        /// </returns>
        object GetValue();
    }
}
