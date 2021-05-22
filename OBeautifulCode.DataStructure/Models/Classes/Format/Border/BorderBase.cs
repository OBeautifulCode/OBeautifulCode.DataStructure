﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BorderBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Drawing;

    using OBeautifulCode.Type;

    /// <summary>
    /// Base class for a border.
    /// </summary>
    public abstract partial class BorderBase : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BorderBase"/> class.
        /// </summary>
        /// <param name="weight">The weight of the border.</param>
        /// <param name="style">The style of the border.</param>
        /// <param name="color">The color of the border.</param>
        protected BorderBase(
            BorderWeight weight,
            BorderStyle style,
            Color color)
        {
            this.Weight = weight;
            this.Style = style;
            this.Color = color;
        }

        /// <summary>
        /// Gets the weight of the border.
        /// </summary>
        public BorderWeight Weight { get; private set; }

        /// <summary>
        /// Gets the style of the border.
        /// </summary>
        public BorderStyle Style { get; private set; }

        /// <summary>
        /// Gets the color of the border.
        /// </summary>
        public Color Color { get; private set; }
    }
}
