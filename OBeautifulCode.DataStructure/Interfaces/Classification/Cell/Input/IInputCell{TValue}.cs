// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    /// <summary>
    /// A cell whose value can be inputted with a specified type of value.
    /// </summary>
    /// <typeparam name="TValue">The type of inputted value.</typeparam>
    public interface IInputCell<TValue> : INotSlottedCell, IGetCellValue<TValue>, IClearCellValue, IValidateableCell
    {
        /// <summary>
        /// Gets the events that record the manipulation of this cell's value.
        /// </summary>
        IReadOnlyList<CellInputEventBase> CellInputEvents { get; }

        /// <summary>
        /// Records a <see cref="CellInputEventBase"/>.
        /// </summary>
        /// <param name="cellInputEvent">The event to record.</param>
        void Record(CellInputEventBase cellInputEvent);
    }
}
