// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCellOpExecutionOutcomeOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Gets the outcome of executing an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class GetCellOpExecutionOutcomeOp : ReturningOperationBase<CellOpExecutionOutcome>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCellOpExecutionOutcomeOp"/> class.
        /// </summary>
        /// <param name="cellLocator">A cell locator.</param>
        public GetCellOpExecutionOutcomeOp(
            IReturningOperation<CellLocatorBase> cellLocator)
        {
            if (cellLocator == null)
            {
                throw new ArgumentNullException(nameof(cellLocator));
            }

            this.CellLocator = cellLocator;
        }

        /// <summary>
        /// Gets the cell locator.
        /// </summary>
        public IReturningOperation<CellLocatorBase> CellLocator { get; private set; }
    }
}
