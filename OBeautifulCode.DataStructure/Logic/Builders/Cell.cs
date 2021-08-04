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
        /// Builds an operation that determines whether a cell has a value.
        /// </summary>
        /// <param name="sectionId">The id of the section that contains the cell.</param>
        /// <param name="cellId">The id of the cell.</param>
        /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
        /// <param name="slotSelectionStrategy">OPTIONAL strategy to use to select a slot if addressing an <see cref="ISlottedCell"/>.  DEFAULT is to throw if addressing an <see cref="ISlottedCell"/> -AND- <paramref name="slotId"/> is not specified.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static HasCellValueOp HasValue(
            string sectionId,
            string cellId,
            string slotId = null,
            SlotSelectionStrategy slotSelectionStrategy = SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
        {
            var cellLocator = new CellLocator(sectionId, cellId, slotId, slotSelectionStrategy);

            var result = new HasCellValueOp(cellLocator);

            return result;
        }

        /// <summary>
        /// Builds an operation that gets the value of a cell.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="sectionId">The id of the section that contains the cell.</param>
        /// <param name="cellId">The id of the cell.</param>
        /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
        /// <param name="slotSelectionStrategy">OPTIONAL strategy to use to select a slot if addressing an <see cref="ISlottedCell"/>.  DEFAULT is to throw if addressing an <see cref="ISlottedCell"/> -AND- <paramref name="slotId"/> is not specified.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static GetCellValueOp<TValue> GetValue<TValue>(
            string sectionId,
            string cellId,
            string slotId = null,
            SlotSelectionStrategy slotSelectionStrategy = SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
        {
            var cellLocator = new CellLocator(sectionId, cellId, slotId, slotSelectionStrategy);

            var result = new GetCellValueOp<TValue>(cellLocator);

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
        /// <param name="validationEvents">OPTIONAL events that record the validation of this cell.  DEFAULT is a cell that has not yet been validated.</param>
        /// <param name="operationExecutionEvents">OPTIONAL events that record the execution of <paramref name="operation"/>.  DEFAULT is a cell who's operation has not yet been executed.</param>
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
            IReadOnlyList<CellValidationEventBase> validationEvents = null,
            IReadOnlyList<CellOpExecutionEventBase> operationExecutionEvents = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
        {
            var result = new OperationCell<TValue>(operation, id, columnsSpanned, details, validation, validationEvents, operationExecutionEvents, valueFormat, format, hoverOver, link);

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
        /// <param name="validation">OPTIONAL validation to perform.  DEFAULT is no validation.</param>
        /// <param name="validationEvents">OPTIONAL events that record the validation of this cell.  DEFAULT is a cell that has not yet been validated.</param>
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
            Validation validation = null,
            IReadOnlyList<CellValidationEventBase> validationEvents = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
        {
            var result = new ConstCell<TValue>(value, id, columnsSpanned, details, validation, validationEvents, valueFormat, format, hoverOver, link);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="IConstOutputCell{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="validation">OPTIONAL validation to perform.  DEFAULT is no validation.</param>
        /// <param name="validationEvents">OPTIONAL events that record the validation of this cell.  DEFAULT is a cell that has not yet been validated.</param>
        /// <param name="inputEvents">OPTIONAL events that record the manipulation of this cell's value.  DEFAULT is a cell with no inputted value.</param>
        /// <param name="valueFormat">OPTIONAL format to apply to the cell value.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public static InputCell<TValue> CreateInput<TValue>(
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            Validation validation = null,
            IReadOnlyList<CellValidationEventBase> validationEvents = null,
            IReadOnlyList<CellInputEventBase> inputEvents = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null)
        {
            var result = new InputCell<TValue>(id, columnsSpanned, details, validation, validationEvents, inputEvents, valueFormat, format, hoverOver);

            return result;
        }

        /// <summary>
        /// Builds a <see cref="Validation"/>.
        /// </summary>
        /// <param name="operation">The operation to execute to get the validity of the subject.</param>
        /// <param name="details">OPTIONAL details about this validation.  DEFAULT is to omit any details.</param>
        /// <returns>
        /// The validation.
        /// </returns>
        public static Validation CreateValidation(
            IReturningOperation<ValidationResult> operation,
            string details = null)
        {
            var result = new Validation(operation, details);

            return result;
        }
    }
}
