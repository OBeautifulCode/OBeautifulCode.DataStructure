// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputCellBase{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

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
        /// <param name="inputAppliedToCellEvent">The input that was applied to the cell.</param>
        /// <param name="validationConditions">A list of conditions that determine the validity of the cell's value.</param>
        /// <param name="cellValidationEvent">The result of validating the cell's value.</param>
        /// <param name="id">The cell's unique identifier.</param>
        /// <param name="columnsSpanned">The number of columns spanned or null if none (cell occupies a single column).</param>
        /// <param name="details">Details about the cell.</param>
        protected InputCellBase(
            InputAppliedToCellEvent<TValue> inputAppliedToCellEvent,
            ValidationConditions validationConditions,
            CellValidationEventBase cellValidationEvent,
            string id,
            int? columnsSpanned,
            string details)
            : base(id, columnsSpanned, details)
        {
            this.InputAppliedToCellEvent = inputAppliedToCellEvent;
            this.ValidationConditions = validationConditions;
            this.CellValidationEvent = cellValidationEvent;
        }

        /// <inheritdoc />
        public InputAppliedToCellEvent<TValue> InputAppliedToCellEvent { get; private set; }

        /// <inheritdoc />
        public ValidationConditions ValidationConditions { get; private set; }

        /// <inheritdoc />
        public CellValidationEventBase CellValidationEvent { get; private set; }

        /// <inheritdoc />
        public void RecordInput(
            InputAppliedToCellEvent<TValue> inputAppliedToCellEvent)
        {
            if (inputAppliedToCellEvent == null)
            {
                throw new ArgumentNullException(nameof(inputAppliedToCellEvent));
            }

            this.InputAppliedToCellEvent = inputAppliedToCellEvent;
        }

        /// <inheritdoc />
        public TValue GetCellValue()
        {
            if (this.InputAppliedToCellEvent == null)
            {
                throw new InvalidOperationException("No input has been applied to the cell.");
            }

            var result = this.InputAppliedToCellEvent.Value;

            return result;
        }

        /// <inheritdoc />
        public void ClearCellValue()
        {
            this.InputAppliedToCellEvent = null;
        }

        /// <inheritdoc />
        public void RecordValidation(
            CellValidationEventBase cellValidationEvent)
        {
            if (cellValidationEvent == null)
            {
                throw new ArgumentNullException(nameof(cellValidationEvent));
            }

            this.CellValidationEvent = cellValidationEvent;
        }

        /// <inheritdoc />
        public void ClearValidation()
        {
            this.CellValidationEvent = null;
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
        public bool HasCellValue() => this.InputAppliedToCellEvent != null;

        /// <inheritdoc />
        public object GetCellObjectValue() => this.GetCellValue();
    }
}