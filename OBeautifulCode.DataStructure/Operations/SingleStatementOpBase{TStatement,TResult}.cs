// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingleStatementOpBase{TStatement,TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// An operation that executes over a single-statement input.
    /// </summary>
    /// <typeparam name="TStatement">The type of the statements.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class SingleStatementOpBase<TStatement, TResult> : ReturningOperationBase<TResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleStatementOpBase{TStatement, TResult}"/> class.
        /// </summary>
        /// <param name="statement">The statement.</param>
        protected SingleStatementOpBase(
            IReturningOperation<TStatement> statement)
        {
            if (statement == null)
            {
                throw new ArgumentNullException(nameof(statement));
            }

            this.Statement = statement;
        }

        /// <summary>
        /// Gets the statement.
        /// </summary>
        public IReturningOperation<TStatement> Statement { get; private set; }
    }
}
