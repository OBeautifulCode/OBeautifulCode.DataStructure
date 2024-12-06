// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureConvenienceProtocols{TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Some protocols for convenience.
    /// </summary>
    /// <typeparam name="TResult">The type of value.</typeparam>
    public class DataStructureConvenienceProtocols<TResult> :
          ISyncAndAsyncReturningProtocol<IfThenElseOp<TResult>, TResult>,
          ISyncAndAsyncReturningProtocol<IsEqualToOp<TResult>, bool>,
          ISyncAndAsyncReturningProtocol<AndAlsoOp, bool>,
          ISyncAndAsyncReturningProtocol<OrElseOp, bool>,
          ISyncAndAsyncReturningProtocol<NotOp, bool>,
          ISyncAndAsyncReturningProtocol<SumOp, decimal>,
          ISyncAndAsyncReturningProtocol<CompareOp, bool>,
          ISyncAndAsyncReturningProtocol<GetNumberOfSignificantDigitsOp, int>,
          ISyncAndAsyncReturningProtocol<DivideOp, decimal>,
          ISyncAndAsyncReturningProtocol<ValidateOp, ValidationResult>,
          ISyncAndAsyncReturningProtocol<CheckAvailabilityOp, AvailabilityCheckResult>
    {
        private readonly IProtocolFactory protocolFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStructureConvenienceProtocols{TValue}"/> class.
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
        public TResult Execute(
            IfThenElseOp<TResult> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            TResult result;

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (this.protocolFactory.GetProtocolAndExecuteViaReflection<bool>(operation.Condition))
            {
                result = this.protocolFactory.GetProtocolAndExecuteViaReflection<TResult>(operation.Statement);
            }
            else
            {
                result = this.protocolFactory.GetProtocolAndExecuteViaReflection<TResult>(operation.ElseStatement);
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<TResult> ExecuteAsync(
            IfThenElseOp<TResult> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            TResult result;

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<bool>(operation.Condition))
            {
                result = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<TResult>(operation.Statement);
            }
            else
            {
                result = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<TResult>(operation.ElseStatement);
            }

            return result;
        }

        /// <inheritdoc />
        public bool Execute(
            IsEqualToOp<TResult> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var value1 = this.protocolFactory.GetProtocolAndExecuteViaReflection<TResult>(operation.Statement1);

            var value2 = this.protocolFactory.GetProtocolAndExecuteViaReflection<TResult>(operation.Statement2);

            var result = value1.IsEqualTo(value2);

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> ExecuteAsync(
            IsEqualToOp<TResult> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var value1 = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<TResult>(operation.Statement1);

            var value2 = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<TResult>(operation.Statement2);

            var result = value1.IsEqualTo(value2);

            return result;
        }

        /// <inheritdoc />
        public bool Execute(
            AndAlsoOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            foreach (var statement in operation.Statements)
            {
                if (!this.protocolFactory.GetProtocolAndExecuteViaReflection<bool>(statement))
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> ExecuteAsync(
            AndAlsoOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            foreach (var statement in operation.Statements)
            {
                if (!(await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<bool>(statement)))
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public bool Execute(
            OrElseOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            foreach (var statement in operation.Statements)
            {
                if (this.protocolFactory.GetProtocolAndExecuteViaReflection<bool>(statement))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public async Task<bool> ExecuteAsync(
            OrElseOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            foreach (var statement in operation.Statements)
            {
                if (await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<bool>(statement))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool Execute(
            NotOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var result = !this.protocolFactory.GetProtocolAndExecuteViaReflection<bool>(operation.Statement);

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> ExecuteAsync(
            NotOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var result = !(await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<bool>(operation.Statement));

            return result;
        }

        /// <inheritdoc />
        public decimal Execute(
            SumOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var result = operation.Statements.Sum(_ => this.protocolFactory.GetProtocolAndExecuteViaReflection<decimal>(_));

            return result;
        }

        /// <inheritdoc />
        public async Task<decimal> ExecuteAsync(
            SumOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var result = 0m;

            foreach (var statement in operation.Statements)
            {
                result = result + await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<decimal>(statement);
            }

            return result;
        }

        /// <inheritdoc />
        public bool Execute(
            CompareOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var left = this.protocolFactory.GetProtocolAndExecuteViaReflection<decimal>(operation.Left);

            var @operator = this.protocolFactory.GetProtocolAndExecuteViaReflection<CompareOperator>(operation.Operator);

            var right = this.protocolFactory.GetProtocolAndExecuteViaReflection<decimal>(operation.Right);

            var result = Compare(left, @operator, right);

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> ExecuteAsync(
            CompareOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var left = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<decimal>(operation.Left);

            var @operator = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<CompareOperator>(operation.Operator);

            var right = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<decimal>(operation.Right);

            var result = Compare(left, @operator, right);

            return result;
        }

        /// <inheritdoc />
        public int Execute(
            GetNumberOfSignificantDigitsOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var value = this.protocolFactory.GetProtocolAndExecuteViaReflection<decimal>(operation.Statement);

            var result = GetNumberOfSignificantDigit(value);

            return result;
        }

        /// <inheritdoc />
        public async Task<int> ExecuteAsync(
            GetNumberOfSignificantDigitsOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var value = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<decimal>(operation.Statement);

            var result = GetNumberOfSignificantDigit(value);

            return result;
        }

        /// <inheritdoc />
        public decimal Execute(
            DivideOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var numerator = this.protocolFactory.GetProtocolAndExecuteViaReflection<decimal>(operation.Numerator);

            var denominator = this.protocolFactory.GetProtocolAndExecuteViaReflection<decimal>(operation.Denominator);

            var result = numerator / denominator;

            return result;
        }

        /// <inheritdoc />
        public async Task<decimal> ExecuteAsync(
            DivideOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var numerator = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<decimal>(operation.Numerator);

            var denominator = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<decimal>(operation.Denominator);

            var result = numerator / denominator;

            return result;
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

        private static bool Compare(
            decimal left,
            CompareOperator @operator,
            decimal right)
        {
            bool result;

            switch (@operator)
            {
                case CompareOperator.GreaterThan:
                    result = left > right;
                    break;
                case CompareOperator.GreaterThanOrEqualTo:
                    result = left >= right;
                    break;
                case CompareOperator.LessThan:
                    result = left < right;
                    break;
                case CompareOperator.LessThanOrEqualTo:
                    result = left <= right;
                    break;
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(CompareOperator)} is not supported: {@operator}."));
            }

            return result;
        }

        private static int GetNumberOfSignificantDigit(
            decimal value)
        {
            // adapted from: https://stackoverflow.com/a/42265036/356790
            var n = value / 1.000000000000000000000000000000m;

            var bits = decimal.GetBits(n);

            var result = bits[3] >> 16 & 255;

            if (result < 0)
            {
                throw new InvalidOperationException("Expected result to be >= 0.");
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
