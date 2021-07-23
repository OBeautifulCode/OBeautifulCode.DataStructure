// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Do.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Builder methods related to operations.
    /// </summary>
    public static class Do
    {
        /// <summary>
        /// Builds an operation that throws a <see cref="OpExecutionAbortedException"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="details">The details to use with the exception.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static ThrowOpExecutionAbortedExceptionOp<TValue> Stop<TValue>(
            string details)
        {
            var result = new ThrowOpExecutionAbortedExceptionOp<TValue>(details);

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
        public static GetConstOp<TValue> Value<TValue>(
            TValue value)
        {
            var result = new GetConstOp<TValue>(value);

            return result;
        }

        /// <summary>
        /// Builds an operation that throws a <see cref="OpExecutionAbortedException"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="condition">The condition.</param>
        /// <param name="statement">The statement to execute if the condition is true.</param>
        /// <param name="elseStatement">The statement to execute if the condition is false.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static IfThenElseOp<TValue> IfThenElse<TValue>(
            IReturningOperation<bool> condition,
            IReturningOperation<TValue> statement,
            IReturningOperation<TValue> elseStatement)
        {
            var result = new IfThenElseOp<TValue>(condition, statement, elseStatement);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="AndOp"/>.
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static AndOp And(
            params IReturningOperation<bool>[] statements)
        {
            var result = new AndOp(statements);

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
        /// Builds an <see cref="OrOp"/>.
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static OrOp Or(
            params IReturningOperation<bool>[] statements)
        {
            var result = new OrOp(statements);

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
    }
}
