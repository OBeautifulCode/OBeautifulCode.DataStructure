// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlCellValueFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// A cell value format for an HTML value.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class HtmlCellValueFormat : StandardCellValueFormatBase<string>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlCellValueFormat"/> class.
        /// </summary>
        /// <param name="missingValueText">OPTIONAL text to use when the cell is missing a value.  DEFAULT is to leave this text unspecified.</param>
        public HtmlCellValueFormat(
            string missingValueText = null)
            : base(missingValueText)
        {
        }
    }
}
