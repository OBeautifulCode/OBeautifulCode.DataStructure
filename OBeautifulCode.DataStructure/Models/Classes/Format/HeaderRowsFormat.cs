// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderRowsFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// The format to apply to the header rows in a tree table.
    /// </summary>
    public partial class HeaderRowsFormat : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderRowsFormat"/> class.
        /// </summary>
        /// <param name="rowsFormat">OPTIONAL format to apply to all header rows.  DEFAULT is to leave the format unchanged.</param>
        public HeaderRowsFormat(
            RowFormat rowsFormat = null)
        {
            this.RowsFormat = rowsFormat;
        }

        /// <summary>
        /// Gets the format to apply to all header rows.
        /// </summary>
        public RowFormat RowsFormat { get; private set; }
    }
}
