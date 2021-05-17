// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods on <see cref="DataRow"/>.
    /// </summary>
    public static class RowExtensions
    {
        /// <summary>
        /// Gets the number of columns spanned by a row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>
        /// The number of columns spanned the row.
        /// </returns>
        public static int GetNumberOfColumnsSpanned(
            this DataRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            var result = row
                .Cells
                .GetNumberOfColumnsSpanned();

            return result;
        }

        /// <summary>
        /// Gets the number of columns spanned by a set of cells.
        /// </summary>
        /// <param name="cells">The cells.</param>
        /// <returns>
        /// The number of columns spanned by the cells.
        /// </returns>
        public static int GetNumberOfColumnsSpanned(
            this IReadOnlyList<ICell> cells)
        {
            if (cells == null)
            {
                throw new ArgumentNullException(nameof(cells));
            }

            var result = cells
                .Select(_ => _ is IColumnSpanningCell spanningCell ? spanningCell.ColumnsSpanned : 1)
                .Sum();

            return result;
        }

        /// <summary>
        /// Gets all of the data rows, descendants included, in the order they appear in the table.
        /// </summary>
        /// <param name="dataRows">The data rows.</param>
        /// <returns>The data rows, descendants included, in the order they appear in the table.</returns>
        public static IReadOnlyList<DataRow> GetAllDataRowsInOrder(
            this DataRows dataRows)
        {
            var result = GetAllDataRowsInOrder(dataRows?.Rows ?? new DataRow[0]);

            return result;
        }

        private static IReadOnlyList<DataRow> GetAllDataRowsInOrder(
            IReadOnlyList<DataRow> rows)
        {
            var result = new List<DataRow>();

            foreach (var row in rows)
            {
                result.Add(row);

                result.AddRange(GetAllDataRowsInOrder(row.ChildRows));
            }

            return result;
        }
    }
}
