// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeTableExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Reflection.Recipes;
    using static System.FormattableString;

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

        /// <summary>
        /// Replaces a cell with another cell.
        /// </summary>
        /// <param name="treeTable">The tree table containing the cell to swap out.</param>
        /// <param name="cellToReplace">The cell to replace.</param>
        /// <param name="cellToUse">The cell that is replacing <paramref name="cellToReplace"/>.</param>
        public static void ReplaceCell(
            this TreeTable treeTable,
            ICell cellToReplace,
            ICell cellToUse)
        {
            if (treeTable == null)
            {
                throw new ArgumentNullException(nameof(treeTable));
            }

            if (cellToReplace == null)
            {
                throw new ArgumentNullException(nameof(cellToReplace));
            }

            if (cellToUse == null)
            {
                throw new ArgumentNullException(nameof(cellToUse));
            }

            if ((cellToReplace.ColumnsSpanned ?? 1) != (cellToUse.ColumnsSpanned ?? 1))
            {
                throw new ArgumentException(Invariant($"{nameof(cellToReplace)} and {nameof(cellToUse)} do not span the same number of columns."));
            }

            var rows = treeTable.GetAllRowsInOrder();

            var cellComparer = new ReferenceEqualityComparer<ICell>();
            var notSlottedCellComparer = new ReferenceEqualityComparer<INotSlottedCell>();
            var notSlottedCellToReplace = cellToReplace as INotSlottedCell;
            var notSlottedCellToUse = cellToUse as INotSlottedCell;

            void ValidateTreeTable()
            {
                // This forces the constructor to run validation (e.g. cellToUse doesn't have an id that conflicts with some other id).
                // ReSharper disable once ObjectCreationAsStatement
                try
                {
                    new TreeTable(treeTable.TableColumns, treeTable.TableRows);
                }
                catch (ArgumentException ex)
                {
                    throw new InvalidOperationException(Invariant($"Replacing {nameof(cellToReplace)} with {nameof(cellToUse)} results in a malformed {nameof(TreeTable)}.  See inner exception."), ex);
                }
            }

            // Cell references are guaranteed to be unique by the constructor of the tree table,
            // so we can stop when we've hit the first match.
            foreach (var row in rows)
            {
                // Check the row for the cell to swap out.
                for (var x = 0; x < row.Cells.Count; x++)
                {
                    if (cellComparer.Equals(row.Cells[x], cellToReplace))
                    {
                        var updatedCells = row.Cells.ToList();
                        updatedCells[x] = cellToUse;

                        // The setter is on RowBase, thus the need to qualify MemberRelationships
                        row.SetPropertyValue(nameof(RowBase.Cells), updatedCells, MemberRelationships.DeclaredInAncestorTypes);

                        ValidateTreeTable();

                        return;
                    }
                }

                // If the cell to swap out is not slotted, then it is potentially contained within a slotted cell.
                if (notSlottedCellToReplace != null)
                {
                    var slottedCells = row.Cells.OfType<ISlottedCell>().ToList();

                    foreach (var slottedCell in slottedCells)
                    {
                        foreach (var slotIdAndCellKvp in slottedCell.SlotIdToCellMap)
                        {
                            if (notSlottedCellComparer.Equals(slotIdAndCellKvp.Value, notSlottedCellToReplace))
                            {
                                if (notSlottedCellToUse == null)
                                {
                                    throw new InvalidOperationException(Invariant($"{nameof(cellToReplace)} was found in a slot of an {nameof(ISlottedCell)}, but {nameof(cellToUse)} is not an {nameof(INotSlottedCell)}."));
                                }

                                var updatedSlotIdToCellMap = slottedCell.SlotIdToCellMap.ToDictionary(_ => _.Key, _ => _.Value);
                                updatedSlotIdToCellMap[slotIdAndCellKvp.Key] = notSlottedCellToUse;
                                slottedCell.SetPropertyValue(nameof(ISlottedCell.SlotIdToCellMap), updatedSlotIdToCellMap);

                                ValidateTreeTable();

                                return;
                            }
                        }
                    }
                }
            }

            throw new InvalidOperationException(Invariant($"{nameof(cellToReplace)} was not found."));
        }
    }
}
