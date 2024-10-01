// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellValidationDeterminedCellInvalidEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The validation of a cell determined that the cell is invalid.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellValidationDeterminedCellInvalidEvent : CellValidationEventBase, IHaveMessage, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValidationDeterminedCellInvalidEvent"/> class.
        /// </summary>
        /// <param name="message">The message to emit about the invalid cell.</param>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">OPTIONAL details about the validation that determined that the cell is invalid.  DEFAULT is no details.</param>
        public CellValidationDeterminedCellInvalidEvent(
            string message,
            DateTime timestampUtc,
            string details = null)
            : base(timestampUtc, details)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message to emit about the invalid cell.
        /// </summary>
        public string Message { get; private set; }
    }
}