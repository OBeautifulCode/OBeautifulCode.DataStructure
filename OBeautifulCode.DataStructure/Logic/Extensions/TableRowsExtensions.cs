// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableRowsExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="TableRows"/>.
    /// </summary>
    public static class TableRowsExtensions
    {
        /// <summary>
        /// Gets all of the rows, header and descendants included, in the order they appear in the table.
        /// </summary>
        /// <param name="tableRows">The table rows.</param>
        /// <returns>
        /// The rows, header and descendants included, in the order they appear in the table.
        /// </returns>
        public static IReadOnlyList<RowBase> GetAllRowsInOrder(
            this TableRows tableRows)
        {
            if (tableRows == null)
            {
                throw new ArgumentNullException(nameof(tableRows));
            }

            var result = new List<RowBase>();

            if (tableRows.HeaderRows != null)
            {
                result.AddRange(tableRows.HeaderRows.Rows);
            }

            if (tableRows.DataRows != null)
            {
                result.AddRange(GetAllDataRowsInOrder(tableRows.DataRows.Rows));
            }

            if (tableRows.FooterRows != null)
            {
                result.AddRange(tableRows.FooterRows.Rows);
            }

            return result;
        }

        /// <summary>
        /// Gets all cells in a table.
        /// </summary>
        /// <param name="tableRows">The table rows.</param>
        /// <returns>
        /// All cells in a table.
        /// </returns>
        public static IReadOnlyCollection<ICell> GetAllCells(
            this TableRows tableRows)
        {
            if (tableRows == null)
            {
                throw new ArgumentNullException(nameof(tableRows));
            }

            var allRowsInOrder = tableRows.GetAllRowsInOrder();

            var result = new List<ICell>(allRowsInOrder.SelectMany(_ => _.Cells));

            var slottedCells = result.OfType<ISlottedCell>().SelectMany(_ => _.SlotIdToCellMap.Values).ToList();

            result.AddRange(slottedCells);

            return result;
        }

        private static IReadOnlyList<RowBase> GetAllDataRowsInOrder(
            IReadOnlyList<RowBase> rowBases)
        {
            var result = new List<RowBase>();

            foreach (var rowBase in rowBases)
            {
                result.Add(rowBase);

                if (rowBase is FlatRow)
                {
                    // no-op
                }
                else if (rowBase is Row row)
                {
                    if (row.CollapsedSummaryRows != null)
                    {
                        result.AddRange(row.CollapsedSummaryRows);
                    }

                    result.AddRange(GetAllDataRowsInOrder(row.ChildRows ?? new Row[0]));

                    if (row.ExpandedSummaryRows != null)
                    {
                        result.AddRange(row.ExpandedSummaryRows);
                    }
                }
                else
                {
                    throw new NotSupportedException(Invariant($"This type of {nameof(RowBase)} is not supported: {rowBase.GetType().ToStringReadable()}"));
                }
            }

            return result;
        }
    }
}
