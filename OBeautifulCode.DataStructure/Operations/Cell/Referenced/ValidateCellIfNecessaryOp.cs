// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateCellIfNecessaryOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Performs validation on a cell if it hasn't been already been
    /// performed or if there is a prior, but now stale validation.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ValidateCellIfNecessaryOp : VoidOperationBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateCellIfNecessaryOp"/> class.
        /// </summary>
        /// <param name="cell">The cell.</param>
        public ValidateCellIfNecessaryOp(
            IValidationCell cell)
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
        public IValidationCell Cell { get; private set; }
    }
}
