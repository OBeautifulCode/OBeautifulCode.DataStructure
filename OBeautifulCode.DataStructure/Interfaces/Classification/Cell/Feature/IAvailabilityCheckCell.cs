// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAvailabilityCheckCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
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
    }
}
