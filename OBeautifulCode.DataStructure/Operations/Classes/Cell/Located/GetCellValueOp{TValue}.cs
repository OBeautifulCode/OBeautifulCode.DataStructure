// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCellValueOp{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Gets the value of a cell.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class GetCellValueOp<TValue> : ReturningOperationBase<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCellValueOp{TValue}"/> class.
        /// </summary>
        /// <param name="cellLocator">A cell locator.</param>
        /// <param name="defaultValue">OPTIONAL value to use if the cell does not have a value.  DEFAULT is to throw if cell does not have a value.</param>
        public GetCellValueOp(
            CellLocator cellLocator,
            IReturningOperation<TValue> defaultValue = null)
        {
            if (cellLocator == null)
            {
                throw new ArgumentNullException(nameof(cellLocator));
            }

            this.CellLocator = cellLocator;
            this.DefaultValue = defaultValue;
        }

        /// <summary>
        /// Gets the cell locator.
        /// </summary>
        public CellLocator CellLocator { get; private set; }

        /// <summary>
        /// Gets the value to use if the cell does not have a value.
        /// </summary>
        public IReturningOperation<TValue> DefaultValue { get; private set; }
    }
}
