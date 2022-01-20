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
            // Arrange
            var actual = Record.Exception(() => RowBaseExtensions.GetNumberOfColumnsSpanned(null));

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
    }
}
