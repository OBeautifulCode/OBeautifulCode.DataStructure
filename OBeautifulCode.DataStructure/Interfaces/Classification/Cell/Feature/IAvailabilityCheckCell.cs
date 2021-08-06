// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAvailabilityCheckCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A cell that can check it's availability.
    /// </summary>
    public interface IAvailabilityCheckCell : ICell
    {
        /// <summary>
        /// Gets the initial availability of the cell (before <see cref="AvailabilityCheck"/> is run).
        /// </summary>
        Availability InitialAvailability { get; }

        /// <summary>
        /// Gets an object that determines the availability of the cell.
        /// </summary>
        AvailabilityCheck AvailabilityCheck { get; }

        /// <summary>
        /// Gets the events that record the availability checks performed on this cell.
        /// </summary>
        IReadOnlyList<CellAvailabilityCheckEventBase> AvailabilityCheckEvents { get; }

        /// <summary>
        /// Records a <see cref="CellAvailabilityCheckEventBase"/>.
        /// </summary>
        /// <param name="availabilityCheckEvent">The event to record.</param>
        void Record(CellAvailabilityCheckEventBase availabilityCheckEvent);

        /// <summary>
        /// Clears any availability check that has been performed.
        /// </summary>
        /// <param name="timestampUtc">The timestamp (in UTC) to use on the event that records the clearing-out of a cell's availability check.</param>
        /// <param name="details">OPTIONAL details related to clearing the cell's availability check.  DEFAULT is to omit any details.</param>
        void ClearAvailabilityCheck(DateTime timestampUtc, string details);
    }
}
