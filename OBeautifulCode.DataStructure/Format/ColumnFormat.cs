// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// The format to apply to a column in a tree table.
    /// </summary>
    public partial class ColumnFormat : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnFormat"/> class.
        /// </summary>
        /// <param name="format">OPTIONAL format to apply to all cells in the column.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="widthInPixels">OPTIONAL width, in pixels, to apply to the column.  DEFAULT is to leave the width unchanged.</param>
        /// <param name="autoFitColumnWidth">OPTIONAL value indicating whether to auto-fit the width of the column.</param>
        /// <param name="isHidden">OPTIONAL value indicating whether the column is hidden.  DEFAULT is to leave the visibility unchanged.</param>
        public ColumnFormat(
            CellFormat format = null,
            int? widthInPixels = null,
            bool? autoFitColumnWidth = null,
            bool? isHidden = null)
        {
            this.Format = format;
            this.WidthInPixels = widthInPixels;
            this.AutoFitColumnWidth = autoFitColumnWidth;
            this.IsHidden = isHidden;
        }

        /// <summary>
        /// Gets the format to apply to all cells in the column.
        /// </summary>
        public CellFormat Format { get; private set; }

        /// <summary>
        /// Gets the width, in pixels, to apply to the column.
        /// </summary>
        public int? WidthInPixels { get; private set; }

        /// <summary>
        /// Gets a value indicating whether to auto-fit the width of the column.
        /// </summary>
        public bool? AutoFitColumnWidth { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the column is hidden.
        /// </summary>
        public bool? IsHidden { get; private set; }
    }
}
