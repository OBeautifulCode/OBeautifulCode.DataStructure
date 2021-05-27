// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InnerBorder.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Drawing;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A border applied to the edges of the cells inside some region of a tree table.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class InnerBorder : BorderBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InnerBorder"/> class.
        /// </summary>
        /// <param name="weight">The weight of the border.</param>
        /// <param name="style">The style of the border.</param>
        /// <param name="color">The color of the border.</param>
        /// <param name="edges">The edges to apply the border to.</param>
        public InnerBorder(
            BorderWeight weight,
            BorderStyle style,
            Color color,
            InnerBorderEdges edges)
            : base(weight, style, color)
        {
            if (edges == InnerBorderEdges.None)
            {
                throw new ArgumentException(Invariant($"{nameof(edges)} is {nameof(InnerBorderEdges.None)}."));
            }

            this.Edges = edges;
        }

        /// <summary>
        /// Gets the edges to apply the border to.
        /// </summary>
        public InnerBorderEdges Edges { get; private set; }
    }
}
