﻿// --------------------------------------------------------------------------------------------------------------------
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
    public abstract partial class InputCellBase<TValue> : CellBase, IInputCell<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputCellBase{TValue}"/> class.
        /// </summary>
        /// <param name="cellInputEvents">The events that record the manipulation of this cell's value.</param>
        /// <param name="validationConditions">A list of conditions that determine the validity of the cell's value.</param>
        /// <param name="cellValidationEvents">The events that record the validation of this cell's value.</param>
        /// <param name="id">The cell's unique identifier.</param>
        /// <param name="columnsSpanned">The number of columns spanned or null if none (cell occupies a single column).</param>
        /// <param name="details">Details about the cell.</param>
        protected InputCellBase(
            IReadOnlyList<CellInputEventBase> cellInputEvents,
            ValidationConditions validationConditions,
            IReadOnlyList<CellValidationEventBase> cellValidationEvents,
            string id,
            int? columnsSpanned,
            string details)
            : base(id, columnsSpanned, details)
        {
            if ((cellInputEvents != null) && cellInputEvents.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(cellInputEvents)} contains a null element."));
            }

            if ((cellValidationEvents != null) && cellValidationEvents.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(cellValidationEvents)} contains a null element."));
            }

            this.CellInputEvents = cellInputEvents;
            this.ValidationConditions = validationConditions;
            this.CellValidationEvents = cellValidationEvents;
        }

        /// <inheritdoc />
        public IReadOnlyList<CellInputEventBase> CellInputEvents { get; private set; }

        /// <inheritdoc />
        public ValidationConditions ValidationConditions { get; private set; }

        /// <inheritdoc />
        public IReadOnlyList<CellValidationEventBase> CellValidationEvents { get; private set; }

        /// <inheritdoc />
        public void Record(
            CellInputEventBase cellInputEvent)
        {
            if (cellInputEvent == null)
            {
                throw new ArgumentNullException(nameof(cellInputEvent));
            }

            this.CellInputEvents = new CellInputEventBase[0]
                .Concat(this.CellInputEvents ?? new CellInputEventBase[0])
                .Concat(new[] { cellInputEvent })
                .ToList();
        }

        /// <inheritdoc />
        public TValue GetCellValue()
        {
            if (!this.HasCellValue())
            {
                throw new InvalidOperationException("No input has been applied to the cell.");
            }

            var result = ((CellInputAppliedEvent<TValue>)this.CellInputEvents.Last()).Value;

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
        public void Record(
            CellValidationEventBase cellValidationEvent)
        {
            if (cellValidationEvent == null)
            {
                throw new ArgumentNullException(nameof(cellValidationEvent));
            }

            this.CellValidationEvents = new CellValidationEventBase[0]
                .Concat(this.CellValidationEvents ?? new CellValidationEventBase[0])
                .Concat(new[] { cellValidationEvent })
                .ToList();
        }

        /// <inheritdoc />
        public void ClearValidation(
            DateTime timestampUtc,
            string details)
        {
            var cellValidationClearedEvent = new CellValidationClearedEvent(timestampUtc, details);

            this.Record(cellValidationClearedEvent);
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
        public bool HasCellValue() => this.CellInputEvents?.LastOrDefault() is CellInputAppliedEvent<TValue>;

        /// <inheritdoc />
        public object GetCellObjectValue() => this.GetCellValue();
    }
}