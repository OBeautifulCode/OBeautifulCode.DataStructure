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
    /// Validation performed on a cell has been cleared out.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellValidationClearedEvent : CellValidationEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValidationClearedEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the condition that aborted the validation.</param>
        public CellValidationClearedEvent(
            DateTime timestampUtc,
            string details)
            : base(timestampUtc, details)
        {
        }
    }
}