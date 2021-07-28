// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellValidationDeemedNotApplicableEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// A validation condition deemed the validation not applicable.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellValidationDeemedNotApplicableEvent : CellValidationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValidationDeemedNotApplicableEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the condition that aborted the validation.</param>
        public CellValidationDeemedNotApplicableEvent(
            DateTime timestampUtc,
            string details)
            : base(timestampUtc, details)
        {
        }
    }
}