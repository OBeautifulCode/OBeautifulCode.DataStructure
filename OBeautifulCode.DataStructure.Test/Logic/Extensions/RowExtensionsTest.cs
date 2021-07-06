// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
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
                    Row = new FlatRow(Some.ReadOnlyDummies<DecimalCell>(1).ToList()),
                    Expected = 1,
                },
                new
                {
                    Row = new FlatRow(Some.ReadOnlyDummies<DecimalCell>(2).ToList()),
                    Expected = 2,
                },
                new
                {
                    Row = new FlatRow(Some.ReadOnlyDummies<DecimalCell>(3).ToList()),
                    Expected = 3,
                },
                new
                {
                    Row = new FlatRow(
                        new ICell[]
                        {
                            new ColumnSpanningHtmlCell(A.Dummy<string>(), 2),
                        }),
                    Expected = 2,
                },
                new
                {
                    Row = new FlatRow(
                        new ICell[]
                        {
                            new ColumnSpanningHtmlCell(A.Dummy<string>(), 3),
                        }),
                    Expected = 3,
                },
                new
                {
                    Row = new FlatRow(
                        new ICell[]
                        {
                            new HtmlCell(A.Dummy<string>()),
                            new ColumnSpanningHtmlCell(A.Dummy<string>(), 2),
                        }),
                    Expected = 3,
                },
                new
                {
                    Row = new FlatRow(
                        new ICell[]
                        {
                            new ColumnSpanningHtmlCell(A.Dummy<string>(), 2),
                            new HtmlCell(A.Dummy<string>()),
                            new ColumnSpanningHtmlCell(A.Dummy<string>(), 3),
                        }),
                    Expected = 6,
                },
            };

            var dataRowsAndExpected = new[]
            {
                new
                {
                    Row = new Row(Some.ReadOnlyDummies<DecimalCell>(1).ToList()),
                    Expected = 1,
                },
                new
                {
                    Row = new Row(Some.ReadOnlyDummies<DecimalCell>(2).ToList()),
                    Expected = 2,
                },
                new
                {
                    Row = new Row(Some.ReadOnlyDummies<DecimalCell>(3).ToList()),
                    Expected = 3,
                },
                new
                {
                    Row = new Row(
                        new ICell[]
                        {
                            new ColumnSpanningHtmlCell(A.Dummy<string>(), 2),
                        }),
                    Expected = 2,
                },
                new
                {
                    Row = new Row(
                        new ICell[]
                        {
                            new ColumnSpanningHtmlCell(A.Dummy<string>(), 3),
                        }),
                    Expected = 3,
                },
                new
                {
                    Row = new Row(
                        new ICell[]
                        {
                            new HtmlCell(A.Dummy<string>()),
                            new ColumnSpanningHtmlCell(A.Dummy<string>(), 2),
                        }),
                    Expected = 3,
                },
                new
                {
                    Row = new Row(
                        new ICell[]
                        {
                            new ColumnSpanningHtmlCell(A.Dummy<string>(), 2),
                            new HtmlCell(A.Dummy<string>()),
                            new ColumnSpanningHtmlCell(A.Dummy<string>(), 3),
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
        public static void GetAllRowsInOrder___Should_return_all_rows_in_order___When_called()
        {
            // Arrange
            var allHeaderRows = new[]
            {
                new FlatRow(Some.ReadOnlyDummies<DecimalCell>(3).ToList(), "1"),
                new FlatRow(Some.ReadOnlyDummies<DecimalCell>(3).ToList(), "2"),
                new FlatRow(Some.ReadOnlyDummies<DecimalCell>(3).ToList(), "3"),
            };

            var headerRows = new HeaderRows(allHeaderRows);

            var allDataRows = new[]
            {
                new Row(
                    Some.ReadOnlyDummies<DecimalCell>(3).ToList(),
                    "4",
                    childRows: new[]
                    {
                        new Row(Some.ReadOnlyDummies<DecimalCell>(3).ToList(), "5"),
                        new Row(
                            Some.ReadOnlyDummies<DecimalCell>(3).ToList(),
                            "6",
                            childRows: new[]
                            {
                                new Row(Some.ReadOnlyDummies<DecimalCell>(3).ToList(), "7"),
                                new Row(Some.ReadOnlyDummies<DecimalCell>(3).ToList(), "8"),
                            }),
                    }),
                new Row(
                    Some.ReadOnlyDummies<DecimalCell>(3).ToList(),
                    "9",
                    childRows: new[]
                    {
                        new Row(
                            Some.ReadOnlyDummies<DecimalCell>(3).ToList(),
                            "10",
                            childRows: new[]
                            {
                                new Row(Some.ReadOnlyDummies<DecimalCell>(3).ToList(), "11"),
                                new Row(Some.ReadOnlyDummies<DecimalCell>(3).ToList(), "12"),
                            }),
                        new Row(Some.ReadOnlyDummies<DecimalCell>(3).ToList(), "13"),
                    }),
                new Row(Some.ReadOnlyDummies<DecimalCell>(3).ToList(), "14"),
            };

            var dataRows = new DataRows(allDataRows);

            var tableRows = new TableRows(headerRows, dataRows);

            var expectedIds = Enumerable.Range(1, 14).Select(_ => _.ToString()).ToList();

            // Act
            var actual = tableRows.GetAllRowsInOrder();

            // Assert
            var actualIds = actual.Select(_ => _.Id).ToList();
            actualIds.AsTest().Must().BeEqualTo(expectedIds);
        }
    }
}
