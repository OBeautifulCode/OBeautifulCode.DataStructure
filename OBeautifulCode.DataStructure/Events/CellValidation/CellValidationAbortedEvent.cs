// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellValidationAbortedEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The validation of a cell was aborted.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellValidationAbortedEvent : CellValidationEventBase, IHaveMessage, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValidationAbortedEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the aborted validation.</param>
        /// <param name="message">The message to emit about the aborted validation.</param>
        public CellValidationAbortedEvent(
            DateTime timestampUtc,
            string details,
            string message)
            : base(timestampUtc, details)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message to emit about determination that the validation is not applicable.
        /// </summary>
        public string Message { get; private set; }
    }
}