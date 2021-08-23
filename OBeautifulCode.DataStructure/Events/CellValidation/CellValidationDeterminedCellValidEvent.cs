// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellValidationDeterminedCellValidEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The validation of a cell determined that the cell is valid.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellValidationDeterminedCellValidEvent : CellValidationEventBase, IHaveMessage, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValidationDeterminedCellValidEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the validation that determined that the cell is valid.</param>
        /// <param name="message">The message to emit about the valid cell.</param>
        public CellValidationDeterminedCellValidEvent(
            DateTime timestampUtc,
            string details,
            string message)
            : base(timestampUtc, details)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message to emit about the valid cell.
        /// </summary>
        public string Message { get; private set; }
    }
}