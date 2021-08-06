// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstOutputCellBase{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// Base implementation of <see cref="IConstOutputCell{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of output value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class ConstOutputCellBase<TValue> : OutputCellBase<TValue>, IConstOutputCell<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstOutputCellBase{TValue}"/> class.
        /// </summary>
        /// <param name="id">The cell's unique identifier.</param>
        /// <param name="columnsSpanned">The number of columns spanned or null if none (cell occupies a single column).</param>
        /// <param name="details">Details about the cell.</param>
        /// <param name="validation">The validation to perform.</param>
        /// <param name="validationEvents">The events that record the validation of this cell.</param>
        /// <param name="defaultAvailability">The default availability of the cell (before <paramref name="availabilityCheck"/> is run).</param>
        /// <param name="availabilityCheck">The availability check to perform.</param>
        /// <param name="availabilityCheckEvents">The events that record the availability checks on this cell.</param>
        /// <param name="value">The cell's value.</param>
        protected ConstOutputCellBase(
            string id,
            int? columnsSpanned,
            string details,
            Validation validation,
            IReadOnlyList<CellValidationEventBase> validationEvents,
            Availability defaultAvailability,
            AvailabilityCheck availabilityCheck,
            IReadOnlyList<CellAvailabilityCheckEventBase> availabilityCheckEvents,
            TValue value)
            : base(id, columnsSpanned, details, validation, validationEvents, defaultAvailability, availabilityCheck, availabilityCheckEvents)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the cell's value.
        /// </summary>
        public TValue Value { get; private set; }

        /// <inheritdoc />
        public override TValue GetCellValue()
        {
            var result = this.Value;

            return result;
        }

        /// <inheritdoc />
        public override bool IsConstCell() => true;

        /// <inheritdoc />
        public override bool IsInputCell() => false;

        /// <inheritdoc />
        public override bool IsOperationCell() => false;

        /// <inheritdoc />
        public override Type GetValueTypeOrNull() => typeof(TValue);

        /// <inheritdoc />
        public override bool HasCellValue() => true;
    }
}