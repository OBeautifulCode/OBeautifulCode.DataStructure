// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportCacheTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    public static class ReportCacheTest
    {
        [Fact]
        public static void GetCell___Should_throw_ArgumentNullException___When_parameter_treeTable_is_null()
        {
            // Arrange
            var actual = Record.Exception(() => TreeTableExtensions.GetCell(null, A.Dummy<string>()));

            // Act, Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("treeTable");
        }

        [Fact]
        public static void GetCell___Should_throw_ArgumentNullException___When_parameter_cellId_is_null()
        {
            // Arrange
            var actual = Record.Exception(() => A.Dummy<TreeTable>().GetCell(null));

            // Act, Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cellId");
        }

        [Fact]
        public static void GetCell___Should_throw_ArgumentException___When_parameter_cellId_is_white_space()
        {
            // Arrange
            var actual = Record.Exception(() => A.Dummy<TreeTable>().GetCell(" \r\n  "));

            // Act, Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("cellId");
        }

        [Fact]
        public static void GetCell___Should_throw_ArgumentException___When_there_is_no_cell_with_the_specified_id()
        {
            // Arrange
            var treeTable = A.Dummy<TreeTable>();

            var cellId = A.Dummy<string>();

            // Act
            var actual = Record.Exception(() => treeTable.GetCell(cellId));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no cell with id '{cellId}'."));
        }

        [Fact]
        public static void GetCell___Should_return_cell___When_slotId_is_null()
        {
            // Arrange
            var treeTable = A.Dummy<TreeTable>().Whose(_ => _.TableRows.GetAllRowsInOrder().Any());

            var allRowsInOrder = treeTable.TableRows.GetAllRowsInOrder();

            var randomRow = allRowsInOrder[ThreadSafeRandom.Next(0, allRowsInOrder.Count)];

            var expected = randomRow.Cells[ThreadSafeRandom.Next(0, randomRow.Cells.Count)];

            // Act
            var actual = treeTable.GetCell(expected.Id);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell___Should_throw_ArgumentException___When_slotId_specified_but_addressed_cell_is_not_a_slotted_cell()
        {
            // Arrange
            var treeTable = A.Dummy<TreeTable>().Whose(_ => _.TableRows.GetAllRowsInOrder().SelectMany(r => r.Cells).Any(c => !(c is ISlottedCell)));

            var allNotSlottedCells = treeTable.TableRows.GetAllRowsInOrder().SelectMany(_ => _.Cells).Where(_ => !(_ is ISlottedCell)).ToList();

            var cell = allNotSlottedCells[ThreadSafeRandom.Next(0, allNotSlottedCells.Count)];

            var slotId = A.Dummy<string>();

            // Act
            var actual = Record.Exception(() => treeTable.GetCell(cell.Id, slotId));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().BeEqualTo(Invariant($"Slot id '{slotId}' was specified, but the addressed cell ('{cell.Id}') is not a slotted cell"));
        }

        [Fact]
        public static void GetCell___Should_throw_ArgumentException___When_slotId_specified_but_there_is_no_slot_with_that_id()
        {
            // Arrange
            var treeTable = A.Dummy<TreeTable>().Whose(_ => _.TableRows.GetAllRowsInOrder().SelectMany(r => r.Cells).Any(c => c is ISlottedCell));

            var allSlottedCells = treeTable.TableRows.GetAllRowsInOrder().SelectMany(_ => _.Cells).OfType<ISlottedCell>().ToList();

            var cell = allSlottedCells[ThreadSafeRandom.Next(0, allSlottedCells.Count)];

            var slotId = A.Dummy<string>();

            // Act
            var actual = Record.Exception(() => treeTable.GetCell(cell.Id, slotId));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().BeEqualTo(Invariant($"Slot id '{slotId}' was specified, but the addressed cell ('{cell.Id}') does not contain a slot having that id."));
        }

        [Fact]
        public static void GetCell___Should_return_cell___When_slotId_specified()
        {
            // Arrange
            var treeTable = A.Dummy<TreeTable>().Whose(_ => _.TableRows.GetAllRowsInOrder().SelectMany(r => r.Cells).Any(c => c is ISlottedCell));

            var allSlottedCells = treeTable.TableRows.GetAllRowsInOrder().SelectMany(_ => _.Cells).OfType<ISlottedCell>().ToList();

            var slottedCell = allSlottedCells[ThreadSafeRandom.Next(0, allSlottedCells.Count)];

            var slotIdAndCell = slottedCell.SlotIdToCellMap.ElementAt(ThreadSafeRandom.Next(0, slottedCell.SlotIdToCellMap.Count));

            var slotId = slotIdAndCell.Key;

            var expected = (ICell)slotIdAndCell.Value;

            // Act
            var actual = treeTable.GetCell(slottedCell.Id, slotId);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }
    }
}
