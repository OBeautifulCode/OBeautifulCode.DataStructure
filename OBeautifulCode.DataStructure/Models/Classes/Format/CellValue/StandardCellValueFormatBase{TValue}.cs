// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardCellValueFormatBase{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Base class for a cell value format supporting standard/common-used features.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class StandardCellValueFormatBase<TValue> : CellValueFormatBase<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardCellValueFormatBase{TValue}"/> class.
        /// </summary>
        /// <param name="missingValueText">The text to use when the cell is missing a value.</param>
        protected StandardCellValueFormatBase(
            string missingValueText)
        {
            this.MissingValueText = missingValueText;
        }

        /// <summary>
        /// Gets the text to use when the cell is missing a value.
        /// </summary>
        public string MissingValueText { get; private set; }
    }
}
