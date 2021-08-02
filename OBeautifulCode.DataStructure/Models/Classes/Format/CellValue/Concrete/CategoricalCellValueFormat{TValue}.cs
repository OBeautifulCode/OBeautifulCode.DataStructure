// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoricalCellValueFormat{TValue}.cs" company="OBeautifulCode">
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
    /// A cell value format for a categorical value.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CategoricalCellValueFormat<TValue> : StandardCellValueFormatBase<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoricalCellValueFormat{TValue}"/> class.
        /// </summary>
        /// <param name="valueToTextMap">A dictionary of values mapped to the text to use to represent those values.</param>
        /// <param name="missingValueText">OPTIONAL text to use when the cell is missing a value.  DEFAULT is to leave this text unspecified.</param>
        public CategoricalCellValueFormat(
            IReadOnlyDictionary<TValue, string> valueToTextMap,
            string missingValueText = null)
            : base(missingValueText)
        {
            if (valueToTextMap == null)
            {
                throw new ArgumentNullException(nameof(valueToTextMap));
            }

            if (!valueToTextMap.Any())
            {
                throw new ArgumentException(Invariant($"{nameof(valueToTextMap)} is empty."));
            }

            if (valueToTextMap.Any(_ => string.IsNullOrWhiteSpace(_.Value)))
            {
                throw new ArgumentException(Invariant($"{nameof(valueToTextMap)} is contains an null or white space value."));
            }

            this.ValueToTextMap = valueToTextMap;
        }

        /// <summary>
        /// Gets a dictionary of values mapped to the text to use to represent those values.
        /// </summary>
        public IReadOnlyDictionary<TValue, string> ValueToTextMap { get; private set; }
    }
}
