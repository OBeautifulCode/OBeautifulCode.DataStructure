// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiCellRegionFormatBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Base class for the format to apply to a region of a tree table that can contain multiple cells.
    /// </summary>
    public abstract partial class MultiCellRegionFormatBase : RegionFormatBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiCellRegionFormatBase"/> class.
        /// </summary>
        /// <param name="outerBorders">Borders to apply to the outside of the region, in the order that they should be applied.</param>
        /// <param name="innerBorders">Borders to apply to the cells inside the region, in the order that they should be applied.</param>
        protected MultiCellRegionFormatBase(
            IReadOnlyList<OuterBorder> outerBorders,
            IReadOnlyList<InnerBorder> innerBorders)
            : base(outerBorders)
        {
            if (innerBorders != null)
            {
                if (!innerBorders.Any())
                {
                    throw new ArgumentException(Invariant($"{nameof(innerBorders)} is an empty enumerable."));
                }

                if (innerBorders.Any(_ => _ == null))
                {
                    throw new ArgumentException(Invariant($"{nameof(innerBorders)} contains at least one null element."));
                }
            }

            this.InnerBorders = innerBorders;
        }

        /// <summary>
        /// Gets the borders to apply to the cells inside the region, in the order that they should be applied.
        /// </summary>
        public IReadOnlyList<InnerBorder> InnerBorders { get; private set; }
    }
}
