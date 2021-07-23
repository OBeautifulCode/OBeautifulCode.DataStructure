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
        /// <param name="id">The cell's unique identifier.</param>
        /// <param name="columnsSpanned">The number of columns spanned or null if none (cell occupies a single column).</param>
        protected OperationOutputCellBase(
            IReturningOperation<TValue> operation,
            CellOpExecutionEventBase cellOpExecutionEvent,
            string id,
            int? columnsSpanned)
            : base(id, columnsSpanned)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            this.Operation = operation;
            this.CellOpExecutionEvent = cellOpExecutionEvent;
        }

        /// <inheritdoc />
        public IReturningOperation<TValue> Operation { get; private set; }

        /// <inheritdoc />
        public CellOpExecutionEventBase CellOpExecutionEvent { get; private set; }

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
                throw new InvalidOperationException("The operation hasn't been executed.");
            }

            if (!(this.CellOpExecutionEvent is CellOpExecutionCompletedEvent<TValue> succeededInExecutingCellOpEvent))
            {
                throw new InvalidOperationException("The operation failed or was aborted while executing.");
            }

            var result = succeededInExecutingCellOpEvent.ExecutionResult;

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
        public override bool HasCellValue() => (this.CellOpExecutionEvent != null) && (this.CellOpExecutionEvent is CellOpExecutionCompletedEvent<TValue>);
    }
}