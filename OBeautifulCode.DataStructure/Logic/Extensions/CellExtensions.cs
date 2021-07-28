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
                result = ValidationStatus.Unconditioned;
            }
            else if (cell.CellValidationEvent == null)
            {
                result = ValidationStatus.Unvalidated;
            }
            else if (cell.CellValidationEvent is CellValidationDeemedNotApplicableEvent)
            {
                result = ValidationStatus.DeemedNotApplicable;
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
            else if (cell.CellValidationEvent is CellValidationConditionUnmetEvent)
            {
                result = ValidationStatus.Invalid;
            }
            else
            {
                throw new InvalidOperationException(Invariant($"Cannot determine the {nameof(ValidationStatus)} of the specified cell."));
            }

            return result;
        }

        /// <summary>
        /// Gets the status of the execution of an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="cell">The cell.</param>
        /// <returns>
        /// The status of the execution of an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.
        /// </returns>
        public static CellOpExecutionStatus GetExecutionStatus<TValue>(
            this IOperationOutputCell<TValue> cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            CellOpExecutionStatus result;

            if (cell.CellOpExecutionEvent == null)
            {
                result = CellOpExecutionStatus.NotExecuted;
            }
            else if (cell.CellOpExecutionEvent is CellOpExecutionAbortedEvent)
            {
                result = CellOpExecutionStatus.Aborted;
            }
            else if (cell.CellOpExecutionEvent is CellOpExecutionCompletedEvent<TValue>)
            {
                result = CellOpExecutionStatus.Completed;
            }
            else if (cell.CellOpExecutionEvent is CellOpExecutionDeemedNotApplicableEvent)
            {
                result = CellOpExecutionStatus.DeemedNotApplicable;
            }
            else if (cell.CellOpExecutionEvent is CellOpExecutionFailedEvent)
            {
                result = CellOpExecutionStatus.Failed;
            }
            else
            {
                throw new InvalidOperationException(Invariant($"Cannot determine the {nameof(CellOpExecutionStatus)} of the specified cell."));
            }

            return result;
        }
    }
}
