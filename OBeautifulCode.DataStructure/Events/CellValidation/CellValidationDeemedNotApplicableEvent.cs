// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellValidationDeemedNotApplicableEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The validation of a cell was deemed not applicable.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellValidationDeemedNotApplicableEvent : CellValidationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValidationDeemedNotApplicableEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the validation that is not applicable.</param>
        /// <param name="message">The message to emit about determination that the validation is not applicable.</param>
        public CellValidationDeemedNotApplicableEvent(
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