// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstOutputCellBase{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

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
        /// <param name="value">The cell's value.</param>
        /// <param name="id">The cell's unique identifier.</param>
        /// <param name="columnsSpanned">The number of columns spanned or null if none (cell occupies a single column).</param>
        /// <param name="details">Details about the cell.</param>
        protected ConstOutputCellBase(
            TValue value,
            string id,
            int? columnsSpanned,
            string details)
            : base(id, columnsSpanned, details)
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