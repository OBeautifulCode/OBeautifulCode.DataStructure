// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// A cell whose value can be inputted with a specified type of value.
    /// </summary>
    /// <typeparam name="TValue">The type of inputted value.</typeparam>
    public interface IInputCell<TValue> : INotSlottedCell, IGetCellValue<TValue>, IClearCellValue, IValidateCellValue, IHaveCellValidationConditions
    {
        /// <summary>
        /// Gets the input that was applied to the cell.
        /// </summary>
        InputAppliedToCellEvent<TValue> InputAppliedToCellEvent { get; }

        /// <summary>
        /// Records input.
        /// </summary>
        /// <param name="inputAppliedToCellEvent">The result of executing the operation.</param>
        void RecordInput(InputAppliedToCellEvent<TValue> inputAppliedToCellEvent);
    }
}
