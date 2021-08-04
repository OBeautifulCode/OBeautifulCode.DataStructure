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
        /// <param name="id">The cell's unique identifier.</param>
        /// <param name="columnsSpanned">The number of columns spanned or null if none (cell occupies a single column).</param>
        /// <param name="details">Details about the cell.</param>
        /// <param name="validation">The validation to perform.</param>
        /// <param name="validationEvents">The events that record the validation of this cell.</param>
        /// <param name="availabilityCheck">The availability check to perform.</param>
        /// <param name="availabilityCheckEvents">The events that record the availability checks on this cell.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="operationExecutionEvents">The events that record the execution of <paramref name="operation"/>.</param>
        protected OperationOutputCellBase(
            string id,
            int? columnsSpanned,
            string details,
            Validation validation,
            IReadOnlyList<CellValidationEventBase> validationEvents,
            AvailabilityCheck availabilityCheck,
            IReadOnlyList<CellAvailabilityCheckEventBase> availabilityCheckEvents,
            IReturningOperation<TValue> operation,
            IReadOnlyList<CellOpExecutionEventBase> operationExecutionEvents)
            : base(id, columnsSpanned, details, validation, validationEvents, availabilityCheck, availabilityCheckEvents)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if ((operationExecutionEvents != null) && operationExecutionEvents.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(operationExecutionEvents)} contains a null element."));
            }

            this.Operation = operation;
            this.OperationExecutionEvents = operationExecutionEvents;
        }

        /// <inheritdoc />
        public IReturningOperation<TValue> Operation { get; private set; }

        /// <inheritdoc />
        public IReadOnlyList<CellOpExecutionEventBase> OperationExecutionEvents { get; private set; }

        /// <inheritdoc />
        public void Record(
            CellOpExecutionEventBase operationExecutionEvent)
        {
            if (operationExecutionEvent == null)
            {
                throw new ArgumentNullException(nameof(operationExecutionEvent));
            }

            this.OperationExecutionEvents = new CellOpExecutionEventBase[0]
                .Concat(this.OperationExecutionEvents ?? new CellOpExecutionEventBase[0])
                .Concat(new[] { operationExecutionEvent })
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

            var result = ((CellOpExecutionCompletedEvent<TValue>)this.OperationExecutionEvents.Last()).ExecutionResult;

            return result;
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
        public override bool HasCellValue() => this.OperationExecutionEvents?.LastOrDefault() is CellOpExecutionCompletedEvent<TValue>;
    }
}