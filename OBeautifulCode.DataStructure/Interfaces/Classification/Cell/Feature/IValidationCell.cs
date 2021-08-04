// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidationCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A cell having validation.
    /// </summary>
    public interface IValidationCell : ICell
    {
        /// <summary>
        /// Gets the validation to perform.
        /// </summary>
        Validation Validation { get; }

        /// <summary>
        /// Gets the events that record the validation of this cell.
        /// </summary>
        IReadOnlyList<CellValidationEventBase> ValidationEvents { get; }

        /// <summary>
        /// Records a <see cref="CellValidationEventBase"/>.
        /// </summary>
        /// <param name="validationEvent">The result of validating the cell.</param>
        void Record(CellValidationEventBase validationEvent);

        /// <summary>
        /// Clears any validation that has been performed.
        /// </summary>
        /// <param name="timestampUtc">The timestamp (in UTC) to use on the event that records the clearing-out of a cell's validation.</param>
        /// <param name="details">OPTIONAL details related to clearing the cell's validation.  DEFAULT is to omit any details.</param>
        void ClearValidation(DateTime timestampUtc, string details);
    }
}
