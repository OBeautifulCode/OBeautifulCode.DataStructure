// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanCellValueFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Linq;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A cell value format for a boolean value.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class BooleanCellValueFormat : StandardCellValueFormatBase<bool>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanCellValueFormat"/> class.
        /// </summary>
        /// <param name="trueText">The text to use to represent a true value.</param>
        /// <param name="falseText">The text to use represent a false value.</param>
        /// <param name="missingValueText">OPTIONAL text to use when the cell is missing a value.  DEFAULT is to leave this text unspecified.</param>
        public BooleanCellValueFormat(
            string trueText,
            string falseText,
            string missingValueText = null)
            : base(missingValueText)
        {
            if (trueText == null)
            {
                throw new ArgumentNullException(nameof(trueText));
            }

            if (string.IsNullOrWhiteSpace(trueText))
            {
                throw new ArgumentException(Invariant($"{nameof(trueText)} is white space."));
            }

            if (falseText == null)
            {
                throw new ArgumentNullException(nameof(falseText));
            }

            if (string.IsNullOrWhiteSpace(falseText))
            {
                throw new ArgumentException(Invariant($"{nameof(falseText)} is white space."));
            }

            if (new[] { trueText, falseText, missingValueText }.Distinct().Count() != 3)
            {
                throw new ArgumentException(Invariant($"{nameof(trueText)}, {nameof(falseText)}, and {nameof(missingValueText)} are not distinct."));
            }

            this.TrueText = trueText;
            this.FalseText = falseText;
        }

        /// <summary>
        /// Gets the text to use to represent a true value.
        /// </summary>
        public string TrueText { get; private set; }

        /// <summary>
        /// Gets the text to use to represent a false value.
        /// </summary>
        public string FalseText { get; private set; }
    }
}
