// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellOpExecutedEvent{TResult}.cs" company="OBeautifulCode">
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
    public class CellOpExecutedEvent<TResult> : EventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellOpExecutedEvent{TResult}"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="result">The result of executing the operation.</param>
        public CellOpExecutedEvent(
            DateTime timestampUtc,
            TResult result)
            : base(timestampUtc)
        {
            this.Result = result;
        }

        /// <summary>
        /// Gets the result of executing the operation.
        /// </summary>
        public TResult Result { get; private set; }
    }
}