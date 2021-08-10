// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullSingleStatementOp{TStatement,TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Null object pattern implementation of an operation that executes over a single-statement input.
    /// </summary>
    /// <typeparam name="TStatement">The type of the statements.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class NullSingleStatementOp<TStatement, TResult> : SingleStatementOpBase<TStatement, TResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullSingleStatementOp{TStatement, TResult}"/> class.
        /// </summary>
        /// <param name="statement">The statement.</param>
        public NullSingleStatementOp(
            IReturningOperation<TStatement> statement)
            : base(statement)
        {
        }
    }
}
