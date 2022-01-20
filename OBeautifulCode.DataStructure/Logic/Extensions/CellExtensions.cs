// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Linq;

    using OBeautifulCode.Type.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="ICell"/> and inheritors.
    /// </summary>
    public static class CellExtensions
    {
        /// <summary>
        /// Gets the status of the execution of an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>
        /// The status of the execution of an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.
        /// </returns>
        public static CellOpExecutionStatus GetCellOpExecutionStatus(
            this IOperationOutputCell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            CellOpExecutionStatus result;

            var lastCellOpExecutionEvent = cell.OperationExecutionEvents?.LastOrDefault();

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
            else if (lastCellOpExecutionEvent is CellOpExecutionDeemedNotApplicableEvent)
            {
                result = CellOpExecutionStatus.DeemedNotApplicable;
            }
            else if (lastCellOpExecutionEvent is CellOpExecutionFailedEvent)
            {
                result = CellOpExecutionStatus.Failed;
            }
            else if (lastCellOpExecutionEvent.GetType().GetGenericTypeDefinitionOrSpecifiedType() == typeof(CellOpExecutionCompletedEvent<>))
            {
                result = CellOpExecutionStatus.Completed;
            }
            else
            {
                throw new InvalidOperationException(Invariant($"Cannot determine the {nameof(CellOpExecutionStatus)} of the specified cell."));
            }

            return result;
        }

        /// <summary>
        /// Gets the outcome of executing an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>
        /// The outcome of executing an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.
        /// </returns>
        public static CellOpExecutionOutcome GetCellOpExecutionOutcome(
            this IOperationOutputCell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            var executionStatus = cell.GetCellOpExecutionStatus();

            switch (executionStatus)
            {
                case CellOpExecutionStatus.DeemedNotApplicable:
                    return CellOpExecutionOutcome.NotApplicable;
                case CellOpExecutionStatus.Aborted:
                    return CellOpExecutionOutcome.Aborted;
                case CellOpExecutionStatus.Failed:
                    return CellOpExecutionOutcome.Failed;
                case CellOpExecutionStatus.Completed:
                    return CellOpExecutionOutcome.Completed;
                default:
                    return CellOpExecutionOutcome.Unknown;
            }
        }

        /// <summary>
        /// Gets the validation status of a specified cell.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>
        /// The validation status of the specified cell.
        /// </returns>
        public static ValidationStatus GetValidationStatus(
            this IValidationCell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            ValidationStatus result;

            var lastValidationEvent = cell.ValidationEvents?.LastOrDefault();

            if (cell.Validation == null)
            {
                result = ValidationStatus.ValidationMissing;
            }
            else if (lastValidationEvent == null)
            {
                result = ValidationStatus.Unvalidated;
            }
            else if (lastValidationEvent is CellValidationClearedEvent)
            {
                result = ValidationStatus.Unvalidated;
            }
            else if (lastValidationEvent is CellValidationDeemedNotApplicableEvent)
            {
                result = ValidationStatus.DeemedNotApplicable;
            }
            else if (lastValidationEvent is CellValidationAbortedEvent)
            {
                result = ValidationStatus.Aborted;
            }
            else if (lastValidationEvent is CellValidationDeterminedCellValidEvent)
            {
                result = ValidationStatus.DeterminedSubjectIsValid;
            }
            else if (lastValidationEvent is CellValidationFailedEvent)
            {
                result = ValidationStatus.Failed;
            }
            else if (lastValidationEvent is CellValidationDeterminedCellInvalidEvent)
            {
                result = ValidationStatus.DeterminedSubjectIsInvalid;
            }
            else
            {
                throw new InvalidOperationException(Invariant($"Cannot determine the {nameof(ValidationStatus)} of the specified cell."));
            }

            return result;
        }

        /// <summary>
        /// Gets the validation message for a specified cell or null if none exists.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>
        /// The validation message for the specified cell or null if none exists.
        /// </returns>
        public static string GetValidationMessageOrNull(
            this IValidationCell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            string result;

            var lastValidationEvent = cell.ValidationEvents?.LastOrDefault();

            if (lastValidationEvent == null)
            {
                result = null;
            }
            else if (lastValidationEvent is IHaveMessage haveMessageEvent)
            {
                result = haveMessageEvent.Message;
            }
            else
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Gets the validity of a specified cell.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>
        /// The validity of the specified cell.
        /// </returns>
        public static Validity GetValidity(
            this IValidationCell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            var validationStatus = cell.GetValidationStatus();

            switch (validationStatus)
            {
                case ValidationStatus.ValidationMissing:
                case ValidationStatus.DeterminedSubjectIsValid:
                    return Validity.Valid;
                case ValidationStatus.DeemedNotApplicable:
                    return Validity.NotApplicable;
                case ValidationStatus.DeterminedSubjectIsInvalid:
                    return Validity.Invalid;
                case ValidationStatus.Aborted:
                    return Validity.Aborted;
                default:
                    return Validity.Unknown;
            }
        }

        /// <summary>
        /// Gets the availability check status of a specified cell.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>
        /// The availability check status of the specified cell.
        /// </returns>
        public static AvailabilityCheckStatus GetAvailabilityCheckStatus(
            this IAvailabilityCheckCell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            AvailabilityCheckStatus result;

            var lastAvailabilityCheckEvent = cell.AvailabilityCheckEvents?.LastOrDefault();

            if (cell.AvailabilityCheck == null)
            {
                result = AvailabilityCheckStatus.AvailabilityCheckMissing;
            }
            else if (lastAvailabilityCheckEvent == null)
            {
                result = AvailabilityCheckStatus.Unchecked;
            }
            else if (lastAvailabilityCheckEvent is CellAvailabilityCheckClearedEvent)
            {
                result = AvailabilityCheckStatus.Unchecked;
            }
            else if (lastAvailabilityCheckEvent is CellAvailabilityCheckDeterminedCellEnabledEvent)
            {
                result = AvailabilityCheckStatus.DeterminedSubjectIsEnabled;
            }
            else if (lastAvailabilityCheckEvent is CellAvailabilityCheckDeterminedCellDisabledEvent)
            {
                result = AvailabilityCheckStatus.DeterminedSubjectIsDisabled;
            }
            else if (lastAvailabilityCheckEvent is CellAvailabilityCheckFailedEvent)
            {
                result = AvailabilityCheckStatus.Failed;
            }
            else
            {
                throw new InvalidOperationException(Invariant($"Cannot determine the {nameof(AvailabilityCheckStatus)} of the specified cell."));
            }

            return result;
        }

        /// <summary>
        /// Gets the availability check message for a specified cell or null if none exists.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>
        /// The availability check message for the specified cell or null if none exists.
        /// </returns>
        public static string GetAvailabilityCheckMessageOrNull(
            this IAvailabilityCheckCell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            string result;

            var lastAvailabilityCheckEvent = cell.AvailabilityCheckEvents?.LastOrDefault();

            if (lastAvailabilityCheckEvent == null)
            {
                result = null;
            }
            else if (lastAvailabilityCheckEvent is IHaveMessage haveMessageEvent)
            {
                result = haveMessageEvent.Message;
            }
            else
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Gets the availability of a specified cell.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>
        /// The availability of the specified cell.
        /// </returns>
        public static Availability GetAvailability(
            this IAvailabilityCheckCell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            var availabilityCheckStatus = cell.GetAvailabilityCheckStatus();

            switch (availabilityCheckStatus)
            {
                case AvailabilityCheckStatus.AvailabilityCheckMissing:
                case AvailabilityCheckStatus.Unchecked:
                    return cell.DefaultAvailability;
                case AvailabilityCheckStatus.DeterminedSubjectIsEnabled:
                    return Availability.Enabled;
                case AvailabilityCheckStatus.DeterminedSubjectIsDisabled:
                    return Availability.Disabled;
                default:
                    return Availability.Unknown;
            }
        }

        /// <summary>
        /// Creates a single-cell <see cref="FlatRow"/>.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="id">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// The single-cell <see cref="FlatRow"/>.
        /// </returns>
        public static FlatRow ToFlatRow(
            this ICell cell,
            string id = null,
            RowFormat format = null)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            var result = new FlatRow(new[] { cell }, id, format);

            return result;
        }

        /// <summary>
        /// Creates a single-cell <see cref="Row"/>.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="id">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// The single-cell <see cref="Row"/>.
        /// </returns>
        public static Row ToRow(
            this ICell cell,
            string id = null,
            RowFormat format = null)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            var result = new Row(new[] { cell }, id, format);

            return result;
        }
    }
}
