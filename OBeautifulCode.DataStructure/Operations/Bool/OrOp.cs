// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// Performs an OR (|).
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class OrOp : MultiStatementOpBase<bool, bool>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrOp"/> class.
        /// </summary>
        /// <param name="statements">The statements.</param>
        public OrOp(
            IReadOnlyCollection<IReturningOperation<bool>> statements)
            : base(statements)
        {
        }
    }
}
