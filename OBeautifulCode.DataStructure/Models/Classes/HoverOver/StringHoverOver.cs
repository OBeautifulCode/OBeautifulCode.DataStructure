// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringHoverOver.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A string hover-over.
    /// </summary>
    public partial class StringHoverOver : HoverOverBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringHoverOver"/> class.
        /// </summary>
        /// <param name="value">The hover-over string value.</param>
        public StringHoverOver(
            string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(Invariant($"{nameof(value)} is white space"));
            }

            this.Value = value;
        }

        /// <summary>
        /// Gets the hover-over string value.
        /// </summary>
        public string Value { get; private set; }
    }
}
