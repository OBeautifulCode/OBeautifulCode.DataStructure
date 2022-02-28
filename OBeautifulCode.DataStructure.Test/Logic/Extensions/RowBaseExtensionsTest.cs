// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowBaseExtensionsTest.cs" company="OBeautifulCode">
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

    public static class RowBaseExtensionsTest
    {
        [Fact]
        public static void GetNumberOfColumnsSpanned___Should_throw_ArgumentNullException___When_parameter_row_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => RowBaseExtensions.GetNumberOfColumnsSpanned(null));

            // Act
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
        public static void Pad_FlatRow___Should_throw_ArgumentNullException___When_parameter_row_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => RowBaseExtensions.Pad((FlatRow)null, 4));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("row");
        }

        [Fact]
        public static void Pad_FlatRow___Should_throw_ArgumentOutOfRangeException___When_parameter_row_is_null()
        {
            // Arrange
            var row = A.Dummy<FlatRow>();

            // Act
            var actual1 = Record.Exception(() => row.Pad(0));
            var actual2 = Record.Exception(() => row.Pad(A.Dummy<NegativeInteger>()));

            // Assert
            actual1.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
            actual1.Message.AsTest().Must().ContainString("requiredNumberOfColumnsSpanned must be > 0; provided value: 0");

            actual2.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
            actual2.Message.AsTest().Must().ContainString("requiredNumberOfColumnsSpanned must be > 0; provided value");
        }

        [Fact]
        public static void Pad_FlatRow___Should_return_same_row___When_row_spans_required_number_of_columns()
        {
            // Arrange
            var expected1 = new FlatRow(
                new[] { new NullCell() });

            var expected2 = new FlatRow(
                new[]
                {
                    new NullCell(columnsSpanned: 2),
                    new NullCell(columnsSpanned: 3),
                });

            // Act
            var actual1 = expected1.Pad(1, useSingleCell: A.Dummy<bool>());
            var actual2a = expected2.Pad(1, useSingleCell: A.Dummy<bool>());
            var actual2b = expected2.Pad(4, useSingleCell: A.Dummy<bool>());
            var actual2c = expected2.Pad(5, useSingleCell: A.Dummy<bool>());

            // Assert
            actual1.AsTest().Must().BeSameReferenceAs(expected1);
            actual2a.AsTest().Must().BeSameReferenceAs(expected2);
            actual2b.AsTest().Must().BeSameReferenceAs(expected2);
            actual2c.AsTest().Must().BeSameReferenceAs(expected2);
        }

        [Fact]
        public static void Pad_FlatRow___Should_return_padded_row___When_row_spans_less_than_required_number_of_columns_and_useSingleCell_is_false()
        {
            // Arrange
            var row1 = new FlatRow(
                new[] { new NullCell() });

            var expected1 = new FlatRow(
                new[] { new NullCell(), new NullCell() });

            var row2 = new FlatRow(
                new[] { new NullCell(columnsSpanned: 2) });

            var expected2a = new FlatRow(
                new[] { new NullCell(columnsSpanned: 2), new NullCell() });

            var expected2b = new FlatRow(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(), new NullCell() });

            var row3 = new FlatRow(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(columnsSpanned: 3) });

            var expected3 = new FlatRow(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(columnsSpanned: 3), new NullCell(), new NullCell(), new NullCell(), new NullCell() });

            // Act
            var actual1 = row1.Pad(2, useSingleCell: false);
            var actual2a = row2.Pad(3, useSingleCell: false);
            var actual2b = row2.Pad(4, useSingleCell: false);
            var actual3 = row3.Pad(9, useSingleCell: false);

            // Assert
            actual1.AsTest().Must().BeEqualTo(expected1);
            actual2a.AsTest().Must().BeEqualTo(expected2a);
            actual2b.AsTest().Must().BeEqualTo(expected2b);
            actual3.AsTest().Must().BeEqualTo(expected3);
        }

        [Fact]
        public static void Pad_FlatRow___Should_return_padded_row___When_row_spans_less_than_required_number_of_columns_and_useSingleCell_is_true()
        {
            // Arrange
            var row1 = new FlatRow(
                new[] { new NullCell() });

            var expected1 = new FlatRow(
                new[] { new NullCell(), new NullCell(columnsSpanned: 1) });

            var row2 = new FlatRow(
                new[] { new NullCell(columnsSpanned: 2) });

            var expected2a = new FlatRow(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(columnsSpanned: 1) });

            var expected2b = new FlatRow(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(columnsSpanned: 2) });

            var row3 = new FlatRow(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(columnsSpanned: 3) });

            var expected3 = new FlatRow(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(columnsSpanned: 3), new NullCell(columnsSpanned: 4) });

            // Act
            var actual1 = row1.Pad(2, useSingleCell: true);
            var actual2a = row2.Pad(3, useSingleCell: true);
            var actual2b = row2.Pad(4, useSingleCell: true);
            var actual3 = row3.Pad(9, useSingleCell: true);

            // Assert
            actual1.AsTest().Must().BeEqualTo(expected1);
            actual2a.AsTest().Must().BeEqualTo(expected2a);
            actual2b.AsTest().Must().BeEqualTo(expected2b);
            actual3.AsTest().Must().BeEqualTo(expected3);
        }

        [Fact]
        public static void Pad_Row___Should_throw_ArgumentNullException___When_parameter_row_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => RowBaseExtensions.Pad((Row)null, 4));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("row");
        }

        [Fact]
        public static void Pad_Row___Should_throw_ArgumentOutOfRangeException___When_parameter_row_is_null()
        {
            // Arrange
            var row = A.Dummy<Row>();

            // Act
            var actual1 = Record.Exception(() => row.Pad(0));
            var actual2 = Record.Exception(() => row.Pad(A.Dummy<NegativeInteger>()));

            // Assert
            actual1.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
            actual1.Message.AsTest().Must().ContainString("requiredNumberOfColumnsSpanned must be > 0; provided value: 0");

            actual2.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
            actual2.Message.AsTest().Must().ContainString("requiredNumberOfColumnsSpanned must be > 0; provided value");
        }

        [Fact]
        public static void Pad_Row___Should_return_same_row___When_row_spans_required_number_of_columns()
        {
            // Arrange
            var expected1 = new Row(
                new[] { new NullCell() });

            var expected2 = new Row(
                new[]
                {
                    new NullCell(columnsSpanned: 2),
                    new NullCell(columnsSpanned: 3),
                });

            // Act
            var actual1 = expected1.Pad(1, useSingleCell: A.Dummy<bool>());
            var actual2a = expected2.Pad(1, useSingleCell: A.Dummy<bool>());
            var actual2b = expected2.Pad(4, useSingleCell: A.Dummy<bool>());
            var actual2c = expected2.Pad(5, useSingleCell: A.Dummy<bool>());

            // Assert
            actual1.AsTest().Must().BeSameReferenceAs(expected1);
            actual2a.AsTest().Must().BeSameReferenceAs(expected2);
            actual2b.AsTest().Must().BeSameReferenceAs(expected2);
            actual2c.AsTest().Must().BeSameReferenceAs(expected2);
        }

        [Fact]
        public static void Pad_Row___Should_return_padded_row___When_row_spans_less_than_required_number_of_columns_and_useSingleCell_is_false()
        {
            // Arrange
            var row1 = new Row(
                new[] { new NullCell() });

            var expected1 = new Row(
                new[] { new NullCell(), new NullCell() });

            var row2 = new Row(
                new[] { new NullCell(columnsSpanned: 2) });

            var expected2a = new Row(
                new[] { new NullCell(columnsSpanned: 2), new NullCell() });

            var expected2b = new Row(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(), new NullCell() });

            var row3 = new Row(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(columnsSpanned: 3) });

            var expected3 = new Row(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(columnsSpanned: 3), new NullCell(), new NullCell(), new NullCell(), new NullCell() });

            // Act
            var actual1 = row1.Pad(2, useSingleCell: false);
            var actual2a = row2.Pad(3, useSingleCell: false);
            var actual2b = row2.Pad(4, useSingleCell: false);
            var actual3 = row3.Pad(9, useSingleCell: false);

            // Assert
            actual1.AsTest().Must().BeEqualTo(expected1);
            actual2a.AsTest().Must().BeEqualTo(expected2a);
            actual2b.AsTest().Must().BeEqualTo(expected2b);
            actual3.AsTest().Must().BeEqualTo(expected3);
        }

        [Fact]
        public static void Pad_Row___Should_return_padded_row___When_row_spans_less_than_required_number_of_columns_and_useSingleCell_is_true()
        {
            // Arrange
            var row1 = new Row(
                new[] { new NullCell() });

            var expected1 = new Row(
                new[] { new NullCell(), new NullCell(columnsSpanned: 1) });

            var row2 = new Row(
                new[] { new NullCell(columnsSpanned: 2) });

            var expected2a = new Row(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(columnsSpanned: 1) });

            var expected2b = new Row(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(columnsSpanned: 2) });

            var row3 = new Row(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(columnsSpanned: 3) });

            var expected3 = new Row(
                new[] { new NullCell(columnsSpanned: 2), new NullCell(columnsSpanned: 3), new NullCell(columnsSpanned: 4) });

            // Act
            var actual1 = row1.Pad(2, useSingleCell: true);
            var actual2a = row2.Pad(3, useSingleCell: true);
            var actual2b = row2.Pad(4, useSingleCell: true);
            var actual3 = row3.Pad(9, useSingleCell: true);

            // Assert
            actual1.AsTest().Must().BeEqualTo(expected1);
            actual2a.AsTest().Must().BeEqualTo(expected2a);
            actual2b.AsTest().Must().BeEqualTo(expected2b);
            actual3.AsTest().Must().BeEqualTo(expected3);
        }
    }
}
