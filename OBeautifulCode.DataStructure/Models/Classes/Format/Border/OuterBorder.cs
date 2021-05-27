// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OuterBorder.cs" company="OBeautifulCode">
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
    /// A border on the outside of a region of a tree table.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class OuterBorder : BorderBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OuterBorder"/> class.
        /// </summary>
        /// <param name="weight">The weight of the border.</param>
        /// <param name="style">The style of the border.</param>
        /// <param name="color">The color of the border.</param>
        /// <param name="sides">The sides to apply the border to.</param>
        public OuterBorder(
            BorderWeight weight,
            BorderStyle style,
            Color color,
            OuterBorderSides sides)
            : base(weight, style, color)
        {
            if (sides == OuterBorderSides.None)
            {
                throw new ArgumentException(Invariant($"{nameof(sides)} is {nameof(OuterBorderSides.None)}."));
            }

            this.Sides = sides;
        }

        /// <summary>
        /// Gets the sides to apply the border to.
        /// </summary>
        public OuterBorderSides Sides { get; private set; }
    }
}
