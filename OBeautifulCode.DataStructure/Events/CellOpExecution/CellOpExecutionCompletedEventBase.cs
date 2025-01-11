// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellOpExecutionCompletedEventBase.cs" company="OBeautifulCode">
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
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class CellOpExecutionCompletedEventBase : CellOpExecutionEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellOpExecutionCompletedEventBase"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">OPTIONAL details about the completed execution.  DEFAULT is no details.</param>
        protected CellOpExecutionCompletedEventBase(
            DateTime timestampUtc,
            string details = null)
            : base(timestampUtc, details)
        {
        }

        /// <summary>
        /// Gets the result of executing the operation as an object.
        /// </summary>
        /// <returns>
        /// The result of executing the operation.
        /// </returns>
        public abstract object GetExecutionResultObjectValue();

        /// <summary>
        /// Gets the declared type of the execution result.
        /// </summary>
        /// <returns>
        /// The declared type of the execution result.
        /// </returns>
        public abstract Type GetExecutionResultType();
    }
}