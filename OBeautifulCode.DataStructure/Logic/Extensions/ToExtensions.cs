// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.String.Recipes;

    /// <summary>
    /// Extension methods to build higher level objects from lower-level objects.
    /// </summary>
    public static class ToExtensions
    {
        /// <summary>
        /// Converts a <see cref="TreeTable"/> to a single-<see cref="Section"/> <see cref="Report"/>.
        /// </summary>
        /// <param name="treeTable">The tree table.</param>
        /// <param name="title">OPTIONAL report title to use.  DEFAULT is no title.</param>
        /// <returns>
        /// A single-<see cref="Section"/> <see cref="Report"/> containing the specified <paramref name="treeTable"/>.
        /// </returns>
        public static Report ToReport(
            this TreeTable treeTable,
            string title = null)
        {
            if (treeTable == null)
            {
                throw new ArgumentNullException(nameof(treeTable));
            }

            var result = new Report(Guid.NewGuid().ToStringInvariantPreferred(), new[] { treeTable.ToSection() }, title);

            return result;
        }

        /// <summary>
        /// Converts a <see cref="TreeTable"/> to a <see cref="Section"/>.
        /// </summary>
        /// <param name="treeTable">The tree table.</param>
        /// <returns>
        /// A <see cref="Section"/> containing the specified <paramref name="treeTable"/>.
        /// </returns>
        public static Section ToSection(
            this TreeTable treeTable)
        {
            if (treeTable == null)
            {
                throw new ArgumentNullException(nameof(treeTable));
            }

            var result = new Section(Guid.NewGuid().ToStringInvariantPreferred(), treeTable);

            return result;
        }

        /// <summary>
        /// Creates a single-cell <see cref="FlatRow"/>.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="id">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// The single-cell <see cref="FlatRow"/>.
        /// </returns>
        public static FlatRow ToFlatRow(
            this ICell cell,
            string id = null,
            RowFormat format = null)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            var result = new FlatRow(new[] { cell }, id, format);

            return result;
        }

        /// <summary>
        /// Creates a single-cell <see cref="Row"/>.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="id">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// The single-cell <see cref="Row"/>.
        /// </returns>
        public static Row ToRow(
            this ICell cell,
            string id = null,
            RowFormat format = null)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            var result = new Row(new[] { cell }, id, format);

            return result;
        }
    }
}
