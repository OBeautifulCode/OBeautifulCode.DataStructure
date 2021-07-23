// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AndOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// Performs an AND (&amp;).
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class AndOp : MultiStatementOpBase<bool, bool>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndOp"/> class.
        /// </summary>
        /// <param name="statements">The statements.</param>
        public AndOp(
            IReadOnlyCollection<IReturningOperation<bool>> statements)
            : base(statements)
        {
        }
    }
}
