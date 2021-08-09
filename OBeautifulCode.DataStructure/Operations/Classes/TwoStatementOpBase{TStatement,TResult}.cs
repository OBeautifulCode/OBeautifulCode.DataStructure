// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TwoStatementOpBase{TStatement,TResult}.cs" company="OBeautifulCode">
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
    public abstract partial class TwoStatementOpBase<TStatement, TResult> : ReturningOperationBase<TResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwoStatementOpBase{TStatement, TResult}"/> class.
        /// </summary>
        /// <param name="statement1">The first statement.</param>
        /// <param name="statement2">The second statement.</param>
        protected TwoStatementOpBase(
            IReturningOperation<TStatement> statement1,
            IReturningOperation<TStatement> statement2)
        {
            if (statement1 == null)
            {
                throw new ArgumentNullException(nameof(statement1));
            }

            if (statement2 == null)
            {
                throw new ArgumentNullException(nameof(statement2));
            }

            this.Statement1 = statement1;
            this.Statement2 = statement2;
        }

        /// <summary>
        /// Gets the first statement.
        /// </summary>
        public IReturningOperation<TStatement> Statement1 { get; private set; }

        /// <summary>
        /// Gets the second statement.
        /// </summary>
        public IReturningOperation<TStatement> Statement2 { get; private set; }
    }
}
