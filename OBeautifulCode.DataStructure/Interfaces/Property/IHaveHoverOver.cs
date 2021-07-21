// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveHoverOver.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies a hover-over on a cell.
    /// </summary>
    public interface IHaveHoverOver
    {
        /// <summary>
        /// Gets the cell's hover-over.
        /// </summary>
        IHoverOver HoverOver { get; }
    }
}
