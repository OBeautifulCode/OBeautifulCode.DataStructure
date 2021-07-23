// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HasCellValueOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Determines if a cell has a value.
    /// </summary>
    public partial class HasCellValueOp : ReturningOperationBase<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HasCellValueOp"/> class.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        public HasCellValueOp(
            CellLocator cellLocator)
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
        public CellLocator CellLocator { get; private set; }
    }
}
