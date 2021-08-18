// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportCache.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Equality.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// A cache of various elements contained within a report.
    /// </summary>
    public class ReportCache
    {
        private readonly Dictionary<string, Dictionary<string, ICell>> sectionIdToCellIdToCellMap;

        private readonly Dictionary<ICell, string> cellToSectionIdMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportCache"/> class.
        /// </summary>
        /// <param name="report">The report.</param>
        public ReportCache(
            Report report)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            var sectionIdToAllCellsMap = report.Sections.ToDictionary(_ => _.Id, _ => _.TreeTable.GetAllCells());

            var allCells = sectionIdToAllCellsMap.Values.SelectMany(_ => _).ToList();

            var operationCells = allCells.OfType<IOperationOutputCell>().ToList();

            var inputCells = allCells.OfType<IInputCell>().ToList();

            var validationCells = allCells.OfType<IValidationCell>().ToList();

            var availabilityCheckCells = allCells.OfType<IAvailabilityCheckCell>().ToList();

            var localSectionIdToCellIdToCellMap = sectionIdToAllCellsMap.ToDictionary(
                _ => _.Key,
                _ => _.Value
                    .Where(cell => !string.IsNullOrWhiteSpace(cell.Id))
                    .ToDictionary(cell => cell.Id, cell => cell));

            var localCellToSectionIdMap = sectionIdToAllCellsMap
                .SelectMany(_ => _.Value.Select(cell => new { SectionId = _.Key, Cell = cell }))
                .ToDictionary(_ => _.Cell, _ => _.SectionId, new ReferenceEqualityComparer<ICell>());

            this.sectionIdToCellIdToCellMap = localSectionIdToCellIdToCellMap;
            this.cellToSectionIdMap = localCellToSectionIdMap;

            this.OperationCells = operationCells;
            this.InputCells = inputCells;
            this.ValidationCells = validationCells;
            this.AvailabilityCheckCells = availabilityCheckCells;
        }

        /// <summary>
        /// Gets all operation cells.
        /// </summary>
        public IReadOnlyCollection<IOperationOutputCell> OperationCells { get; }

        /// <summary>
        /// Gets all input cells.
        /// </summary>
        public IReadOnlyCollection<IInputCell> InputCells { get; }

        /// <summary>
        /// Gets all validation cells.
        /// </summary>
        public IReadOnlyCollection<IValidationCell> ValidationCells { get; }

        /// <summary>
        /// Gets all availability check cells.
        /// </summary>
        public IReadOnlyCollection<IAvailabilityCheckCell> AvailabilityCheckCells { get; }

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <param name="reportCellLocator">The report cell locator.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public ICell GetCell(
            ReportCellLocator reportCellLocator)
        {
            if (reportCellLocator == null)
            {
                throw new ArgumentNullException(nameof(reportCellLocator));
            }

            if (!this.sectionIdToCellIdToCellMap.ContainsKey(reportCellLocator.SectionId))
            {
                throw new CellNotFoundException(Invariant($"There is no section with id {reportCellLocator.SectionId}."), reportCellLocator);
            }

            var cellIdToCellMap = this.sectionIdToCellIdToCellMap[reportCellLocator.SectionId];

            var cellId = reportCellLocator.CellId;

            if (!cellIdToCellMap.TryGetValue(cellId, out var cell))
            {
                throw new InvalidOperationException(Invariant($"There is no cell with id '{reportCellLocator.CellId}' in the specified section."));
            }

            ICell result;

            var slotId = reportCellLocator.SlotId;

            if (string.IsNullOrWhiteSpace(slotId))
            {
                result = cell;
            }
            else
            {
                if (cell is ISlottedCell slottedCell)
                {
                    if (slottedCell.SlotIdToCellMap.ContainsKey(slotId))
                    {
                        result = slottedCell.SlotIdToCellMap[slotId];
                    }
                    else
                    {
                        throw new CellNotFoundException(Invariant($"Slot id '{slotId}' was specified, but the addressed cell ('{cellId}') does not contain a slot having that id."), reportCellLocator);
                    }
                }
                else
                {
                    throw new CellNotFoundException(Invariant($"Slot id '{slotId}' was specified, but the addressed cell ('{cellId}') is not a slotted cell"), reportCellLocator);
                }
            }

            if (result is ISlottedCell addressedSlottedCell)
            {
                if (reportCellLocator.SlotSelectionStrategy == SlotSelectionStrategy.DefaultSlot)
                {
                    result = addressedSlottedCell.SlotIdToCellMap[addressedSlottedCell.DefaultSlotId];
                }
                else if (reportCellLocator.SlotSelectionStrategy == SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
                {
                    throw new CellNotFoundException(Invariant($"The operation addresses an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."), reportCellLocator);
                }
                else
                {
                    throw new NotSupportedException(Invariant($"This {nameof(SlotSelectionStrategy)} is not supported: {reportCellLocator.SlotSelectionStrategy}."));
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <param name="sectionCellLocator">The section cell locator.</param>
        /// <param name="currentCell">The current cell.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public ICell GetCell(
            SectionCellLocator sectionCellLocator,
            ICell currentCell)
        {
            if (!this.cellToSectionIdMap.TryGetValue(currentCell, out var sectionId))
            {
                throw new CellNotFoundException(Invariant($"{nameof(currentCell)} is not a cell in the report."), sectionCellLocator);
            }

            var reportCellLocator = new ReportCellLocator(sectionId, sectionCellLocator.CellId, sectionCellLocator.SlotId, sectionCellLocator.SlotSelectionStrategy);

            var result = this.GetCell(reportCellLocator);

            return result;
        }
    }
}
