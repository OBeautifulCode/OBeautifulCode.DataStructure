// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InnerBorderEdges.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    /// <summary>
    /// Specifies the edges of the cells inside some region of a tree table.
    /// </summary>
    /// <remarks>
    /// There is no concept of a side here.  For example, if the region contains two
    /// horizontally adjacent cells, then left cell's right border is the right cell's
    /// left border.  Put together, we call that border a vertical edge.
    /// </remarks>
    [Flags]
    public enum InnerBorderEdges
    {
        /// <summary>
        /// None (default).
        /// </summary>
        None = 0,

        /// <summary>
        /// The vertical edges.
        /// </summary>
        Vertical = 1,

        /// <summary>
        /// The horizontal edges.
        /// </summary>
        Horizontal = 2,

        /// <summary>
        /// All edges.
        /// </summary>
        All = Vertical | Horizontal,
    }
}