﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeTableExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods on <see cref="TreeTable"/>.
    /// </summary>
    public static class TreeTableExtensions
    {
        /// <summary>
        /// Gets all of the rows, header and descendants included, in the order they appear in the table.
        /// </summary>
        /// <param name="treeTable">The table.</param>
        /// <returns>
        /// The rows, header and descendants included, in the order they appear in the table.
        /// </returns>
        public static IReadOnlyList<RowBase> GetAllRowsInOrder(
            this TreeTable treeTable)
        {
            if (treeTable == null)
            {
                throw new ArgumentNullException(nameof(treeTable));
            }

            var result = treeTable.TableRows == null
                ? new List<RowBase>()
                : treeTable.TableRows.GetAllRowsInOrder();

            return result;
        }

        /// <summary>
        /// Gets all cells in a table.
        /// </summary>
        /// <param name="treeTable">The table.</param>
        /// <returns>
        /// All cells in a table.
        /// </returns>
        public static IReadOnlyCollection<ICell> GetAllCells(
            this TreeTable treeTable)
        {
            if (treeTable == null)
            {
                throw new ArgumentNullException(nameof(treeTable));
            }

            var result = treeTable.TableRows == null
                ? new List<ICell>()
                : treeTable.TableRows.GetAllCells();

            return result;
        }
    }
}
