// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringHoverOver.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

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
            this.Value = value;
        }

        /// <summary>
        /// Gets the hover-over string value.
        /// </summary>
        public string Value { get; private set; }
    }
}
