﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureCellProtocols{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Threading.Tasks;

    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// The core <see cref="ICell"/>-related protocols.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    public class DataStructureCellProtocols<TValue> :
          ISyncAndAsyncReturningProtocol<GetCellValueOp<TValue>, TValue>,
          ISyncAndAsyncVoidProtocol<ExecuteOperationCellIfNecessaryOp<TValue>>,
          ISyncAndAsyncReturningProtocol<GetConstValueOp<TValue>, TValue>,
          ISyncAndAsyncReturningProtocol<HasCellValueOp, bool>
    {
        private readonly Report report;

        private readonly IProtocolFactory protocolFactory;

        private readonly DateTime timestampUtc;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStructureCellProtocols{TValue}"/> class.
        /// </summary>
        /// <param name="report">The report in-context.</param>
        /// <param name="protocolFactory">The protocol factory to use when executing an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.</param>
        /// <param name="timestampUtc">The timestamp (in UTC) to use when recording a <see cref="CellOpExecutionEventBase"/> with an <see cref="IOperationOutputCell{TValue}"/>.</param>
        public DataStructureCellProtocols(
            Report report,
            IProtocolFactory protocolFactory,
            DateTime timestampUtc)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            if (protocolFactory == null)
            {
                throw new ArgumentNullException(nameof(protocolFactory));
            }

            if (timestampUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(Invariant($"{nameof(timestampUtc)} is not in UTC time."));
            }

            this.report = report;
            this.protocolFactory = protocolFactory;
            this.timestampUtc = timestampUtc;
        }

        /// <inheritdoc />
        public TValue Execute(
            GetCellValueOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cell = this.GetCellHavingTypedValue(operation.CellLocator);

            if (cell is IOperationOutputCell<TValue> operationCell)
            {
                this.ExecuteOperationCellIfNecessary(operationCell);
            }

            TValue result;

            try
            {
                result = cell.GetCellValue();
            }
            catch (Exception ex)
            {
                throw new CellValueMissingException(ex.Message, operation.CellLocator);
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<TValue> ExecuteAsync(
            GetCellValueOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cell = this.GetCellHavingTypedValue(operation.CellLocator);

            if (cell is IOperationOutputCell<TValue> operationCell)
            {
                await this.ExecuteOperationCellIfNecessaryAsync(operationCell);
            }

            TValue result;

            try
            {
                result = cell.GetCellValue();
            }
            catch (Exception ex)
            {
                throw new CellValueMissingException(ex.Message, operation.CellLocator);
            }

            return result;
        }

        /// <inheritdoc />
        public void Execute(
            ExecuteOperationCellIfNecessaryOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            this.ExecuteOperationCellIfNecessary(operation.Cell);
        }

        /// <inheritdoc />
        public async Task ExecuteAsync(
            ExecuteOperationCellIfNecessaryOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            await this.ExecuteOperationCellIfNecessaryAsync(operation.Cell);
        }

        /// <inheritdoc />
        public TValue Execute(
            GetConstValueOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var result = operation.Value;

            return result;
        }

        /// <inheritdoc />
        public async Task<TValue> ExecuteAsync(
            GetConstValueOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var result = await Task.FromResult(operation.Value);

            return result;
        }

        /// <inheritdoc />
        public bool Execute(
            HasCellValueOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cell = this.GetCellHavingValue(operation.CellLocator);

            var result = cell.HasCellValue();

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> ExecuteAsync(
            HasCellValueOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cell = this.GetCellHavingValue(operation.CellLocator);

            var result = await Task.FromResult(cell.HasCellValue());

            return result;
        }

        private IGetCellValue<TValue> GetCellHavingTypedValue(
            CellLocator cellLocator)
        {
            var cell = this.GetCellHavingValue(cellLocator);

            if (!(cell is IGetCellValue<TValue> result))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IGetCellValue<TValue>).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            return result;
        }

        private IGetCellValue GetCellHavingValue(
            CellLocator cellLocator)
        {
            ICell cell;

            try
            {
                cell = this.report.GetCell(cellLocator.SectionId, cellLocator.CellId, cellLocator.SlotId);
            }
            catch (Exception ex)
            {
                throw new CellNotFoundException(ex.Message, cellLocator);
            }

            if (cell is ISlottedCell slottedCell)
            {
                if (!string.IsNullOrWhiteSpace(cellLocator.SlotId))
                {
                    throw new InvalidOperationException(Invariant($"Something went wrong.  The only way to address a slotted cell is if the slot id is not provided."));
                }

                if (cellLocator.SlotSelectionStrategy == SlotSelectionStrategy.DefaultSlot)
                {
                    cell = slottedCell.SlotIdToCellMap[slottedCell.DefaultSlotId];
                }
                else if (cellLocator.SlotSelectionStrategy == SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
                {
                    throw new CellNotFoundException(Invariant($"The operation addresses an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."), cellLocator);
                }
                else
                {
                    throw new NotSupportedException(Invariant($"This {nameof(SlotSelectionStrategy)} is not supported: {cellLocator.SlotSelectionStrategy}."));
                }
            }

            if (!(cell is IGetCellValue result))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IGetCellValue).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            return result;
        }

        private void ExecuteOperationCellIfNecessary(
            IOperationOutputCell<TValue> cell)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (cell.CellOpExecutionEvent == null)
            {
                CellOpExecutionEventBase cellOpExecutionEvent;

                try
                {
                    var operationResult = this.protocolFactory.GetProtocolAndExecuteViaReflection<TValue>(cell.Operation);

                    cellOpExecutionEvent = new SucceededInExecutingCellOpEvent<TValue>(this.timestampUtc, null, operationResult);
                }
                catch (CellValueMissingException ex)
                {
                    cellOpExecutionEvent = new CellValueWasMissingWhenExecutingCellOpEvent(this.timestampUtc, ex.CellLocator, ex.Message);
                }
                catch (CellNotFoundException ex)
                {
                    cellOpExecutionEvent = new CellNotFoundWhenExecutingCellOpEvent(this.timestampUtc, ex.CellLocator, ex.Message);
                }
                catch (OpExecutionAbortedExceptionBase ex)
                {
                    cellOpExecutionEvent = new ExecutionOfCellOpAbortedEvent(this.timestampUtc, ex.ToString());
                }
                catch (OpExecutionFailedExceptionBase ex)
                {
                    // Redundant; this does the same thing as catching Exception below.
                    // Just noting that the "proper" exception for a protocol to throw is an OpExecutionFailedExceptionBase.
                    // Protocol authors might not comply.
                    cellOpExecutionEvent = new FailedToExecuteCellOpEvent(this.timestampUtc, ex.ToString());
                }
                catch (Exception ex)
                {
                    cellOpExecutionEvent = new FailedToExecuteCellOpEvent(this.timestampUtc, ex.ToString());
                }

                cell.RecordExecution(cellOpExecutionEvent);
            }
            else if (cell.CellOpExecutionEvent.TimestampUtc != this.timestampUtc)
            {
                throw new InvalidOperationException("Something went wrong.  The operation was executed, but the recorded timestamp doesn't match this timestamp.");
            }
        }

        private async Task ExecuteOperationCellIfNecessaryAsync(
            IOperationOutputCell<TValue> cell)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (cell.CellOpExecutionEvent == null)
            {
                CellOpExecutionEventBase cellOpExecutionEvent;

                try
                {
                    var operationResult = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<TValue>(cell.Operation);

                    cellOpExecutionEvent = new SucceededInExecutingCellOpEvent<TValue>(this.timestampUtc, null, operationResult);
                }
                catch (CellValueMissingException ex)
                {
                    cellOpExecutionEvent = new CellValueWasMissingWhenExecutingCellOpEvent(this.timestampUtc, ex.CellLocator, ex.Message);
                }
                catch (CellNotFoundException ex)
                {
                    cellOpExecutionEvent = new CellNotFoundWhenExecutingCellOpEvent(this.timestampUtc, ex.CellLocator, ex.Message);
                }
                catch (OpExecutionAbortedExceptionBase ex)
                {
                    cellOpExecutionEvent = new ExecutionOfCellOpAbortedEvent(this.timestampUtc, ex.ToString());
                }
                catch (OpExecutionFailedExceptionBase ex)
                {
                    // Redundant; this does the same thing as catching Exception below.
                    // Just noting that the "proper" exception for a protocol to throw is an OpExecutionFailedExceptionBase.
                    // Protocol authors might not comply.
                    cellOpExecutionEvent = new FailedToExecuteCellOpEvent(this.timestampUtc, ex.ToString());
                }
                catch (Exception ex)
                {
                    cellOpExecutionEvent = new FailedToExecuteCellOpEvent(this.timestampUtc, ex.ToString());
                }

                cell.RecordExecution(cellOpExecutionEvent);
            }
            else if (cell.CellOpExecutionEvent.TimestampUtc != this.timestampUtc)
            {
                throw new InvalidOperationException("Something went wrong.  The operation was executed, but the recorded timestamp doesn't match this timestamp.");
            }
        }
    }
}
