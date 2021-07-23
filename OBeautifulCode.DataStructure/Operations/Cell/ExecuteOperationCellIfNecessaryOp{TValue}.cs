// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteOperationCellIfNecessaryOp{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Executes the operation of an <see cref="IOperationOutputCell{TValue}"/> if it
    /// hasn't been executed or if there is a prior, but now stale execution.
    /// </summary>
    /// <typeparam name="TValue">The type of value contained in the cell.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ExecuteOperationCellIfNecessaryOp<TValue> : VoidOperationBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteOperationCellIfNecessaryOp{TValue}"/> class.
        /// </summary>
        /// <param name="cell">The cell.</param>
        public ExecuteOperationCellIfNecessaryOp(
            IOperationOutputCell<TValue> cell)
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
        public IOperationOutputCell<TValue> Cell { get; private set; }
    }
}
