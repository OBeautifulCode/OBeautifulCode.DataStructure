// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IfThenElseOp{TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Performs an if/then/else.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class IfThenElseOp<TResult> : ReturningOperationBase<TResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IfThenElseOp{TValue}"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="statement">The statement to execute if the condition is true.</param>
        /// <param name="elseStatement">The statement to execute if the condition is false.</param>
        public IfThenElseOp(
            IReturningOperation<bool> condition,
            IReturningOperation<TResult> statement,
            IReturningOperation<TResult> elseStatement)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (statement == null)
            {
                throw new ArgumentNullException(nameof(statement));
            }

            if (elseStatement == null)
            {
                throw new ArgumentNullException(nameof(elseStatement));
            }

            this.Condition = condition;
            this.Statement = statement;
            this.ElseStatement = elseStatement;
        }

        /// <summary>
        /// Gets the condition.
        /// </summary>
        public IReturningOperation<bool> Condition { get; private set; }

        /// <summary>
        /// Gets the statement to execute if the condition is true.
        /// </summary>
        public IReturningOperation<TResult> Statement { get; private set; }

        /// <summary>
        /// Gets the statement to execute if the condition is false.
        /// </summary>
        public IReturningOperation<TResult> ElseStatement { get; private set; }
    }
}
