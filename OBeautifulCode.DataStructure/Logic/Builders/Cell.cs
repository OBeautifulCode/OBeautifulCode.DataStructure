// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// Builder methods related to cells.
    /// </summary>
    public static class Cell
    {
        /// <summary>
        /// Builds a <see cref="ThisCellLocator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ThisCellLocator"/>.
        /// </returns>
        public static ThisCellLocator This()
        {
            var result = new ThisCellLocator();

            return result;
        }

        /// <summary>
        /// Builds a <see cref="SectionCellLocator"/>.
        /// </summary>
        /// <param name="cellId">The id of the cell.</param>
        /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
        /// <param name="slotSelectionStrategy">OPTIONAL strategy to use to select a slot if addressing an <see cref="ISlottedCell"/>.  DEFAULT is to throw if addressing an <see cref="ISlottedCell"/> -AND- <paramref name="slotId"/> is not specified.</param>
        /// <returns>
        /// A <see cref="SectionCellLocator"/>.
        /// </returns>
        public static SectionCellLocator InThisSection(
            string cellId,
            string slotId = null,
            SlotSelectionStrategy slotSelectionStrategy = SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
        {
            var result = new SectionCellLocator(cellId, slotId, slotSelectionStrategy);

            return result;
        }

        /// <summary>
        /// Builds a <see cref="ReportCellLocator"/>.
        /// </summary>
        /// <param name="sectionId">The id of the section that contains the cell.</param>
        /// <param name="cellId">The id of the cell.</param>
        /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
        /// <param name="slotSelectionStrategy">OPTIONAL strategy to use to select a slot if addressing an <see cref="ISlottedCell"/>.  DEFAULT is to throw if addressing an <see cref="ISlottedCell"/> -AND- <paramref name="slotId"/> is not specified.</param>
        /// <returns>
        /// A <see cref="ReportCellLocator"/>.
        /// </returns>
        public static ReportCellLocator InThisReport(
            string sectionId,
            string cellId,
            string slotId = null,
            SlotSelectionStrategy slotSelectionStrategy = SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
        {
            var result = new ReportCellLocator(sectionId, cellId, slotId, slotSelectionStrategy);

            return result;
        }

        /// <summary>
        /// Builds an operation that determines whether a cell has a value.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static HasCellValueOp HasValue(
            this CellLocatorBase cellLocator)
        {
            var result = new HasCellValueOp(Op.Const(cellLocator));

            return result;
        }

        /// <summary>
        /// Builds an operation that gets the value of a cell.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="cellLocator">The cell locator.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static GetCellValueOp<TValue> GetValue<TValue>(
            this CellLocatorBase cellLocator)
        {
            var result = new GetCellValueOp<TValue>(Op.Const(cellLocator));

            return result;
        }

        /// <summary>
        /// Builds an <see cref="GetCellOpExecutionOutcomeOp"/>.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static GetCellOpExecutionOutcomeOp GetOpExecutionOutcome(
            this CellLocatorBase cellLocator)
        {
            var result = new GetCellOpExecutionOutcomeOp(Op.Const(cellLocator));

            return result;
        }

        /// <summary>
        /// Builds an <see cref="GetValidityOp"/>.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static GetValidityOp GetValidity(
            this CellLocatorBase cellLocator)
        {
            var result = new GetValidityOp(Op.Const(cellLocator));

            return result;
        }

        /// <summary>
        /// Builds an <see cref="GetAvailabilityOp"/>.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static GetAvailabilityOp GetAvailability(
            this CellLocatorBase cellLocator)
        {
            var result = new GetAvailabilityOp(Op.Const(cellLocator));

            return result;
        }

        /// <summary>
        /// Builds an <see cref="IOperationOutputCell{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="operation">The operation.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="validation">OPTIONAL validation to perform.  DEFAULT is no validation.</param>
        /// <param name="valueFormat">OPTIONAL format to apply to the cell value.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public static OperationCell<TValue> CreateOp<TValue>(
            IReturningOperation<TValue> operation,
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            Validation validation = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
        {
            var result = new OperationCell<TValue>(operation, id, columnsSpanned, details, validation, null, Availability.Enabled, null, null, null, valueFormat, format, hoverOver, link);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="IConstOutputCell{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="value">The cell's value.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="valueFormat">OPTIONAL format to apply to the cell value.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public static ConstCell<TValue> CreateConst<TValue>(
            TValue value,
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
        {
            var result = new ConstCell<TValue>(value, id, columnsSpanned, details, null, null, Availability.Enabled, null, null, valueFormat, format, hoverOver, link);

            return result;
        }

        /// <summary>
        /// Builds an enabled <see cref="IConstOutputCell{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="validation">OPTIONAL validation to perform.  DEFAULT is no validation.</param>
        /// <param name="availabilityCheck">OPTIONAL availability check to perform.  DEFAULT is a cell with no availability check.</param>
        /// <param name="valueFormat">OPTIONAL format to apply to the cell value.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public static InputCell<TValue> CreateEnabledInput<TValue>(
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            Validation validation = null,
            AvailabilityCheck availabilityCheck = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null)
        {
            var result = new InputCell<TValue>(id, columnsSpanned, details, validation, null, Availability.Enabled, availabilityCheck, null, null, valueFormat, format, hoverOver);

            return result;
        }

        /// <summary>
        /// Builds a disabled <see cref="IConstOutputCell{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="validation">OPTIONAL validation to perform.  DEFAULT is no validation.</param>
        /// <param name="availabilityCheck">OPTIONAL availability check to perform.  DEFAULT is a cell with no availability check.</param>
        /// <param name="valueFormat">OPTIONAL format to apply to the cell value.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public static InputCell<TValue> CreateDisabledInput<TValue>(
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            Validation validation = null,
            AvailabilityCheck availabilityCheck = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null)
        {
            var result = new InputCell<TValue>(id, columnsSpanned, details, validation, null, Availability.Disabled, availabilityCheck, null, null, valueFormat, format, hoverOver);

            return result;
        }

        /// <summary>
        /// Builds a <see cref="Validation"/>.
        /// </summary>
        /// <param name="operation">The operation to execute to get the validity of the subject.</param>
        /// <param name="messageFormatKind">OPTIONAL kind of format to apply to the message that is emitted about the validity of the subject.  DEFAULT is to omit this specification.</param>
        /// <param name="details">OPTIONAL details about this validation.  DEFAULT is to omit any details.</param>
        /// <returns>
        /// The validation.
        /// </returns>
        public static Validation CreateValidation(
            this IReturningOperation<ValidationResult> operation,
            MessageFormatKind? messageFormatKind = null,
            string details = null)
        {
            var result = new Validation(operation, messageFormatKind, details);

            return result;
        }

        /// <summary>
        /// Builds a <see cref="Validation"/>.
        /// </summary>
        /// <param name="steps">The individual validation steps/checks.</param>
        /// <param name="endMessageOp">OPTIONAL operation to execute to get the message that should be emitted when all <paramref name="steps"/> have been evaluated and none have stopped the validation (we've reached the end of the chain).  DEFAULT is to omit this message.</param>
        /// <param name="endValidity">OPTIONAL value that specifies the validity of the subject when all <paramref name="steps"/> have been evaluated and none have stopped the validation (we've reached the end of the chain).  DEFAULT is to determine that the subject is valid.</param>
        /// <param name="messageFormatKind">OPTIONAL kind of format to apply to the message that is emitted about the validity of the subject.  DEFAULT is to omit this specification.</param>
        /// <param name="details">OPTIONAL details about this validation.  DEFAULT is to omit any details.</param>
        /// <returns>
        /// The validation.
        /// </returns>
        public static Validation CreateValidation(
            IReadOnlyList<ValidationStep> steps,
            IReturningOperation<string> endMessageOp = null,
            Validity endValidity = Validity.Valid,
            MessageFormatKind? messageFormatKind = null,
            string details = null)
        {
            var operation = steps.Validate(endMessageOp, endValidity);

            var result = operation.CreateValidation(messageFormatKind, details);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="AvailabilityCheck"/>.
        /// </summary>
        /// <param name="operation">The operation to execute to check the availability of the subject.</param>
        /// <param name="messageFormatKind">OPTIONAL kind of format to apply to the message that is emitted about the availability of the subject.  DEFAULT is to omit this specification.</param>
        /// <param name="details">OPTIONAL details about this availability check.  DEFAULT is to omit any details.</param>
        /// <returns>
        /// The validation.
        /// </returns>
        public static AvailabilityCheck CreateAvailabilityCheck(
            this IReturningOperation<AvailabilityCheckResult> operation,
            MessageFormatKind? messageFormatKind = null,
            string details = null)
        {
            var result = new AvailabilityCheck(operation, messageFormatKind, details);

            return result;
        }

        /// <summary>
        /// Builds a <see cref="AvailabilityCheck"/>.
        /// </summary>
        /// <param name="steps">The individual availability check steps.</param>
        /// <param name="endMessageOp">OPTIONAL operation to execute to get the message that should be emitted when all <paramref name="steps"/> have been evaluated and none have stopped the availability check (we've reached the end of the chain).  DEFAULT is to omit this message.</param>
        /// <param name="endAvailability">OPTIONAL value that specifies the availability of the subject when all <paramref name="steps"/> have been evaluated and none have stopped the availability check (we've reached the end of the chain).  DEFAULT is to determine that the subject is enabled.</param>
        /// <param name="messageFormatKind">OPTIONAL kind of format to apply to the message that is emitted about the availability of the subject.  DEFAULT is to omit this specification.</param>
        /// <param name="details">OPTIONAL details about this availability check.  DEFAULT is to omit any details.</param>
        /// <returns>
        /// The validation.
        /// </returns>
        public static AvailabilityCheck CreateAvailabilityCheck(
            IReadOnlyList<AvailabilityCheckStep> steps,
            IReturningOperation<string> endMessageOp = null,
            Availability endAvailability = Availability.Enabled,
            MessageFormatKind? messageFormatKind = null,
            string details = null)
        {
            var operation = steps.CheckAvailability(endMessageOp, endAvailability);

            var result = operation.CreateAvailabilityCheck(messageFormatKind, details);

            return result;
        }
    }
}
