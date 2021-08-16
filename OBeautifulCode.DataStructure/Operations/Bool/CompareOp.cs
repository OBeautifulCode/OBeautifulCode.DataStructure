// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompareOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Performs a comparison.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CompareOp : IModelViaCodeGen, IReturningOperation<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareOp"/> class.
        /// </summary>
        /// <param name="left">The value to the left of the <paramref name="operator"/>.</param>
        /// <param name="operator">The comparison operator to use.</param>
        /// <param name="right">The value to the right of the <paramref name="operator"/>.</param>
        public CompareOp(
            IReturningOperation<decimal> left,
            IReturningOperation<CompareOperator> @operator,
            IReturningOperation<decimal> right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (@operator == null)
            {
                throw new ArgumentNullException(nameof(@operator));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            this.Left = left;
            this.Operator = @operator;
            this.Right = right;
        }

        /// <summary>
        /// Gets value to the left of the <see cref="Operator"/>.
        /// </summary>
        public IReturningOperation<decimal> Left { get; private set; }

        /// <summary>
        /// Gets the comparison operator to use.
        /// </summary>
        public IReturningOperation<CompareOperator> Operator { get; private set; }

        /// <summary>
        /// Gets value to the right of the <see cref="Operator"/>.
        /// </summary>
        public IReturningOperation<decimal> Right { get; private set; }
    }
}