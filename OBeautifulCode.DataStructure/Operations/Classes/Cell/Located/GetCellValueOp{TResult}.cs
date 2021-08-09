// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCellValueOp{TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Gets the value of a cell.
    /// </summary>
    /// <typeparam name="TResult">The type of value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class GetCellValueOp<TResult> : LocatedCellOpBase<TResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCellValueOp{TResult}"/> class.
        /// </summary>
        /// <param name="cellLocator">A cell locator.</param>
        /// <param name="defaultValue">OPTIONAL value to use if the cell does not have a value.  DEFAULT is to throw if cell does not have a value.</param>
        public GetCellValueOp(
            IReturningOperation<CellLocatorBase> cellLocator,
            IReturningOperation<TResult> defaultValue = null)
            : base(cellLocator)
        {
            this.DefaultValue = defaultValue;
        }

        /// <summary>
        /// Gets the value to use if the cell does not have a value.
        /// </summary>
        public IReturningOperation<TResult> DefaultValue { get; private set; }
    }
}
