// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableColumns.cs" company="OBeautifulCode">
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
    /// Specifies the columns of the tree table.
    /// </summary>
    public partial class TableColumns : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableColumns"/> class.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <param name="columnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        public TableColumns(
            IReadOnlyList<Column> columns,
            ColumnFormat columnsFormat = null)
        {
            if (columns == null)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            if (!columns.Any())
            {
                throw new ArgumentException(Invariant($"{nameof(columns)} is an empty enumerable."));
            }

            if (columns.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(columns)} contains at least one null element."));
            }

            this.Columns = columns;
            this.ColumnsFormat = columnsFormat;
        }

        /// <summary>
        /// Gets the columns.
        /// </summary>
        public IReadOnlyList<Column> Columns { get; private set; }

        /// <summary>
        /// Gets the format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// </summary>
        public ColumnFormat ColumnsFormat { get; private set; }
    }
}