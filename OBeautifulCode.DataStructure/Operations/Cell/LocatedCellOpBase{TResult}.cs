// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocatedCellOpBase{TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Base class for an operation that references a cell via a <see cref="CellLocator"/>.
    /// </summary>
    /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class LocatedCellOpBase<TResult> : ReturningOperationBase<TResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocatedCellOpBase{TResult}"/> class.
        /// </summary>
        /// <param name="cellLocator">A cell locator.</param>
        protected LocatedCellOpBase(
            IReturningOperation<ICellLocator> cellLocator)
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
        public IReturningOperation<ICellLocator> CellLocator { get; private set; }
    }
}
