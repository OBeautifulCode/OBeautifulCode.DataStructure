﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataRowsFormat.cs" company="OBeautifulCode">
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
    /// The format to apply to the data rows (the rows below the header row) in a tree table.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class DataRowsFormat : MultiCellRegionFormatBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataRowsFormat"/> class.
        /// </summary>
        /// <param name="outerBorders">OPTIONAL borders to apply to the outside of the data rows, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="innerBorders">OPTIONAL borders to apply to the cells inside the data rows, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="rowsFormat">OPTIONAL format to apply to all data rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="rowsRepeatingFormat">OPTIONAL formats to apply in order for successive data rows, that repeat after the last format is applied.  For example, this can be used to achieve a zebra striped table by specifying two <see cref="RowFormat"/> objects having different background colors.</param>
        public DataRowsFormat(
            IReadOnlyList<OuterBorder> outerBorders = null,
            IReadOnlyList<InnerBorder> innerBorders = null,
            RowFormat rowsFormat = null,
            IReadOnlyList<RowFormat> rowsRepeatingFormat = null)
            : base(outerBorders, innerBorders)
        {
            if (rowsRepeatingFormat != null)
            {
                if (!rowsRepeatingFormat.Any())
                {
                    throw new ArgumentException(Invariant($"{nameof(rowsRepeatingFormat)} is an empty enumerable."));
                }

                if (rowsRepeatingFormat.Any(_ => _ == null))
                {
                    throw new ArgumentException(Invariant($"{nameof(rowsRepeatingFormat)} contains at least one null element."));
                }
            }

            this.RowsFormat = rowsFormat;
            this.RowsRepeatingFormat = rowsRepeatingFormat;
        }

        /// <summary>
        /// Gets the format to apply to all data rows, individually.
        /// </summary>
        public RowFormat RowsFormat { get; private set; }

        /// <summary>
        /// Gets the formats to apply in order for successive data rows, that repeat after the last format is applied.
        /// </summary>
        public IReadOnlyList<RowFormat> RowsRepeatingFormat { get; private set; }
    }
}
