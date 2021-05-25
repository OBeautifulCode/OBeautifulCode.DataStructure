// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// The format to apply to a tree table.
    /// </summary>
    public partial class TableFormat : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableFormat"/> class.
        /// </summary>
        /// <param name="cellsFormat">OPTIONAL format to apply to all cells in the table, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="outerBorder">OPTIONAL border to apply to the outside of the table.  DEFAULT is no border.</param>
        /// <param name="innerBorder">OPTIONAL border to apply to the cells inside the table.  DEFAULT is no border.</param>
        public TableFormat(
            CellFormat cellsFormat = null,
            OuterBorder outerBorder = null,
            InnerBorder innerBorder = null)
        {
            this.CellsFormat = cellsFormat;
            this.OuterBorder = outerBorder;
            this.InnerBorder = innerBorder;
        }

        /// <summary>
        /// Gets the format to apply to all cells in the table, individually.
        /// </summary>
        public CellFormat CellsFormat { get; private set; }

        /// <summary>
        /// Gets the border to apply to the outside of the table.
        /// </summary>
        public OuterBorder OuterBorder { get; private set; }

        /// <summary>
        /// Gets the border to apply to the cells inside the table.
        /// </summary>
        public InnerBorder InnerBorder { get; private set; }
    }
}
