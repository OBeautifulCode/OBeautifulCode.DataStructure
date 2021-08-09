// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRecordCellOpExecutionEvents.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    /// <summary>
    /// Records <see cref="CellOpExecutionEventBase"/> events.
    /// </summary>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IRecordCellOpExecutionEvents
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
    }
}
