// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// The format to apply to a tree table.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class TableFormat : MultiCellRegionFormatBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableFormat"/> class.
        /// </summary>
        /// <param name="outerBorders">OPTIONAL borders to apply to the outside of the table, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="innerBorders">OPTIONAL borders to apply to the cells inside the table, in the order that they should be applied.  DEFAULT is no border.</param>
        /// <param name="cellsFormat">OPTIONAL format to apply to all cells in the table, individually.  DEFAULT is to leave the format unchanged.</param>
        public TableFormat(
            IReadOnlyList<OuterBorder> outerBorders = null,
            IReadOnlyList<InnerBorder> innerBorders = null,
            CellFormat cellsFormat = null)
            : base(outerBorders, innerBorders)
        {
            this.CellsFormat = cellsFormat;
        }

        /// <summary>
        /// Gets the format to apply to all cells in the table, individually.
        /// </summary>
        public CellFormat CellsFormat { get; private set; }
    }
}
