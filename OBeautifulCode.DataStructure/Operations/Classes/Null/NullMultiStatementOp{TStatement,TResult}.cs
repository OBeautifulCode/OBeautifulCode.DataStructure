// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullMultiStatementOp{TStatement,TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Null object pattern implementation of an operation that executes over a multi-statement input.
    /// </summary>
    /// <typeparam name="TStatement">The type of the statements.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
    public partial class NullMultiStatementOp<TStatement, TResult> : MultiStatementOpBase<TStatement, TResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullMultiStatementOp{TStatement, TResult}"/> class.
        /// </summary>
        /// <param name="statements">The statements.</param>
        public NullMultiStatementOp(
            IReadOnlyCollection<IReturningOperation<TStatement>> statements)
            : base(statements)
        {
        }
    }
}
