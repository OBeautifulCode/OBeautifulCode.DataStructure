// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HasCellValueOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Determines if a cell has a value.
    /// </summary>
    public partial class HasCellValueOp : LocatedCellOpBase<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HasCellValueOp"/> class.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        public HasCellValueOp(
            IReturningOperation<CellLocatorBase> cellLocator)
            : base(cellLocator)
        {
        }
    }
}
