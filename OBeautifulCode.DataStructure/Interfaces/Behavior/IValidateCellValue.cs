// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidateCellValue.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Validates a cell's value.
    /// </summary>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IValidateCellValue
    {
        /// <summary>
        /// Gets the cell's validation status.
        /// </summary>
        /// <returns>
        /// The validation status of the cell.
        /// </returns>
        ValidationStatus GetValidationStatus();

        /// <summary>
        /// Records validation that has been performed on a cell.
        /// </summary>
        /// <param name="cellValidationEvent">The result of validating the cell.</param>
        void RecordValidation(CellValidationEventBase cellValidationEvent);

        /// <summary>
        /// Clears any validation that has been performed.
        /// </summary>
        void ClearValidation();
    }
}
