// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellValidationConditionUnmetEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// A validation condition failed to be met.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellValidationConditionUnmetEvent : CellValidationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValidationConditionUnmetEvent"/> class.
        /// </summary>
        /// <param name="failureMessage">A message about the failure to meet the condition.</param>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the condition that failed to be met.</param>
        public CellValidationConditionUnmetEvent(
            string failureMessage,
            DateTime timestampUtc,
            string details)
            : base(timestampUtc, details)
        {
            this.FailureMessage = failureMessage;
        }

        /// <summary>
        /// Gets a message about the failure to meet the condition.
        /// </summary>
        public string FailureMessage { get; private set; }
    }
}