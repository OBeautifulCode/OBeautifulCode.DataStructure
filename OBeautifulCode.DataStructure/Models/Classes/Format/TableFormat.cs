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
        /// <param name="rowFormat">OPTIONAL format to apply to all rows in the table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="columnFormat">OPTIONAL format to apply to all columns in the table.  DEFAULT is to leave the format unchanged.</param>
        public TableFormat(
            RowFormat rowFormat = null,
            ColumnFormat columnFormat = null)
        {
            this.RowFormat = rowFormat;
            this.ColumnFormat = columnFormat;
        }

        /// <summary>
        /// Gets the format to apply to all cells in the table.
        /// </summary>
        public RowFormat RowFormat { get; private set; }

        /// <summary>
        /// Gets the format to apply to all cells in the table.
        /// </summary>
        public ColumnFormat ColumnFormat { get; private set; }
    }
}
