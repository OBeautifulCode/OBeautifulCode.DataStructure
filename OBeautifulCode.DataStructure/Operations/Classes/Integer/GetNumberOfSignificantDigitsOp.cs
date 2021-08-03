// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetNumberOfSignificantDigitsOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Gets the number of significant digits of a decimal.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class GetNumberOfSignificantDigitsOp : SingleStatementOpBase<decimal, int>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetNumberOfSignificantDigitsOp"/> class.
        /// </summary>
        /// <param name="statement">The statement.</param>
        public GetNumberOfSignificantDigitsOp(
            IReturningOperation<decimal> statement)
            : base(statement)
        {
        }
    }
}