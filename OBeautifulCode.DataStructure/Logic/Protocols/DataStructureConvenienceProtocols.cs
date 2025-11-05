// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureConvenienceProtocols.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Threading.Tasks;
    using OBeautifulCode.CoreOperation;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Some protocols for convenience.
    /// </summary>
    public class DataStructureConvenienceProtocols :
          ISyncAndAsyncReturningProtocol<ValidateOp, ValidationResult>,
          ISyncAndAsyncReturningProtocol<CheckAvailabilityOp, AvailabilityCheckResult>
    {
        private readonly IProtocolFactory protocolFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStructureConvenienceProtocols"/> class.
        /// </summary>
        /// <param name="protocolFactory">The protocol factory to use when executing an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.</param>
        public DataStructureConvenienceProtocols(
            IProtocolFactory protocolFactory)
        {
            if (protocolFactory == null)
            {
                throw new ArgumentNullException(nameof(protocolFactory));
            }

            this.protocolFactory = protocolFactory;
        }

        /// <inheritdoc />
        public ValidationResult Execute(
            ValidateOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            ValidationResult result = null;

            var validationChain = operation.ValidationChain;

            foreach (var validationStep in validationChain.Steps)
            {
                Validity validity;
                IReturningOperation<string> stopMessageOp;

                if (validationStep is BooleanValidationStepBase booleanValidationStep)
                {
                    bool operationResult;

                    if (booleanValidationStep is SimpleBooleanValidationStep simpleBooleanValidationStep)
                    {
                        operationResult = this.protocolFactory.GetProtocolAndExecuteViaReflection<bool>(simpleBooleanValidationStep.Operation);
                        stopMessageOp = Op.Const(simpleBooleanValidationStep.StopMessage);
                    }
                    else if (booleanValidationStep is MessageContainedBooleanValidationStep messageContainedBooleanValidationStep)
                    {
                        var messageContainedResult = this.protocolFactory.GetProtocolAndExecuteViaReflection<ValidationBoolWithMessage>(messageContainedBooleanValidationStep.Operation);
                        operationResult = messageContainedResult.Outcome;
                        stopMessageOp = Op.Const(messageContainedResult.Message);
                    }
                    else if (booleanValidationStep is MessageByOpBooleanValidationStep messageByOpBooleanValidationStep)
                    {
                        operationResult = this.protocolFactory.GetProtocolAndExecuteViaReflection<bool>(messageByOpBooleanValidationStep.Operation);
                        stopMessageOp = messageByOpBooleanValidationStep.StopMessageOp;
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This type of {nameof(booleanValidationStep)} is not supported: {booleanValidationStep.GetType().ToStringReadable()}."));
                    }

                    var validationStepAction = operationResult
                        ? booleanValidationStep.TrueAction
                        : booleanValidationStep.FalseAction;

                    if (validationStepAction == ValidationStepAction.NextStep)
                    {
                        continue;
                    }

                    validity = GetValidity(validationStepAction);
                }
                else
                {
                    throw new NotSupportedException(Invariant($"This type of {nameof(validationStep)} is not supported: {validationStep.GetType().ToStringReadable()}."));
                }

                result = new ValidationResult(Op.Const(validity), stopMessageOp);

                break;
            }

            if (result == null)
            {
                result = new ValidationResult(Op.Const(validationChain.EndValidity), validationChain.EndMessageOp);
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<ValidationResult> ExecuteAsync(
            ValidateOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            ValidationResult result = null;

            var validationChain = operation.ValidationChain;

            foreach (var validationStep in validationChain.Steps)
            {
                Validity validity;
                IReturningOperation<string> stopMessageOp;

                if (validationStep is BooleanValidationStepBase booleanValidationStep)
                {
                    bool operationResult;

                    if (booleanValidationStep is SimpleBooleanValidationStep simpleBooleanValidationStep)
                    {
                        operationResult = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<bool>(simpleBooleanValidationStep.Operation);
                        stopMessageOp = Op.Const(simpleBooleanValidationStep.StopMessage);
                    }
                    else if (booleanValidationStep is MessageContainedBooleanValidationStep messageContainedBooleanValidationStep)
                    {
                        var messageContainedResult = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<ValidationBoolWithMessage>(messageContainedBooleanValidationStep.Operation);
                        operationResult = messageContainedResult.Outcome;
                        stopMessageOp = Op.Const(messageContainedResult.Message);
                    }
                    else if (booleanValidationStep is MessageByOpBooleanValidationStep messageByOpBooleanValidationStep)
                    {
                        operationResult = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<bool>(messageByOpBooleanValidationStep.Operation);
                        stopMessageOp = messageByOpBooleanValidationStep.StopMessageOp;
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"This type of {nameof(booleanValidationStep)} is not supported: {booleanValidationStep.GetType().ToStringReadable()}."));
                    }

                    var validationStepAction = operationResult
                        ? booleanValidationStep.TrueAction
                        : booleanValidationStep.FalseAction;

                    if (validationStepAction == ValidationStepAction.NextStep)
                    {
                        continue;
                    }

                    validity = GetValidity(validationStepAction);
                }
                else
                {
                    throw new NotSupportedException(Invariant($"This type of {nameof(validationStep)} is not supported: {validationStep.GetType().ToStringReadable()}."));
                }

                result = new ValidationResult(Op.Const(validity), stopMessageOp);

                break;
            }

            if (result == null)
            {
                result = new ValidationResult(Op.Const(validationChain.EndValidity), validationChain.EndMessageOp);
            }

            return result;
        }

        /// <inheritdoc />
        public AvailabilityCheckResult Execute(
            CheckAvailabilityOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            AvailabilityCheckResult result = null;

            var availabilityCheckChain = operation.AvailabilityCheckChain;

            foreach (var availabilityCheckStep in availabilityCheckChain.Steps)
            {
                var operationResult = this.protocolFactory.GetProtocolAndExecuteViaReflection<bool>(availabilityCheckStep.Operation);

                var availabilityCheckStepAction = operationResult
                    ? availabilityCheckStep.TrueAction
                    : availabilityCheckStep.FalseAction;

                if (availabilityCheckStepAction == AvailabilityCheckStepAction.NextStep)
                {
                    continue;
                }

                var availability = GetAvailability(availabilityCheckStepAction);

                result = new AvailabilityCheckResult(Op.Const(availability), availabilityCheckStep.StopMessageOp);

                break;
            }

            if (result == null)
            {
                result = new AvailabilityCheckResult(Op.Const(availabilityCheckChain.EndAvailability), availabilityCheckChain.EndMessageOp);
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<AvailabilityCheckResult> ExecuteAsync(
            CheckAvailabilityOp operation)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            AvailabilityCheckResult result = null;

            var availabilityCheckChain = operation.AvailabilityCheckChain;

            foreach (var availabilityCheckStep in availabilityCheckChain.Steps)
            {
                var operationResult = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<bool>(availabilityCheckStep.Operation);

                var availabilityCheckStepAction = operationResult
                    ? availabilityCheckStep.TrueAction
                    : availabilityCheckStep.FalseAction;

                if (availabilityCheckStepAction == AvailabilityCheckStepAction.NextStep)
                {
                    continue;
                }

                var availability = GetAvailability(availabilityCheckStepAction);

                result = new AvailabilityCheckResult(Op.Const(availability), availabilityCheckStep.StopMessageOp);

                break;
            }

            if (result == null)
            {
                result = new AvailabilityCheckResult(Op.Const(availabilityCheckChain.EndAvailability), availabilityCheckChain.EndMessageOp);
            }

            return result;
        }

        private static Validity GetValidity(
            ValidationStepAction validationStepAction)
        {
            Validity result;

            switch (validationStepAction)
            {
                case ValidationStepAction.StopAsInvalid:
                    result = Validity.Invalid;
                    break;
                case ValidationStepAction.StopAsNotApplicable:
                    result = Validity.NotApplicable;
                    break;
                case ValidationStepAction.StopAsValid:
                    result = Validity.Valid;
                    break;
                case ValidationStepAction.StopToAbort:
                    result = Validity.Aborted;
                    break;
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(ValidationStepAction)} is not supported: {validationStepAction}."));
            }

            return result;
        }

        private static Availability GetAvailability(
            AvailabilityCheckStepAction availabilityCheckStepAction)
        {
            Availability result;

            switch (availabilityCheckStepAction)
            {
                case AvailabilityCheckStepAction.StopAsDisabled:
                    result = Availability.Disabled;
                    break;
                case AvailabilityCheckStepAction.StopAsEnabled:
                    result = Availability.Enabled;
                    break;
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(AvailabilityCheckStepAction)} is not supported: {availabilityCheckStepAction}."));
            }

            return result;
        }
    }
}
