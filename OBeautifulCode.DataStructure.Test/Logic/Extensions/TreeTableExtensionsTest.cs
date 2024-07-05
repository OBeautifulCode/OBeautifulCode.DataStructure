// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeTableExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Math.Recipes;
    using Xunit;

    public static class TreeTableExtensionsTest
    {
        [Fact]
        public static void GetAllRowsInOrder___Should_throw_ArgumentNullException___When_parameter_treeTable_is_null()
        {
            // Arrange
            var actual = Record.Exception(() => TreeTableExtensions.GetAllRowsInOrder(null));

            // Act, Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("treeTable");
        }

        [Fact]
        public static void GetAllRowsInOrder___Should_return_all_rows_in_order___When_all_kinds_of_rows_present()
        {
            // Arrange
            var tableRows = GetTableRowsForTesting().Item1;

            var treeTable = new TreeTable(new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList()), tableRows);

            var expectedIds = Enumerable.Range(1, 17).Select(_ => _.ToString()).ToList();

            // Act
            var actual = treeTable.GetAllRowsInOrder();

            // Assert
            var actualIds = actual.Select(_ => _.Id).ToList();
            actualIds.AsTest().Must().BeEqualTo(expectedIds);
        }

        [Fact]
        public static void GetAllRowsInOrder___Should_return_all_rows_in_order___When_only_header_rows_present()
        {
            // Arrange
            var tableRows = new TableRows(headerRows: GetHeaderRowsForTesting().Item1);

            var treeTable = new TreeTable(new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList()), tableRows);

            var expectedIds = Enumerable.Range(1, 3).Select(_ => _.ToString()).ToList();

            // Act
            var actual = treeTable.GetAllRowsInOrder();

            // Assert
            var actualIds = actual.Select(_ => _.Id).ToList();
            actualIds.AsTest().Must().BeEqualTo(expectedIds);
        }

        [Fact]
        public static void GetAllRowsInOrder___Should_return_all_rows_in_order___When_only_data_rows_present()
        {
            // Arrange
            var tableRows = new TableRows(dataRows: GetDataRowsForTesting().Item1);

            var treeTable = new TreeTable(new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList()), tableRows);

            var expectedIds = Enumerable.Range(4, 11).Select(_ => _.ToString()).ToList();

            // Act
            var actual = treeTable.GetAllRowsInOrder();

            // Assert
            var actualIds = actual.Select(_ => _.Id).ToList();
            actualIds.AsTest().Must().BeEqualTo(expectedIds);
        }

        [Fact]
        public static void GetAllRowsInOrder___Should_return_all_rows_in_order___When_only_footer_rows_present()
        {
            // Arrange
            var tableRows = new TableRows(footerRows: GetFooterRowsForTesting().Item1);

            var treeTable = new TreeTable(new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList()), tableRows);

            var expectedIds = Enumerable.Range(15, 3).Select(_ => _.ToString()).ToList();

            // Act
            var actual = treeTable.GetAllRowsInOrder();

            // Assert
            var actualIds = actual.Select(_ => _.Id).ToList();
            actualIds.AsTest().Must().BeEqualTo(expectedIds);
        }

        [Fact]
        public static void GetAllCells___Should_throw_ArgumentNullException___When_parameter_treeTable_is_null()
        {
            // Arrange
            var actual = Record.Exception(() => TreeTableExtensions.GetAllCells(null));

            // Act, Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("treeTable");
        }

        [Fact]
        public static void GetAllCells___Should_return_all_rows_in_order___When_all_kinds_of_rows_present()
        {
            // Arrange
            var tableRowsResult = GetTableRowsForTesting();

            var treeTable = new TreeTable(new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList()), tableRowsResult.Item1);

            var expected = tableRowsResult.Item2;

            // Act
            var actual = treeTable.GetAllCells();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetAllCells___Should_return_all_rows_in_order___When_only_header_rows_present()
        {
            // Arrange
            var headerRowsResult = GetHeaderRowsForTesting();

            var tableRows = new TableRows(headerRows: headerRowsResult.Item1);

            var treeTable = new TreeTable(new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList()), tableRows);

            var expected = headerRowsResult.Item2;

            // Act
            var actual = treeTable.GetAllCells();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetAllCells___Should_return_all_rows_in_order___When_only_data_rows_present()
        {
            // Arrange
            var dataRowsResult = GetDataRowsForTesting();

            var tableRows = new TableRows(dataRows: dataRowsResult.Item1);

            var treeTable = new TreeTable(new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList()), tableRows);

            var expected = dataRowsResult.Item2;

            // Act
            var actual = treeTable.GetAllCells();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetAllCells___Should_return_all_rows_in_order___When_only_footer_rows_present()
        {
            // Arrange
            var footerRowsResult = GetFooterRowsForTesting();

            var tableRows = new TableRows(footerRows: footerRowsResult.Item1);

            var treeTable = new TreeTable(new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList()), tableRows);

            var expected = footerRowsResult.Item2;

            // Act
            var actual = treeTable.GetAllCells();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ReplaceCell___Should_throw_ArgumentNullException___When_parameter_treeTable_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TreeTableExtensions.ReplaceCell(null, A.Dummy<ICell>(), A.Dummy<ICell>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("treeTable");
        }

        [Fact]
        public static void ReplaceCell___Should_throw_ArgumentNullException___When_parameter_cellToReplace_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => A.Dummy<TreeTable>().ReplaceCell(null, A.Dummy<ICell>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cellToReplace");
        }

        [Fact]
        public static void ReplaceCell___Should_throw_ArgumentNullException___When_parameter_cellToUse_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => A.Dummy<TreeTable>().ReplaceCell(A.Dummy<ICell>(), null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cellToUse");
        }

        [Fact]
        public static void ReplaceCell___Should_throw_ArgumentException___When_parameter_cellToReplace_does_not_span_the_same_number_of_columns_as_parameter_cellToUse()
        {
            // Arrange
            var treeTable = A.Dummy<TreeTable>();
            var cellToReplace = A.Dummy<ICell>();
            var cellToUse = A.Dummy<ICell>().Whose(_ => (_.ColumnsSpanned ?? 1) != (cellToReplace.ColumnsSpanned ?? 1));

            // Act
            var actual = Record.Exception(() => treeTable.ReplaceCell(cellToReplace, cellToUse));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("cellToReplace and cellToUse do not span the same number of columns");
        }

        [Fact]
        public static void ReplaceCell___Should_throw_InvalidOperationException___When_cellToReplace_does_not_exist_in_tree_table()
        {
            // Arrange
            var treeTable = A.Dummy<TreeTable>();
            var cellToReplace = A.Dummy<ICell>();
            var cellToUse = A.Dummy<ICell>().Whose(_ => (_.ColumnsSpanned ?? 1) == (cellToReplace.ColumnsSpanned ?? 1));

            // Act
            var actual = Record.Exception(() => treeTable.ReplaceCell(cellToReplace, cellToUse));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().ContainString("cellToReplace was not found.");
        }

        [Fact]
        public static void ReplaceCell___Should_throw_InvalidOperationException___When_cellToReplace_is_contained_within_a_slot_but_cellToUse_is_not_an_INotSlottedCell()
        {
            // Arrange
            var treeTable = A.Dummy<TreeTable>().Whose(_ => _.GetAllCells().OfType<ISlottedCell>().Any());
            var slottedCells = treeTable.GetAllCells().OfType<ISlottedCell>().ToList();
            var slottedCell = slottedCells[ThreadSafeRandom.Next(0, slottedCells.Count)];
            var cellToReplace = slottedCell.SlotIdToCellMap.ElementAt(ThreadSafeRandom.Next(0, slottedCell.SlotIdToCellMap.Count)).Value;
            var cellToUse = A.Dummy<SlottedCell>().Whose(_ => (_.ColumnsSpanned ?? 1) == (cellToReplace.ColumnsSpanned ?? 1));

            // Act
            var actual = Record.Exception(() => treeTable.ReplaceCell(cellToReplace, cellToUse));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().ContainString("cellToReplace was found in a slot of an ISlottedCell, but cellToUse is not an INotSlottedCell.");
        }

        [Fact]
        public static void ReplaceCell___Should_throw_InvalidOperationException_with_ArgumentException_in_InnerException___When_the_replacement_results_in_a_malformed_tree_table()
        {
            // Arrange
            var treeTable = A.Dummy<TreeTable>().Whose(_ => _.GetAllCells().Count > 2);
            var cells = treeTable.GetAllCells().ToList();
            var randomCellIndex = ThreadSafeRandom.Next(0, cells.Count);
            var cellToReplace = cells[randomCellIndex];
            cells.RemoveAt(randomCellIndex);
            var cellToUse = A.Dummy<ConstCell<Version>>()  // use an INotSlottedCell, which can be used to replace a cell within a slot or a cell within a row
                .Whose(_ => (_.ColumnsSpanned ?? 1) == (cellToReplace.ColumnsSpanned ?? 1))
                .DeepCloneWithId(cells[ThreadSafeRandom.Next(0, cells.Count)].Id);

            // Act
            var actual = Record.Exception(() => treeTable.ReplaceCell(cellToReplace, cellToUse));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().ContainString("Replacing cellToReplace with cellToUse results in a malformed TreeTable.  See inner exception.");
            actual.InnerException.AsTest().Must().BeOfType<ArgumentException>();
            actual.InnerException.Message.AsTest().Must().ContainString("Two or more elements (i.e. columns, rows, cells) have the same identifier");
        }

        [Fact]
        public static void ReplaceCell___Should_replace_cell_within_header_row___When_called()
        {
            // Arrange
            var cellToUse = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);

            var tableColumns = new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList());

            var headerRow1Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);
            var headerRow1Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var headerRow2Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var headerRow2Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var actualAllHeaderRows = new[]
            {
                new FlatRow(
                    new ICell[]
                    {
                        headerRow1Cell1,
                        headerRow1Cell2,
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        headerRow2Cell1,
                        headerRow2Cell2,
                    }),
            };

            var expectedAllHeaderRows = new[]
            {
                new FlatRow(
                    new ICell[]
                    {
                        headerRow1Cell1,
                        headerRow1Cell2,
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        headerRow2Cell1,
                        cellToUse,
                    }),
            };

            var actualHeaderRows = new HeaderRows(actualAllHeaderRows);
            var expectedHeaderRows = new HeaderRows(expectedAllHeaderRows);

            var dataRow1Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(3);
            var dataRow2Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(null)).ToList();
            var dataRow3Cells = new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(null)).ToList());
            var dataRow4Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var defaultSlot = A.Dummy<string>();
            var otherSlot = A.Dummy<string>();
            var dataRow4Cell2Slot1 = (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);
            var dataRow4Cell2Slot2 = (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var dataRow4Cell2 = new SlottedCell(
                new Dictionary<string, INotSlottedCell>
                {
                    { otherSlot, dataRow4Cell2Slot1 },
                    { defaultSlot, dataRow4Cell2Slot2 },
                },
                defaultSlot);

            var allDataRows = new[]
            {
                new Row(new[] { dataRow1Cell1 }),
                new Row(
                    dataRow2Cells,
                    childRows: new[]
                    {
                        dataRow3Cells,
                        new Row(
                            new ICell[]
                            {
                                dataRow4Cell1,
                                dataRow4Cell2,
                            }),
                    }),
            };

            var dataRows = new DataRows(allDataRows);

            var footerRow1Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);
            var footerRow1Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var footerRow2Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);
            var footerRow2Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);
            var footerRow2Cell3 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var allFooterRows = new[]
            {
                new FlatRow(
                    new ICell[]
                    {
                        footerRow1Cell1,
                        footerRow1Cell2,
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        footerRow2Cell1,
                        footerRow2Cell2,
                        footerRow2Cell3,
                    }),
            };

            var footerRows = new FooterRows(allFooterRows);
            var expectedTableRows = new TableRows(expectedHeaderRows, dataRows, footerRows);
            var actualTableRows = new TableRows(actualHeaderRows, dataRows, footerRows);
            var actual = new TreeTable(tableColumns, actualTableRows);
            var expected = new TreeTable(tableColumns, expectedTableRows);

            // Act
            actual.ReplaceCell(headerRow2Cell2, cellToUse);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ReplaceCell___Should_replace_cell_within_data_row___When_called()
        {
            // Arrange
            var cellToUse = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);

            var tableColumns = new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList());

            var headerRow1Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);
            var headerRow1Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var headerRow2Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var headerRow2Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var allHeaderRows = new[]
            {
                new FlatRow(
                    new ICell[]
                    {
                        headerRow1Cell1,
                        headerRow1Cell2,
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        headerRow2Cell1,
                        headerRow2Cell2,
                    }),
            };

            var headerRows = new HeaderRows(allHeaderRows);

            var dataRow1Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(3);
            var dataRow2Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(null)).ToList();
            var dataRow3Cells = new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(null)).ToList());
            var dataRow4Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var defaultSlot = A.Dummy<string>();
            var otherSlot = A.Dummy<string>();
            var dataRow4Cell2Slot1 = (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);
            var dataRow4Cell2Slot2 = (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var dataRow4Cell2 = new SlottedCell(
                new Dictionary<string, INotSlottedCell>
                {
                    { otherSlot, dataRow4Cell2Slot1 },
                    { defaultSlot, dataRow4Cell2Slot2 },
                },
                defaultSlot);

            var allActualDataRows = new[]
            {
                new Row(new[] { dataRow1Cell1 }),
                new Row(
                    dataRow2Cells,
                    childRows: new[]
                    {
                        dataRow3Cells,
                        new Row(
                            new ICell[]
                            {
                                dataRow4Cell1,
                                dataRow4Cell2,
                            }),
                    }),
            };

            var allExpectedDataRows = new[]
            {
                new Row(new[] { dataRow1Cell1 }),
                new Row(
                    dataRow2Cells,
                    childRows: new[]
                    {
                        dataRow3Cells,
                        new Row(
                            new ICell[]
                            {
                                cellToUse,
                                dataRow4Cell2,
                            }),
                    }),
            };

            var expectedDataRows = new DataRows(allExpectedDataRows);
            var actualDataRows = new DataRows(allActualDataRows);

            var footerRow1Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);
            var footerRow1Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var footerRow2Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);
            var footerRow2Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);
            var footerRow2Cell3 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var allFooterRows = new[]
            {
                new FlatRow(
                    new ICell[]
                    {
                        footerRow1Cell1,
                        footerRow1Cell2,
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        footerRow2Cell1,
                        footerRow2Cell2,
                        footerRow2Cell3,
                    }),
            };

            var footerRows = new FooterRows(allFooterRows);
            var expectedTableRows = new TableRows(headerRows, expectedDataRows, footerRows);
            var actualTableRows = new TableRows(headerRows, actualDataRows, footerRows);
            var actual = new TreeTable(tableColumns, actualTableRows);
            var expected = new TreeTable(tableColumns, expectedTableRows);

            // Act
            actual.ReplaceCell(dataRow4Cell1, cellToUse);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ReplaceCell___Should_replace_cell_within_footer_row___When_called()
        {
            // Arrange
            var cellToUse = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var tableColumns = new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList());

            var headerRow1Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);
            var headerRow1Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var headerRow2Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var headerRow2Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var allHeaderRows = new[]
            {
                new FlatRow(
                    new ICell[]
                    {
                        headerRow1Cell1,
                        headerRow1Cell2,
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        headerRow2Cell1,
                        headerRow2Cell2,
                    }),
            };

            var headerRows = new HeaderRows(allHeaderRows);

            var dataRow1Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(3);
            var dataRow2Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(null)).ToList();
            var dataRow3Cells = new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(null)).ToList());
            var dataRow4Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var defaultSlot = A.Dummy<string>();
            var otherSlot = A.Dummy<string>();
            var dataRow4Cell2Slot1 = (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);
            var dataRow4Cell2Slot2 = (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var dataRow4Cell2 = new SlottedCell(
                new Dictionary<string, INotSlottedCell>
                {
                    { otherSlot, dataRow4Cell2Slot1 },
                    { defaultSlot, dataRow4Cell2Slot2 },
                },
                defaultSlot);

            var allDataRows = new[]
            {
                new Row(new[] { dataRow1Cell1 }),
                new Row(
                    dataRow2Cells,
                    childRows: new[]
                    {
                        dataRow3Cells,
                        new Row(
                            new ICell[]
                            {
                                dataRow4Cell1,
                                dataRow4Cell2,
                            }),
                    }),
            };

            var dataRows = new DataRows(allDataRows);

            var footerRow1Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);
            var footerRow1Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var footerRow2Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);
            var footerRow2Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);
            var footerRow2Cell3 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var actualAllFooterRows = new[]
            {
                new FlatRow(
                    new ICell[]
                    {
                        footerRow1Cell1,
                        footerRow1Cell2,
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        footerRow2Cell1,
                        footerRow2Cell2,
                        footerRow2Cell3,
                    }),
            };

            var expectedAllFooterRows = new[]
            {
                new FlatRow(
                    new ICell[]
                    {
                        footerRow1Cell1,
                        footerRow1Cell2,
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        footerRow2Cell1,
                        cellToUse,
                        footerRow2Cell3,
                    }),
            };

            var actualFooterRows = new FooterRows(actualAllFooterRows);
            var expectedFooterRows = new FooterRows(expectedAllFooterRows);
            var expectedTableRows = new TableRows(headerRows, dataRows, expectedFooterRows);
            var actualTableRows = new TableRows(headerRows, dataRows, actualFooterRows);
            var actual = new TreeTable(tableColumns, actualTableRows);
            var expected = new TreeTable(tableColumns, expectedTableRows);

            // Act
            actual.ReplaceCell(footerRow2Cell2, cellToUse);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ReplaceCell___Should_replace_cell_in_slot___When_called()
        {
            // Arrange
            var cellToUse = (NotSlottedCellBase)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var tableColumns = new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList());

            var headerRow1Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);
            var headerRow1Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var headerRow2Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var headerRow2Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var allHeaderRows = new[]
            {
                new FlatRow(
                    new ICell[]
                    {
                        headerRow1Cell1,
                        headerRow1Cell2,
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        headerRow2Cell1,
                        headerRow2Cell2,
                    }),
            };

            var headerRows = new HeaderRows(allHeaderRows);

            var dataRow1Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(3);
            var dataRow2Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(null)).ToList();
            var dataRow3Cells = new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(null)).ToList());
            var dataRow4Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var defaultSlot = A.Dummy<string>();
            var otherSlot = A.Dummy<string>();
            var dataRow4Cell2Slot1 = (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);
            var dataRow4Cell2Slot2 = (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var actualDataRow4Cell2 = new SlottedCell(
                new Dictionary<string, INotSlottedCell>
                {
                    { otherSlot, dataRow4Cell2Slot1 },
                    { defaultSlot, dataRow4Cell2Slot2 },
                },
                defaultSlot);

            var expectedDataRow4Cell2 = new SlottedCell(
                new Dictionary<string, INotSlottedCell>
                {
                    { otherSlot, dataRow4Cell2Slot1 },
                    { defaultSlot, cellToUse },
                },
                defaultSlot);

            var allActualDataRows = new[]
            {
                new Row(new[] { dataRow1Cell1 }),
                new Row(
                    dataRow2Cells,
                    childRows: new[]
                    {
                        dataRow3Cells,
                        new Row(
                            new ICell[]
                            {
                                dataRow4Cell1,
                                actualDataRow4Cell2,
                            }),
                    }),
            };

            var allExpectedDataRows = new[]
            {
                new Row(new[] { dataRow1Cell1 }),
                new Row(
                    dataRow2Cells,
                    childRows: new[]
                    {
                        dataRow3Cells,
                        new Row(
                            new ICell[]
                            {
                                dataRow4Cell1,
                                expectedDataRow4Cell2,
                            }),
                    }),
            };

            var expectedDataRows = new DataRows(allExpectedDataRows);
            var actualDataRows = new DataRows(allActualDataRows);

            var footerRow1Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);
            var footerRow1Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2);
            var footerRow2Cell1 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);
            var footerRow2Cell2 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(null);
            var footerRow2Cell3 = A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1);

            var allFooterRows = new[]
            {
                new FlatRow(
                    new ICell[]
                    {
                        footerRow1Cell1,
                        footerRow1Cell2,
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        footerRow2Cell1,
                        footerRow2Cell2,
                        footerRow2Cell3,
                    }),
            };

            var footerRows = new FooterRows(allFooterRows);
            var expectedTableRows = new TableRows(headerRows, expectedDataRows, footerRows);
            var actualTableRows = new TableRows(headerRows, actualDataRows, footerRows);
            var actual = new TreeTable(tableColumns, actualTableRows);
            var expected = new TreeTable(tableColumns, expectedTableRows);

            // Act
            actual.ReplaceCell(dataRow4Cell2Slot2, cellToUse);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        public static Tuple<TableRows, IReadOnlyCollection<ICell>> GetTableRowsForTesting()
        {
            var headerRowsResult = GetHeaderRowsForTesting();

            var dataRowsResult = GetDataRowsForTesting();

            var footerRowsResult = GetFooterRowsForTesting();

            var tableRows = new TableRows(headerRowsResult.Item1, dataRowsResult.Item1, footerRowsResult.Item1);

            var allCells = new ICell[0]
                .Concat(headerRowsResult.Item2)
                .Concat(dataRowsResult.Item2)
                .Concat(footerRowsResult.Item2)
                .ToList();

            var result = new Tuple<TableRows, IReadOnlyCollection<ICell>>(tableRows, allCells);

            return result;
        }

        public static Tuple<HeaderRows, IReadOnlyCollection<ICell>> GetHeaderRowsForTesting()
        {
            var row1Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();
            var row2SlottedCells = new[]
            {
                (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
            };
            var row2Cells = new ICell[]
            {
                A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                new SlottedCell(
                    new Dictionary<string, INotSlottedCell>
                    {
                        { "first", row2SlottedCells[0] },
                    },
                    "first"),
                new SlottedCell(
                    new Dictionary<string, INotSlottedCell>
                    {
                        { "second", row2SlottedCells[1] },
                        { "third", row2SlottedCells[2] },
                    },
                    "second"),
            };
            var row3Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();

            var allHeaderRows = new[]
            {
                new FlatRow(row1Cells, "1"),
                new FlatRow(row2Cells, "2"),
                new FlatRow(row3Cells, "3"),
            };

            var allCells = new ICell[0]
                .Concat(row1Cells)
                .Concat(row2SlottedCells)
                .Concat(row2Cells)
                .Concat(row3Cells)
                .ToList();

            var headerRows = new HeaderRows(allHeaderRows);

            var result = new Tuple<HeaderRows, IReadOnlyCollection<ICell>>(headerRows, allCells);

            return result;
        }

        public static Tuple<DataRows, IReadOnlyCollection<ICell>> GetDataRowsForTesting()
        {
            var row1Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();
            var row1Child1Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();
            var row1Child2Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();
            var row11Child2Grandchild1Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();
            var row11Child2Grandchild2Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();

            var row2Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();
            var row2Child1Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();
            var row2Child1Grandchild1Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();
            var row2Child1Grandchild2Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();
            var row2Child2Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();
            var row3SlottedCells = new[]
            {
                (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
            };
            var row3Cells = new ICell[]
            {
                A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                new SlottedCell(
                    new Dictionary<string, INotSlottedCell>
                    {
                        { "second", row3SlottedCells[0] },
                        { "third", row3SlottedCells[1] },
                    },
                    "second"),
            };

            var allDataRows = new RowBase[]
            {
                new Row(
                    row1Cells,
                    "4",
                    childRows: new[]
                    {
                        new Row(
                            row1Child1Cells,
                            "5"),
                        new Row(
                            row1Child2Cells,
                            "6",
                            childRows: new RowBase[]
                            {
                                new Row(row11Child2Grandchild1Cells, "7"),
                                new FlatRow(row11Child2Grandchild2Cells, "8"),
                            }),
                    }),
                new Row(
                    row2Cells,
                    "9",
                    childRows: new[]
                    {
                        new Row(
                            row2Child1Cells,
                            "10",
                            childRows: new RowBase[]
                            {
                                new FlatRow(row2Child1Grandchild1Cells, "11"),
                                new Row(row2Child1Grandchild2Cells, "12"),
                            }),
                        new Row(row2Child2Cells, "13"),
                    }),
                new FlatRow(row3Cells, "14"),
            };

            var allCells = new ICell[0]
                .Concat(row1Cells)
                .Concat(row1Child1Cells)
                .Concat(row1Child2Cells)
                .Concat(row11Child2Grandchild1Cells)
                .Concat(row11Child2Grandchild2Cells)
                .Concat(row2Cells)
                .Concat(row2Child1Cells)
                .Concat(row2Child1Grandchild1Cells)
                .Concat(row2Child1Grandchild2Cells)
                .Concat(row2Child2Cells)
                .Concat(row3SlottedCells)
                .Concat(row3Cells)
                .ToList();

            var dataRows = new DataRows(allDataRows);

            var result = new Tuple<DataRows, IReadOnlyCollection<ICell>>(dataRows, allCells);

            return result;
        }

        public static Tuple<FooterRows, IReadOnlyCollection<ICell>> GetFooterRowsForTesting()
        {
            var row1Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();
            var row2Cells = Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList();
            var row3SlottedCells = new[]
            {
                (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                (INotSlottedCell)A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
            };
            var row3Cells = new ICell[]
            {
                A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                new SlottedCell(
                    new Dictionary<string, INotSlottedCell>
                    {
                        { "first", row3SlottedCells[0] },
                        { "second", row3SlottedCells[1] },
                        { "third", row3SlottedCells[2] },
                    },
                    "second"),
            };

            var allFooterRows = new[]
            {
                new FlatRow(row1Cells, "15"),
                new FlatRow(row2Cells, "16"),
                new FlatRow(row3Cells, "17"),
            };

            var allCells = new ICell[0]
                .Concat(row1Cells)
                .Concat(row2Cells)
                .Concat(row3SlottedCells)
                .Concat(row3Cells)
                .ToList();

            var footerRows = new FooterRows(allFooterRows);

            var result = new Tuple<FooterRows, IReadOnlyCollection<ICell>>(footerRows, allCells);

            return result;
        }
    }
}
