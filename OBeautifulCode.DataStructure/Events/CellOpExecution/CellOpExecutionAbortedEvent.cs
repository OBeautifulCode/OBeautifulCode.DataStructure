// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellOpExecutionAbortedEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// The execution of an <see cref="IOperationOutputCell{TResult}"/>'s <see cref="IOperationOutputCell{TResult}.Operation"/> was aborted.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellOpExecutionAbortedEvent : CellOpExecutionEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellOpExecutionAbortedEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the aborted execution.</param>
        public CellOpExecutionAbortedEvent(
            DateTime timestampUtc,
            string details)
            : base(timestampUtc, details)
        {
            if (details == null)
            {
                throw new ArgumentNullException(nameof(details));
            }

            if (string.IsNullOrWhiteSpace(details))
            {
                throw new ArgumentException(Invariant($"{nameof(details)} is white space."));
            }
        }
    }
}