// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Op.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// Builder methods related to operations.
    /// </summary>
    public static class Op
    {
        /// <summary>
        /// Builds an operation that throws a <see cref="OpExecutionAbortedException"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="details">OPTIONAL details to use with the exception.  DEFAULT is to omit details.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static ThrowOpExecutionAbortedExceptionOp<TValue> Abort<TValue>(
            string details = null)
        {
            var result = new ThrowOpExecutionAbortedExceptionOp<TValue>(details);

            return result;
        }

        /// <summary>
        /// Builds an operation that throws a <see cref="OpExecutionDeemedNotApplicableException"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="details">OPTIONAL details to use with the exception.  DEFAULT is to omit details.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static ThrowOpExecutionDeemedNotApplicableExceptionOp<TValue> NotApplicable<TValue>(
            string details = null)
        {
            var result = new ThrowOpExecutionDeemedNotApplicableExceptionOp<TValue>(details);

            return result;
        }

        /// <summary>
        /// Builds an operation that gets a specified const value.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>
        /// An operation that gets the specified value.
        /// </returns>
        public static GetConstOp<TValue> Const<TValue>(
            TValue value)
        {
            var result = new GetConstOp<TValue>(value);

            return result;
        }

        /// <summary>
        /// Builds an operation that throws a <see cref="OpExecutionAbortedException"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="condition">The condition.</param>
        /// <param name="statement">The statement to execute if the condition is true.</param>
        /// <param name="elseStatement">The statement to execute if the condition is false.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static IfThenElseOp<TResult> IfThenElse<TResult>(
            IReturningOperation<bool> condition,
            IReturningOperation<TResult> statement,
            IReturningOperation<TResult> elseStatement)
        {
            var result = new IfThenElseOp<TResult>(condition, statement, elseStatement);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="AndAlsoOp"/>.
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static AndAlsoOp AndAlso(
            params IReturningOperation<bool>[] statements)
        {
            var result = new AndAlsoOp(statements);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="OrElseOp"/>.
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static OrElseOp OrElse(
            params IReturningOperation<bool>[] statements)
        {
            var result = new OrElseOp(statements);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="IsEqualToOp{TResult}"/>.
        /// </summary>
        /// <typeparam name="TStatement">The type of the statements to compare.</typeparam>
        /// <param name="statement1">The first statement.</param>
        /// <param name="statement2">The second statement.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static IsEqualToOp<TStatement> IsEqualTo<TStatement>(
            IReturningOperation<TStatement> statement1,
            IReturningOperation<TStatement> statement2)
        {
            var result = new IsEqualToOp<TStatement>(statement1, statement2);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="NotOp"/>.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static NotOp Not(
            IReturningOperation<bool> statement)
        {
            var result = new NotOp(statement);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="SumOp"/>.
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static SumOp Sum(
            params IReturningOperation<decimal>[] statements)
        {
            var result = new SumOp(statements);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="CompareOp"/>.
        /// </summary>
        /// <param name="left">The value to the left of the greater-than operator.</param>
        /// <param name="right">The value to the right of the greater-than operator.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static CompareOp IsGreaterThan(
            IReturningOperation<decimal> left,
            IReturningOperation<decimal> right)
        {
            var result = new CompareOp(left, Op.Const(CompareOperator.GreaterThan), right);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="CompareOp"/>.
        /// </summary>
        /// <param name="left">The value to the left of the greater-than-or-equal-to operator.</param>
        /// <param name="right">The value to the right of the greater-than-or-equal-to operator.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static CompareOp IsGreaterThanOrEqualTo(
            IReturningOperation<decimal> left,
            IReturningOperation<decimal> right)
        {
            var result = new CompareOp(left, Op.Const(CompareOperator.GreaterThanOrEqualTo), right);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="CompareOp"/>.
        /// </summary>
        /// <param name="left">The value to the left of the less-than operator.</param>
        /// <param name="right">The value to the right of the less-than operator.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static CompareOp IsLessThan(
            IReturningOperation<decimal> left,
            IReturningOperation<decimal> right)
        {
            var result = new CompareOp(left, Op.Const(CompareOperator.LessThan), right);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="CompareOp"/>.
        /// </summary>
        /// <param name="left">The value to the left of the less-than-or-equal-to operator.</param>
        /// <param name="right">The value to the right of the less-than-or-equal-to operator.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static CompareOp IsLessThanOrEqualTo(
            IReturningOperation<decimal> left,
            IReturningOperation<decimal> right)
        {
            var result = new CompareOp(left, Op.Const(CompareOperator.LessThanOrEqualTo), right);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="GetNumberOfSignificantDigitsOp"/>.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static GetNumberOfSignificantDigitsOp GetNumberOfSignificantDigits(
            IReturningOperation<decimal> statement)
        {
            var result = new GetNumberOfSignificantDigitsOp(statement);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="DivideOp"/>.
        /// </summary>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static DivideOp Divide(
            IReturningOperation<decimal> numerator,
            IReturningOperation<decimal> denominator)
        {
            var result = new DivideOp(numerator, denominator);

            return result;
        }

        /// <summary>
        /// Builds a <see cref="ValidateOp"/>.
        /// </summary>
        /// <param name="steps">The individual validation steps/checks.</param>
        /// <param name="endMessageOp">OPTIONAL operation to execute to get the message that should be emitted when all <paramref name="steps"/> have been evaluated and none have stopped the validation (we've reached the end of the chain).  DEFAULT is to omit this message.</param>
        /// <param name="endValidity">OPTIONAL value that specifies the validity of the subject when all <paramref name="steps"/> have been evaluated and none have stopped the validation (we've reached the end of the chain).  DEFAULT is to determine that the subject is valid.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static ValidateOp Validate(
            this IReadOnlyList<ValidationStepBase> steps,
            IReturningOperation<string> endMessageOp = null,
            Validity endValidity = Validity.Valid)
        {
            var validationChain = new ValidationChain(steps, endMessageOp, endValidity);

            var result = new ValidateOp(validationChain);

            return result;
        }

        /// <summary>
        /// Builds a <see cref="CheckAvailabilityOp"/>.
        /// </summary>
        /// <param name="steps">The individual availability check steps.</param>
        /// <param name="endMessageOp">OPTIONAL operation to execute to get the message that should be emitted when all <paramref name="steps"/> have been evaluated and none have stopped the availability check (we've reached the end of the chain).  DEFAULT is to omit this message.</param>
        /// <param name="endAvailability">OPTIONAL value that specifies the availability of the subject when all <paramref name="steps"/> have been evaluated and none have stopped the availability check (we've reached the end of the chain).  DEFAULT is to determine that the subject is enabled.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static CheckAvailabilityOp CheckAvailability(
            this IReadOnlyList<AvailabilityCheckStep> steps,
            IReturningOperation<string> endMessageOp = null,
            Availability endAvailability = Availability.Enabled)
        {
            var availabilityCheckChain = new AvailabilityCheckChain(steps, endMessageOp, endAvailability);

            var result = new CheckAvailabilityOp(availabilityCheckChain);

            return result;
        }
    }
}
