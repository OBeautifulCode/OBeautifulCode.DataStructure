// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Type;
    using static System.FormattableString;

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
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="validation">OPTIONAL validation to perform.  DEFAULT is no validation.</param>
        /// <param name="validationEvents">OPTIONAL events that record the validation of this cell.  DEFAULT is a cell that has not yet been validated.</param>
        /// <param name="defaultAvailability">OPTIONAL default availability of the cell (before <paramref name="availabilityCheck"/> is run).  DEFAULT is an enabled cell.</param>
        /// <param name="availabilityCheck">OPTIONAL availability check to perform.  DEFAULT is a cell with no availability check.</param>
        /// <param name="availabilityCheckEvents">OPTIONAL events that record the availability checks on this cell.  DEFAULT is a cell that has not yet had an availability check.</param>
        /// <param name="inputEvents">OPTIONAL events that record the manipulation of this cell's value.  DEFAULT is a cell with no inputted value.</param>
        /// <param name="valueFormat">OPTIONAL format to apply to the cell value.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        public InputCell(
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            Validation validation = null,
            IReadOnlyList<CellValidationEventBase> validationEvents = null,
            Availability defaultAvailability = Availability.Enabled,
            AvailabilityCheck availabilityCheck = null,
            IReadOnlyList<CellAvailabilityCheckEventBase> availabilityCheckEvents = null,
            IReadOnlyList<CellInputEventBase> inputEvents = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null)
            : base(id, columnsSpanned, details, validation, validationEvents, defaultAvailability, availabilityCheck, availabilityCheckEvents, inputEvents)
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

        /// <inheritdoc />
        public ICellValueFormat GetCellValueFormat()
        {
            var result = this.ValueFormat;

            return result;
        }

        /// <inheritdoc />
        public override IConstOutputCell ToConstOutputCell()
        {
            if (!this.HasCellValue())
            {
                throw new InvalidOperationException(Invariant($"This cell's value has not been set."));
            }

            var value = this.GetCellValue();

            var result = new ConstCell<TValue>(
                value,
                this.Id,
                this.ColumnsSpanned,
                this.Details,
                this.Validation,
                this.ValidationEvents,
                this.DefaultAvailability,
                this.AvailabilityCheck,
                this.AvailabilityCheckEvents,
                this.ValueFormat,
                this.Format,
                this.HoverOver,
                link: null);

            return result;
        }
    }
}