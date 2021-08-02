// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationOutputCellBase{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Type;

    using static System.FormattableString;

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
        /// <param name="cellOpExecutionEvents">The results of executing the operation.</param>
        /// <param name="validationConditions">A list of conditions that determine the validity of the cell's value.</param>
        /// <param name="cellValidationEvent">The result of validating the cell's value.</param>
        /// <param name="id">The cell's unique identifier.</param>
        /// <param name="columnsSpanned">The number of columns spanned or null if none (cell occupies a single column).</param>
        /// <param name="details">Details about the cell.</param>
        protected OperationOutputCellBase(
            IReturningOperation<TValue> operation,
            IReadOnlyList<CellOpExecutionEventBase> cellOpExecutionEvents,
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

            if ((cellOpExecutionEvents != null) && cellOpExecutionEvents.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(cellOpExecutionEvents)} contains a null element."));
            }

            this.Operation = operation;
            this.CellOpExecutionEvents = cellOpExecutionEvents;
            this.ValidationConditions = validationConditions;
            this.CellValidationEvent = cellValidationEvent;
        }

        /// <inheritdoc />
        public IReturningOperation<TValue> Operation { get; private set; }

        /// <inheritdoc />
        public IReadOnlyList<CellOpExecutionEventBase> CellOpExecutionEvents { get; private set; }

        /// <inheritdoc />
        public ValidationConditions ValidationConditions { get; private set; }

        /// <inheritdoc />
        public CellValidationEventBase CellValidationEvent { get; private set; }

        /// <inheritdoc />
        public void Record(
            CellOpExecutionEventBase cellOpExecutionEvent)
        {
            if (cellOpExecutionEvent == null)
            {
                throw new ArgumentNullException(nameof(cellOpExecutionEvent));
            }

            this.CellOpExecutionEvents = new CellOpExecutionEventBase[0]
                .Concat(this.CellOpExecutionEvents ?? new CellOpExecutionEventBase[0])
                .Concat(new[] { cellOpExecutionEvent })
                .ToList();
        }

        /// <inheritdoc />
        public void ClearCellValue(
            DateTime timestampUtc,
            string details = null)
        {
            var cellOpExecutionClearedEvent = new CellOpExecutionClearedEvent(timestampUtc, details);

            this.Record(cellOpExecutionClearedEvent);
        }

        /// <inheritdoc />
        public override TValue GetCellValue()
        {
            if (!this.HasCellValue())
            {
                throw new InvalidOperationException("The operation hasn't been executed to completion.");
            }

            var result = ((CellOpExecutionCompletedEvent<TValue>)this.CellOpExecutionEvents.Last()).ExecutionResult;

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
        public override bool HasCellValue() => this.CellOpExecutionEvents?.LastOrDefault() is CellOpExecutionCompletedEvent<TValue>;
    }
}