// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegionFormatBase.cs" company="OBeautifulCode">
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
    /// Base class for the format to apply to a region of a tree table.
    /// </summary>
    public abstract partial class RegionFormatBase : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegionFormatBase"/> class.
        /// </summary>
        /// <param name="outerBorders">Borders to apply to the outside of the region, in the order that they should be applied.</param>
        protected RegionFormatBase(
            IReadOnlyList<OuterBorder> outerBorders)
        {
            if (outerBorders != null)
            {
                if (!outerBorders.Any())
                {
                    throw new ArgumentException(Invariant($"{nameof(outerBorders)} is an empty enumerable."));
                }

                if (outerBorders.Any(_ => _ == null))
                {
                    throw new ArgumentException(Invariant($"{nameof(outerBorders)} contains at least one null element."));
                }
            }

            this.OuterBorders = outerBorders;
        }

        /// <summary>
        /// Gets the borders to apply to the outside of the region, in the order that they should be applied.
        /// </summary>
        public IReadOnlyList<OuterBorder> OuterBorders { get; private set; }
    }
}
