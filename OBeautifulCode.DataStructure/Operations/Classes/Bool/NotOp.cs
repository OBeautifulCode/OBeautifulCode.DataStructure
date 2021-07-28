// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Performs a NOT (!).
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class NotOp : SingleStatementOpBase<bool, bool>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotOp"/> class.
        /// </summary>
        /// <param name="statement">The statement.</param>
        public NotOp(
            IReturningOperation<bool> statement)
            : base(statement)
        {
        }
    }
}
