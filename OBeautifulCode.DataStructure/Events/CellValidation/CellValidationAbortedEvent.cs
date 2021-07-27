// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellValidationAbortedEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// A validation condition aborted the validation.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellValidationAbortedEvent : CellValidationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValidationAbortedEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the condition that aborted the validation.</param>
        public CellValidationAbortedEvent(
            DateTime timestampUtc,
            string details)
            : base(timestampUtc, details)
        {
        }
    }
}