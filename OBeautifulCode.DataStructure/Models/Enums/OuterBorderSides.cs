// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OuterBorderSides.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    /// <summary>
    /// Specifies the sides of the outer border of some region of a tree table.
    /// </summary>
    [Flags]
    public enum OuterBorderSides
    {
        /// <summary>
        /// None (default).
        /// </summary>
        None = 0,

        /// <summary>
        /// The top border.
        /// </summary>
        Top = 1,

        /// <summary>
        /// The bottom border.
        /// </summary>
        Bottom = 2,

        /// <summary>
        /// The left border.
        /// </summary>
        Left = 4,

        /// <summary>
        /// The right border.
        /// </summary>
        Right = 8,

        /// <summary>
        /// All sides of the border.
        /// </summary>
        All = Top | Bottom | Left | Right,
    }
}