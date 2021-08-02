// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Implementation of <see cref="IInputCell{TValue}"/> with a standard set of features.
    /// </summary>
    /// <typeparam name="TValue">The type of input value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class InputCell<TValue> : InputCellBase<TValue>, IHaveStandardCellFeatures<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputCell{TValue}"/> class.
        /// </summary>
        /// <param name="cellInputEvents">OPTIONAL events that track the manipulation of this cell's value.  DEFAULT is a cell with no inputted value.</param>
        /// <param name="validationConditions">OPTIONAL list of conditions that determine the validity of the cell's value.  DEFAULT is to omit validation.</param>
        /// <param name="cellValidationEvent">OPTIONAL result of validating the cell's value.  DEFAULT is a cell that has not yet been validated.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="valueFormat">OPTIONAL format to apply to the cell value.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        public InputCell(
            IReadOnlyList<CellInputEventBase> cellInputEvents = null,
            ValidationConditions validationConditions = null,
            CellValidationEventBase cellValidationEvent = null,
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null)
            : base(cellInputEvents, validationConditions, cellValidationEvent, id, columnsSpanned, details)
        {
            this.ValueFormat = valueFormat;
            this.Format = format;
            this.HoverOver = hoverOver;
        }

        /// <inheritdoc />
        public ICellValueFormat<TValue> ValueFormat { get; private set; }

        /// <inheritdoc />
        public CellFormat Format { get; private set; }

        /// <inheritdoc />
        public IHoverOver HoverOver { get; private set; }
    }
}