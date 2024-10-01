// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellValidationClearedEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The validation of a cell was cleared-out.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellValidationClearedEvent : CellValidationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValidationClearedEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">OPTIONAL details about the cleared-out validation.  DEFAULT is no details.</param>
        public CellValidationClearedEvent(
            DateTime timestampUtc,
            string details = null)
            : base(timestampUtc, details)
        {
        }
    }
}