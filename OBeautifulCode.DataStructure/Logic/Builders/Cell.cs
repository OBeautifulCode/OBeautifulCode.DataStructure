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
        /// <param name="cellOpExecutionEvents">OPTIONAL results of executing the operation.  DEFAULT is a cell who's operation has not yet been executed.</param>
        /// <param name="validationConditions">OPTIONAL list of conditions that determine the validity of the cell's value.  DEFAULT is to omit validation.</param>
        /// <param name="cellValidationEvent">OPTIONAL result of validating the cell's value.  DEFAULT is a cell that has not yet been validated.</param>
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
        public static OperationCell<TValue> CreateOp<TValue>(
            IReturningOperation<TValue> operation,
            IReadOnlyList<CellOpExecutionEventBase> cellOpExecutionEvents = null,
            ValidationConditions validationConditions = null,
            CellValidationEventBase cellValidationEvent = null,
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
        {
            var result = new OperationCell<TValue>(operation, cellOpExecutionEvents, validationConditions, cellValidationEvent, id, columnsSpanned, details, valueFormat, format, hoverOver, link);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="IConstOutputCell{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="value">The value.</param>
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
            var result = new ConstCell<TValue>(value, id, columnsSpanned, details, valueFormat, format, hoverOver, link);

            return result;
        }

        /// <summary>
        /// Builds an <see cref="IConstOutputCell{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="cellInputEvents">OPTIONAL events that track the manipulation of this cell's value.  DEFAULT is a cell with no inputted value.</param>
        /// <param name="validationConditions">OPTIONAL list of conditions that determine the validity of the cell's value.  DEFAULT is to omit validation.</param>
        /// <param name="cellValidationEvent">OPTIONAL result of validating the cell's value.  DEFAULT is a cell that has not yet been validated.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="valueFormat">OPTIONAL format to apply to the cell value.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public static InputCell<TValue> CreateInput<TValue>(
            IReadOnlyList<CellInputEventBase> cellInputEvents = null,
            ValidationConditions validationConditions = null,
            CellValidationEventBase cellValidationEvent = null,
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null)
        {
            var result = new InputCell<TValue>(cellInputEvents, validationConditions, cellValidationEvent, id, columnsSpanned, details, valueFormat, format, hoverOver);

            return result;
        }
    }
}
