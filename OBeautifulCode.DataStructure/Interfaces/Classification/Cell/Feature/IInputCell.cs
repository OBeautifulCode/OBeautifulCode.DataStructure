// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A cell whose value can be inputted.
    /// </summary>
    public interface IInputCell : INotSlottedCell, IGetCellValue, IClearCellValue
    {
        /// <summary>
        /// Gets the events that record the manipulation of this cell's value.
        /// </summary>
        IReadOnlyList<CellInputEventBase> InputEvents { get; }

        /// <summary>
        /// Records a <see cref="CellInputEventBase"/>.
        /// </summary>
        /// <param name="inputEvent">The event to record.</param>
        void Record(
            CellInputEventBase inputEvent);

        /// <summary>
        /// Attempts to record a <see cref="CellInputEventBase"/>.
        /// </summary>
        /// <param name="inputEvent">The event to record.</param>
        /// <returns>true if the event was recorded; otherwise false (the cell value type does not allow for recording the event).</returns>
        bool TryRecord(
            CellInputEventBase inputEvent);

        /// <summary>
        /// Sets the value of the cell.
        /// </summary>
        /// <param name="value">The value to use.</param>
        /// <param name="timestampUtc">The timestamp (in UTC) to use on the event that records the setting of a cell's value.</param>
        /// <param name="details">OPTIONAL details related to setting the cell's value.  DEFAULT is to omit any details.</param>
        void SetCellValue(
            object value,
            DateTime timestampUtc,
            string details = null);

        /// <summary>
        /// Attempts to set the value of the cell.
        /// </summary>
        /// <param name="value">The value to use.</param>
        /// <param name="timestampUtc">The timestamp (in UTC) to use on the event that records the setting of a cell's value.</param>
        /// <param name="details">OPTIONAL details related to setting the cell's value.  DEFAULT is to omit any details.</param>
        /// <returns>true if the value was set; otherwise false (the cell value type does not allow for setting it's value using <paramref name="value"/>).</returns>
        bool TrySetCellValue(
            object value,
            DateTime timestampUtc,
            string details = null);

        /// <summary>
        /// Gets the <see cref="CellInputAppliedEventBase"/> that recorded the cell's value.
        /// </summary>
        /// <returns>
        /// The <see cref="CellInputAppliedEventBase"/> that recorded the cell's value.
        /// </returns>
        CellInputAppliedEventBase GetCellValueCellInputAppliedEventBase();
    }
}
