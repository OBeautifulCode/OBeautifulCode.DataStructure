// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveHoverOverCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// A cell having a hover-over.
    /// </summary>
    public interface IHaveHoverOverCell : ICell
    {
        /// <summary>
        /// Gets the cell's hover-over.
        /// </summary>
        IHoverOver HoverOver { get; }
    }
}
