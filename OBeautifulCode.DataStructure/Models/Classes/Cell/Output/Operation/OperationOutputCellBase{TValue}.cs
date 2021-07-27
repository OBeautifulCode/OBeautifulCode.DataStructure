// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationOutputCellBase{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Base implementation of <see cref="IOperationOutputCell{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of output value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class OperationOutputCellBase<TValue> : OutputCellBase<TValue>, IOperationOutputCell<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationOutputCellBase{TValue}"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="cellOpExecutionEvent">The result of executing the operation.</param>
        /// <param name="validationConditions">A list of conditions that determine the validity of the cell's value.</param>
        /// <param name="cellValidationEvent">The result of validating the cell's value.</param>
        /// <param name="id">The cell's unique identifier.</param>
        /// <param name="columnsSpanned">The number of columns spanned or null if none (cell occupies a single column).</param>
        /// <param name="details">Details about the cell.</param>
        protected OperationOutputCellBase(
            IReturningOperation<TValue> operation,
            CellOpExecutionEventBase cellOpExecutionEvent,
            ValidationConditions validationConditions,
            CellValidationEventBase cellValidationEvent,
            string id,
            int? columnsSpanned,
            string details)
            : base(id, columnsSpanned, details)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            this.Operation = operation;
            this.CellOpExecutionEvent = cellOpExecutionEvent;
            this.ValidationConditions = validationConditions;
            this.CellValidationEvent = cellValidationEvent;
        }

        /// <inheritdoc />
        public IReturningOperation<TValue> Operation { get; private set; }

        /// <inheritdoc />
        public CellOpExecutionEventBase CellOpExecutionEvent { get; private set; }

        /// <inheritdoc />
        public ValidationConditions ValidationConditions { get; private set; }

        /// <inheritdoc />
        public CellValidationEventBase CellValidationEvent { get; private set; }

        /// <inheritdoc />
        public void RecordExecution(
            CellOpExecutionEventBase cellOpExecutionEvent)
        {
            if (cellOpExecutionEvent == null)
            {
                throw new ArgumentNullException(nameof(cellOpExecutionEvent));
            }

            this.CellOpExecutionEvent = cellOpExecutionEvent;
        }

        /// <inheritdoc />
        public void ClearCellValue()
        {
            this.CellOpExecutionEvent = null;
        }

        /// <inheritdoc />
        public override TValue GetCellValue()
        {
            if (!this.HasCellValue())
            {
                throw new InvalidOperationException("The operation hasn't been executed to completion.");
            }

            var result = ((CellOpExecutionCompletedEvent<TValue>)this.CellOpExecutionEvent).ExecutionResult;

            return result;
        }

        /// <inheritdoc />
        public void RecordValidation(
            CellValidationEventBase cellValidationEvent)
        {
            if (cellValidationEvent == null)
            {
                throw new ArgumentNullException(nameof(cellValidationEvent));
            }

            this.CellValidationEvent = cellValidationEvent;
        }

        /// <inheritdoc />
        public void ClearValidation()
        {
            this.CellValidationEvent = null;
        }

        /// <inheritdoc />
        public override bool IsConstCell() => false;

        /// <inheritdoc />
        public override bool IsInputCell() => false;

        /// <inheritdoc />
        public override bool IsOperationCell() => true;

        /// <inheritdoc />
        public override Type GetValueTypeOrNull() => typeof(TValue);

        /// <inheritdoc />
        public override bool HasCellValue() => (this.CellOpExecutionEvent != null) && (this.CellOpExecutionEvent is CellOpExecutionCompletedEvent<TValue>);
    }
}