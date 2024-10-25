// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellOpExecutionCompletedEvent{TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The execution of an <see cref="IOperationOutputCell{TResult}"/>'s <see cref="IOperationOutputCell{TResult}.Operation"/> completed.
    /// </summary>
    /// <typeparam name="TResult">The type returned when the operation was executed.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellOpExecutionCompletedEvent<TResult> : CellOpExecutionEventBase, ICellOpExecutionCompletedEvent, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellOpExecutionCompletedEvent{TResult}"/> class.
        /// </summary>
        /// <param name="executionResult">The result of executing the operation.  DEFAULT is no result.</param>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">OPTIONAL details about the completed execution.  DEFAULT is no details.</param>
        public CellOpExecutionCompletedEvent(
            TResult executionResult,
            DateTime timestampUtc,
            string details = null)
            : base(timestampUtc, details)
        {
            this.ExecutionResult = executionResult;
        }

        /// <summary>
        /// Gets the result of executing the operation.
        /// </summary>
        public TResult ExecutionResult { get; private set; }

        /// <inheritdoc />
        public object GetExecutionResultObjectValue()
        {
            var result = this.ExecutionResult;

            return result;
        }
    }
}