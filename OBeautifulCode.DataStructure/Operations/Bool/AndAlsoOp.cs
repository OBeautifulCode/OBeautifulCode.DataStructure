// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AndAlsoOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// Performs an AND-ALSO (&amp;&amp;).
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class AndAlsoOp : MultiStatementOpBase<bool, bool>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndAlsoOp"/> class.
        /// </summary>
        /// <param name="statements">The statements.</param>
        public AndAlsoOp(
            IReadOnlyCollection<IReturningOperation<bool>> statements)
            : base(statements)
        {
        }
    }
}