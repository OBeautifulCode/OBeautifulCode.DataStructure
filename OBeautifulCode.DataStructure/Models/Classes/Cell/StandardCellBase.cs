// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardCellBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Base implementation of <see cref="ICell"/>.
    /// </summary>
    public abstract partial class StandardCellBase : CellBase, IStandardCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardCellBase"/> class.
        /// </summary>
        /// <param name="format">Format to apply to the cell.</param>
        /// <param name="hoverOver">Hover-over for the cell.</param>
        /// <param name="link">Link to some resource.</param>
        protected StandardCellBase(
            CellFormat format,
            IHoverOver hoverOver,
            ILink link)
        {
            this.Format = format;
            this.HoverOver = hoverOver;
            this.Link = link;
        }

        /// <inheritdoc />
        public CellFormat Format { get; private set; }

        /// <inheritdoc />
        public IHoverOver HoverOver { get; private set; }

        /// <inheritdoc />
        public ILink Link { get; private set; }
    }
}