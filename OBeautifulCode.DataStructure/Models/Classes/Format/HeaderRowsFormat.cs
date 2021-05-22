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
        /// <param name="rowsFormat">OPTIONAL format to apply to all header rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="outerBorder">OPTIONAL border to apply to the outside of the header rows.  DEFAULT is no border.</param>
        /// <param name="innerBorder">OPTIONAL border to apply to the cells inside the header rows.  DEFAULT is no border.</param>
        public HeaderRowsFormat(
            RowFormat rowsFormat = null,
            OuterBorder outerBorder = null,
            InnerBorder innerBorder = null)
        {
            this.RowsFormat = rowsFormat;
            this.OuterBorder = outerBorder;
            this.InnerBorder = innerBorder;
        }

        /// <summary>
        /// Gets the format to apply to all header rows, individually.
        /// </summary>
        public RowFormat RowsFormat { get; private set; }

        /// <summary>
        /// Gets the border to apply to the outside of the header rows.
        /// </summary>
        public OuterBorder OuterBorder { get; private set; }

        /// <summary>
        /// Gets the border to apply to the cells inside the header rows.
        /// </summary>
        public InnerBorder InnerBorder { get; private set; }
    }
}
