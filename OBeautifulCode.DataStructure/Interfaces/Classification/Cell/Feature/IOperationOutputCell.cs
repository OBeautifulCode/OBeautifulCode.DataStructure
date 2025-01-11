// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOperationOutputCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    /// <summary>
    /// A cell who's output is the result of executing an operation.
    /// </summary>
    public interface IOperationOutputCell : IOutputCell, IClearCellValue
    {
        /// <summary>
        /// Gets the events that record the execution of an operation.
        /// </summary>
        IReadOnlyList<CellOpExecutionEventBase> OperationExecutionEvents { get; }

        /// <summary>
        /// Records a <see cref="CellOpExecutionEventBase"/>.
        /// </summary>
        /// <param name="operationExecutionEvent">The event to record.</param>
        void Record(CellOpExecutionEventBase operationExecutionEvent);

        /// <summary>
        /// Gets the <see cref="CellOpExecutionCompletedEventBase"/> that recorded the cell's value.
        /// </summary>
        /// <returns>
        /// The <see cref="CellOpExecutionCompletedEventBase"/> that recorded the cell's value.
        /// </returns>
        CellOpExecutionCompletedEventBase GetCellValueCellOpExecutionCompletedEventBase();
    }
}
