// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidateCellValue.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    /// <summary>
    /// Validates a cell's value.
    /// </summary>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IValidateCellValue
    {
        /// <summary>
        /// Records a <see cref="CellValidationEventBase"/>.
        /// </summary>
        /// <param name="cellValidationEvent">The result of validating the cell.</param>
        void Record(CellValidationEventBase cellValidationEvent);

        /// <summary>
        /// Clears any validation that has been performed.
        /// </summary>
        /// <param name="timestampUtc">The timestamp (in UTC) to use on the event that records the clearing-out of a cell's validation.</param>
        /// <param name="details">OPTIONAL details related to clearing the cell's validation.  DEFAULT is to omit any details.</param>
        void ClearValidation(DateTime timestampUtc, string details);
    }
}
