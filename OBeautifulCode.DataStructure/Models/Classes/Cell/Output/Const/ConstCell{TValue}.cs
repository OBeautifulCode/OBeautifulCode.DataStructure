// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Implementation of <see cref="IConstOutputCell{TValue}"/> with a standard set of features.
    /// </summary>
    /// <typeparam name="TValue">The type of output value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ConstCell<TValue> : ConstOutputCellBase<TValue>, IHaveStandardCellFeatures, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstCell{TValue}"/> class.
        /// </summary>
        /// <param name="value">The cell's value.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public ConstCell(
            TValue value,
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(value, id, columnsSpanned, details)
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