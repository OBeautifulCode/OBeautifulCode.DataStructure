// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeTableExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="TreeTable"/>.
    /// </summary>
    public static class TreeTableExtensions
    {
        /// <summary>
        /// Gets a cell by it's identifier with an optional slot identifier.
        /// </summary>
        /// <param name="treeTable">The tree table.</param>
        /// <param name="cellId">The id of the cell.</param>
        /// <param name="slotId">OPTIONAL id of the slot to use.  DEFAULT is to address a cell that is not contained within a slot.</param>
        /// <returns>
        /// The addressed cell.
        /// </returns>
        public static ICell GetCell(
            this TreeTable treeTable,
            string cellId,
            string slotId = null)
        {
            if (treeTable == null)
            {
                throw new ArgumentNullException(nameof(treeTable));
            }

            if (cellId == null)
            {
                throw new ArgumentNullException(nameof(cellId));
            }

            if (string.IsNullOrWhiteSpace(cellId))
            {
                throw new ArgumentException(Invariant($"{nameof(cellId)} is white space."));
            }

            if (!treeTable.GetCellIdToCellMap().TryGetValue(cellId, out var cell))
            {
                throw new ArgumentException(Invariant($"There is no cell with id '{cellId}'."));
            }

            ICell result;

            if (string.IsNullOrWhiteSpace(slotId))
            {
                result = cell;
            }
            else
            {
                if (cell is SlottedCell slottedCell)
                {
                    if (slottedCell.SlotIdToCellMap.ContainsKey(slotId))
                    {
                        result = slottedCell.SlotIdToCellMap[slotId];
                    }
                    else
                    {
                        throw new ArgumentException(Invariant($"Slot id '{slotId}' was specified, but the addressed cell ('{cellId}') does not contain a slot having that id."));
                    }
                }
                else
                {
                    throw new ArgumentException(Invariant($"Slot id '{slotId}' was specified, but the addressed cell ('{cellId}') is not a slotted cell"));
                }
            }

            return result;
        }
    }
}
