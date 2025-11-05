// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureCellProtocols{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CoreOperation;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// The core <see cref="ICell"/>-related protocols.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = ObcSuppressBecause.CA1506_AvoidExcessiveClassCoupling_DisagreeWithAssessment)]
    public class DataStructureCellProtocols<TValue> :
        DataStructureCellProtocolsBase,
        ISyncAndAsyncReturningProtocol<ThrowOpExecutionAbortedExceptionOp<TValue>, TValue>,
        ISyncAndAsyncReturningProtocol<ThrowOpExecutionDeemedNotApplicableExceptionOp<TValue>, TValue>,
        ISyncAndAsyncReturningProtocol<ThrowOpExecutionFailedExceptionOp<TValue>, TValue>,
        ISyncAndAsyncVoidProtocol<ExecuteOperationCellIfNecessaryOp<TValue>>,
        ISyncAndAsyncReturningProtocol<GetCellValueOp<TValue>, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataStructureCellProtocols{TValue}"/> class.
        /// </summary>
        /// <param name="reportAgent">The report cache in-context.</param>
        /// <param name="protocolFactory">The protocol factory to use when executing an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.</param>
        /// <param name="timestampUtc">The timestamp (in UTC) to use when recording a <see cref="CellOpExecutionEventBase"/> with an <see cref="IOperationOutputCell{TValue}"/>.</param>
        /// <param name="getRecalcPhaseFunc">Func that gets the <see cref="RecalcPhase"/>.</param>
        /// <param name="currentCellStack">Gets a stack of cells that are the "current" cell.</param>
        public DataStructureCellProtocols(
            ReportAgent reportAgent,
            IProtocolFactory protocolFactory,
            DateTime timestampUtc,
            Func<RecalcPhase> getRecalcPhaseFunc,
            Stack<ICell> currentCellStack)
            : base(reportAgent, protocolFactory, timestampUtc, getRecalcPhaseFunc, currentCellStack)
        {
        }

        /// <inheritdoc />
        public TValue Execute(
            ThrowOpExecutionAbortedExceptionOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            throw new OpExecutionAbortedException(operation.Details, abortingOperation: operation);
        }

        /// <inheritdoc />
        public async Task<TValue> ExecuteAsync(
            ThrowOpExecutionAbortedExceptionOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            await Task.FromResult(0);

            throw new OpExecutionAbortedException(operation.Details, abortingOperation: operation);
        }

        /// <inheritdoc />
        public TValue Execute(
            ThrowOpExecutionDeemedNotApplicableExceptionOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            throw new OpExecutionDeemedNotApplicableException(operation.Details, operation);
        }

        /// <inheritdoc />
        public async Task<TValue> ExecuteAsync(
            ThrowOpExecutionDeemedNotApplicableExceptionOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            await Task.FromResult(0);

            throw new OpExecutionDeemedNotApplicableException(operation.Details, operation);
        }

        /// <inheritdoc />
        public TValue Execute(
            ThrowOpExecutionFailedExceptionOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            throw new OpExecutionFailedException(operation.Details, operation: operation);
        }

        /// <inheritdoc />
        public async Task<TValue> ExecuteAsync(
            ThrowOpExecutionFailedExceptionOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            await Task.FromResult(0);

            throw new OpExecutionFailedException(operation.Details, operation: operation);
        }

        /// <inheritdoc />
        public void Execute(
            ExecuteOperationCellIfNecessaryOp<TValue> operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cell = operation.Cell;

            try
            {
                this.CurrentCellStack.Push(cell);

                this.ExecuteOperationCellIfNecessary(cell);
            }
            finally
            {
                var poppedCell = this.CurrentCellStack.Pop();

                ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
            }
        }

        /// <inheritdoc />
        public async Task ExecuteAsync(
            ExecuteOperationCellIfNecessaryOp<TValue> operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cell = operation.Cell;

            try
            {
                this.CurrentCellStack.Push(cell);

                await this.ExecuteOperationCellIfNecessaryAsync(cell);
            }
            finally
            {
                var poppedCell = this.CurrentCellStack.Pop();

                ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
            }
        }

        /// <inheritdoc />
        public TValue Execute(
            GetCellValueOp<TValue> operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var locatedCell = this.GetCellHavingValueAndExecuteOperationIfNecessary(operation.CellLocator);

            TValue result;

            if (locatedCell.Cell.HasCellValue())
            {
                if (!(locatedCell.Cell is IGetCellValue<TValue> getCellValueCell))
                {
                    throw new CellNotFoundException(Invariant($"Addressing a cell whose type is not an {typeof(IGetCellValue<TValue>).ToStringReadable()}: {locatedCell.Cell.GetType().ToStringReadable()}."), locatedCell.CellLocator);
                }

                result = getCellValueCell.GetCellValue();
            }
            else
            {
                if (operation.DefaultValue == null)
                {
                    throw new CellValueMissingException(locatedCell.CellLocator);
                }

                result = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<TValue>(operation.DefaultValue);
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<TValue> ExecuteAsync(
            GetCellValueOp<TValue> operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var locatedCell = await this.GetCellAndExecuteOperationIfNecessaryAsync(operation.CellLocator);

            TValue result;

            if (locatedCell.Cell.HasCellValue())
            {
                if (!(locatedCell.Cell is IGetCellValue<TValue> getCellValueCell))
                {
                    throw new CellNotFoundException(Invariant($"Addressing a cell whose type is not an {typeof(IGetCellValue<TValue>).ToStringReadable()}: {locatedCell.Cell.GetType().ToStringReadable()}."), locatedCell.CellLocator);
                }

                result = getCellValueCell.GetCellValue();
            }
            else
            {
                if (operation.DefaultValue == null)
                {
                    throw new CellValueMissingException(locatedCell.CellLocator);
                }

                result = await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync<TValue>(operation.DefaultValue);
            }

            return result;
        }

        private void ExecuteOperationCellIfNecessary(
            IOperationOutputCell<TValue> cell)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (cell.GetCellOpExecutionStatus() == CellOpExecutionStatus.NotExecuted)
            {
                CellOpExecutionEventBase operationExecutionEvent;

                try
                {
                    var operationResult = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<TValue>(cell.Operation);

                    operationExecutionEvent = new CellOpExecutionCompletedEvent<TValue>(operationResult, this.TimestampUtc);
                }
                catch (OpExecutionAbortedExceptionBase ex)
                {
                    operationExecutionEvent = new CellOpExecutionAbortedEvent(this.TimestampUtc, ex.ToString());
                }
                catch (OpExecutionDeemedNotApplicableExceptionBase ex)
                {
                    operationExecutionEvent = new CellOpExecutionDeemedNotApplicableEvent(this.TimestampUtc, ex.ToString());
                }
                catch (Exception ex)
                {
                    // The "proper" exception for a protocol to throw is an OpExecutionFailedExceptionBase.
                    // Protocol authors might not comply.
                    operationExecutionEvent = new CellOpExecutionFailedEvent(this.TimestampUtc, ex.ToString());
                }

                cell.Record(operationExecutionEvent);
            }
            else if (cell.OperationExecutionEvents.Last().TimestampUtc != this.TimestampUtc)
            {
                throw new InvalidOperationException("Something went wrong.  The operation was executed, but the recorded timestamp doesn't match this timestamp.");
            }
        }

        private async Task ExecuteOperationCellIfNecessaryAsync(
            IOperationOutputCell<TValue> cell)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (cell.GetCellOpExecutionStatus() == CellOpExecutionStatus.NotExecuted)
            {
                CellOpExecutionEventBase operationExecutionEvent;

                try
                {
                    var operationResult = await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync<TValue>(cell.Operation);

                    operationExecutionEvent = new CellOpExecutionCompletedEvent<TValue>(operationResult, this.TimestampUtc);
                }
                catch (OpExecutionAbortedExceptionBase ex)
                {
                    operationExecutionEvent = new CellOpExecutionAbortedEvent(this.TimestampUtc, ex.ToString());
                }
                catch (OpExecutionDeemedNotApplicableExceptionBase ex)
                {
                    operationExecutionEvent = new CellOpExecutionDeemedNotApplicableEvent(this.TimestampUtc, ex.ToString());
                }
                catch (Exception ex)
                {
                    // The "proper" exception for a protocol to throw is an OpExecutionFailedExceptionBase.
                    // Protocol authors might not comply.
                    operationExecutionEvent = new CellOpExecutionFailedEvent(this.TimestampUtc, ex.ToString());
                }

                cell.Record(operationExecutionEvent);
            }
            else if (cell.OperationExecutionEvents.Last().TimestampUtc != this.TimestampUtc)
            {
                throw new InvalidOperationException("Something went wrong.  The operation was executed, but the recorded timestamp doesn't match this timestamp.");
            }
        }
    }
}
