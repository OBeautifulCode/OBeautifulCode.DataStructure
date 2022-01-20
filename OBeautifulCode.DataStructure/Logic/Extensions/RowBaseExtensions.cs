// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowBaseExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Linq;

    /// <summary>
    /// Extension methods on <see cref="RowBase"/>.
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
    }
}
