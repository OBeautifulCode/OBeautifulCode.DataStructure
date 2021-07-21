// --------------------------------------------------------------------------------------------------------------------
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
    public class DataStructureCellProtocols<TValue>
        : ISyncAndAsyncReturningProtocol<GetCellValueByCellReferenceOp<TValue>, TValue>,
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
        /// <param name="timestampUtc">The timestamp (in UTC) to use when recording a <see cref="CellOpExecutedEvent{TResult}"/> with an <see cref="IOperationOutputCell{TValue}"/>.</param>
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
            GetCellValueByCellReferenceOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var result = this.GetCellValue(operation.Cell);

            return result;
        }

        /// <inheritdoc />
        public async Task<TValue> ExecuteAsync(
            GetCellValueByCellReferenceOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var result = await this.GetCellValueAsync(operation.Cell);

            return result;
        }

        /// <inheritdoc />
        public TValue Execute(
            GetCellValueOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cell = this.GetCellWithTypedValue(operation.SectionId, operation.CellId, operation.SlotId, operation.SlotSelectionStrategy);

            var result = this.GetCellValue(cell);

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

            var cell = this.GetCellWithTypedValue(operation.SectionId, operation.CellId, operation.SlotId, operation.SlotSelectionStrategy);

            var result = await this.GetCellValueAsync(cell);

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

            var cell = this.GetCellWithValue(operation.SectionId, operation.CellId, operation.SlotId, operation.SlotSelectionStrategy);

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

            var cell = this.GetCellWithValue(operation.SectionId, operation.CellId, operation.SlotId, operation.SlotSelectionStrategy);

            var result = await Task.FromResult(cell.HasCellValue());

            return result;
        }

        private IGetCellValue<TValue> GetCellWithTypedValue(
            string sectionId,
            string cellId,
            string slotId,
            SlotSelectionStrategy slotSelectionStrategy)
        {
            var cell = this.GetCellWithValue(sectionId, cellId, slotId, slotSelectionStrategy);

            if (!(cell is IGetCellValue<TValue> result))
            {
                throw new InvalidOperationException(Invariant($"The operation addresses a cell whose type is not an {typeof(IGetCellValue<TValue>).ToStringReadable()}: {cell.GetType().ToStringReadable()}."));
            }

            return result;
        }

        private IGetCellValue GetCellWithValue(
            string sectionId,
            string cellId,
            string slotId,
            SlotSelectionStrategy slotSelectionStrategy)
        {
            var cell = this.report.GetCell(sectionId, cellId, slotId);

            if (cell is ISlottedCell slottedCell)
            {
                if (!string.IsNullOrWhiteSpace(slotId))
                {
                    throw new InvalidOperationException(Invariant($"Something went wrong.  The only way to address a slotted cell is if the slot id is not provided."));
                }

                if (slotSelectionStrategy == SlotSelectionStrategy.DefaultSlot)
                {
                    cell = slottedCell.SlotIdToCellMap[slottedCell.DefaultSlotId];
                }
                else if (slotSelectionStrategy == SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
                {
                    throw new InvalidOperationException(Invariant($"The operation addresses an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."));
                }
                else
                {
                    throw new NotSupportedException(Invariant($"This {nameof(SlotSelectionStrategy)} is not supported: {slotSelectionStrategy}."));
                }
            }

            if (!(cell is IGetCellValue result))
            {
                throw new InvalidOperationException(Invariant($"The operation addresses a cell whose type is not an {typeof(IGetCellValue).ToStringReadable()}: {cell.GetType().ToStringReadable()}."));
            }

            return result;
        }

        private TValue GetCellValue(
            IGetCellValue<TValue> cell)
        {
            if (cell is IOperationOutputCell<TValue> operationCell)
            {
                this.ExecuteOperationCellIfNecessary(operationCell);
            }

            var result = cell.GetCellValue();

            return result;
        }

        private async Task<TValue> GetCellValueAsync(
            IGetCellValue<TValue> cell)
        {
            if (cell is IOperationOutputCell<TValue> operationCell)
            {
                await this.ExecuteOperationCellIfNecessaryAsync(operationCell);
            }

            var result = cell.GetCellValue();

            return result;
        }

        private void ExecuteOperationCellIfNecessary(
            IOperationOutputCell<TValue> cell)
        {
            // if it's a Skipped or CellOpFailedEvent, then record Skipped
            // else if
            if ((cell.CellOpExecutedEvent == null) || (cell.CellOpExecutedEvent.TimestampUtc != this.timestampUtc))
            {
                try
                {
                    var operationResult =
                        this.protocolFactory.GetProtocolAndExecuteViaReflection<TValue>(cell.Operation);

                    var cellOpExecutedEvent = new CellOpExecutedEvent<TValue>(this.timestampUtc, operationResult);

                    cell.RecordExecution(cellOpExecutedEvent);
                }
                ////catch (CellValueMissingException)
                ////{
                ////    // Skip
                ////}
                ////catch (CellNotFoundException)
                ////{
                ////    // Skip
                ////}
                ////catch (OpExecutionAbortedExceptionBase(IOperation))
                ////catch (OpExecutionAbortedException(IOperation) )
                ////{
                ////   IThrowOpExecutionAbortedException : IHaveDetails
                ////   ThrowOpExecutionAbortedExceptionReturningOp{TValue} : IReturningOp{TValue}  // has to play into else statement
                ////   ThrowOpExecutionAbortedExceptionVoidOp : IVoidOp
                ////}
                ////catch (OperationExecutionFailedExceptionBase)
                ////catch (OperationExecutionFailedException)
                ////{
                ////    // record CellOpFailed (include the op that failed)
                ////}
                catch (Exception)
                {
                    // record CellOpFailed
                }
            }
        }

        private async Task ExecuteOperationCellIfNecessaryAsync(
            IOperationOutputCell<TValue> cell)
        {
            if ((cell.CellOpExecutedEvent == null) || (cell.CellOpExecutedEvent.TimestampUtc != this.timestampUtc))
            {
                var operationResult = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<TValue>(cell.Operation);

                var cellOpExecutedEvent = new CellOpExecutedEvent<TValue>(this.timestampUtc, operationResult);

                cell.RecordExecution(cellOpExecutedEvent);
            }
        }
    }
}
