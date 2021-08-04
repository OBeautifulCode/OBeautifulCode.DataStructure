﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckAvailabilityOfCellOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Performs an availability check on a cell.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CheckAvailabilityOfCellOp : VoidOperationBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckAvailabilityOfCellOp"/> class.
        /// </summary>
        /// <param name="cell">The cell.</param>
        public CheckAvailabilityOfCellOp(
            IAvailabilityCheckCell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            this.Cell = cell;
        }

        /// <summary>
        /// Gets the cell.
        /// </summary>
        public IAvailabilityCheckCell Cell { get; private set; }
    }
}
