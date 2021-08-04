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
        /// <param name="inputEvents">The events that record the manipulation of this cell's value.</param>
        protected InputCellBase(
            string id,
            int? columnsSpanned,
            string details,
            Validation validation,
            IReadOnlyList<CellValidationEventBase> validationEvents,
            IReadOnlyList<CellInputEventBase> inputEvents)
            : base(id, columnsSpanned, details, validation, validationEvents)
        {
            if ((inputEvents != null) && inputEvents.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(inputEvents)} contains a null element."));
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

            this.InputEvents = new CellInputEventBase[0]
                .Concat(this.InputEvents ?? new CellInputEventBase[0])
                .Concat(new[] { inputEvent })
                .ToList();
        }

        /// <inheritdoc />
        public TValue GetCellValue()
        {
            if (!this.HasCellValue())
            {
                throw new InvalidOperationException("No input has been applied to the cell.");
            }

            var result = ((CellInputAppliedEvent<TValue>)this.InputEvents.Last()).Value;

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
        public override bool IsConstCell() => false;

        /// <inheritdoc />
        public override bool IsInputCell() => true;

        /// <inheritdoc />
        public override bool IsOperationCell() => false;

        /// <inheritdoc />
        public override Type GetValueTypeOrNull() => typeof(TValue);

        /// <inheritdoc />
        public bool HasCellValue() => this.InputEvents?.LastOrDefault() is CellInputAppliedEvent<TValue>;

        /// <inheritdoc />
        public object GetCellObjectValue() => this.GetCellValue();
    }
}