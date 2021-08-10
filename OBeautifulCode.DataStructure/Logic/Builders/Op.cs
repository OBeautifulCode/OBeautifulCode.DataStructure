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
        /// Builds an operation that returns an <see cref="AvailabilityCheckResult"/>.
        /// </summary>
        /// <param name="availability">The availability of the subject.</param>
        /// <param name="messageOp">OPTIONAL operation to execute to get the message to emit about the availability.  DEFAULT is no message.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static IReturningOperation<AvailabilityCheckResult> GetAvailabilityCheckResult(
            this Availability availability,
            IReturningOperation<string> messageOp = null)
        {
            var result = Op.Const(new AvailabilityCheckResult(Op.Const(availability), messageOp));

            return result;
        }

        /// <summary>
        /// Builds an operation that returns an <see cref="AvailabilityCheckResult"/>.
        /// </summary>
        /// <param name="availabilityOp">Operation to execute to get the availability of the subject.</param>
        /// <param name="messageOp">OPTIONAL operation to execute to get the message to emit about the availability.  DEFAULT is no message.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static IReturningOperation<AvailabilityCheckResult> GetAvailabilityCheckResult(
            this IReturningOperation<Availability> availabilityOp,
            IReturningOperation<string> messageOp = null)
        {
            var result = Op.Const(new AvailabilityCheckResult(availabilityOp, messageOp));

            return result;
        }

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
        /// Builds an <see cref="OrElseOp"/>.
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
        /// Builds an <see cref="SumOp"/>.
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
            var result = new CompareOp(left, CompareOperator.GreaterThan, right);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="SumOp"/>.
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
            var result = new CompareOp(left, CompareOperator.GreaterThanOrEqualTo, right);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="SumOp"/>.
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
            var result = new CompareOp(left, CompareOperator.LessThan, right);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="SumOp"/>.
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
            var result = new CompareOp(left, CompareOperator.LessThanOrEqualTo, right);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="ValidateUsingConditionsOp"/>.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static ValidateUsingConditionsOp ValidateUsingConditions(
            IReadOnlyList<ValidationCondition> conditions)
        {
            var result = new ValidateUsingConditionsOp(conditions);

            return result;
        }
    }
}
