// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// A cell that contains a string value.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class StringCell : CellBase, IStringCell, IFormattableCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringCell"/> class.
        /// </summary>
        /// <param name="value">The cell's string value.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        protected StringCell(
            string value,
            CellFormat format = null)
        {
            this.Value = value;
            this.Format = format;
        }

        /// <inheritdoc />
        public string Value { get; private set; }

        /// <inheritdoc />
        public CellFormat Format { get; private set; }

        /// <inheritdoc />
        public object GetValue() => this.Value;
    }
}