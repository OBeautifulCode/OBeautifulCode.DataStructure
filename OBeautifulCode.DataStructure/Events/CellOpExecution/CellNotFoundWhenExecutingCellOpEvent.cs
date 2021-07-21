// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellNotFoundWhenExecutingCellOpEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Executing an <see cref="IOperationOutputCell{TResult}"/>'s <see cref="IOperationOutputCell{TResult}.Operation"/> failed.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public class CellNotFoundWhenExecutingCellOpEvent : CellOpExecutionEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellNotFoundWhenExecutingCellOpEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="cellLocator">The cell locator.</param>
        /// <param name="details">Details about the attempt to find the cell.</param>
        public CellNotFoundWhenExecutingCellOpEvent(
            DateTime timestampUtc,
            CellLocator cellLocator,
            string details)
            : base(timestampUtc, details)
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