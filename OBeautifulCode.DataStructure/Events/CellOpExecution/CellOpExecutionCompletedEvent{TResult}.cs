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
    /// An <see cref="IOperationOutputCell{TResult}"/>'s <see cref="IOperationOutputCell{TResult}.Operation"/> has been executed.
    /// </summary>
    /// <typeparam name="TResult">The type returned when the operation was executed.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellOpExecutionCompletedEvent<TResult> : CellOpExecutionEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellOpExecutionCompletedEvent{TResult}"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the completed execution.</param>
        /// <param name="executionResult">The result of executing the operation.</param>
        public CellOpExecutionCompletedEvent(
            DateTime timestampUtc,
            string details,
            TResult executionResult)
            : base(timestampUtc, details)
        {
            this.ExecutionResult = executionResult;
        }

        /// <summary>
        /// Gets the result of executing the operation.
        /// </summary>
        public TResult ExecutionResult { get; private set; }
    }
}