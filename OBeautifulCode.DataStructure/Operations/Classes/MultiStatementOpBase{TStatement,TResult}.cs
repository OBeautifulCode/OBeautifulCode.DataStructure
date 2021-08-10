// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiStatementOpBase{TStatement,TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// An operation that executes over a multi-statement input.
    /// </summary>
    /// <typeparam name="TStatement">The type of the statements.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
    public abstract partial class MultiStatementOpBase<TStatement, TResult> : ReturningOperationBase<TResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiStatementOpBase{TStatement, TResult}"/> class.
        /// </summary>
        /// <param name="statements">The statements.</param>
        protected MultiStatementOpBase(
            IReadOnlyCollection<IReturningOperation<TStatement>> statements)
        {
            if (statements == null)
            {
                throw new ArgumentNullException(nameof(statements));
            }

            if (statements.Count < 2)
            {
                throw new ArgumentException(Invariant($"{nameof(statements)} contains less than 2 elements."));
            }

            if (statements.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(statements)} contains a null element."));
            }

            this.Statements = statements;
        }

        /// <summary>
        /// Gets the statements.
        /// </summary>
        public IReadOnlyCollection<IReturningOperation<TStatement>> Statements { get; private set; }
    }
}
