﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnSpanningStringCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// A cell that contains a string value and spans multiple columns.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ColumnSpanningStringCell : ColumnSpanningStandardCellBase, IHaveValueCell, IHaveDisplayValueCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSpanningStringCell"/> class.
        /// </summary>
        /// <param name="value">The cell's string value.</param>
        /// <param name="columnsSpanned">The number of columns spanned.</param>
        /// <param name="displayValue">OPTIONAL display value for the cell.  DEFAULT is to use default function to convert <paramref name="value"/> into a display value.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public ColumnSpanningStringCell(
            string value,
            int columnsSpanned,
            string displayValue = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(columnsSpanned, format, hoverOver, link)
        {
            this.Value = value;
            this.DisplayValue = displayValue;
        }

        /// <summary>
        /// Gets the cell's string value.
        /// </summary>
        public string Value { get; private set; }

        /// <inheritdoc />
        public string DisplayValue { get; private set; }

        /// <inheritdoc />
        public object GetValue() => this.Value;
    }
}