// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableRowsExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Linq;
    using OBeautifulCode.Assertion.Recipes;
    using Xunit;

    public static class TableRowsExtensionsTest
    {
        [Fact]
        public static void GetAllRowsInOrder___Should_throw_ArgumentNullException___When_parameter_tableRows_is_null()
        {
            // Arrange
            var actual = Record.Exception(() => TableRowsExtensions.GetAllRowsInOrder(null));

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
            var actual = Record.Exception(() => TableRowsExtensions.GetAllCells(null));

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
