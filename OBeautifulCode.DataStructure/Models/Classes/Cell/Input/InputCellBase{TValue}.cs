// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputCellBase{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Base implementation of <see cref="IInputCell{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of input value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class InputCellBase<TValue> : NotSlottedCellBase, IInputCell<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputCellBase{TValue}"/> class.
        /// </summary>
        /// <param name="id">The cell's unique identifier.</param>
        /// <param name="columnsSpanned">The number of columns spanned or null if none (cell occupies a single column).</param>
        /// <param name="details">Details about the cell.</param>
        /// <param name="validation">The validation to perform.</param>
        /// <param name="validationEvents">The events that record the validation of this cell.</param>
        /// <param name="defaultAvailability">The default availability of the cell (before <paramref name="availabilityCheck"/> is run).</param>
        /// <param name="availabilityCheck">The availability check to perform.</param>
        /// <param name="availabilityCheckEvents">The events that record the availability checks on this cell.</param>
        /// <param name="inputEvents">The events that record the manipulation of this cell's value.</param>
        protected InputCellBase(
            string id,
            int? columnsSpanned,
            string details,
            Validation validation,
            IReadOnlyList<CellValidationEventBase> validationEvents,
            Availability defaultAvailability,
            AvailabilityCheck availabilityCheck,
            IReadOnlyList<CellAvailabilityCheckEventBase> availabilityCheckEvents,
            IReadOnlyList<CellInputEventBase> inputEvents)
            : base(id, columnsSpanned, details, validation, validationEvents, defaultAvailability, availabilityCheck, availabilityCheckEvents)
        {
            if ((inputEvents != null) && inputEvents.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(inputEvents)} contains at least one null element."));
            }

            this.InputEvents = inputEvents;
        }

        /// <inheritdoc />
        public IReadOnlyList<CellInputEventBase> InputEvents { get; private set; }

        /// <inheritdoc />
        public void Record(
            CellInputEventBase inputEvent)
        {
            if (inputEvent == null)
            {
                throw new ArgumentNullException(nameof(inputEvent));
            }

            if ((!(inputEvent is CellInputAppliedEvent<TValue>)) && (!(inputEvent is CellInputClearedEvent)))
            {
                throw new ArgumentException(Invariant($"{nameof(inputEvent)} is of type '{inputEvent.GetType().ToStringReadable()}', which is not applicable to this cell, which is of type '{this.GetType().ToStringReadable()}'."));
            }

            this.InputEvents = new CellInputEventBase[0]
                .Concat(this.InputEvents ?? new CellInputEventBase[0])
                .Concat(new[] { inputEvent })
                .ToList();
        }

        /// <inheritdoc />
        public TValue GetCellValue()
        {
            var result = this.GetCellValueCellInputAppliedEvent().Value;

            return result;
        }

        /// <inheritdoc />
        public CellInputAppliedEvent<TValue> GetCellValueCellInputAppliedEvent()
        {
            if (!this.HasCellValue())
            {
                throw new InvalidOperationException("No input has been applied to the cell.");
            }

            var result = (CellInputAppliedEvent<TValue>)this.InputEvents.Last();

            return result;
        }

        /// <inheritdoc />
        public void ClearCellValue(
            DateTime timestampUtc,
            string details)
        {
            var inputClearedFromCellEvent = new CellInputClearedEvent(timestampUtc, details);

            this.Record(inputClearedFromCellEvent);
        }

        /// <inheritdoc />
        public void SetCellValue(
            TValue value,
            DateTime timestampUtc,
            string details = null)
        {
            var cellInputAppliedEvent = new CellInputAppliedEvent<TValue>(value, timestampUtc, details);

            this.Record(cellInputAppliedEvent);
        }

        /// <inheritdoc />
        public void SetCellValue(
            object value,
            DateTime timestampUtc,
            string details = null)
        {
            if (value == null)
            {
                if (typeof(TValue).IsTypeAssignableToNull())
                {
                    this.SetCellValue(default, timestampUtc, details);
                }
                else
                {
                    throw new ArgumentException(Invariant($"{nameof(value)} is null, which is not assignable to a value of type {typeof(TValue).ToStringReadable()}."));
                }
            }
            else if (value is TValue typedValue)
            {
                this.SetCellValue(typedValue, timestampUtc, details);
            }
            else
            {
                throw new ArgumentException(Invariant($"{nameof(value)} is not of type {typeof(TValue).ToStringReadable()}."));
            }
        }

        /// <inheritdoc />
        public bool TrySetCellValue(
            object value,
            DateTime timestampUtc,
            string details = null)
        {
            var result = false;

            if (value == null)
            {
                if (typeof(TValue).IsTypeAssignableToNull())
                {
                    this.SetCellValue(default, timestampUtc, details);

                    result = true;
                }
            }
            else if (value is TValue typedValue)
            {
                this.SetCellValue(typedValue, timestampUtc, details);

                result = true;
            }

            return result;
        }

        /// <inheritdoc />
        public override Type GetValueTypeOrNull() => typeof(TValue);

        /// <inheritdoc />
        public bool HasCellValue() => this.InputEvents?.LastOrDefault() is CellInputAppliedEvent<TValue>;

        /// <inheritdoc />
        public object GetCellObjectValue() => this.GetCellValue();

        /// <inheritdoc />
        public ICellInputAppliedEvent GetCellValueCellInputAppliedEventInterface()
        {
            var result = (ICellInputAppliedEvent)this.GetCellValueCellInputAppliedEvent();

            return result;
        }
    }
}