// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OutputCellBase{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Base implementation of <see cref="IOutputCell{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of output value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class OutputCellBase<TValue> : CellBase, IOutputCell<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputCellBase{TValue}"/> class.
        /// </summary>
        /// <param name="id">The cell's unique identifier.</param>
        /// <param name="columnsSpanned">The number of columns spanned or null if none (cell occupies a single column).</param>
        /// <param name="details">Details about the cell.</param>
        protected OutputCellBase(
            string id,
            int? columnsSpanned,
            string details)
            : base(id, columnsSpanned, details)
        {
        }

        /// <inheritdoc />
        public abstract TValue GetCellValue();

        /// <inheritdoc />
        public abstract bool HasCellValue();

        /// <inheritdoc />
        public object GetCellObjectValue() => this.GetCellValue();
    }
}