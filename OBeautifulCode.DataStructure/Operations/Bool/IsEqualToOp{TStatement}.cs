// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsEqualToOp{TStatement}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Determines if two value are equal.
    /// </summary>
    /// <typeparam name="TStatement">The type of the statements to compare.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class IsEqualToOp<TStatement> : TwoStatementOpBase<TStatement, bool>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsEqualToOp{TStatement}"/> class.
        /// </summary>
        /// <param name="statement1">The first statement.</param>
        /// <param name="statement2">The second statement.</param>
        public IsEqualToOp(
            IReturningOperation<TStatement> statement1,
            IReturningOperation<TStatement> statement2)
            : base(statement1, statement2)
        {
        }
    }
}
