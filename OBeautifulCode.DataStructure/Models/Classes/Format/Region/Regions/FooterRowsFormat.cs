// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FooterRowsFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// The format to apply to the footer rows in a tree table.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class FooterRowsFormat : MultiCellRegionFormatBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FooterRowsFormat"/> class.
        /// </summary>
        /// <param name="outerBorders">OPTIONAL borders to apply to the outside of the footer rows, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="innerBorders">OPTIONAL borders to apply to the cells inside the footer rows, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="rowsFormat">OPTIONAL format to apply to all footer rows, individually.  DEFAULT is to leave the format unchanged.</param>
        public FooterRowsFormat(
            IReadOnlyList<OuterBorder> outerBorders = null,
            IReadOnlyList<InnerBorder> innerBorders = null,
            RowFormat rowsFormat = null)
            : base(outerBorders, innerBorders)
        {
            this.RowsFormat = rowsFormat;
        }

        /// <summary>
        /// Gets the format to apply to all footer rows, individually.
        /// </summary>
        public RowFormat RowsFormat { get; private set; }
    }
}
