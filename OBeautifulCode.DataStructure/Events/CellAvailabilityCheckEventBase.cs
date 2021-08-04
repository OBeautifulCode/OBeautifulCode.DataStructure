// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellAvailabilityCheckEventBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Base class for events that record an availability check on a cell.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class CellAvailabilityCheckEventBase : EventBase, IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellAvailabilityCheckEventBase"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the event.</param>
        protected CellAvailabilityCheckEventBase(
            DateTime timestampUtc,
            string details)
            : base(timestampUtc)
        {
            this.Details = details;
        }

        /// <inheritdoc />
        public string Details { get; private set; }
    }
}