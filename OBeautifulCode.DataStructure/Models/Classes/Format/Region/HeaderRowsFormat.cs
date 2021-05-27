﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderRowsFormat.cs" company="OBeautifulCode">
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
    /// The format to apply to the header rows in a tree table.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class HeaderRowsFormat : RegionFormatBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderRowsFormat"/> class.
        /// </summary>
        /// <param name="rowsFormat">OPTIONAL format to apply to all header rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="outerBorders">OPTIONAL borders to apply to the outside of the header rows, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="innerBorders">OPTIONAL borders to apply to the cells inside the header rows, in the order that they should be applied.  DEFAULT is no border.</param>
        public HeaderRowsFormat(
            RowFormat rowsFormat = null,
            IReadOnlyList<OuterBorder> outerBorders = null,
            IReadOnlyList<InnerBorder> innerBorders = null)
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

            this.RowsFormat = rowsFormat;
            this.OuterBorders = outerBorders;
            this.InnerBorders = innerBorders;
        }

        /// <summary>
        /// Gets the format to apply to all header rows, individually.
        /// </summary>
        public RowFormat RowsFormat { get; private set; }

        /// <summary>
        /// Gets the borders to apply to the outside of the header rows, in the order that they should be applied.
        /// </summary>
        public IReadOnlyList<OuterBorder> OuterBorders { get; private set; }

        /// <summary>
        /// Gets the borders to apply to the cells inside the header rows, in the order that they should be applied.
        /// </summary>
        public IReadOnlyList<InnerBorder> InnerBorders { get; private set; }
    }
}
