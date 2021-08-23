// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellAvailabilityCheckDeterminedCellEnabledEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The availability check on a cell determined that the cell is enabled.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellAvailabilityCheckDeterminedCellEnabledEvent : CellAvailabilityCheckEventBase, IHaveMessage, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellAvailabilityCheckDeterminedCellEnabledEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the availability check that determined that the cell is enabled.</param>
        /// <param name="message">The message to emit about the enabled cell.</param>
        public CellAvailabilityCheckDeterminedCellEnabledEvent(
            DateTime timestampUtc,
            string details,
            string message)
            : base(timestampUtc, details)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message to emit about the enabled cell.
        /// </summary>
        public string Message { get; private set; }
    }
}