// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Column.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A column in a tree table.
    /// </summary>
    public partial class Column : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="id">OPTIONAL id of the column.  DEFAULT is non-identified column.</param>
        /// <param name="format">
        /// OPTIONAL format to apply to the whole column.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        public Column(
            string id = null,
            ColumnFormat format = null)
        {
            if ((id != null) && string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(Invariant($"{nameof(id)} is white space"));
            }

            this.Id = id;
            this.Format = format;
        }

        /// <summary>
        /// Gets the id of the column.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the format to apply to the whole column.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// </summary>
        public ColumnFormat Format { get; private set; }
    }
}