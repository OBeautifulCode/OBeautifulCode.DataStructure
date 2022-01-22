// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowBaseExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="RowBase"/> and derivatives.
    /// </summary>
    public static class RowBaseExtensions
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
                .Select(_ => _.ColumnsSpanned ?? 1)
                .Sum();

            return result;
        }

        /// <summary>
        /// Pads a row, adding a <see cref="NullCell" /> if needed, such that the resulting row spans a specified number of columns.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="requiredNumberOfColumnsSpanned">The required number of columns spanned in the resulting row.</param>
        /// <returns>
        /// The padded row.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "More useful to end-user to return concrete type.")]
        public static FlatRow Pad(
            this FlatRow row,
            int requiredNumberOfColumnsSpanned)
        {
            var result = (FlatRow)row.PadInternal(requiredNumberOfColumnsSpanned);

            return result;
        }

        /// <summary>
        /// Pads a row, adding a <see cref="NullCell" /> if needed, such that the resulting row spans a specified number of columns.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="requiredNumberOfColumnsSpanned">The required number of columns spanned in the resulting row.</param>
        /// <returns>
        /// The padded row.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "More useful to end-user to return concrete type.")]
        public static Row Pad(
            this Row row,
            int requiredNumberOfColumnsSpanned)
        {
            var result = (Row)row.PadInternal(requiredNumberOfColumnsSpanned);

            return result;
        }

        private static RowBase PadInternal(
            this RowBase row,
            int requiredNumberOfColumnsSpanned)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            if (requiredNumberOfColumnsSpanned <= 0)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(requiredNumberOfColumnsSpanned)} must be > 0; provided value: {requiredNumberOfColumnsSpanned}."));
            }

            RowBase result;

            var currentNumberOfColumnsSpanned = row.GetNumberOfColumnsSpanned();

            if (currentNumberOfColumnsSpanned >= requiredNumberOfColumnsSpanned)
            {
                result = row;
            }
            else
            {
                var additionalColumnsSpanned = requiredNumberOfColumnsSpanned - currentNumberOfColumnsSpanned;

                var newCells = row.Cells.ToList();

                newCells.Add(new NullCell(columnsSpanned: additionalColumnsSpanned));

                result = row.DeepCloneWithCells(newCells);
            }

            return result;
        }
    }
}
