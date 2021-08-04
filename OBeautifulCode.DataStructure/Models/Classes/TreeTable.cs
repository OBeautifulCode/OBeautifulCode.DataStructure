// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeTable.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A table (rows and columns) where cells can have arbitrary data, with parent-child relationships between rows
    /// along with various visualization (e.g. setting cell colors) and interaction (e.g. sorting) options.
    /// </summary>
    public partial class TreeTable : IModelViaCodeGen
    {
        private readonly IReadOnlyDictionary<string, ICell> cellIdToCellMap;

        private readonly IReadOnlyCollection<INotSlottedCell> operationCells;

        private readonly IReadOnlyCollection<IValidationCell> validationCells;

        private readonly IReadOnlyCollection<IAvailabilityCheckCell> availabilityCheckCells;

        private readonly IReadOnlyCollection<INotSlottedCell> inputCells;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeTable"/> class.
        /// </summary>
        /// <param name="tableColumns">The columns of the table.</param>
        /// <param name="tableRows">OPTIONAL rows of the table.  DEFAULT is no rows.</param>
        /// <param name="format">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = ObcSuppressBecause.CA1502_AvoidExcessiveComplexity_DisagreeWithAssessment)]
        public TreeTable(
            TableColumns tableColumns,
            TableRows tableRows = null,
            TableFormat format = null)
        {
            if (tableColumns == null)
            {
                throw new ArgumentNullException(nameof(tableColumns));
            }

            var numberOfColumns = tableColumns.Columns.Count;

            var ids = new List<string>();

            ids.AddRange(tableColumns.Columns.Where(_ => !string.IsNullOrWhiteSpace(_.Id)).Select(_ => _.Id));

            var allCells = new List<ICell>();

            var allCellsWithIds = new List<ICell>();

            if (tableRows != null)
            {
                var allRowsInOrder = tableRows.GetAllRowsInOrder();

                if (allRowsInOrder.Any(_ => _.GetNumberOfColumnsSpanned() != numberOfColumns))
                {
                    throw new ArgumentException(Invariant($"{nameof(tableRows)} contains a row or descendant row that does not span all {numberOfColumns} of the defined columns."));
                }

                if ((tableRows.HeaderRows != null) && tableRows.HeaderRows.Rows.Any())
                {
                    if (tableRows.HeaderRows.Rows.Last().Cells.Count != numberOfColumns)
                    {
                        throw new ArgumentException(Invariant($"The last row in {nameof(tableRows)}.{nameof(this.TableRows.HeaderRows)} does not contain one cell for all {numberOfColumns} of the defined columns.  Spanning is disallowed for the last header row."));
                    }
                }

                ids.AddRange(allRowsInOrder.Where(_ => !string.IsNullOrWhiteSpace(_.Id)).Select(_ => _.Id));

                allCells = new List<ICell>(allRowsInOrder.SelectMany(_ => _.Cells).ToList());

                var slottedCells = allCells.OfType<ISlottedCell>().SelectMany(_ => _.SlotIdToCellMap.Values).ToList();

                allCells.AddRange(slottedCells);

                allCellsWithIds = allCells.Where(_ => !string.IsNullOrWhiteSpace(_.Id)).ToList();

                ids.AddRange(allCellsWithIds.Select(_ => _.Id));

                var inputCellsWithoutId = allCells.Where(_ => _.IsInputCell()).Where(_ => string.IsNullOrWhiteSpace(_.Id)).ToList();

                if (inputCellsWithoutId.Any())
                {
                    throw new ArgumentException(Invariant($"One or more input cell(s) does not have an identifier and thus cannot be addressed."));
                }
            }

            if (ids.Distinct().Count() != ids.Count)
            {
                throw new ArgumentException(Invariant($"Two or more elements (i.e. columns, rows, cells) have the same identifier."));
            }

            this.cellIdToCellMap = allCellsWithIds.ToDictionary(_ => _.Id, _ => _);
            this.operationCells = allCells.Where(_ => _.IsOperationCell()).Cast<INotSlottedCell>().ToList();
            this.inputCells = allCells.Where(_ => _.IsInputCell()).Cast<INotSlottedCell>().ToList();
            this.validationCells = allCells.OfType<IValidationCell>().ToList();
            this.availabilityCheckCells = allCells.OfType<IAvailabilityCheckCell>().ToList();

            this.TableColumns = tableColumns;
            this.TableRows = tableRows;
            this.Format = format;
        }

        /// <summary>
        /// Gets the columns of the table.
        /// </summary>
        public TableColumns TableColumns { get; private set; }

        /// <summary>
        /// Gets the rows of the table.
        /// </summary>
        public TableRows TableRows { get; private set; }

        /// <summary>
        /// Gets the format to apply to the whole table.
        /// </summary>
        public TableFormat Format { get; private set; }

        /// <summary>
        /// Gets a map of cell id to the corresponding cell.
        /// </summary>
        /// <returns>
        /// A map of cell id to the corresponding cell.
        /// </returns>
        public IReadOnlyDictionary<string, ICell> GetCellIdToCellMap() => this.cellIdToCellMap;

        /// <summary>
        /// Gets all <see cref="IAvailabilityCheckCell"/>s.
        /// </summary>
        /// <returns>
        /// All <see cref="IAvailabilityCheckCell"/>s in the tree table.
        /// </returns>
        public IReadOnlyCollection<IAvailabilityCheckCell> GetAvailabilityCheckCells() => this.availabilityCheckCells;

        /// <summary>
        /// Gets all <see cref="IInputCell{TValue}"/>s.
        /// </summary>
        /// <returns>
        /// All <see cref="IInputCell{TValue}"/>s in the tree table.
        /// </returns>
        public IReadOnlyCollection<INotSlottedCell> GetInputCells() => this.inputCells;

        /// <summary>
        /// Gets all <see cref="IOperationOutputCell{TValue}"/>s.
        /// </summary>
        /// <returns>
        /// All <see cref="IOperationOutputCell{TValue}"/>s in the tree table.
        /// </returns>
        public IReadOnlyCollection<INotSlottedCell> GetOperationCells() => this.operationCells;

        /// <summary>
        /// Gets all <see cref="IValidationCell"/>s.
        /// </summary>
        /// <returns>
        /// All <see cref="IValidationCell"/>s in the tree table.
        /// </returns>
        public IReadOnlyCollection<IValidationCell> GetValidationCells() => this.validationCells;
    }
}