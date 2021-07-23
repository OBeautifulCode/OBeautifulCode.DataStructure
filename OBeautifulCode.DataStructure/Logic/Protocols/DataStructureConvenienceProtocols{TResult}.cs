// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureConvenienceProtocols{TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Threading.Tasks;

    using OBeautifulCode.Type;

    /// <summary>
    /// Some protocols for convenience.
    /// </summary>
    /// <typeparam name="TResult">The type of value.</typeparam>
    public class DataStructureConvenienceProtocols<TResult> :
          ISyncAndAsyncReturningProtocol<IfThenElseOp<TResult>, TResult>
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
    }
}
