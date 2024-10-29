// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellInputAppliedEvent{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Input has been applied to an <see cref="IInputCell{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of inputted value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellInputAppliedEvent<TValue> : CellInputEventBase, ICellInputAppliedEvent, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellInputAppliedEvent{TValue}"/> class.
        /// </summary>
        /// <param name="value">The inputted value.</param>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">OPTIONAL details about the input that was applied to the cell.  DEFAULT is to omit any details.</param>
        public CellInputAppliedEvent(
            TValue value,
            DateTime timestampUtc,
            string details = null)
            : base(timestampUtc, details)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the inputted value.
        /// </summary>
        public TValue Value { get; private set; }

        /// <inheritdoc />
        public object GetObjectValue()
        {
            var result = this.Value;

            return result;
        }

        /// <inheritdoc />
        public Type GetValueType()
        {
            var result = typeof(TValue);

            return result;
        }
    }
}