// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    /// <summary>
    /// A cell whose value can be inputted with a specified type of value.
    /// </summary>
    /// <typeparam name="TValue">The type of inputted value.</typeparam>
    public interface IInputCell<TValue> : IInputCell, IGetCellValue<TValue>
    {
        /// <summary>
        /// Sets the value of the cell.
        /// </summary>
        /// <param name="value">The value to use.</param>
        /// <param name="timestampUtc">The timestamp (in UTC) to use on the event that records the setting of a cell's value.</param>
        /// <param name="details">OPTIONAL details related to setting the cell's value.  DEFAULT is to omit any details.</param>
        void SetCellValue(
            TValue value,
            DateTime timestampUtc,
            string details = null);

        /// <summary>
        /// Gets the <see cref="CellInputAppliedEvent{TValue}"/> that recorded the cell's value.
        /// </summary>
        /// <returns>
        /// The <see cref="CellInputAppliedEvent{TValue}"/> that recorded the cell's value.
        /// </returns>
        CellInputAppliedEvent<TValue> GetCellValueCellInputAppliedEvent();
    }
}
