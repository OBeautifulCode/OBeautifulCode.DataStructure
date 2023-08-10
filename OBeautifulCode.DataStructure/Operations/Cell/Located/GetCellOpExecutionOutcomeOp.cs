// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCellOpExecutionOutcomeOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Gets the outcome of executing an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class GetCellOpExecutionOutcomeOp : LocatedCellOpBase<CellOpExecutionOutcome>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCellOpExecutionOutcomeOp"/> class.
        /// </summary>
        /// <param name="cellLocator">A cell locator.</param>
        public GetCellOpExecutionOutcomeOp(
            IReturningOperation<ICellLocator> cellLocator)
            : base(cellLocator)
        {
        }
    }
}
