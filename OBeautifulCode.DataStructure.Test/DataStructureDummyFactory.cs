﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureDummyFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.DataStructure.Test source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Math.Recipes;

    /// <summary>
    /// A Dummy Factory for types in <see cref="OBeautifulCode.DataStructure"/>.
    /// </summary>
#if !OBeautifulCodeDataStructureSolution
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.DataStructure.Test", "See package version number")]
    internal
#else
    public
#endif
    class DataStructureDummyFactory : DefaultDataStructureDummyFactory
    {
        public DataStructureDummyFactory()
        {
            // <------------------- ENUMS ------------------------>
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MediaReferenceKind.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(BorderStyle.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(BorderWeight.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(FillPatternStyle.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(HorizontalAlignment.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(InnerBorderEdges.None);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(OuterBorderSides.None);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(VerticalAlignment.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(BytesPayloadLinkedResourceKind.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(LinkTarget.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(StringPayloadLinkedResourceKind.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(UrlLinkedResourceKind.Unknown);

            // <------------------- INTERFACES ------------------------>
            AutoFixtureBackedDummyFactory.AddDummyCreator<IColumnSpanningCell>(A.Dummy<ColumnSpanningStandardCellBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<IFormattableCell>(A.Dummy<StandardCellBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<ICell>(A.Dummy<CellBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<IHoverOver>(A.Dummy<HoverOverBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<ILink>(A.Dummy<LinkBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<ILinkedResource>(A.Dummy<LinkedResourceBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<IHaveValueCell>(() =>
            {
                IHaveValueCell result;

                do
                {
                    result = A.Dummy<StandardCellBase>() as IHaveValueCell;
                } while (result == null);
                
                return result;
            });

            // <------------------- CLASSES ------------------------>
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var columns = Some.ReadOnlyDummies<Column>().ToList();

                var tableColumns = new TableColumns(columns, A.Dummy<ColumnFormat>());

                var tableRows = BuildTableRows(columns.Count);

                var result = new TreeTable(tableColumns, tableRows, A.Dummy<TableFormat>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = BuildTableRows(ThreadSafeRandom.Next(1, 6));

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var headerRows = BuildFlatRows(ThreadSafeRandom.Next(1, 6));

                var result = new HeaderRows(headerRows, A.Dummy<HeaderRowsFormat>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var allDataRows = BuildDataRows(ThreadSafeRandom.Next(1, 6));

                var result = new DataRows(allDataRows, A.Dummy<DataRowsFormat>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = BuildDataRow(ThreadSafeRandom.Next(1, 6));

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var cells = Some.ReadOnlyDummies<IHaveValueCell>().Whose(_ => !_.Any(e => e is IColumnSpanningCell));

                var slotIdToCellMap = cells.ToDictionary(_ => A.Dummy<string>(), _ => _);

                var defaultSlotId = slotIdToCellMap.ElementAt(ThreadSafeRandom.Next(0, slotIdToCellMap.Count)).Key;

                var result = new SlottedCell(slotIdToCellMap, defaultSlotId);

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new ColumnSpanningDecimalCell(A.Dummy<decimal>(), A.Dummy<PositiveInteger>().ThatIs(_ => _ != 1), A.Dummy<string>(), A.Dummy<string>(), A.Dummy<CellFormat>(), A.Dummy<IHoverOver>(), A.Dummy<ILink>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new ColumnSpanningHtmlCell(A.Dummy<string>(), A.Dummy<PositiveInteger>().ThatIs(_ => _ != 1), A.Dummy<string>(), A.Dummy<CellFormat>(), A.Dummy<IHoverOver>(), A.Dummy<ILink>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new ColumnSpanningMediaReferenceCell(A.Dummy<MediaReference>(), A.Dummy<PositiveInteger>().ThatIs(_ => _ != 1), A.Dummy<string>(), A.Dummy<CellFormat>(), A.Dummy<IHoverOver>(), A.Dummy<ILink>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new ColumnSpanningNullCell(A.Dummy<PositiveInteger>().ThatIs(_ => _ != 1), A.Dummy<string>(), A.Dummy<string>(), A.Dummy<CellFormat>(), A.Dummy<IHoverOver>(), A.Dummy<ILink>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var slottedCell = A.Dummy<SlottedCell>();

                var result = new ColumnSpanningSlottedCell(slottedCell.SlotIdToCellMap, slottedCell.DefaultSlotId, A.Dummy<PositiveInteger>().ThatIs(_ => _ != 1));

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new ColumnSpanningStringCell(A.Dummy<string>(), A.Dummy<PositiveInteger>().ThatIs(_ => _ != 1), A.Dummy<string>(), A.Dummy<string>(), A.Dummy<CellFormat>(), A.Dummy<IHoverOver>(), A.Dummy<ILink>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = Color.FromArgb(A.Dummy<byte>(), A.Dummy<byte>(), A.Dummy<byte>());

                return result;
            });
        }

        private TableRows BuildTableRows(
            int numberOfColumns)
        {
            var allHeaderRows = BuildFlatRows(numberOfColumns);

            var headerRows = new HeaderRows(allHeaderRows, A.Dummy<HeaderRowsFormat>());

            var allDataRows = BuildDataRows(numberOfColumns);

            var dataRows = new DataRows(allDataRows, A.Dummy<DataRowsFormat>());

            var result = new TableRows(headerRows, dataRows, A.Dummy<RowFormat>());

            return result;
        }

        private IReadOnlyList<FlatRow> BuildFlatRows(
            int numberOfColumns)
        {
            var numberOfRows = ThreadSafeRandom.Next(0, 4);

            var result = new List<FlatRow>();

            for (var x = 0; x < numberOfRows; x++)
            {
                var allowSpanningCells = (x + 1) != numberOfRows;

                var headerRow = BuildFlatRow(numberOfColumns, allowSpanningCells);

                result.Add(headerRow);
            }

            return result;
        }

        private FlatRow BuildFlatRow(
            int numberOfColumns,
            bool allowSpanningCells)
        {
            var cells = BuildRowCells(numberOfColumns, allowSpanningCells);

            var result = new FlatRow(cells, A.Dummy<string>(), A.Dummy<RowFormat>());

            return result;

        }

        private IReadOnlyList<Row> BuildDataRows(
            int numberOfColumns,
            int depth = 0)
        {
            var numberOfRows = ThreadSafeRandom.Next(0, 4);

            var result = new List<Row>();

            for (var x = 0; x < numberOfRows; x++)
            {
                var dataRow = BuildDataRow(numberOfColumns, depth);

                result.Add(dataRow);
            }

            return result;
        }

        private Row BuildDataRow(
            int numberOfColumns,
            int depth = 0)
        {
            var cells = BuildRowCells(numberOfColumns, allowSpanningCells: true);

            var childRows = depth == 2
                ? new Row[0]
                : BuildDataRows(numberOfColumns, depth + 1);

            var expandedSummaryRow = childRows.Any()
                ? BuildFlatRow(numberOfColumns, allowSpanningCells: true)
                : null;

            var collapsedSummaryRow = childRows.Any()
                ? BuildFlatRow(numberOfColumns, allowSpanningCells: true)
                : null;

            var result = new Row(cells, A.Dummy<string>(), A.Dummy<RowFormat>(), childRows, expandedSummaryRow, collapsedSummaryRow);

            return result;
        }

        private IReadOnlyList<ICell> BuildRowCells(
            int numberOfColumns,
            bool allowSpanningCells)
        {
            var columnsSpanned = 0;

            var result = new List<ICell>();

            while (columnsSpanned != numberOfColumns)
            {
                ICell cell;

                if (allowSpanningCells && (ThreadSafeRandom.Next(0, 2) == 0) && ((columnsSpanned + 1) != numberOfColumns))
                {
                    var columnsSpannedByThisCell = ThreadSafeRandom.Next(2, (numberOfColumns - columnsSpanned + 1));

                    cell = A.Dummy<ColumnSpanningStandardCellBase>().DeepCloneWithColumnsSpanned(columnsSpannedByThisCell);

                    columnsSpanned += columnsSpannedByThisCell;
                }
                else
                {
                    cell = A.Dummy<StandardCellBase>().ThatIs(_ => !(_ is IColumnSpanningCell));

                    columnsSpanned += 1;
                }

                result.Add(cell);
            }

            return result;
        }
    }
}