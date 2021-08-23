// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DivideOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Performs a division.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class DivideOp : ReturningOperationBase<decimal>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivideOp"/> class.
        /// </summary>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        public DivideOp(
            IReturningOperation<decimal> numerator,
            IReturningOperation<decimal> denominator)
        {
            if (numerator == null)
            {
                throw new ArgumentNullException(nameof(numerator));
            }

            if (denominator == null)
            {
                throw new ArgumentNullException(nameof(denominator));
            }

            this.Numerator = numerator;
            this.Denominator = denominator;
        }

        /// <summary>
        /// Gets the numerator.
        /// </summary>
        public IReturningOperation<decimal> Numerator { get; private set; }

        /// <summary>
        /// Gets the denominator.
        /// </summary>
        public IReturningOperation<decimal> Denominator { get; private set; }
    }
}