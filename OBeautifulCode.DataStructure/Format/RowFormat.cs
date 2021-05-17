// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// The format to apply to a row in a tree table.
    /// </summary>
    public partial class RowFormat : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RowFormat"/> class.
        /// </summary>
        /// <param name="format">OPTIONAL format to apply to all cells in the row.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="heightInPixels">OPTIONAL height, in pixels, to apply to the row.  DEFAULT is to leave the height unchanged.</param>
        public RowFormat(
            CellFormat format = null,
            int? heightInPixels = null)
        {
            this.Format = format;
            this.HeightInPixels = heightInPixels;
        }

        /// <summary>
        /// Gets the format to apply to all cells in the row.
        /// </summary>
        public CellFormat Format { get; private set; }

        /// <summary>
        /// Gets the height, in pixels, to apply to the row.
        /// </summary>
        public int? HeightInPixels { get; private set; }
    }
}
