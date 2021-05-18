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
    /// Extension methods on <see cref="RowBase"/>.
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
            this RowBase row)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            var result = row
                .Cells
                .Select(_ => _ is IColumnSpanningCell spanningCell ? spanningCell.ColumnsSpanned : 1)
                .Sum();

            return result;
        }

        /// <summary>
        /// Gets all of the data rows, descendants included, in the order they appear in the table.
        /// </summary>
        /// <param name="dataRows">The data rows.</param>
        /// <returns>The data rows, descendants included, in the order they appear in the table.</returns>
        public static IReadOnlyList<RowBase> GetAllDataRowsInOrder(
            this DataRows dataRows)
        {
            var result = GetAllDataRowsInOrder(dataRows?.Rows ?? new Row[0]);

            return result;
        }

        private static IReadOnlyList<RowBase> GetAllDataRowsInOrder(
            IReadOnlyList<Row> rows)
        {
            var result = new List<RowBase>();

            foreach (var row in rows)
            {
                result.Add(row);

                if (row.CollapsedSummaryRow != null)
                {
                    result.Add(row.CollapsedSummaryRow);
                }

                result.AddRange(GetAllDataRowsInOrder(row.ChildRows ?? new Row[0]));

                if (row.ExpandedSummaryRow != null)
                {
                    result.Add(row.ExpandedSummaryRow);
                }
            }

            return result;
        }
    }
}
