// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetValidityOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Gets the validity of a cell.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class GetValidityOp : LocatedCellOpBase<Validity>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetValidityOp"/> class.
        /// </summary>
        /// <param name="cellLocator">A cell locator.</param>
        public GetValidityOp(
            IReturningOperation<CellLocatorBase> cellLocator)
            : base(cellLocator)
        {
        }
    }
}
