// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SumOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// Performs a sum (addition).
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class SumOp : MultiStatementOpBase<decimal, decimal>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SumOp"/> class.
        /// </summary>
        /// <param name="statements">The statements.</param>
        public SumOp(
            IReadOnlyCollection<IReturningOperation<decimal>> statements)
            : base(statements)
        {
        }
    }
}