// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    using NamedDecimalSet = System.Collections.Generic.IReadOnlyList<OBeautifulCode.Type.NamedValue<decimal>>;

    /// <summary>
    /// Builder methods related to cells.
    /// </summary>
    public static class Cell
    {
        /// <summary>
        /// Builder methods to get cell properties.
        /// </summary>
        public static class Get
        {
            /// <summary>
            /// Builder methods to get the value of a cell.
            /// </summary>
            public static class ValueOf
            {
                /// <summary>
                /// Builds an operation that gets the value of a cell that contains a decimal named vector.
                /// </summary>
                /// <param name="sectionId">The id of the section that contains the cell.</param>
                /// <param name="cellId">The id of the cell.</param>
                /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
                /// <param name="slotSelectionStrategy">OPTIONAL strategy to use to select a slot if addressing an <see cref="ISlottedCell"/>.  DEFAULT is to throw if addressing an <see cref="ISlottedCell"/> -AND- <paramref name="slotId"/> is not specified.</param>
                /// <returns>
                /// The operation.
                /// </returns>
                public static GetCellValueOp<NamedDecimalSet> NamedDecimalSet(
                    string sectionId,
                    string cellId,
                    string slotId = null,
                    SlotSelectionStrategy slotSelectionStrategy = SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
                {
                    var result = new GetCellValueOp<NamedDecimalSet>(sectionId, cellId, slotId, slotSelectionStrategy);

                    return result;
                }

                /// <summary>
                /// Builds an operation that gets the value of a cell that contains an integer.
                /// </summary>
                /// <param name="sectionId">The id of the section that contains the cell.</param>
                /// <param name="cellId">The id of the cell.</param>
                /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
                /// <param name="slotSelectionStrategy">OPTIONAL strategy to use to select a slot if addressing an <see cref="ISlottedCell"/>.  DEFAULT is to throw if addressing an <see cref="ISlottedCell"/> -AND- <paramref name="slotId"/> is not specified.</param>
                /// <returns>
                /// The operation.
                /// </returns>
                public static GetCellValueOp<int> Int(
                    string sectionId,
                    string cellId,
                    string slotId = null,
                    SlotSelectionStrategy slotSelectionStrategy = SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
                {
                    var result = new GetCellValueOp<int>(sectionId, cellId, slotId, slotSelectionStrategy);

                    return result;
                }

                /// <summary>
                /// Builds an operation that gets the value of a cell that contains a decimal.
                /// </summary>
                /// <param name="sectionId">The id of the section that contains the cell.</param>
                /// <param name="cellId">The id of the cell.</param>
                /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
                /// <param name="slotSelectionStrategy">OPTIONAL strategy to use to select a slot if addressing an <see cref="ISlottedCell"/>.  DEFAULT is to throw if addressing an <see cref="ISlottedCell"/> -AND- <paramref name="slotId"/> is not specified.</param>
                /// <returns>
                /// The operation.
                /// </returns>
                public static GetCellValueOp<decimal> Decimal(
                    string sectionId,
                    string cellId,
                    string slotId = null,
                    SlotSelectionStrategy slotSelectionStrategy = SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
                {
                    var result = new GetCellValueOp<decimal>(sectionId, cellId, slotId, slotSelectionStrategy);

                    return result;
                }

                /// <summary>
                /// Builds an operation that gets the value of a cell that contains a boolean.
                /// </summary>
                /// <param name="sectionId">The id of the section that contains the cell.</param>
                /// <param name="cellId">The id of the cell.</param>
                /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
                /// <param name="slotSelectionStrategy">OPTIONAL strategy to use to select a slot if addressing an <see cref="ISlottedCell"/>.  DEFAULT is to throw if addressing an <see cref="ISlottedCell"/> -AND- <paramref name="slotId"/> is not specified.</param>
                /// <returns>
                /// The operation.
                /// </returns>
                public static GetCellValueOp<bool> Bool(
                    string sectionId,
                    string cellId,
                    string slotId = null,
                    SlotSelectionStrategy slotSelectionStrategy = SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
                {
                    var result = new GetCellValueOp<bool>(sectionId, cellId, slotId, slotSelectionStrategy);

                    return result;
                }
            }
        }

        /// <summary>
        /// Builder methods to construct cells.
        /// </summary>
        public static class Make
        {
            /// <summary>
            /// Builder methods to construct <see cref="IOperationOutputCell{TValue}"/>.
            /// </summary>
            public static class ForOperationOf
            {
                /// <summary>
                /// Builds an <see cref="IOperationOutputCell{TValue}"/> for a decimal named vector.
                /// </summary>
                /// <param name="operation">The operation.</param>
                /// <param name="cellOpExecutedEvent">OPTIONAL result of executing the operation.  DEFAULT is a cell who's operation has not yet been executed.</param>
                /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
                /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
                /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
                /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
                /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
                /// <returns>
                /// The cell.
                /// </returns>
                public static OperationCell<NamedDecimalSet> NamedDecimalSet(
                    IReturningOperation<NamedDecimalSet> operation,
                    CellOpExecutedEvent<NamedDecimalSet> cellOpExecutedEvent = null,
                    string id = null,
                    int? columnsSpanned = null,
                    CellFormat format = null,
                    IHoverOver hoverOver = null,
                    ILink link = null)
                {
                    var result = new OperationCell<NamedDecimalSet>(operation, cellOpExecutedEvent, id, columnsSpanned, format, hoverOver, link);

                    return result;
                }

                /// <summary>
                /// Builds an <see cref="IOperationOutputCell{TValue}"/> for an integer.
                /// </summary>
                /// <param name="operation">The operation.</param>
                /// <param name="cellOpExecutedEvent">OPTIONAL result of executing the operation.  DEFAULT is a cell who's operation has not yet been executed.</param>
                /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
                /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
                /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
                /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
                /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
                /// <returns>
                /// The cell.
                /// </returns>
                public static OperationCell<int> Int(
                    IReturningOperation<int> operation,
                    CellOpExecutedEvent<int> cellOpExecutedEvent = null,
                    string id = null,
                    int? columnsSpanned = null,
                    CellFormat format = null,
                    IHoverOver hoverOver = null,
                    ILink link = null)
                {
                    var result = new OperationCell<int>(operation, cellOpExecutedEvent, id, columnsSpanned, format, hoverOver, link);

                    return result;
                }

                /// <summary>
                /// Builds an <see cref="IOperationOutputCell{TValue}"/> for a decimal.
                /// </summary>
                /// <param name="operation">The operation.</param>
                /// <param name="cellOpExecutedEvent">OPTIONAL result of executing the operation.  DEFAULT is a cell who's operation has not yet been executed.</param>
                /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
                /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
                /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
                /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
                /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
                /// <returns>
                /// The cell.
                /// </returns>
                public static OperationCell<decimal> Decimal(
                    IReturningOperation<decimal> operation,
                    CellOpExecutedEvent<decimal> cellOpExecutedEvent = null,
                    string id = null,
                    int? columnsSpanned = null,
                    CellFormat format = null,
                    IHoverOver hoverOver = null,
                    ILink link = null)
                {
                    var result = new OperationCell<decimal>(operation, cellOpExecutedEvent, id, columnsSpanned, format, hoverOver, link);

                    return result;
                }

                /// <summary>
                /// Builds an <see cref="IOperationOutputCell{TValue}"/> for a boolean.
                /// </summary>
                /// <param name="operation">The operation.</param>
                /// <param name="cellOpExecutedEvent">OPTIONAL result of executing the operation.  DEFAULT is a cell who's operation has not yet been executed.</param>
                /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
                /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
                /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
                /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
                /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
                /// <returns>
                /// The cell.
                /// </returns>
                public static OperationCell<bool> Bool(
                    IReturningOperation<bool> operation,
                    CellOpExecutedEvent<bool> cellOpExecutedEvent = null,
                    string id = null,
                    int? columnsSpanned = null,
                    CellFormat format = null,
                    IHoverOver hoverOver = null,
                    ILink link = null)
                {
                    var result = new OperationCell<bool>(operation, cellOpExecutedEvent, id, columnsSpanned, format, hoverOver, link);

                    return result;
                }
            }
        }
    }
}
