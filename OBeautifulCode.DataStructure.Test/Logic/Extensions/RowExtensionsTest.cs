// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowExtensionsTest.cs" company="OBeautifulCode">
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

    public static class RowExtensionsTest
    {
        [Fact]
        public static void GetNumberOfColumnsSpanned___Should_throw_ArgumentNullException___When_parameter_row_is_null()
        {
            // Arrange
            var actual = Record.Exception(() => RowExtensions.GetNumberOfColumnsSpanned(null));

            // Act, Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("row");
        }

        [Fact]
        public static void GetNumberOfColumnsSpanned___Should_return_number_of_columns_spanned___When_called()
        {
            // Arrange
            var flatRowsAndExpected = new[]
            {
                new
                {
                    Row = new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(1).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                    Expected = 1,
                },
                new
                {
                    Row = new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(2).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                    Expected = 2,
                },
                new
                {
                    Row = new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                    Expected = 3,
                },
                new
                {
                    Row = new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(1).Select(_ => _.DeepCloneWithColumnsSpanned(2)).ToList()),
                    Expected = 2,
                },
                new
                {
                    Row = new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(1).Select(_ => _.DeepCloneWithColumnsSpanned(3)).ToList()),
                    Expected = 3,
                },
                new
                {
                    Row = new FlatRow(
                        new ICell[]
                        {
                            A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                            A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2),
                        }),
                    Expected = 3,
                },
                new
                {
                    Row = new FlatRow(
                        new ICell[]
                        {
                            A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2),
                            A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                            A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(3),
                        }),
                    Expected = 6,
                },
            };

            var dataRowsAndExpected = new[]
            {
                new
                {
                    Row = new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(1).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                    Expected = 1,
                },
                new
                {
                    Row = new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(2).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                    Expected = 2,
                },
                new
                {
                    Row = new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                    Expected = 3,
                },
                new
                {
                    Row = new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(1).Select(_ => _.DeepCloneWithColumnsSpanned(2)).ToList()),
                    Expected = 2,
                },
                new
                {
                    Row = new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(1).Select(_ => _.DeepCloneWithColumnsSpanned(3)).ToList()),
                    Expected = 3,
                },
                new
                {
                    Row = new Row(
                        new ICell[]
                        {
                            A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                            A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2),
                        }),
                    Expected = 3,
                },
                new
                {
                    Row = new Row(
                        new ICell[]
                        {
                            A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(2),
                            A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(1),
                            A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(3),
                        }),
                    Expected = 6,
                },
            };

            var expected1 = flatRowsAndExpected.Select(_ => _.Expected).ToList();
            var expected2 = dataRowsAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual1 = flatRowsAndExpected.Select(_ => _.Row.GetNumberOfColumnsSpanned()).ToList();
            var actual2 = dataRowsAndExpected.Select(_ => _.Row.GetNumberOfColumnsSpanned()).ToList();

            // Assert
            actual1.AsTest().Must().BeEqualTo(expected1);
            actual2.AsTest().Must().BeEqualTo(expected2);
        }

        [Fact]
        public static void GetAllRowsInOrder___Should_throw_ArgumentNullException___When_parameter_tableRows_is_null()
        {
            // Arrange
            var actual = Record.Exception(() => RowExtensions.GetAllRowsInOrder(null));

            // Act, Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("tableRows");
        }

        [Fact]
        public static void GetAllRowsInOrder___Should_return_all_rows_in_order___When_all_kinds_of_rows_present()
        {
            // Arrange
            var tableRows = TreeTableExtensionsTest.GetTableRowsForTesting().Item1;

            var expectedIds = Enumerable.Range(1, 17).Select(_ => _.ToString()).ToList();

            // Act
            var actual = tableRows.GetAllRowsInOrder();

            // Assert
            var actualIds = actual.Select(_ => _.Id).ToList();
            actualIds.AsTest().Must().BeEqualTo(expectedIds);
        }

        [Fact]
        public static void GetAllRowsInOrder___Should_return_all_rows_in_order___When_only_header_rows_present()
        {
            // Arrange
            var tableRows = new TableRows(headerRows: TreeTableExtensionsTest.GetHeaderRowsForTesting().Item1);

            var expectedIds = Enumerable.Range(1, 3).Select(_ => _.ToString()).ToList();

            // Act
            var actual = tableRows.GetAllRowsInOrder();

            // Assert
            var actualIds = actual.Select(_ => _.Id).ToList();
            actualIds.AsTest().Must().BeEqualTo(expectedIds);
        }

        [Fact]
        public static void GetAllRowsInOrder___Should_return_all_rows_in_order___When_only_data_rows_present()
        {
            // Arrange
            var tableRows = new TableRows(dataRows: TreeTableExtensionsTest.GetDataRowsForTesting().Item1);

            var expectedIds = Enumerable.Range(4, 11).Select(_ => _.ToString()).ToList();

            // Act
            var actual = tableRows.GetAllRowsInOrder();

            // Assert
            var actualIds = actual.Select(_ => _.Id).ToList();
            actualIds.AsTest().Must().BeEqualTo(expectedIds);
        }

        [Fact]
        public static void GetAllRowsInOrder___Should_return_all_rows_in_order___When_only_footer_rows_present()
        {
            // Arrange
            var tableRows = new TableRows(footerRows: TreeTableExtensionsTest.GetFooterRowsForTesting().Item1);

            var expectedIds = Enumerable.Range(15, 3).Select(_ => _.ToString()).ToList();

            // Act
            var actual = tableRows.GetAllRowsInOrder();

            // Assert
            var actualIds = actual.Select(_ => _.Id).ToList();
            actualIds.AsTest().Must().BeEqualTo(expectedIds);
        }

        [Fact]
        public static void GetAllCells___Should_throw_ArgumentNullException___When_parameter_tableRows_is_null()
        {
            // Arrange
            var actual = Record.Exception(() => RowExtensions.GetAllCells(null));

            // Act, Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("tableRows");
        }

        [Fact]
        public static void GetAllCells___Should_return_all_rows_in_order___When_all_kinds_of_rows_present()
        {
            // Arrange
            var tableRowsResult = TreeTableExtensionsTest.GetTableRowsForTesting();

            var expected = tableRowsResult.Item2;

            // Act
            var actual = tableRowsResult.Item1.GetAllCells();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetAllCells___Should_return_all_rows_in_order___When_only_header_rows_present()
        {
            // Arrange
            var headerRowsResult = TreeTableExtensionsTest.GetHeaderRowsForTesting();

            var tableRows = new TableRows(headerRows: headerRowsResult.Item1);

            var expected = headerRowsResult.Item2;

            // Act
            var actual = tableRows.GetAllCells();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetAllCells___Should_return_all_rows_in_order___When_only_data_rows_present()
        {
            // Arrange
            var dataRowsResult = TreeTableExtensionsTest.GetDataRowsForTesting();

            var tableRows = new TableRows(dataRows: dataRowsResult.Item1);

            var expected = dataRowsResult.Item2;

            // Act
            var actual = tableRows.GetAllCells();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetAllCells___Should_return_all_rows_in_order___When_only_footer_rows_present()
        {
            // Arrange
            var footerRowsResult = TreeTableExtensionsTest.GetFooterRowsForTesting();

            var tableRows = new TableRows(footerRows: footerRowsResult.Item1);

            var expected = footerRowsResult.Item2;

            // Act
            var actual = tableRows.GetAllCells();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }
    }
}
