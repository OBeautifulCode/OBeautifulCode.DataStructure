// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrElseOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// Performs an OR-ELSE (||).
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class OrElseOp : MultiStatementOpBase<bool, bool>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrElseOp"/> class.
        /// </summary>
        /// <param name="statements">The statements.</param>
        public OrElseOp(
            IReadOnlyCollection<IReturningOperation<bool>> statements)
            : base(statements)
        {
        }
    }
}
