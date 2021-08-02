// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Linq;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="ICell"/> and inheritors.
    /// </summary>
    public static class CellExtensions
    {
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

            var lastCellOpExecutionEvent = cell.CellOpExecutionEvents?.LastOrDefault();

            if (lastCellOpExecutionEvent == null)
            {
                result = CellOpExecutionStatus.NotExecuted;
            }
            else if (lastCellOpExecutionEvent is CellOpExecutionClearedEvent)
            {
                result = CellOpExecutionStatus.NotExecuted;
            }
            else if (lastCellOpExecutionEvent is CellOpExecutionAbortedEvent)
            {
                result = CellOpExecutionStatus.Aborted;
            }
            else if (lastCellOpExecutionEvent is CellOpExecutionCompletedEvent<TValue>)
            {
                result = CellOpExecutionStatus.Completed;
            }
            else if (lastCellOpExecutionEvent is CellOpExecutionDeemedNotApplicableEvent)
            {
                result = CellOpExecutionStatus.DeemedNotApplicable;
            }
            else if (lastCellOpExecutionEvent is CellOpExecutionFailedEvent)
            {
                result = CellOpExecutionStatus.Failed;
            }
            else
            {
                throw new InvalidOperationException(Invariant($"Cannot determine the {nameof(CellOpExecutionStatus)} of the specified cell."));
            }

            return result;
        }

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

            var lastCellValidationEvent = cell.CellValidationEvents?.LastOrDefault();

            if (cell.ValidationConditions == null)
            {
                result = ValidationStatus.Unconditioned;
            }
            else if (lastCellValidationEvent == null)
            {
                result = ValidationStatus.Unvalidated;
            }
            else if (lastCellValidationEvent is CellValidationClearedEvent)
            {
                result = ValidationStatus.Unvalidated;
            }
            else if (lastCellValidationEvent is CellValidationDeemedNotApplicableEvent)
            {
                result = ValidationStatus.DeemedNotApplicable;
            }
            else if (lastCellValidationEvent is CellValidationAbortedEvent)
            {
                result = ValidationStatus.Aborted;
            }
            else if (lastCellValidationEvent is CellValidationCompletedEvent)
            {
                result = ValidationStatus.Valid;
            }
            else if (lastCellValidationEvent is CellValidationFailedEvent)
            {
                result = ValidationStatus.Failed;
            }
            else if (lastCellValidationEvent is CellValidationConditionUnmetEvent)
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
