// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellAvailabilityEventBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Base class for events that record the availability (enabled/disabled) of a cell.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class CellAvailabilityEventBase : EventBase, IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellAvailabilityEventBase"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the event.</param>
        protected CellAvailabilityEventBase(
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