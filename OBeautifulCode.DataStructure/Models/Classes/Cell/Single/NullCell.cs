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
    public partial class NullCell : StandardCellBase, IHaveDisplayValueCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullCell"/> class.
        /// </summary>
        /// <param name="displayValue">OPTIONAL display value for the cell.  DEFAULT is no display value.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public NullCell(
            string displayValue = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(format, hoverOver, link)
        {
            this.DisplayValue = displayValue;
        }

        /// <inheritdoc />
        public string DisplayValue { get; private set; }
    }
}