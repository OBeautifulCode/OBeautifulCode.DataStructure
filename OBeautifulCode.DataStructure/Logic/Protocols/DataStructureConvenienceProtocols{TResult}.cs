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

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Some protocols for convenience.
    /// </summary>
    /// <typeparam name="TResult">The type of value.</typeparam>
    public class DataStructureConvenienceProtocols<TResult> :
          ISyncAndAsyncReturningProtocol<IfThenElseOp<TResult>, TResult>,
          ISyncAndAsyncReturningProtocol<AndAlsoOp, bool>,
          ISyncAndAsyncReturningProtocol<OrElseOp, bool>,
          ISyncAndAsyncReturningProtocol<NotOp, bool>,
          ISyncAndAsyncReturningProtocol<SumOp, decimal>,
          ISyncAndAsyncReturningProtocol<CompareOp, bool>
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

            var right = this.protocolFactory.GetProtocolAndExecuteViaReflection<decimal>(operation.Right);

            var result = Compare(left, operation.Operator, right);

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

            var right = await this.protocolFactory.GetProtocolAndExecuteViaReflectionAsync<decimal>(operation.Right);

            var result = Compare(left, operation.Operator, right);

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
    }
}
