﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOperationOutputCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// A cell who's output is the result of executing an operation.
    /// </summary>
    /// <typeparam name="TValue">The type of output value.</typeparam>
    public interface IOperationOutputCell<TValue> : IOperationOutputCell, IOutputCell<TValue>
    {
        /// <summary>
        /// Gets the operation.
        /// </summary>
        IReturningOperation<TValue> Operation { get; }

        /// <summary>
        /// Gets the <see cref="CellOpExecutionCompletedEvent{TValue}"/> that recorded the cell's value.
        /// </summary>
        /// <returns>
        /// The <see cref="CellOpExecutionCompletedEvent{TValue}"/> that recorded the cell's value.
        /// </returns>
        CellOpExecutionCompletedEvent<TValue> GetCellValueCellOpExecutionCompletedEvent();
    }
}
