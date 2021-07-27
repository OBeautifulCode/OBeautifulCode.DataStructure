// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="ICell"/> and inheritors.
    /// </summary>
    public static class CellExtensions
    {
        /// <summary>
        /// Gets the validation status of a specified cell.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>
        /// The validation status of the specified cell.
        /// </returns>
        public static ValidationStatus GetValidationStatus(
            this IValidateableCell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            ValidationStatus result;

            if (cell.ValidationConditions == null)
            {
                result = ValidationStatus.NoConditions;
            }
            else if (cell.CellValidationEvent == null)
            {
                result = ValidationStatus.NotValidated;
            }
            else if (cell.CellValidationEvent is CellValidationAbortedEvent)
            {
                result = ValidationStatus.Aborted;
            }
            else if (cell.CellValidationEvent is CellValidationCompletedEvent)
            {
                result = ValidationStatus.Valid;
            }
            else if (cell.CellValidationEvent is CellValidationFailedEvent)
            {
                result = ValidationStatus.Failed;
            }
            else if (cell.CellValidationEvent is CellValidationFailedToMeetConditionEvent)
            {
                result = ValidationStatus.Invalid;
            }
            else
            {
                throw new InvalidOperationException(Invariant($"Cannot determine the {nameof(ValidationStatus)} of the specified cell."));
            }

            return result;
        }
    }
}
