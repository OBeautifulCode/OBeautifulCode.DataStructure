// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureCellProtocols.cs" company="OBeautifulCode">
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
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// The core <see cref="ICell"/>-related protocols.
    /// </summary>
    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = ObcSuppressBecause.CA1506_AvoidExcessiveClassCoupling_DisagreeWithAssessment)]
    public class DataStructureCellProtocols :
        DataStructureCellProtocolsBase,
        ISyncAndAsyncVoidProtocol<ValidateCellIfNecessaryOp>,
        ISyncAndAsyncVoidProtocol<CheckAvailabilityOfCellIfNecessaryOp>,
        ISyncAndAsyncReturningProtocol<HasCellValueOp, bool>,
        ISyncAndAsyncReturningProtocol<GetCellOpExecutionOutcomeOp, CellOpExecutionOutcome>,
        ISyncAndAsyncReturningProtocol<GetValidityOp, Validity>,
        ISyncAndAsyncReturningProtocol<GetAvailabilityOp, Availability>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataStructureCellProtocols"/> class.
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
                this.CurrentCellStack.Push(cell);

                this.ValidateCellIfNecessary(cell);
            }
            finally
            {
                var poppedCell = this.CurrentCellStack.Pop();

                ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
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
                this.CurrentCellStack.Push(cell);

                await this.ValidateCellIfNecessaryAsync(cell);
            }
            finally
            {
                var poppedCell = this.CurrentCellStack.Pop();

                ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
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
                this.CurrentCellStack.Push(cell);

                this.CheckAvailabilityOfCellIfNecessary(cell);
            }
            finally
            {
                var poppedCell = this.CurrentCellStack.Pop();

                ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
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
                this.CurrentCellStack.Push(cell);

                await this.CheckAvailabilityOfCellIfNecessaryAsync(cell);
            }
            finally
            {
                var poppedCell = this.CurrentCellStack.Pop();

                ThrowIfUnexpectedCellPoppedOffCurrentCellStack(cell, poppedCell);
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

            var locatedCell = this.GetCellHavingValueAndExecuteOperationIfNecessary(operation.CellLocator);

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
        public CellOpExecutionOutcome Execute(
            GetCellOpExecutionOutcomeOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var locatedCell = this.GetCellHavingValueAndExecuteOperationIfNecessary(operation.CellLocator);

            if (!(locatedCell.Cell is IOperationOutputCell operationCell))
            {
                throw new CellNotFoundException(Invariant($"Addressing a cell whose type is not an {typeof(IOperationOutputCell).ToStringReadable()}: {locatedCell.Cell.GetType().ToStringReadable()}."), locatedCell.CellLocator);
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

            if (!(locatedCell.Cell is IOperationOutputCell operationCell))
            {
                throw new CellNotFoundException(Invariant($"Addressing a cell whose type is not an {typeof(IOperationOutputCell).ToStringReadable()}: {locatedCell.Cell.GetType().ToStringReadable()}."), locatedCell.CellLocator);
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

            var recalcPhase = this.GetRecalcPhaseFunc();

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

            var recalcPhase = this.GetRecalcPhaseFunc();

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

            var recalcPhase = this.GetRecalcPhaseFunc();

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

            var recalcPhase = this.GetRecalcPhaseFunc();

            if (recalcPhase != RecalcPhase.AvailabilityCheck)
            {
                throw new InvalidOperationException(Invariant($"Cannot execute {nameof(GetAvailabilityOp)} during the {recalcPhase} phase."));
            }

            var cell = await this.GetCellAndCheckAvailabilityIfNecessaryAsync(operation.CellLocator);

            var result = cell.GetAvailability();

            return result;
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

                    var validationResult = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<ValidationResult>(validation.Operation);

                    string message = null;

                    if (validationResult.MessageOp != null)
                    {
                        message = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<string>(validationResult.MessageOp);
                    }

                    var validity = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<Validity>(validationResult.ValidityOp);

                    if (validity == Validity.Invalid)
                    {
                        validationEvent = new CellValidationDeterminedCellInvalidEvent(message, this.TimestampUtc);
                    }
                    else if (validity == Validity.Valid)
                    {
                        validationEvent = new CellValidationDeterminedCellValidEvent(message, this.TimestampUtc);
                    }
                    else if (validity == Validity.NotApplicable)
                    {
                        validationEvent = new CellValidationDeemedNotApplicableEvent(message, this.TimestampUtc);
                    }
                    else if (validity == Validity.Aborted)
                    {
                        validationEvent = new CellValidationAbortedEvent(message, this.TimestampUtc);
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This {nameof(Validity)} is not supported: {validity}."));
                    }
                }
                catch (OpExecutionAbortedExceptionBase ex)
                {
                    // Here are are purposefully setting message to null because we have no idea who the thrower is
                    // nor whether the report author wants to emit this message to the user.
                    validationEvent = new CellValidationAbortedEvent(null, this.TimestampUtc, ex.ToString());
                }
                catch (OpExecutionDeemedNotApplicableExceptionBase ex)
                {
                    // Here are are purposefully setting message to null because we have no idea who the thrower is
                    // nor whether the report author wants to emit this message to the user.
                    validationEvent = new CellValidationDeemedNotApplicableEvent(null, this.TimestampUtc, ex.ToString());
                }
                catch (Exception ex)
                {
                    // The "proper" exception for a protocol to throw is an OpExecutionFailedExceptionBase.
                    // Protocol authors might not comply.
                    validationEvent = new CellValidationFailedEvent(this.TimestampUtc, ex.ToString());
                }

                cell.Record(validationEvent);
            }
            else if (cell.ValidationEvents.Last().TimestampUtc != this.TimestampUtc)
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

                    var validationResult = await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync<ValidationResult>(validation.Operation);

                    string message = null;

                    if (validationResult.MessageOp != null)
                    {
                        message = await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync<string>(validationResult.MessageOp);
                    }

                    var validity = await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync<Validity>(validationResult.ValidityOp);

                    if (validity == Validity.Invalid)
                    {
                        validationEvent = new CellValidationDeterminedCellInvalidEvent(message, this.TimestampUtc);
                    }
                    else if (validity == Validity.Valid)
                    {
                        validationEvent = new CellValidationDeterminedCellValidEvent(message, this.TimestampUtc);
                    }
                    else if (validity == Validity.NotApplicable)
                    {
                        validationEvent = new CellValidationDeemedNotApplicableEvent(message, this.TimestampUtc);
                    }
                    else if (validity == Validity.Aborted)
                    {
                        validationEvent = new CellValidationAbortedEvent(message, this.TimestampUtc);
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This {nameof(Validity)} is not supported: {validity}."));
                    }
                }
                catch (OpExecutionAbortedExceptionBase ex)
                {
                    // Here are are purposefully setting message to null because we have no idea who the thrower is
                    // nor whether the report author wants to emit this message to the user.
                    validationEvent = new CellValidationAbortedEvent(null, this.TimestampUtc, ex.ToString());
                }
                catch (OpExecutionDeemedNotApplicableExceptionBase ex)
                {
                    // Here are are purposefully setting message to null because we have no idea who the thrower is
                    // nor whether the report author wants to emit this message to the user.
                    validationEvent = new CellValidationDeemedNotApplicableEvent(null, this.TimestampUtc, ex.ToString());
                }
                catch (Exception ex)
                {
                    // The "proper" exception for a protocol to throw is an OpExecutionFailedExceptionBase.
                    // Protocol authors might not comply.
                    validationEvent = new CellValidationFailedEvent(this.TimestampUtc, ex.ToString());
                }

                cell.Record(validationEvent);
            }
            else if (cell.ValidationEvents.Last().TimestampUtc != this.TimestampUtc)
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

                    var availabilityCheckResult = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<AvailabilityCheckResult>(availabilityCheck.Operation);

                    string message = null;

                    if (availabilityCheckResult.MessageOp != null)
                    {
                        message = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<string>(availabilityCheckResult.MessageOp);
                    }

                    var availability = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<Availability>(availabilityCheckResult.AvailabilityOp);

                    if (availability == Availability.Disabled)
                    {
                        availabilityCheckEvent = new CellAvailabilityCheckDeterminedCellDisabledEvent(message, this.TimestampUtc);
                    }
                    else if (availability == Availability.Enabled)
                    {
                        availabilityCheckEvent = new CellAvailabilityCheckDeterminedCellEnabledEvent(message, this.TimestampUtc);
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
                    availabilityCheckEvent = new CellAvailabilityCheckFailedEvent(this.TimestampUtc, ex.ToString());
                }

                cell.Record(availabilityCheckEvent);
            }
            else if (cell.AvailabilityCheckEvents.Last().TimestampUtc != this.TimestampUtc)
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

                    var availabilityCheckResult = await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync<AvailabilityCheckResult>(availabilityCheck.Operation);

                    string message = null;

                    if (availabilityCheckResult.MessageOp != null)
                    {
                        message = await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync<string>(availabilityCheckResult.MessageOp);
                    }

                    var availability = await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync<Availability>(availabilityCheckResult.AvailabilityOp);

                    if (availability == Availability.Disabled)
                    {
                        availabilityCheckEvent = new CellAvailabilityCheckDeterminedCellDisabledEvent(message, this.TimestampUtc);
                    }
                    else if (availability == Availability.Enabled)
                    {
                        availabilityCheckEvent = new CellAvailabilityCheckDeterminedCellEnabledEvent(message, this.TimestampUtc);
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
                    availabilityCheckEvent = new CellAvailabilityCheckFailedEvent(this.TimestampUtc, ex.ToString());
                }

                cell.Record(availabilityCheckEvent);
            }
            else if (cell.AvailabilityCheckEvents.Last().TimestampUtc != this.TimestampUtc)
            {
                throw new InvalidOperationException("Something went wrong.  The cell was checked for availability, but the recorded timestamp doesn't match this timestamp.");
            }
        }

        private IValidationCell GetCellAndValidateIfNecessary(
            IReturningOperation<ICellLocator> cellLocatorOp)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            var cellLocator = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<ICellLocator>(cellLocatorOp);

            var cell = this.GetCell(cellLocator);

            if (!(cell is IValidationCell result))
            {
                throw new CellNotFoundException(Invariant($"Addressing a cell whose type is not an {typeof(IValidationCell).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            var validateCellIfNecessaryOp = new ValidateCellIfNecessaryOp(result);

            this.ProtocolFactory.GetProtocolAndExecuteViaReflection(validateCellIfNecessaryOp);

            return result;
        }

        private async Task<IValidationCell> GetCellAndValidateIfNecessaryAsync(
            IReturningOperation<ICellLocator> cellLocatorOp)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            var cellLocator = await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync<ICellLocator>(cellLocatorOp);

            var cell = this.GetCell(cellLocator);

            if (!(cell is IValidationCell result))
            {
                throw new CellNotFoundException(Invariant($"Addressing a cell whose type is not an {typeof(IValidationCell).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            var validateCellIfNecessaryOp = new ValidateCellIfNecessaryOp(result);

            await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync(validateCellIfNecessaryOp);

            return result;
        }

        private IAvailabilityCheckCell GetCellAndCheckAvailabilityIfNecessary(
            IReturningOperation<ICellLocator> cellLocatorOp)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            var cellLocator = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<ICellLocator>(cellLocatorOp);

            var cell = this.GetCell(cellLocator);

            if (!(cell is IAvailabilityCheckCell result))
            {
                throw new CellNotFoundException(Invariant($"Addressing a cell whose type is not an {typeof(IAvailabilityCheckCell).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            var checkAvailabilityOfCellIfNecessaryOp = new CheckAvailabilityOfCellIfNecessaryOp(result);

            this.ProtocolFactory.GetProtocolAndExecuteViaReflection(checkAvailabilityOfCellIfNecessaryOp);

            return result;
        }

        private async Task<IAvailabilityCheckCell> GetCellAndCheckAvailabilityIfNecessaryAsync(
            IReturningOperation<ICellLocator> cellLocatorOp)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            var cellLocator = await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync<ICellLocator>(cellLocatorOp);

            var cell = this.GetCell(cellLocator);

            if (!(cell is IAvailabilityCheckCell result))
            {
                throw new CellNotFoundException(Invariant($"Addressing a cell whose type is not an {typeof(IAvailabilityCheckCell).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            var checkAvailabilityOfCellIfNecessaryOp = new CheckAvailabilityOfCellIfNecessaryOp(result);

            await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync(checkAvailabilityOfCellIfNecessaryOp);

            return result;
        }
    }
}
