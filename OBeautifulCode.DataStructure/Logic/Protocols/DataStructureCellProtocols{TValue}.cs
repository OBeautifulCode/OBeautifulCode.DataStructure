// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureCellProtocols{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// The core <see cref="ICell"/>-related protocols.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    public class DataStructureCellProtocols<TValue> :
          ISyncAndAsyncReturningProtocol<GetConstOp<TValue>, TValue>,
          ISyncAndAsyncReturningProtocol<ThrowOpExecutionAbortedExceptionOp<TValue>, TValue>,
          ISyncAndAsyncReturningProtocol<ThrowOpExecutionDeemedNotApplicableExceptionOp<TValue>, TValue>,
          ISyncAndAsyncVoidProtocol<ExecuteOperationCellIfNecessaryOp<TValue>>,
          ISyncAndAsyncVoidProtocol<ValidateCellOp>,
          ISyncAndAsyncVoidProtocol<CheckAvailabilityOfCellOp>,
          ISyncAndAsyncReturningProtocol<HasCellValueOp, bool>,
          ISyncAndAsyncReturningProtocol<GetCellValueOp<TValue>, TValue>
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
            GetConstOp<TValue> operation)
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
            GetConstOp<TValue> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var result = await Task.FromResult(operation.Value);

            return result;
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
                DataStructureCellProtocols.CurrentCellStack.Push(cell);

                this.ExecuteOperationCellIfNecessary(cell);
            }
            finally
            {
                var poppedCell = DataStructureCellProtocols.CurrentCellStack.Pop();

                DataStructureCellProtocols.ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
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
                DataStructureCellProtocols.CurrentCellStack.Push(cell);

                await this.ExecuteOperationCellIfNecessaryAsync(cell);
            }
            finally
            {
                var poppedCell = DataStructureCellProtocols.CurrentCellStack.Pop();

                DataStructureCellProtocols.ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
            }
        }

        /// <inheritdoc />
        public void Execute(
            ValidateCellOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cell = operation.Cell;

            try
            {
                DataStructureCellProtocols.CurrentCellStack.Push(cell);

                this.ValidateCell(cell);
            }
            finally
            {
                var poppedCell = DataStructureCellProtocols.CurrentCellStack.Pop();

                DataStructureCellProtocols.ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
            }
        }

        /// <inheritdoc />
        public async Task ExecuteAsync(
            ValidateCellOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cell = operation.Cell;

            try
            {
                DataStructureCellProtocols.CurrentCellStack.Push(cell);

                await this.ValidateCellAsync(cell);
            }
            finally
            {
                var poppedCell = DataStructureCellProtocols.CurrentCellStack.Pop();

                DataStructureCellProtocols.ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
            }
        }

        /// <inheritdoc />
        public void Execute(
            CheckAvailabilityOfCellOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cell = operation.Cell;

            try
            {
                DataStructureCellProtocols.CurrentCellStack.Push(cell);

                this.CheckAvailabilityOfCell(cell);
            }
            finally
            {
                var poppedCell = DataStructureCellProtocols.CurrentCellStack.Pop();

                DataStructureCellProtocols.ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
            }
        }

        /// <inheritdoc />
        public async Task ExecuteAsync(
            CheckAvailabilityOfCellOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cell = operation.Cell;

            try
            {
                DataStructureCellProtocols.CurrentCellStack.Push(cell);

                await this.CheckAvailabilityOfCellAsync(cell);
            }
            finally
            {
                var poppedCell = DataStructureCellProtocols.CurrentCellStack.Pop();

                DataStructureCellProtocols.ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
            }
        }

        /// <inheritdoc />
        public bool Execute(
            HasCellValueOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cellLocator = this.protocolFactory.GetProtocolAndExecuteViaReflection<CellLocatorBase>(operation.CellLocator);

            var cell = this.GetCellHavingValue(cellLocator);

            // This is necessary because we can't simply use new ExecuteOperationCellIfNecessaryOp<TValue>()
            // TValue is the TValue of THIS protocol factory.
            // HasCellValueOp does not have TValue in it's generic arguments; ALL instances of
            // DataStructureCellProtocols can execute that operation and the chain-of-responsibility
            // protocol factory will simply use the first instance of DataStructureCellProtocols that is registered.
            // So TValue of this factory might be int whereas the cell's TValue is a decimal.
            var executeOperationCellIfNecessaryOp = this.GetExecuteOperationCellIfNecessaryOpOrNull((ICell)cell);

            if (executeOperationCellIfNecessaryOp != null)
            {
                this.protocolFactory.GetProtocolAndExecuteViaReflection(executeOperationCellIfNecessaryOp);
            }

            var result = cell.HasCellValue();

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> ExecuteAsync(
            HasCellValueOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var cellLocator = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<CellLocatorBase>(operation.CellLocator);

            var cell = this.GetCellHavingValue(cellLocator);

            // This is necessary because we can't simply use new ExecuteOperationCellIfNecessaryOp<TValue>()
            // TValue is the TValue of THIS protocol factory.
            // HasCellValueOp does not have TValue in it's generic arguments; ALL instances of
            // DataStructureCellProtocols can execute that operation and the chain-of-responsibility
            // protocol factory will simply use the first instance of DataStructureCellProtocols that is registered.
            // So TValue of this factory might be int whereas the cell's TValue is a decimal.
            var executeOperationCellIfNecessaryOp = this.GetExecuteOperationCellIfNecessaryOpOrNull((ICell)cell);

            if (executeOperationCellIfNecessaryOp != null)
            {
                await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync(executeOperationCellIfNecessaryOp);
            }

            var result = cell.HasCellValue();

            return result;
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

            // This will force the execution of an operation cell if it hasn't been executed yet.
            var hasCellValueOp = new HasCellValueOp(operation.CellLocator);

            var hasCellValue = this.protocolFactory.GetProtocolAndExecuteViaReflection<bool>(hasCellValueOp);

            var cellLocator = this.protocolFactory.GetProtocolAndExecuteViaReflection<CellLocatorBase>(operation.CellLocator);

            TValue result;

            if (hasCellValue)
            {
                var cell = this.GetCellHavingTypedValue(cellLocator);

                result = cell.GetCellValue();
            }
            else
            {
                if (operation.DefaultValue == null)
                {
                    throw new CellValueMissingException(cellLocator);
                }

                result = this.protocolFactory.GetProtocolAndExecuteViaReflection<TValue>(operation.DefaultValue);
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

            // This will force the execution of an operation cell if it hasn't been executed yet.
            var hasCellValueOp = new HasCellValueOp(operation.CellLocator);

            var hasCellValue = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<bool>(hasCellValueOp);

            var cellLocator = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<CellLocatorBase>(operation.CellLocator);

            TValue result;

            if (hasCellValue)
            {
                var cell = this.GetCellHavingTypedValue(cellLocator);

                result = cell.GetCellValue();
            }
            else
            {
                if (operation.DefaultValue == null)
                {
                    throw new CellValueMissingException(cellLocator);
                }

                result = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<TValue>(operation.DefaultValue);
            }

            return result;
        }

        private void ExecuteOperationCellIfNecessary(
            IOperationOutputCell<TValue> cell)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (cell.GetExecutionStatus() == CellOpExecutionStatus.NotExecuted)
            {
                CellOpExecutionEventBase operationExecutionEvent;

                try
                {
                    var operationResult = this.protocolFactory.GetProtocolAndExecuteViaReflection<TValue>(cell.Operation);

                    operationExecutionEvent = new CellOpExecutionCompletedEvent<TValue>(this.timestampUtc, null, operationResult);
                }
                catch (OpExecutionAbortedExceptionBase ex)
                {
                    operationExecutionEvent = new CellOpExecutionAbortedEvent(this.timestampUtc, ex.ToString());
                }
                catch (OpExecutionDeemedNotApplicableExceptionBase ex)
                {
                    operationExecutionEvent = new CellOpExecutionDeemedNotApplicableEvent(this.timestampUtc, ex.ToString());
                }
                catch (Exception ex)
                {
                    // The "proper" exception for a protocol to throw is an OpExecutionFailedExceptionBase.
                    // Protocol authors might not comply.
                    operationExecutionEvent = new CellOpExecutionFailedEvent(this.timestampUtc, ex.ToString());
                }

                cell.Record(operationExecutionEvent);
            }
            else if (cell.OperationExecutionEvents.Last().TimestampUtc != this.timestampUtc)
            {
                throw new InvalidOperationException("Something went wrong.  The operation was executed, but the recorded timestamp doesn't match this timestamp.");
            }
        }

        private async Task ExecuteOperationCellIfNecessaryAsync(
            IOperationOutputCell<TValue> cell)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (cell.GetExecutionStatus() == CellOpExecutionStatus.NotExecuted)
            {
                CellOpExecutionEventBase operationExecutionEvent;

                try
                {
                    var operationResult = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<TValue>(cell.Operation);

                    operationExecutionEvent = new CellOpExecutionCompletedEvent<TValue>(this.timestampUtc, null, operationResult);
                }
                catch (OpExecutionAbortedExceptionBase ex)
                {
                    operationExecutionEvent = new CellOpExecutionAbortedEvent(this.timestampUtc, ex.ToString());
                }
                catch (OpExecutionDeemedNotApplicableExceptionBase ex)
                {
                    operationExecutionEvent = new CellOpExecutionDeemedNotApplicableEvent(this.timestampUtc, ex.ToString());
                }
                catch (Exception ex)
                {
                    // The "proper" exception for a protocol to throw is an OpExecutionFailedExceptionBase.
                    // Protocol authors might not comply.
                    operationExecutionEvent = new CellOpExecutionFailedEvent(this.timestampUtc, ex.ToString());
                }

                cell.Record(operationExecutionEvent);
            }
            else if (cell.OperationExecutionEvents.Last().TimestampUtc != this.timestampUtc)
            {
                throw new InvalidOperationException("Something went wrong.  The operation was executed, but the recorded timestamp doesn't match this timestamp.");
            }
        }

        private void ValidateCell(
            IValidationCell cell)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            var validationStatus = cell.GetValidationStatus();

            if (validationStatus == ValidationStatus.ValidationMissing)
            {
                // no-op
            }
            else if (validationStatus == ValidationStatus.Unvalidated)
            {
                CellValidationEventBase validationEvent;

                try
                {
                    var validation = cell.Validation;

                    var validationResult = this.protocolFactory.GetProtocolAndExecuteViaReflection<ValidationResult>(validation.Operation);

                    string message = null;

                    if (validationResult.MessageOp != null)
                    {
                        message = this.protocolFactory.GetProtocolAndExecuteViaReflection<string>(validationResult.MessageOp);
                    }

                    var validity = validationResult.Validity;

                    if (validity == Validity.Invalid)
                    {
                        validationEvent = new CellValidationDeterminedCellInvalidEvent(this.timestampUtc, null, message);
                    }
                    else if (validity == Validity.Valid)
                    {
                        validationEvent = new CellValidationDeterminedCellValidEvent(this.timestampUtc, null, message);
                    }
                    else if (validity == Validity.NotApplicable)
                    {
                        validationEvent = new CellValidationDeemedNotApplicableEvent(this.timestampUtc, null, message);
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This {nameof(Validity)} is not supported: {validity}."));
                    }
                }
                catch (OpExecutionAbortedExceptionBase ex)
                {
                    validationEvent = new CellValidationAbortedEvent(this.timestampUtc, ex.ToString());
                }
                catch (OpExecutionDeemedNotApplicableExceptionBase ex)
                {
                    validationEvent = new CellValidationDeemedNotApplicableEvent(this.timestampUtc, ex.ToString(), null);
                }
                catch (Exception ex)
                {
                    // The "proper" exception for a protocol to throw is an OpExecutionFailedExceptionBase.
                    // Protocol authors might not comply.
                    validationEvent = new CellValidationFailedEvent(this.timestampUtc, ex.ToString());
                }

                cell.Record(validationEvent);
            }
            else if (cell.ValidationEvents.Last().TimestampUtc != this.timestampUtc)
            {
                throw new InvalidOperationException("Something went wrong.  The cell was validated, but the recorded timestamp doesn't match this timestamp.");
            }
        }

        private async Task ValidateCellAsync(
            IValidationCell cell)
        {
            var validationStatus = cell.GetValidationStatus();

            if (validationStatus == ValidationStatus.ValidationMissing)
            {
                // no-op
            }
            else if (validationStatus == ValidationStatus.Unvalidated)
            {
                CellValidationEventBase validationEvent;

                try
                {
                    var validation = cell.Validation;

                    var validationResult = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<ValidationResult>(validation.Operation);

                    string message = null;

                    if (validationResult.MessageOp != null)
                    {
                        message = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<string>(validationResult.MessageOp);
                    }

                    var validity = validationResult.Validity;

                    if (validity == Validity.Invalid)
                    {
                        validationEvent = new CellValidationDeterminedCellInvalidEvent(this.timestampUtc, null, message);
                    }
                    else if (validity == Validity.Valid)
                    {
                        validationEvent = new CellValidationDeterminedCellValidEvent(this.timestampUtc, null, message);
                    }
                    else if (validity == Validity.NotApplicable)
                    {
                        validationEvent = new CellValidationDeemedNotApplicableEvent(this.timestampUtc, null, message);
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This {nameof(Validity)} is not supported: {validity}."));
                    }
                }
                catch (OpExecutionAbortedExceptionBase ex)
                {
                    validationEvent = new CellValidationAbortedEvent(this.timestampUtc, ex.ToString());
                }
                catch (OpExecutionDeemedNotApplicableExceptionBase ex)
                {
                    validationEvent = new CellValidationDeemedNotApplicableEvent(this.timestampUtc, ex.ToString(), null);
                }
                catch (Exception ex)
                {
                    // The "proper" exception for a protocol to throw is an OpExecutionFailedExceptionBase.
                    // Protocol authors might not comply.
                    validationEvent = new CellValidationFailedEvent(this.timestampUtc, ex.ToString());
                }

                cell.Record(validationEvent);
            }
            else if (cell.ValidationEvents.Last().TimestampUtc != this.timestampUtc)
            {
                throw new InvalidOperationException("Something went wrong.  The cell was validated, but the recorded timestamp doesn't match this timestamp.");
            }
        }

        private void CheckAvailabilityOfCell(
            IAvailabilityCheckCell cell)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            var availabilityCheckStatus = cell.GetAvailabilityCheckStatus();

            if (availabilityCheckStatus == AvailabilityCheckStatus.AvailabilityCheckMissing)
            {
                // no-op
            }
            else if (availabilityCheckStatus == AvailabilityCheckStatus.Unchecked)
            {
                CellAvailabilityCheckEventBase availabilityCheckEvent;

                try
                {
                    var availabilityCheck = cell.AvailabilityCheck;

                    var availabilityCheckResult = this.protocolFactory.GetProtocolAndExecuteViaReflection<AvailabilityCheckResult>(availabilityCheck.Operation);

                    string message = null;

                    if (availabilityCheckResult.MessageOp != null)
                    {
                        message = this.protocolFactory.GetProtocolAndExecuteViaReflection<string>(availabilityCheckResult.MessageOp);
                    }

                    var availability = availabilityCheckResult.Availability;

                    if (availability == Availability.Disabled)
                    {
                        availabilityCheckEvent = new CellAvailabilityCheckDeterminedCellDisabledEvent(this.timestampUtc, null, message);
                    }
                    else if (availability == Availability.Enabled)
                    {
                        availabilityCheckEvent = new CellAvailabilityCheckDeterminedCellEnabledEvent(this.timestampUtc, null, message);
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This {nameof(Availability)} is not supported: {availability}."));
                    }
                }
                catch (Exception ex)
                {
                    // The "proper" exception for a protocol to throw is an OpExecutionFailedExceptionBase.
                    // Protocol authors might not comply.
                    availabilityCheckEvent = new CellAvailabilityCheckFailedEvent(this.timestampUtc, ex.ToString());
                }

                cell.Record(availabilityCheckEvent);
            }
            else if (cell.AvailabilityCheckEvents.Last().TimestampUtc != this.timestampUtc)
            {
                throw new InvalidOperationException("Something went wrong.  The cell was checked for availability, but the recorded timestamp doesn't match this timestamp.");
            }
        }

        private async Task CheckAvailabilityOfCellAsync(
            IAvailabilityCheckCell cell)
        {
            var availabilityCheckStatus = cell.GetAvailabilityCheckStatus();

            if (availabilityCheckStatus == AvailabilityCheckStatus.AvailabilityCheckMissing)
            {
                // no-op
            }
            else if (availabilityCheckStatus == AvailabilityCheckStatus.Unchecked)
            {
                CellAvailabilityCheckEventBase availabilityCheckEvent;

                try
                {
                    var availabilityCheck = cell.AvailabilityCheck;

                    var availabilityCheckResult = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<AvailabilityCheckResult>(availabilityCheck.Operation);

                    string message = null;

                    if (availabilityCheckResult.MessageOp != null)
                    {
                        message = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<string>(availabilityCheckResult.MessageOp);
                    }

                    var availability = availabilityCheckResult.Availability;

                    if (availability == Availability.Disabled)
                    {
                        availabilityCheckEvent = new CellAvailabilityCheckDeterminedCellDisabledEvent(this.timestampUtc, null, message);
                    }
                    else if (availability == Availability.Enabled)
                    {
                        availabilityCheckEvent = new CellAvailabilityCheckDeterminedCellEnabledEvent(this.timestampUtc, null, message);
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This {nameof(Availability)} is not supported: {availability}."));
                    }
                }
                catch (Exception ex)
                {
                    // The "proper" exception for a protocol to throw is an OpExecutionFailedExceptionBase.
                    // Protocol authors might not comply.
                    availabilityCheckEvent = new CellAvailabilityCheckFailedEvent(this.timestampUtc, ex.ToString());
                }

                cell.Record(availabilityCheckEvent);
            }
            else if (cell.AvailabilityCheckEvents.Last().TimestampUtc != this.timestampUtc)
            {
                throw new InvalidOperationException("Something went wrong.  The cell was checked for availability, but the recorded timestamp doesn't match this timestamp.");
            }
        }

        private IOperation GetExecuteOperationCellIfNecessaryOpOrNull(
            ICell cell)
        {
            IOperation result = null;

            if (cell.IsOperationCell())
            {
                var valueType = cell.GetValueTypeOrNull();

                if (!DataStructureCellProtocols.CachedTypeToExecuteOperationCellIfNecessaryOpConstructorInfoMap.TryGetValue(valueType, out var constructorInfo))
                {
                    constructorInfo = typeof(ExecuteOperationCellIfNecessaryOp<>).MakeGenericType(cell.GetValueTypeOrNull()).GetConstructors().Single();

                    DataStructureCellProtocols.CachedTypeToExecuteOperationCellIfNecessaryOpConstructorInfoMap.TryAdd(valueType, constructorInfo);
                }

                // ReSharper disable once CoVariantArrayConversion
                result = (IOperation)constructorInfo.Invoke(new[] { cell });
            }

            return result;
        }

        private IGetCellValue<TValue> GetCellHavingTypedValue(
            CellLocatorBase cellLocator)
        {
            var cell = this.GetCellHavingValue(cellLocator);

            if (!(cell is IGetCellValue<TValue> result))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IGetCellValue<TValue>).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            return result;
        }

        private IGetCellValue GetCellHavingValue(
            CellLocatorBase cellLocator)
        {
            ICell cell;

            if (cellLocator is ReportCellLocator reportCellLocator)
            {
                cell = this.GetCell(reportCellLocator);
            }
            else if (cellLocator is SectionCellLocator sectionCellLocator)
            {
                cell = this.GetCell(sectionCellLocator);
            }
            else if (cellLocator is ThisCellLocator)
            {
                cell = DataStructureCellProtocols.CurrentCellStack.Peek();
            }
            else
            {
                throw new NotSupportedException(Invariant($"This type of {nameof(CellLocatorBase)} is not supported: {cellLocator.GetType().ToStringReadable()}."));
            }

            if (!(cell is IGetCellValue result))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IGetCellValue).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            return result;
        }

        private ICell GetCell(
            SectionCellLocator sectionCellLocator)
        {
            var currentCell = DataStructureCellProtocols.CurrentCellStack.Peek();

            var cellToSectionMap = this.report.GetCellToSectionMap();

            if (!cellToSectionMap.TryGetValue(currentCell, out var section))
            {
                throw new InvalidOperationException(Invariant($"Something went wrong.  Expected to find the current cell in {nameof(Report)}.{nameof(Report.GetCellToSectionMap)}."));
            }

            var reportCellLocator = new ReportCellLocator(section.Id, sectionCellLocator.CellId, sectionCellLocator.SlotId, sectionCellLocator.SlotSelectionStrategy);

            var result = this.GetCell(reportCellLocator);

            return result;
        }

        private ICell GetCell(
            ReportCellLocator reportCellLocator)
        {
            ICell result;

            try
            {
                result = this.report.GetCell(reportCellLocator.SectionId, reportCellLocator.CellId, reportCellLocator.SlotId);
            }
            catch (Exception ex)
            {
                throw new CellNotFoundException(ex.Message, reportCellLocator);
            }

            if (result is ISlottedCell slottedCell)
            {
                if (!string.IsNullOrWhiteSpace(reportCellLocator.SlotId))
                {
                    throw new InvalidOperationException(Invariant($"Something went wrong.  The only way to address a slotted cell is if the slot id is not provided."));
                }

                if (reportCellLocator.SlotSelectionStrategy == SlotSelectionStrategy.DefaultSlot)
                {
                    result = slottedCell.SlotIdToCellMap[slottedCell.DefaultSlotId];
                }
                else if (reportCellLocator.SlotSelectionStrategy == SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
                {
                    throw new CellNotFoundException(Invariant($"The operation addresses an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."), reportCellLocator);
                }
                else
                {
                    throw new NotSupportedException(Invariant($"This {nameof(SlotSelectionStrategy)} is not supported: {reportCellLocator.SlotSelectionStrategy}."));
                }
            }

            return result;
        }
    }
}
