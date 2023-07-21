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
        public static void ToReport___Should_throw_ArgumentNullException___When_parameter_treeTable_is_null()
        {
            // Arrange
            var actual = Record.Exception(() => TreeTableExtensions.ToReport(null));

            // Act, Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("treeTable");
        }

        [Fact]
        public static void ToReport___Should_return_single_section_report_containing_tree_table___When_called()
        {
            // Arrange
            var treeTable = A.Dummy<TreeTable>();

            // Act
            var actual = treeTable.ToReport();

            // Assert
            actual.Sections.AsTest().Must().HaveCount(1);
            actual.Sections.First().TreeTable.AsTest().Must().BeSameReferenceAs(treeTable);
        }

        [Fact]
        public static void ToSection___Should_throw_ArgumentNullException___When_parameter_treeTable_is_null()
        {
            // Arrange
            var actual = Record.Exception(() => TreeTableExtensions.ToSection(null));

            // Act, Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("treeTable");
        }

        [Fact]
        public static void ToSection___Should_return_section_containing_tree_table___When_called()
        {
            // Arrange
            var treeTable = A.Dummy<TreeTable>();

            // Act
            var actual = treeTable.ToSection();

            // Assert
            actual.TreeTable.AsTest().Must().BeSameReferenceAs(treeTable);
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
