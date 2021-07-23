// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Implementation of <see cref="IInputCell{TValue}"/> with a standard set of features.
    /// </summary>
    /// <typeparam name="TValue">The type of input value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class InputCell<TValue> : InputCellBase<TValue>, IHaveStandardFeatures, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputCell{TValue}"/> class.
        /// </summary>
        /// <param name="inputAppliedToCellEvent">OPTIONAL input that was applied to the cell.  DEFAULT is a cell with no inputted value.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public InputCell(
            InputAppliedToCellEvent<TValue> inputAppliedToCellEvent = null,
            string id = null,
            int? columnsSpanned = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(inputAppliedToCellEvent, id, columnsSpanned)
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