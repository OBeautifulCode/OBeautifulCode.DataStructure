// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureCellProtocols{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// The core <see cref="ICell"/>-related protocols.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = ObcSuppressBecause.CA1506_AvoidExcessiveClassCoupling_DisagreeWithAssessment)]
    public class DataStructureCellProtocols<TValue> :
          ISyncAndAsyncReturningProtocol<GetConstOp<TValue>, TValue>,
          ISyncAndAsyncReturningProtocol<ThrowOpExecutionAbortedExceptionOp<TValue>, TValue>,
          ISyncAndAsyncReturningProtocol<ThrowOpExecutionDeemedNotApplicableExceptionOp<TValue>, TValue>,
          ISyncAndAsyncVoidProtocol<ExecuteOperationCellIfNecessaryOp<TValue>>,
          ISyncAndAsyncVoidProtocol<ValidateCellIfNecessaryOp>,
          ISyncAndAsyncVoidProtocol<CheckAvailabilityOfCellIfNecessaryOp>,
          ISyncAndAsyncReturningProtocol<HasCellValueOp, bool>,
          ISyncAndAsyncReturningProtocol<GetCellValueOp<TValue>, TValue>,
          ISyncAndAsyncReturningProtocol<GetCellOpExecutionOutcomeOp, CellOpExecutionOutcome>,
          ISyncAndAsyncReturningProtocol<GetValidityOp, Validity>,
          ISyncAndAsyncReturningProtocol<GetAvailabilityOp, Availability>
    {
        private readonly Report report;

        private readonly IProtocolFactory protocolFactory;

        private readonly DateTime timestampUtc;

        private readonly Func<RecalcPhase> getRecalcPhaseFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStructureCellProtocols{TValue}"/> class.
        /// </summary>
        /// <param name="report">The report in-context.</param>
        /// <param name="protocolFactory">The protocol factory to use when executing an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.</param>
        /// <param name="timestampUtc">The timestamp (in UTC) to use when recording a <see cref="CellOpExecutionEventBase"/> with an <see cref="IOperationOutputCell{TValue}"/>.</param>
        /// <param name="getRecalcPhaseFunc">Func that gets the <see cref="RecalcPhase"/>.</param>
        public DataStructureCellProtocols(
            Report report,
            IProtocolFactory protocolFactory,
            DateTime timestampUtc,
            Func<RecalcPhase> getRecalcPhaseFunc)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            // ReSharper disable once JoinNullCheckWithUsage
            if (protocolFactory == null)
            {
                throw new ArgumentNullException(nameof(protocolFactory));
            }

            if (timestampUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(Invariant($"{nameof(timestampUtc)} is not in UTC time."));
            }

            if (getRecalcPhaseFunc == null)
            {
                throw new ArgumentNullException(nameof(getRecalcPhaseFunc));
            }

            this.report = report;
            this.protocolFactory = protocolFactory;
            this.timestampUtc = timestampUtc;
            this.getRecalcPhaseFunc = getRecalcPhaseFunc;
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
            ValidateCellIfNecessaryOp operation)
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

                this.ValidateCellIfNecessary(cell);
            }
            finally
            {
                var poppedCell = DataStructureCellProtocols.CurrentCellStack.Pop();

                DataStructureCellProtocols.ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
            }
        }

        /// <inheritdoc />
        public async Task ExecuteAsync(
            ValidateCellIfNecessaryOp operation)
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

                await this.ValidateCellIfNecessaryAsync(cell);
            }
            finally
            {
                var poppedCell = DataStructureCellProtocols.CurrentCellStack.Pop();

                DataStructureCellProtocols.ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
            }
        }

        /// <inheritdoc />
        public void Execute(
            CheckAvailabilityOfCellIfNecessaryOp operation)
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

                this.CheckAvailabilityOfCellIfNecessary(cell);
            }
            finally
            {
                var poppedCell = DataStructureCellProtocols.CurrentCellStack.Pop();

                DataStructureCellProtocols.ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
            }
        }

        /// <inheritdoc />
        public async Task ExecuteAsync(
            CheckAvailabilityOfCellIfNecessaryOp operation)
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

                await this.CheckAvailabilityOfCellIfNecessaryAsync(cell);
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

            var locatedCell = this.GetCellAndExecuteOperationIfNecessary(operation.CellLocator);

            var result = locatedCell.Cell.HasCellValue();

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

            var locatedCell = await this.GetCellAndExecuteOperationIfNecessaryAsync(operation.CellLocator);

            var result = locatedCell.Cell.HasCellValue();

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

            var locatedCell = this.GetCellAndExecuteOperationIfNecessary(operation.CellLocator);

            TValue result;

            if (locatedCell.Cell.HasCellValue())
            {
                if (!(locatedCell.Cell is IGetCellValue<TValue> getCellValueCell))
                {
                    throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IGetCellValue<TValue>).ToStringReadable()}: {locatedCell.Cell.GetType().ToStringReadable()}."), locatedCell.CellLocator);
                }

                result = getCellValueCell.GetCellValue();
            }
            else
            {
                if (operation.DefaultValue == null)
                {
                    throw new CellValueMissingException(locatedCell.CellLocator);
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

            var locatedCell = await this.GetCellAndExecuteOperationIfNecessaryAsync(operation.CellLocator);

            TValue result;

            if (locatedCell.Cell.HasCellValue())
            {
                if (!(locatedCell.Cell is IGetCellValue<TValue> getCellValueCell))
                {
                    throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IGetCellValue<TValue>).ToStringReadable()}: {locatedCell.Cell.GetType().ToStringReadable()}."), locatedCell.CellLocator);
                }

                result = getCellValueCell.GetCellValue();
            }
            else
            {
                if (operation.DefaultValue == null)
                {
                    throw new CellValueMissingException(locatedCell.CellLocator);
                }

                result = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<TValue>(operation.DefaultValue);
            }

            return result;
        }

        /// <inheritdoc />
        public CellOpExecutionOutcome Execute(
            GetCellOpExecutionOutcomeOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var locatedCell = this.GetCellAndExecuteOperationIfNecessary(operation.CellLocator);

            if (!(locatedCell.Cell is IRecordCellOpExecutionEvents operationCell))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IOperationOutputCell<TValue>).ToStringReadable()}: {locatedCell.Cell.GetType().ToStringReadable()}."), locatedCell.CellLocator);
            }

            var result = operationCell.GetCellOpExecutionOutcome();

            return result;
        }

        /// <inheritdoc />
        public async Task<CellOpExecutionOutcome> ExecuteAsync(
            GetCellOpExecutionOutcomeOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var locatedCell = await this.GetCellAndExecuteOperationIfNecessaryAsync(operation.CellLocator);

            if (!(locatedCell.Cell is IRecordCellOpExecutionEvents operationCell))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IOperationOutputCell<TValue>).ToStringReadable()}: {locatedCell.Cell.GetType().ToStringReadable()}."), locatedCell.CellLocator);
            }

            var result = operationCell.GetCellOpExecutionOutcome();

            return result;
        }

        /// <inheritdoc />
        public Validity Execute(
            GetValidityOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var recalcPhase = this.getRecalcPhaseFunc();

            if ((recalcPhase != RecalcPhase.Validation) && (recalcPhase != RecalcPhase.AvailabilityCheck))
            {
                throw new InvalidOperationException(Invariant($"Cannot execute {nameof(GetValidityOp)} during the {recalcPhase} phase."));
            }

            var cell = this.GetCellAndValidateIfNecessary(operation.CellLocator);

            var result = cell.GetValidity();

            return result;
        }

        /// <inheritdoc />
        public async Task<Validity> ExecuteAsync(
            GetValidityOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var recalcPhase = this.getRecalcPhaseFunc();

            if ((recalcPhase != RecalcPhase.Validation) && (recalcPhase != RecalcPhase.AvailabilityCheck))
            {
                throw new InvalidOperationException(Invariant($"Cannot execute {nameof(GetValidityOp)} during the {recalcPhase} phase."));
            }

            var cell = await this.GetCellAndValidateIfNecessaryAsync(operation.CellLocator);

            var result = cell.GetValidity();

            return result;
        }

        /// <inheritdoc />
        public Availability Execute(
            GetAvailabilityOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var recalcPhase = this.getRecalcPhaseFunc();

            if (recalcPhase != RecalcPhase.AvailabilityCheck)
            {
                throw new InvalidOperationException(Invariant($"Cannot execute {nameof(GetAvailabilityOp)} during the {recalcPhase} phase."));
            }

            var cell = this.GetCellAndCheckAvailabilityIfNecessary(operation.CellLocator);

            var result = cell.GetAvailability();

            return result;
        }

        /// <inheritdoc />
        public async Task<Availability> ExecuteAsync(
            GetAvailabilityOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var recalcPhase = this.getRecalcPhaseFunc();

            if (recalcPhase != RecalcPhase.AvailabilityCheck)
            {
                throw new InvalidOperationException(Invariant($"Cannot execute {nameof(GetAvailabilityOp)} during the {recalcPhase} phase."));
            }

            var cell = await this.GetCellAndCheckAvailabilityIfNecessaryAsync(operation.CellLocator);

            var result = cell.GetAvailability();

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
            if (cell.GetCellOpExecutionStatus() == CellOpExecutionStatus.NotExecuted)
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

        private void ValidateCellIfNecessary(
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

                    var validity = this.protocolFactory.GetProtocolAndExecuteViaReflection<Validity>(validationResult.ValidityOp);

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

        private async Task ValidateCellIfNecessaryAsync(
            IValidationCell cell)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
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

                    var validity = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<Validity>(validationResult.ValidityOp);

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

        private void CheckAvailabilityOfCellIfNecessary(
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

                    var availability = this.protocolFactory.GetProtocolAndExecuteViaReflection<Availability>(availabilityCheckResult.AvailabilityOp);

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

        private async Task CheckAvailabilityOfCellIfNecessaryAsync(
            IAvailabilityCheckCell cell)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
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

                    var availability = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<Availability>(availabilityCheckResult.AvailabilityOp);

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

        private LocatedCell GetCellAndExecuteOperationIfNecessary(
            IReturningOperation<CellLocatorBase> cellLocatorOp)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            var cellLocator = this.protocolFactory.GetProtocolAndExecuteViaReflection<CellLocatorBase>(cellLocatorOp);

            var cell = this.GetCell(cellLocator);

            if (!(cell is IGetCellValue cellWithValue))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IGetCellValue).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            // This is necessary because we can't simply use new ExecuteOperationCellIfNecessaryOp<TValue>()
            // TValue is the TValue of THIS protocol factory.
            // HasCellValueOp does not have TValue in it's generic arguments; ALL instances of
            // DataStructureCellProtocols can execute that operation and the chain-of-responsibility
            // protocol factory will simply use the first instance of DataStructureCellProtocols that is registered.
            // So TValue of this factory might be int whereas the cell's TValue is a decimal.
            var executeOperationCellIfNecessaryOp = DataStructureCellProtocols.GetExecuteOperationCellIfNecessaryOpOrNull(cell);

            if (executeOperationCellIfNecessaryOp != null)
            {
                this.protocolFactory.GetProtocolAndExecuteViaReflection(executeOperationCellIfNecessaryOp);
            }

            var result = new LocatedCell
            {
                Cell = cellWithValue,
                CellLocator = cellLocator,
            };

            return result;
        }

        private async Task<LocatedCell> GetCellAndExecuteOperationIfNecessaryAsync(
            IReturningOperation<CellLocatorBase> cellLocatorOp)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            var cellLocator = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<CellLocatorBase>(cellLocatorOp);

            var cell = this.GetCell(cellLocator);

            if (!(cell is IGetCellValue cellWithValue))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IGetCellValue).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            // This is necessary because we can't simply use new ExecuteOperationCellIfNecessaryOp<TValue>()
            // TValue is the TValue of THIS protocol factory.
            // HasCellValueOp does not have TValue in it's generic arguments; ALL instances of
            // DataStructureCellProtocols can execute that operation and the chain-of-responsibility
            // protocol factory will simply use the first instance of DataStructureCellProtocols that is registered.
            // So TValue of this factory might be int whereas the cell's TValue is a decimal.
            var executeOperationCellIfNecessaryOp = DataStructureCellProtocols.GetExecuteOperationCellIfNecessaryOpOrNull(cell);

            if (executeOperationCellIfNecessaryOp != null)
            {
                await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync(executeOperationCellIfNecessaryOp);
            }

            var result = new LocatedCell
            {
                Cell = cellWithValue,
                CellLocator = cellLocator,
            };

            return result;
        }

        private IValidationCell GetCellAndValidateIfNecessary(
            IReturningOperation<CellLocatorBase> cellLocatorOp)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            var cellLocator = this.protocolFactory.GetProtocolAndExecuteViaReflection<CellLocatorBase>(cellLocatorOp);

            var cell = this.GetCell(cellLocator);

            if (!(cell is IValidationCell result))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IValidationCell).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            var validateCellIfNecessaryOp = new ValidateCellIfNecessaryOp(result);

            this.protocolFactory.GetProtocolAndExecuteViaReflection(validateCellIfNecessaryOp);

            return result;
        }

        private async Task<IValidationCell> GetCellAndValidateIfNecessaryAsync(
            IReturningOperation<CellLocatorBase> cellLocatorOp)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            var cellLocator = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<CellLocatorBase>(cellLocatorOp);

            var cell = this.GetCell(cellLocator);

            if (!(cell is IValidationCell result))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IValidationCell).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            var validateCellIfNecessaryOp = new ValidateCellIfNecessaryOp(result);

            await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync(validateCellIfNecessaryOp);

            return result;
        }

        private IAvailabilityCheckCell GetCellAndCheckAvailabilityIfNecessary(
            IReturningOperation<CellLocatorBase> cellLocatorOp)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            var cellLocator = this.protocolFactory.GetProtocolAndExecuteViaReflection<CellLocatorBase>(cellLocatorOp);

            var cell = this.GetCell(cellLocator);

            if (!(cell is IAvailabilityCheckCell result))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IAvailabilityCheckCell).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            var checkAvailabilityOfCellIfNecessaryOp = new CheckAvailabilityOfCellIfNecessaryOp(result);

            this.protocolFactory.GetProtocolAndExecuteViaReflection(checkAvailabilityOfCellIfNecessaryOp);

            return result;
        }

        private async Task<IAvailabilityCheckCell> GetCellAndCheckAvailabilityIfNecessaryAsync(
            IReturningOperation<CellLocatorBase> cellLocatorOp)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            var cellLocator = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<CellLocatorBase>(cellLocatorOp);

            var cell = this.GetCell(cellLocator);

            if (!(cell is IAvailabilityCheckCell result))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell whose type is not an {typeof(IAvailabilityCheckCell).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            var checkAvailabilityOfCellIfNecessaryOp = new CheckAvailabilityOfCellIfNecessaryOp(result);

            await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync(checkAvailabilityOfCellIfNecessaryOp);

            return result;
        }

        private ICell GetCell(
            CellLocatorBase cellLocator)
        {
            ICell result;

            if (cellLocator is ReportCellLocator reportCellLocator)
            {
                result = this.GetCell(reportCellLocator);
            }
            else if (cellLocator is SectionCellLocator sectionCellLocator)
            {
                result = this.GetCell(sectionCellLocator);
            }
            else if (cellLocator is ThisCellLocator)
            {
                result = DataStructureCellProtocols.CurrentCellStack.Peek();
            }
            else
            {
                throw new NotSupportedException(Invariant($"This type of {nameof(CellLocatorBase)} is not supported: {cellLocator.GetType().ToStringReadable()}."));
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

        private class LocatedCell
        {
            public IGetCellValue Cell { get; set; }

            public CellLocatorBase CellLocator { get; set; }
        }
    }
}
