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
        /// <param name="cellsFormat">OPTIONAL format to apply to all cells in the table.  DEFAULT is to leave the format unchanged.</param>
        public TableFormat(
            CellFormat cellsFormat = null)
        {
            this.CellsFormat = cellsFormat;
        }

        /// <summary>
        /// Gets the format to apply to all cells in the table.
        /// </summary>
        public CellFormat CellsFormat { get; private set; }
    }
}
