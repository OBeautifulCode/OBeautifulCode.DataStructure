// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// A null cell; contains nothing.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class NullCell : CellBase, IFormattableCell, IHaveHoverOverCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullCell"/> class.
        /// </summary>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        public NullCell(
            CellFormat format = null,
            IHoverOver hoverOver = null)
        {
            this.Format = format;
            this.HoverOver = hoverOver;
        }

        /// <inheritdoc />
        public CellFormat Format { get; private set; }

        /// <inheritdoc />
        public IHoverOver HoverOver { get; private set; }
    }
}