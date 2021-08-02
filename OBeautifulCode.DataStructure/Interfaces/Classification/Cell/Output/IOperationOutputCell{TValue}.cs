﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOperationOutputCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// A cell who's output is the result of executing an operation.
    /// </summary>
    /// <typeparam name="TValue">The type of output value.</typeparam>
    public interface IOperationOutputCell<TValue> : IOutputCell<TValue>, IClearCellValue, IValidateableCell
    {
        /// <summary>
        /// Gets the operation.
        /// </summary>
        IReturningOperation<TValue> Operation { get; }

        /// <summary>
        /// Gets the results of executing the operation.
        /// </summary>
        IReadOnlyList<CellOpExecutionEventBase> CellOpExecutionEvents { get; }

        /// <summary>
        /// Records the execution of the <see cref="Operation"/>.
        /// </summary>
        /// <param name="cellOpExecutionEvent">The result of executing the operation.</param>
        void RecordExecution(CellOpExecutionEventBase cellOpExecutionEvent);
    }
}
