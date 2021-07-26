﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellOpExecutionFailedWithMissingCellValueEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The execution of an <see cref="IOperationOutputCell{TResult}"/>'s <see cref="IOperationOutputCell{TResult}.Operation"/> failed because a referenced cell's value was missing.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellOpExecutionFailedWithMissingCellValueEvent : CellOpExecutionEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellOpExecutionFailedWithMissingCellValueEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="cellLocator">The cell locator.</param>
        /// <param name="details">Details about the missing value.</param>
        public CellOpExecutionFailedWithMissingCellValueEvent(
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