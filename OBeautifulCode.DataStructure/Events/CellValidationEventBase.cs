// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellValidationEventBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Base class for events that record validation of a cell.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class CellValidationEventBase : EventBase, IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValidationEventBase"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the event.</param>
        protected CellValidationEventBase(
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