// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Linq;
    using FakeItEasy;
    using OBeautifulCode.Assertion.Recipes;
    using Xunit;

    public static class ToExtensionsTest
    {
        [Fact]
        public static void ToReport___Should_throw_ArgumentNullException___When_parameter_treeTable_is_null()
        {
            // Arrange
            var actual = Record.Exception(() => ToExtensions.ToReport(null));

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
            var actual = Record.Exception(() => ToExtensions.ToSection(null));

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

        [Fact]
        public static void ToFlatRow___Should_throw_ArgumentNullException___When_parameter_cell_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ToExtensions.ToFlatRow(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cell");
        }

        [Fact]
        public static void ToFlatRow___Should_return_single_cell_FlatRow___When_called()
        {
            // Arrange
            var id = A.Dummy<string>();
            var cell = A.Dummy<ICell>();
            var format = A.Dummy<RowFormat>();

            var expected = new FlatRow(new[] { cell }, id, format);

            // Act
            var actual = cell.ToFlatRow(id, format);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ToRow___Should_throw_ArgumentNullException___When_parameter_cell_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ToExtensions.ToRow(null));
            actual.Message.AsTest().Must().ContainString("cell");

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToRow___Should_return_single_cell_Row___When_called()
        {
            // Arrange
            var id = A.Dummy<string>();
            var cell = A.Dummy<ICell>();
            var format = A.Dummy<RowFormat>();

            var expected = new Row(new[] { cell }, id, format);

            // Act
            var actual = cell.ToRow(id, format);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }
    }
}
