// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCellValueByCellReferenceOp{TValue}.cs" company="OBeautifulCode">
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
    public class GetCellValueByCellReferenceOp<TValue> : ReturningOperationBase<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCellValueByCellReferenceOp{TValue}"/> class.
        /// </summary>
        /// <param name="cell">The cell.</param>
        public GetCellValueByCellReferenceOp(
            IGetCellValue<TValue> cell)
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
        public IGetCellValue<TValue> Cell { get; private set; }
    }
}
