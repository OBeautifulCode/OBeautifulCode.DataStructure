// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellAvailabilityCheckFailedEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// The availability check on a cell failed; an exception was thrown.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellAvailabilityCheckFailedEvent : CellAvailabilityCheckEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellAvailabilityCheckFailedEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the failed availability check.</param>
        public CellAvailabilityCheckFailedEvent(
            DateTime timestampUtc,
            string details)
            : base(timestampUtc, details)
        {
            if (details == null)
            {
                throw new ArgumentNullException(nameof(details));
            }

            if (string.IsNullOrWhiteSpace(details))
            {
                throw new ArgumentException(Invariant($"{nameof(details)} is white space."));
            }
        }
    }
}