// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateCellOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Performs validation on a cell.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ValidateCellOp : VoidOperationBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateCellOp"/> class.
        /// </summary>
        /// <param name="cell">The cell.</param>
        public ValidateCellOp(
            IValidateableCell cell)
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
        public IValidateableCell Cell { get; private set; }
    }
}
