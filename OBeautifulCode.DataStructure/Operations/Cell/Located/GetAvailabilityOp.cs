// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAvailabilityOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Gets the availability of a cell.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class GetAvailabilityOp : LocatedCellOpBase<Availability>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailabilityOp"/> class.
        /// </summary>
        /// <param name="cellLocator">A cell locator.</param>
        public GetAvailabilityOp(
            IReturningOperation<CellLocatorBase> cellLocator)
            : base(cellLocator)
        {
        }
    }
}
