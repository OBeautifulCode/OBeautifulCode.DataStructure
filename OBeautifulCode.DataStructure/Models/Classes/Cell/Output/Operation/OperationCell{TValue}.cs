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
    public partial class OperationCell<TValue> : OperationOutputCellBase<TValue>, IHaveStandardCellFeatures<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationCell{TValue}"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="cellOpExecutionEvent">OPTIONAL result of executing the operation.  DEFAULT is a cell who's operation has not yet been executed.</param>
        /// <param name="validationConditions">OPTIONAL list of conditions that determine the validity of the cell's value.  DEFAULT is to omit validation.</param>
        /// <param name="cellValidationEvent">OPTIONAL result of validating the cell's value.  DEFAULT is a cell that has not yet been validated.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="valueFormat">OPTIONAL format to apply to the cell value.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public OperationCell(
            IReturningOperation<TValue> operation,
            CellOpExecutionEventBase cellOpExecutionEvent = null,
            ValidationConditions validationConditions = null,
            CellValidationEventBase cellValidationEvent = null,
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(operation, cellOpExecutionEvent, validationConditions, cellValidationEvent, id, columnsSpanned, details)
        {
            this.ValueFormat = valueFormat;
            this.Format = format;
            this.HoverOver = hoverOver;
            this.Link = link;
        }

        /// <inheritdoc />
        public ICellValueFormat<TValue> ValueFormat { get; private set; }

        /// <inheritdoc />
        public CellFormat Format { get; private set; }

        /// <inheritdoc />
        public IHoverOver HoverOver { get; private set; }

        /// <inheritdoc />
        public ILink Link { get; private set; }
    }
}