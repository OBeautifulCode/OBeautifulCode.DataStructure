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
    public partial class CellValidationDeterminedCellInvalidEvent : CellValidationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValidationDeterminedCellInvalidEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the validation that determined that the cell is invalid.</param>
        /// <param name="message">The message to emit about the invalid cell.</param>
        public CellValidationDeterminedCellInvalidEvent(
            DateTime timestampUtc,
            string details,
            string message)
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