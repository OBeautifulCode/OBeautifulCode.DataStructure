// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Implementation of <see cref="IOperationOutputCell{TValue}"/> with a standard set of features.
    /// </summary>
    /// <typeparam name="TValue">The type of output value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class OperationCell<TValue> : OperationOutputCellBase<TValue>, IHaveStandardFeatures, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationCell{TValue}"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="cellOpExecutionEvent">OPTIONAL result of executing the operation.  DEFAULT is a cell who's operation has not yet been executed.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public OperationCell(
            IReturningOperation<TValue> operation,
            CellOpExecutionEventBase cellOpExecutionEvent = null,
            string id = null,
            int? columnsSpanned = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(operation, cellOpExecutionEvent, id, columnsSpanned)
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