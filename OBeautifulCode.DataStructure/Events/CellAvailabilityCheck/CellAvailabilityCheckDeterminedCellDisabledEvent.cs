// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellAvailabilityCheckDeterminedCellDisabledEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The availability check on a cell determined that the cell is disabled.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellAvailabilityCheckDeterminedCellDisabledEvent : CellAvailabilityCheckEventBase, IHaveMessage, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellAvailabilityCheckDeterminedCellDisabledEvent"/> class.
        /// </summary>
        /// <param name="message">The message to emit about the disabled cell.</param>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">OPTIONAL details about the availability check that determined that the cell is disabled.  DEFAULT is no details.</param>
        public CellAvailabilityCheckDeterminedCellDisabledEvent(
            string message,
            DateTime timestampUtc,
            string details = null)
            : base(timestampUtc, details)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message to emit about the disabled cell.
        /// </summary>
        public string Message { get; private set; }
    }
}