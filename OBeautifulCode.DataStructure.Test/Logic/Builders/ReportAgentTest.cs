// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportAgentTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FakeItEasy;
    using MathNet.Numerics.Statistics;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Math.Recipes;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using Xunit;
    using static System.FormattableString;
    using NamedDecimalSet = System.Collections.Generic.IReadOnlyList<OBeautifulCode.Type.NamedValue<decimal>>;

    public static class ReportAgentTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_report_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => new ReportAgent(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("report");
        }

        [Fact]
        public static void Constructor__Should_throw_ArgumentException___When_same_cell_object_is_used_multiple_times()
        {
            // Arrange
            var referenceObject = A.Dummy<Report>();

            var treeTable = A.Dummy<TreeTable>().Whose(_ => _.GetAllCells().Count > 0);

            var report = new Report(
                referenceObject.Id,
                new[]
                {
                    new Section("id1", treeTable),
                    new Section("id2", A.Dummy<TreeTable>()),
                    new Section("id3", treeTable),
                },
                referenceObject.Title,
                referenceObject.TimestampUtc,
                referenceObject.DownloadLinks,
                referenceObject.AdditionalInfo,
                referenceObject.Format);

            // Act
            var actual = Record.Exception(() => new ReportAgent(report));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("One or more ICell objects are used multiple times in the report");
        }

        [Fact]
        public static void OperationCells___Should_return_all_operation_cells___When_getting()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var systemUnderTest = new ReportAgent(report);

            IReadOnlyCollection<IOperationOutputCell> expected = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).OfType<IOperationOutputCell>().ToList();

            // Act
            var actual = systemUnderTest.OperationCells;

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void InputCells___Should_return_all_input_cells___When_getting()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var systemUnderTest = new ReportAgent(report);

            IReadOnlyCollection<IInputCell> expected = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).OfType<IInputCell>().ToList();

            // Act
            var actual = systemUnderTest.InputCells;

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ValidationCells___Should_return_all_validation_cells___When_getting()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var systemUnderTest = new ReportAgent(report);

            IReadOnlyCollection<IValidationCell> expected = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).OfType<IValidationCell>().ToList();

            // Act
            var actual = systemUnderTest.ValidationCells;

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void AvailabilityCheckCells___Should_return_all_availability_check_cells___When_getting()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var systemUnderTest = new ReportAgent(report);

            IReadOnlyCollection<IAvailabilityCheckCell> expected = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).OfType<IAvailabilityCheckCell>().ToList();

            // Act
            var actual = systemUnderTest.AvailabilityCheckCells;

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetCell_StandardCellLocator___Should_throw_ArgumentNullException___When_parameter_cellLocator_is_null()
        {
            // Arrange
            var systemUnderTest = new ReportAgent(A.Dummy<Report>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell((StandardCellLocator)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cellLocator");
        }

        [Fact]
        public static void GetCell_StandardCellLocator___Should_throw_CellNotFoundException___When_cell_with_specified_id_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var cellLocator = new StandardCellLocator(A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no cell with id '{cellLocator.CellId}' in the report."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_StandardCellLocator___Should_throw_CellNotFoundException___When_multiple_cells_with_specified_id_are_found()
        {
            // Arrange
            var section1 = A.Dummy<Section>().Whose(_ => _.TreeTable.GetAllCells().Any());
            var section2 = section1.DeepCloneWithId(A.Dummy<string>());

            var report = new Report(A.Dummy<string>(), new[] { section1, section2 });

            var cellLocator = new StandardCellLocator(section1.TreeTable.GetAllCells().First().Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There are multiple cells with id '{cellLocator.CellId}' in the report."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_StandardCellLocator___Should_throw_CellNotFoundException___When_slotId_specified_for_cell_that_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var cell = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).First(_ => _ is INotSlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{cellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' is not a slotted cell"));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_StandardCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_but_there_is_no_slot_having_slotId()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{cellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' does not contain a slot having that id."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_StandardCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_and_SlotId_not_specified_and_SlotSelectionStrategy_is_ThrowIfSlotIdNotSpecified()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id, null, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Located an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_StandardCellLocator___Should_return_cell___When_cell_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var expected = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).First(_ => _ is INotSlottedCell);

            var cellLocator = new StandardCellLocator(expected.Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetCell(cellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_StandardCellLocator___Should_return_cell_in_default_slot___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var expected = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

            var cellLocator = new StandardCellLocator(cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetCell(cellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_StandardCellLocator___Should_return_cell_in_specified_slot___When_addressing_slot_within_slotted_cell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var expected = (ICell)cell.SlotIdToCellMap[slotId];

            var cellLocator = new StandardCellLocator(cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetCell(cellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_TCell_StandardCellLocator___Should_throw_ArgumentNullException___When_parameter_cellLocator_is_null()
        {
            // Arrange
            var systemUnderTest = new ReportAgent(A.Dummy<Report>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ICell>((StandardCellLocator)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cellLocator");
        }

        [Fact]
        public static void GetCell_TCell_StandardCellLocator___Should_throw_CellNotFoundException___When_cell_with_specified_id_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var cellLocator = new StandardCellLocator(A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ICell>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no cell with id '{cellLocator.CellId}' in the report."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_StandardCellLocator___Should_throw_CellNotFoundException___When_multiple_cells_with_specified_id_are_found()
        {
            // Arrange
            var section1 = A.Dummy<Section>().Whose(_ => _.TreeTable.GetAllCells().Any());
            var section2 = section1.DeepCloneWithId(A.Dummy<string>());

            var report = new Report(A.Dummy<string>(), new[] { section1, section2 });

            var cellLocator = new StandardCellLocator(section1.TreeTable.GetAllCells().First().Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ICell>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There are multiple cells with id '{cellLocator.CellId}' in the report."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_StandardCellLocator___Should_throw_CellNotFoundException___When_slotId_specified_for_cell_that_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var cell = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).First(_ => _ is INotSlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ICell>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{cellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' is not a slotted cell"));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_StandardCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_but_there_is_no_slot_having_slotId()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ICell>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{cellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' does not contain a slot having that id."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_StandardCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_and_SlotId_not_specified_and_SlotSelectionStrategy_is_ThrowIfSlotIdNotSpecified()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id, null, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ICell>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Located an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_StandardCellLocator___Should_throw_CellNotFoundException___When_cell_is_not_slotted_and_not_of_type_TCell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var cell = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).First(_ => _ is INotSlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ConstCell<Report>>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Addressing a cell of type {cell.GetType().ToStringReadable()}, which is not assignable to the specified TCell."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_StandardCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot_and_cell_is_not_of_type_TCell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var cellInSlot = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

            var cellLocator = new StandardCellLocator(cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ConstCell<Report>>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Addressing a cell of type {cellInSlot.GetType().ToStringReadable()}, which is not assignable to the specified TCell."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_StandardCellLocator___Should_throw_CellNotFoundException___When_addressing_slot_within_slotted_cell_and_cell_is_not_of_type_TCell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var cellInSlot = (ICell)cell.SlotIdToCellMap[slotId];

            var cellLocator = new StandardCellLocator(cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ConstCell<Report>>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Addressing a cell of type {cellInSlot.GetType().ToStringReadable()}, which is not assignable to the specified TCell."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_StandardCellLocator___Should_return_cell___When_cell_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var expected = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).First(_ => _ is INotSlottedCell);

            var cellLocator = new StandardCellLocator(expected.Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetCell<ICell>(cellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_TCell_StandardCellLocator___Should_return_cell_in_default_slot___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var expected = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

            var cellLocator = new StandardCellLocator(cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetCell<ICell>(cellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_TCell_StandardCellLocator___Should_return_cell_in_specified_slot___When_addressing_slot_within_slotted_cell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var expected = (ICell)cell.SlotIdToCellMap[slotId];

            var cellLocator = new StandardCellLocator(cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetCell<ICell>(cellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void TryGetCell_StandardCellLocator___Should_throw_ArgumentNullException___When_parameter_cellLocator_is_null()
        {
            // Arrange
            var systemUnderTest = new ReportAgent(A.Dummy<Report>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.TryGetCell((StandardCellLocator)null, out var actualCell));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cellLocator");
        }

        [Fact]
        public static void TryGetCell_StandardCellLocator___Should_return_false_with_null_cell___When_cell_with_specified_id_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var cellLocator = new StandardCellLocator(A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_StandardCellLocator___Should_return_false_with_null_cell___When_multiple_cells_with_specified_id_are_found()
        {
            // Arrange
            var section1 = A.Dummy<Section>().Whose(_ => _.TreeTable.GetAllCells().Any());
            var section2 = section1.DeepCloneWithId(A.Dummy<string>());

            var report = new Report(A.Dummy<string>(), new[] { section1, section2 });

            var cellLocator = new StandardCellLocator(section1.TreeTable.GetAllCells().First().Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_StandardCellLocator___Should_return_false_with_null_cell___When_slotId_specified_for_cell_that_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var cell = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).First(_ => _ is INotSlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_StandardCellLocator___Should_return_false_with_null_cell___When_addressing_slotted_cell_but_there_is_no_slot_having_slotId()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_StandardCellLocator___Should_return_false_with_null_cell___When_addressing_slotted_cell_and_SlotId_not_specified_and_SlotSelectionStrategy_is_ThrowIfSlotIdNotSpecified()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id, null, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_StandardCellLocator___Should_return_true_and_set_cell___When_cell_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var expectedCell = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).First(_ => _ is INotSlottedCell);

            var cellLocator = new StandardCellLocator(expectedCell.Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualCell.AsTest().Must().BeSameReferenceAs(expectedCell);
        }

        [Fact]
        public static void TryGetCell_StandardCellLocator___Should_return_true_and_set_cell_in_default_slot___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var expectedCell = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

            var cellLocator = new StandardCellLocator(cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualCell.AsTest().Must().BeSameReferenceAs(expectedCell);
        }

        [Fact]
        public static void TryGetCell_StandardCellLocator___Should_return_true_and_set_cell_in_specified_slot___When_addressing_slot_within_slotted_cell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var expectedCell = (ICell)cell.SlotIdToCellMap[slotId];

            var cellLocator = new StandardCellLocator(cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualCell.AsTest().Must().BeSameReferenceAs(expectedCell);
        }

        [Fact]
        public static void TryGetCell_TCell_StandardCellLocator___Should_throw_ArgumentNullException___When_parameter_cellLocator_is_null()
        {
            // Arrange
            var systemUnderTest = new ReportAgent(A.Dummy<Report>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.TryGetCell<ICell>((StandardCellLocator)null, out var actualCell));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cellLocator");
        }

        [Fact]
        public static void TryGetCell_TCell_StandardCellLocator___Should_return_false_with_null_cell___When_cell_with_specified_id_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var cellLocator = new StandardCellLocator(A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_StandardCellLocator___Should_return_false_with_null_cell___When_multiple_cells_with_specified_id_are_found()
        {
            // Arrange
            var section1 = A.Dummy<Section>().Whose(_ => _.TreeTable.GetAllCells().Any());
            var section2 = section1.DeepCloneWithId(A.Dummy<string>());

            var report = new Report(A.Dummy<string>(), new[] { section1, section2 });

            var cellLocator = new StandardCellLocator(section1.TreeTable.GetAllCells().First().Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_StandardCellLocator___Should_return_false_with_null_cell___When_slotId_specified_for_cell_that_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var cell = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).First(_ => _ is INotSlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_StandardCellLocator___Should_return_false_with_null_cell___When_addressing_slotted_cell_but_there_is_no_slot_having_slotId()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_StandardCellLocator___Should_return_false_with_null_cell___When_addressing_slotted_cell_and_SlotId_not_specified_and_SlotSelectionStrategy_is_ThrowIfSlotIdNotSpecified()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new StandardCellLocator(cell.Id, null, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_StandardCellLocator___Should_return_false_with_null_cell___When_cell_is_not_slotted_and_not_of_type_TCell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var expectedCell = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).First(_ => _ is INotSlottedCell);

            var cellLocator = new StandardCellLocator(expectedCell.Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ConstCell<Report>>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_StandardCellLocator___Should_return_false_with_null_cell___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot_and_cell_is_not_of_type_TCell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var cellLocator = new StandardCellLocator(cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ConstCell<Report>>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_StandardCellLocator___Should_return_false_with_null_cell___When_addressing_slot_within_slotted_cell_and_cell_is_not_of_type_TCell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var cellLocator = new StandardCellLocator(cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ConstCell<Report>>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_StandardCellLocator___Should_return_true_and_set_cell___When_cell_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var expectedCell = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).First(_ => _ is INotSlottedCell);

            var cellLocator = new StandardCellLocator(expectedCell.Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualCell.AsTest().Must().BeSameReferenceAs(expectedCell);
        }

        [Fact]
        public static void TryGetCell_TCell_StandardCellLocator___Should_return_true_and_set_cell_in_default_slot___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var expectedCell = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

            var cellLocator = new StandardCellLocator(cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualCell.AsTest().Must().BeSameReferenceAs(expectedCell);
        }

        [Fact]
        public static void TryGetCell_TCell_StandardCellLocator___Should_return_true_and_set_cell_in_specified_slot___When_addressing_slot_within_slotted_cell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var expectedCell = (ICell)cell.SlotIdToCellMap[slotId];

            var cellLocator = new StandardCellLocator(cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualCell.AsTest().Must().BeSameReferenceAs(expectedCell);
        }

        [Fact]
        public static void GetCell_InReportCellLocator___Should_throw_ArgumentNullException___When_parameter_cellLocator_is_null()
        {
            // Arrange
            var systemUnderTest = new ReportAgent(A.Dummy<Report>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell((InReportCellLocator)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cellLocator");
        }

        [Fact]
        public static void GetCell_InReportCellLocator___Should_throw_CellNotFoundException___When_section_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.First().TreeTable.GetAllCells().Any());

            var cellLocator = new InReportCellLocator(A.Dummy<string>(), report.Sections.First().TreeTable.GetAllCells().First().Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no section with id '{cellLocator.SectionId}'."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_InReportCellLocator___Should_throw_CellNotFoundException___When_cell_with_specified_id_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var section = report.Sections.First();

            var cellLocator = new InReportCellLocator(section.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no cell with id '{cellLocator.CellId}' in section '{section.Id}'."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_InReportCellLocator___Should_throw_CellNotFoundException___When_slotId_specified_for_cell_that_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is INotSlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{cellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' in section '{section.Id}' is not a slotted cell"));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_InReportCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_but_there_is_no_slot_having_slotId()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{cellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' in section '{section.Id}' does not contain a slot having that id."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_InReportCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_and_SlotId_not_specified_and_SlotSelectionStrategy_is_ThrowIfSlotIdNotSpecified()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, null, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Located an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_InReportCellLocator___Should_return_cell___When_cell_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is INotSlottedCell));

            var expected = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, expected.Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetCell(cellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_InReportCellLocator___Should_return_cell_in_default_slot___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var expected = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetCell(cellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_InReportCellLocator___Should_return_cell_in_specified_slot___When_addressing_slot_within_slotted_cell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var expected = (ICell)cell.SlotIdToCellMap[slotId];

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetCell(cellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_TCell_InReportCellLocator___Should_throw_ArgumentNullException___When_parameter_cellLocator_is_null()
        {
            // Arrange
            var systemUnderTest = new ReportAgent(A.Dummy<Report>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ICell>((InReportCellLocator)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cellLocator");
        }

        [Fact]
        public static void GetCell_TCell_InReportCellLocator___Should_throw_CellNotFoundException___When_section_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.First().TreeTable.GetAllCells().Any());

            var cellLocator = new InReportCellLocator(A.Dummy<string>(), report.Sections.First().TreeTable.GetAllCells().First().Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ICell>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no section with id '{cellLocator.SectionId}'."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_InReportCellLocator___Should_throw_CellNotFoundException___When_cell_with_specified_id_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var section = report.Sections.First();

            var cellLocator = new InReportCellLocator(section.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ICell>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no cell with id '{cellLocator.CellId}' in section '{section.Id}'."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_InReportCellLocator___Should_throw_CellNotFoundException___When_slotId_specified_for_cell_that_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is INotSlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ICell>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{cellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' in section '{section.Id}' is not a slotted cell"));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_InReportCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_but_there_is_no_slot_having_slotId()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ICell>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{cellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' in section '{section.Id}' does not contain a slot having that id."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_InReportCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_and_SlotId_not_specified_and_SlotSelectionStrategy_is_ThrowIfSlotIdNotSpecified()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, null, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ICell>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Located an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_InReportCellLocator___Should_throw_CellNotFoundException___When_cell_is_not_slotted_and_not_of_type_TCell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is INotSlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ConstCell<Report>>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Addressing a cell of type {cell.GetType().ToStringReadable()}, which is not assignable to the specified TCell."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_InReportCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot_and_cell_is_not_of_type_TCell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var cellInSlot = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ConstCell<Report>>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Addressing a cell of type {cellInSlot.GetType().ToStringReadable()}, which is not assignable to the specified TCell."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_InReportCellLocator___Should_throw_CellNotFoundException___When_addressing_slot_within_slotted_cell_and_cell_is_not_of_type_TCell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var cellInSlot = (ICell)cell.SlotIdToCellMap[slotId];

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell<ConstCell<Report>>(cellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Addressing a cell of type {cellInSlot.GetType().ToStringReadable()}, which is not assignable to the specified TCell."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        }

        [Fact]
        public static void GetCell_TCell_InReportCellLocator___Should_return_cell___When_cell_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is INotSlottedCell));

            var expected = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, expected.Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetCell<ICell>(cellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_TCell_InReportCellLocator___Should_return_cell_in_default_slot___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var expected = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetCell<ICell>(cellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_TCell_InReportCellLocator___Should_return_cell_in_specified_slot___When_addressing_slot_within_slotted_cell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var expected = (ICell)cell.SlotIdToCellMap[slotId];

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetCell<ICell>(cellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void TryGetCell_InReportCellLocator___Should_throw_ArgumentNullException___When_parameter_cellLocator_is_null()
        {
            // Arrange
            var systemUnderTest = new ReportAgent(A.Dummy<Report>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.TryGetCell((InReportCellLocator)null, out var actualCell));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cellLocator");
        }

        [Fact]
        public static void TryGetCell_InReportCellLocator___Should_return_false_with_null_cell___When_section_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.First().TreeTable.GetAllCells().Any());

            var cellLocator = new InReportCellLocator(A.Dummy<string>(), report.Sections.First().TreeTable.GetAllCells().First().Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_InReportCellLocator___Should_return_false_with_null_cell___When_cell_with_specified_id_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var section = report.Sections.First();

            var cellLocator = new InReportCellLocator(section.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_InReportCellLocator___Should_return_false_with_null_cell___When_slotId_specified_for_cell_that_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is INotSlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_InReportCellLocator___Should_return_false_with_null_cell___When_addressing_slotted_cell_but_there_is_no_slot_having_slotId()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_InReportCellLocator___Should_return_false_with_null_cell___When_addressing_slotted_cell_and_SlotId_not_specified_and_SlotSelectionStrategy_is_ThrowIfSlotIdNotSpecified()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, null, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_InReportCellLocator___Should_return_true_and_set_cell___When_cell_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is INotSlottedCell));

            var expectedCell = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, expectedCell.Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualCell.AsTest().Must().BeSameReferenceAs(expectedCell);
        }

        [Fact]
        public static void TryGetCell_InReportCellLocator___Should_return_true_and_set_cell_in_default_slot___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var expectedCell = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualCell.AsTest().Must().BeSameReferenceAs(expectedCell);
        }

        [Fact]
        public static void TryGetCell_InReportCellLocator___Should_return_true_and_set_cell_in_specified_slot___When_addressing_slot_within_slotted_cell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var expectedCell = (ICell)cell.SlotIdToCellMap[slotId];

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualCell.AsTest().Must().BeSameReferenceAs(expectedCell);
        }

        [Fact]
        public static void TryGetCell_TCell_InReportCellLocator___Should_throw_ArgumentNullException___When_parameter_cellLocator_is_null()
        {
            // Arrange
            var systemUnderTest = new ReportAgent(A.Dummy<Report>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.TryGetCell<ICell>((InReportCellLocator)null, out var actualCell));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cellLocator");
        }

        [Fact]
        public static void TryGetCell_TCell_InReportCellLocator___Should_return_false_with_null_cell___When_section_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.First().TreeTable.GetAllCells().Any());

            var cellLocator = new InReportCellLocator(A.Dummy<string>(), report.Sections.First().TreeTable.GetAllCells().First().Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_InReportCellLocator___Should_return_false_with_null_cell___When_cell_with_specified_id_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var section = report.Sections.First();

            var cellLocator = new InReportCellLocator(section.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_InReportCellLocator___Should_return_false_with_null_cell___When_slotId_specified_for_cell_that_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is INotSlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_InReportCellLocator___Should_return_false_with_null_cell___When_addressing_slotted_cell_but_there_is_no_slot_having_slotId()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_InReportCellLocator___Should_return_false_with_null_cell___When_addressing_slotted_cell_and_SlotId_not_specified_and_SlotSelectionStrategy_is_ThrowIfSlotIdNotSpecified()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, null, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_InReportCellLocator___Should_return_false_with_null_cell___When_cell_is_not_slotted_and_not_of_type_TCell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is INotSlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, cell.Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ConstCell<Report>>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_InReportCellLocator___Should_return_false_with_null_cell___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot_and_cell_is_not_of_type_TCell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ConstCell<Report>>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_InReportCellLocator___Should_return_false_with_null_cell___When_addressing_slot_within_slotted_cell_and_cell_is_not_of_type_TCell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ConstCell<Report>>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualCell.AsTest().Must().BeNull();
        }

        [Fact]
        public static void TryGetCell_TCell_InReportCellLocator___Should_return_cell___When_cell_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is INotSlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is INotSlottedCell));

            var expectedCell = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var cellLocator = new InReportCellLocator(section.Id, expectedCell.Id);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualCell.AsTest().Must().BeSameReferenceAs(expectedCell);
        }

        [Fact]
        public static void TryGetCell_TCell_InReportCellLocator___Should_return_cell_in_default_slot___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var expectedCell = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualCell.AsTest().Must().BeSameReferenceAs(expectedCell);
        }

        [Fact]
        public static void TryGetCell_TCell_InReportCellLocator___Should_return_cell_in_specified_slot___When_addressing_slot_within_slotted_cell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var expectedCell = (ICell)cell.SlotIdToCellMap[slotId];

            var cellLocator = new InReportCellLocator(section.Id, cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.TryGetCell<ICell>(cellLocator, out var actualCell);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualCell.AsTest().Must().BeSameReferenceAs(expectedCell);
        }

        ////[Fact]
        ////public static void GetCell_SectionCellLocator___Should_throw_ArgumentNullException___When_parameter_cellLocator_is_null()
        ////{
        ////    // Arrange
        ////    var systemUnderTest = new ReportAgent(A.Dummy<Report>());

        ////    // Act
        ////    var actual = Record.Exception(() => systemUnderTest.GetCell(null, A.Dummy<ICell>()));

        ////    // Assert
        ////    actual.AsTest().Must().BeOfType<ArgumentNullException>();
        ////    actual.Message.AsTest().Must().ContainString("cellLocator");
        ////}

        ////[Fact]
        ////public static void GetCell_SectionCellLocator___Should_throw_ArgumentNullException___When_parameter_referenceCell_is_null()
        ////{
        ////    // Arrange
        ////    var systemUnderTest = new ReportAgent(A.Dummy<Report>());

        ////    // Act
        ////    var actual = Record.Exception(() => systemUnderTest.GetCell(A.Dummy<InSectionCellLocator>(), null));

        ////    // Assert
        ////    actual.AsTest().Must().BeOfType<ArgumentNullException>();
        ////    actual.Message.AsTest().Must().ContainString("referenceCell");
        ////}

        ////[Fact]
        ////public static void GetCell_SectionCellLocator___Should_throw_ArgumentNullException___When_referenceCell_is_not_a_cell_in_the_report()
        ////{
        ////    // Arrange
        ////    var report = A.Dummy<Report>().Whose(_ => _.Sections.First().TreeTable.GetAllCells().Any());

        ////    var systemUnderTest = new ReportAgent(report);

        ////    var cellLocator = new InSectionCellLocator(report.Sections.First().TreeTable.GetAllCells().First().Id);

        ////    // Act
        ////    var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator, A.Dummy<ICell>()));

        ////    // Assert
        ////    actual.AsTest().Must().BeOfType<CellNotFoundException>();
        ////    actual.Message.AsTest().Must().ContainString("referenceCell is not a cell in the report.");
        ////    ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)cellLocator);
        ////}

        ////[Fact]
        ////public static void GetCell_SectionCellLocator___Should_throw_CellNotFoundException___When_cell_with_specified_id_is_not_found()
        ////{
        ////    // Arrange
        ////    var report = A.Dummy<Report>();

        ////    var section = report.Sections.First();

        ////    var referenceCell = section.TreeTable.GetAllCells().First();

        ////    var cellLocator = new InSectionCellLocator(A.Dummy<string>());

        ////    var systemUnderTest = new ReportAgent(report);

        ////    var expectedReportCellLocator = new InReportCellLocator(section.Id, cellLocator.CellId);

        ////    // Act
        ////    var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator, referenceCell));

        ////    // Assert
        ////    actual.AsTest().Must().BeOfType<CellNotFoundException>();
        ////    actual.Message.AsTest().Must().ContainString(Invariant($"There is no cell with id '{cellLocator.CellId}' in section '{section.Id}'."));
        ////    ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)expectedReportCellLocator);
        ////}

        ////[Fact]
        ////public static void GetCell_SectionCellLocator___Should_throw_CellNotFoundException___When_slotId_specified_for_cell_that_is_not_slotted()
        ////{
        ////    // Arrange
        ////    var report = A.Dummy<Report>();

        ////    var section = report.Sections.First();

        ////    var allCells = section.TreeTable.GetAllCells();

        ////    var cell = allCells.First(_ => _ is INotSlottedCell);

        ////    var referenceCell = allCells.First(_ => !_.Equals(cell));

        ////    var cellLocator = new InSectionCellLocator(cell.Id, A.Dummy<string>());

        ////    var expectedReportCellLocator = new InReportCellLocator(section.Id, cellLocator.CellId, cellLocator.SlotId);

        ////    var systemUnderTest = new ReportAgent(report);

        ////    // Act
        ////    var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator, referenceCell));

        ////    // Assert
        ////    actual.AsTest().Must().BeOfType<CellNotFoundException>();
        ////    actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{cellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' in section '{section.Id}' is not a slotted cell"));
        ////    ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)expectedReportCellLocator);
        ////}

        ////[Fact]
        ////public static void GetCell_SectionCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_but_there_is_no_slot_having_slotId()
        ////{
        ////    // Arrange
        ////    var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

        ////    var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

        ////    var allCells = section.TreeTable.GetAllCells();

        ////    var cell = allCells.First(_ => _ is ISlottedCell);

        ////    var referenceCell = allCells.First(_ => !_.Equals(cell));

        ////    var cellLocator = new InSectionCellLocator(cell.Id, A.Dummy<string>());

        ////    var expectedReportCellLocator = new InReportCellLocator(section.Id, cellLocator.CellId, cellLocator.SlotId);

        ////    var systemUnderTest = new ReportAgent(report);

        ////    // Act
        ////    var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator, referenceCell));

        ////    // Assert
        ////    actual.AsTest().Must().BeOfType<CellNotFoundException>();
        ////    actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{cellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' in section '{section.Id}' does not contain a slot having that id."));
        ////    ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)expectedReportCellLocator);
        ////}

        ////[Fact]
        ////public static void GetCell_SectionCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_and_SlotId_not_specified_and_SlotSelectionStrategy_is_ThrowIfSlotIdNotSpecified()
        ////{
        ////    // Arrange
        ////    var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

        ////    var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

        ////    var allCells = section.TreeTable.GetAllCells();

        ////    var cell = allCells.First(_ => _ is ISlottedCell);

        ////    var referenceCell = allCells.First(_ => !_.Equals(cell));

        ////    var cellLocator = new InSectionCellLocator(cell.Id, null, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

        ////    var expectedReportCellLocator = new InReportCellLocator(section.Id, cellLocator.CellId, cellLocator.SlotId);

        ////    var systemUnderTest = new ReportAgent(report);

        ////    // Act
        ////    var actual = Record.Exception(() => systemUnderTest.GetCell(cellLocator, referenceCell));

        ////    // Assert
        ////    actual.AsTest().Must().BeOfType<CellNotFoundException>();
        ////    actual.Message.AsTest().Must().ContainString(Invariant($"Located an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."));
        ////    ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((ICellLocator)expectedReportCellLocator);
        ////}

        ////[Fact]
        ////public static void GetCell_SectionCellLocator___Should_return_cell___When_cell_is_not_slotted()
        ////{
        ////    // Arrange
        ////    var report = A.Dummy<Report>().Whose(_ => _.Sections.First().TreeTable.GetAllCells().Any(c => c is INotSlottedCell));

        ////    var section = report.Sections.First();

        ////    var allCells = section.TreeTable.GetAllCells();

        ////    var expected = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

        ////    var referenceCell = allCells.First(_ => !_.Equals(expected));

        ////    var cellLocator = new InSectionCellLocator(expected.Id);

        ////    var systemUnderTest = new ReportAgent(report);

        ////    // Act
        ////    var actual = systemUnderTest.GetCell(cellLocator, referenceCell);

        ////    // Assert
        ////    actual.AsTest().Must().BeSameReferenceAs(expected);
        ////}

        ////[Fact]
        ////public static void GetCell_SectionCellLocator___Should_return_cell_in_default_slot___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot()
        ////{
        ////    // Arrange
        ////    var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

        ////    var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

        ////    var allCells = section.TreeTable.GetAllCells();

        ////    var cell = allCells.OfType<ISlottedCell>().First();

        ////    var referenceCell = allCells.First(_ => !_.Equals(cell));

        ////    var expected = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

        ////    var cellLocator = new InSectionCellLocator(cell.Id, null, SlotSelectionStrategy.DefaultSlot);

        ////    var systemUnderTest = new ReportAgent(report);

        ////    // Act
        ////    var actual = systemUnderTest.GetCell(cellLocator, referenceCell);

        ////    // Assert
        ////    actual.AsTest().Must().BeSameReferenceAs(expected);
        ////}

        ////[Fact]
        ////public static void GetCell_SectionCellLocator___Should_return_cell_in_specified_slot___When_addressing_slot_within_slotted_cell()
        ////{
        ////    // Arrange
        ////    var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

        ////    var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => (c is ISlottedCell slottedCell) && (slottedCell.SlotIdToCellMap.Count > 1)));

        ////    var allCells = section.TreeTable.GetAllCells();

        ////    var cell = allCells.OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

        ////    var referenceCell = allCells.First(_ => !_.Equals(cell));

        ////    var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

        ////    var expected = (ICell)cell.SlotIdToCellMap[slotId];

        ////    var cellLocator = new InSectionCellLocator(cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

        ////    var systemUnderTest = new ReportAgent(report);

        ////    // Act
        ////    var actual = systemUnderTest.GetCell(cellLocator, referenceCell);

        ////    // Assert
        ////    actual.AsTest().Must().BeSameReferenceAs(expected);
        ////}

        [Fact]
        public static void GetSection___Should_throw_ArgumentNullException___When_parameter_cell_is_null()
        {
            // Arrange
            var systemUnderTest = new ReportAgent(A.Dummy<Report>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetSection(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("cell");
        }

        [Fact]
        public static void GetSection___Should_throw_InvalidOperationException___When_cell_does_not_exist_in_report()
        {
            // Arrange
            var systemUnderTest = new ReportAgent(A.Dummy<Report>());
            var cell = A.Dummy<ICell>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetSection(cell));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().ContainString("The specified cell does not exist in the report.");
        }

        [Fact]
        public static void GetSection___Should_return_section_containing_cell___When_cell_exists_in_report()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Count > 1);

            var section = report.Sections.ElementAt(ThreadSafeRandom.Next(0, report.Sections.Count));

            var cells = section.TreeTable.GetAllCells();

            var cell = cells.ElementAt(ThreadSafeRandom.Next(0, cells.Count));

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = systemUnderTest.GetSection(cell);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(section);
        }

        [Fact]
        public static void Recalc___Should_throw_ArgumentException___When_parameter_timestampUtc_is_in_UTC_time()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Recalc(DateTime.Now));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.Must().ContainString("timestampUtc");
        }

        [Fact]
        public static async Task RecalcAsync___Should_throw_ArgumentException___When_parameter_timestampUtc_is_in_UTC_time()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var systemUnderTest = new ReportAgent(report);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.RecalcAsync(DateTime.Now));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.Must().ContainString("timestampUtc");
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_availability_check_dependency_has_no_value()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),

            };

            // Act
            reportAgent.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            var isForProfitCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            Record.Exception(() => isForProfitCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var restrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-1");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var partiallyRestrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-1");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_availability_check_dependency_has_no_value()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            // Act
            await reportAgent.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            var isForProfitCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            Record.Exception(() => isForProfitCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var restrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-1");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var partiallyRestrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-1");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_availability_check_dependency_disables_cells()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            var isForProfitCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));

            isForProfitCell.SetCellValue(true, DateTime.UtcNow);

            // Act
            reportAgent.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetCellObjectValue().Must().BeEqualTo((object)true);

            var restrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var partiallyRestrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_availability_check_dependency_disables_cells()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            var isForProfitCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));

            isForProfitCell.SetCellValue(true, DateTime.UtcNow);

            // Act
            await reportAgent.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetCellObjectValue().Must().BeEqualTo((object)true);

            var restrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var partiallyRestrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_availability_check_dependency_enables_cells()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            var isForProfitCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));

            isForProfitCell.SetCellValue(false, DateTime.UtcNow);

            // Act
            reportAgent.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetCellObjectValue().Must().BeEqualTo((object)false);

            var restrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-enabled");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var partiallyRestrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-enabled");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_availability_check_dependency_enables_cells()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            var isForProfitCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));

            isForProfitCell.SetCellValue(false, DateTime.UtcNow);

            // Act
            await reportAgent.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetCellObjectValue().Must().BeEqualTo((object)false);

            var restrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-enabled");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var partiallyRestrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-enabled");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_availability_check_dependency_disables_cells_having_values()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            var isForProfitCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));
            var restrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            var partiallyRestrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));

            isForProfitCell.SetCellValue(true, DateTime.UtcNow);
            restrictedCashCell.SetCellValue(123.45m, DateTime.UtcNow);
            partiallyRestrictedCashCell.SetCellValue(6789.10m, DateTime.UtcNow);

            // Act
            reportAgent.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetCellObjectValue().Must().BeEqualTo((object)true);

            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_availability_check_dependency_disables_cells_having_values()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            var isForProfitCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));
            var restrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            var partiallyRestrictedCashCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));

            isForProfitCell.SetCellValue(true, DateTime.UtcNow);
            restrictedCashCell.SetCellValue(123.45m, DateTime.UtcNow);
            partiallyRestrictedCashCell.SetCellValue(6789.10m, DateTime.UtcNow);

            // Act
            await reportAgent.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            // Assert
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetCellObjectValue().Must().BeEqualTo((object)true);

            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_validation_dependency_has_no_value()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            // Act
            reportAgent.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            var salesFteCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.SalesFte));
            salesFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            salesFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("input required for sales");
            salesFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            salesFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            Record.Exception(() => salesFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var warehouseFteCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.WarehouseFte));
            warehouseFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            warehouseFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("input required for warehouse");
            warehouseFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            Record.Exception(() => warehouseFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var warehouseSupportFteCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.WarehouseSupportFte));
            warehouseSupportFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseSupportFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseSupportFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.NotApplicable);
            Record.Exception(() => warehouseSupportFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var totalFteCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.TotalFte));
            totalFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Aborted);
            totalFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            totalFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Aborted);
            Record.Exception(() => totalFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_validation_dependency_has_no_value()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            // Act
            await reportAgent.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            var salesFteCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.SalesFte));
            salesFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            salesFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("input required for sales");
            salesFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            salesFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            Record.Exception(() => salesFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var warehouseFteCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.WarehouseFte));
            warehouseFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            warehouseFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("input required for warehouse");
            warehouseFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            Record.Exception(() => warehouseFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var warehouseSupportFteCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.WarehouseSupportFte));
            warehouseSupportFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseSupportFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseSupportFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.NotApplicable);
            Record.Exception(() => warehouseSupportFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var totalFteCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.TotalFte));
            totalFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Aborted);
            totalFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            totalFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Aborted);
            Record.Exception(() => totalFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_validation_dependency_has_invalid_values()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            var salesFteCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.SalesFte));
            var warehouseFteCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.WarehouseFte));

            salesFteCell.SetCellValue(-4m, DateTime.UtcNow);
            warehouseFteCell.SetCellValue(-5m, DateTime.UtcNow);

            // Act
            reportAgent.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            salesFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            salesFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("sales must be >= 0");
            salesFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            salesFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            salesFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-4m);

            warehouseFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            warehouseFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("warehouse must be >= 0");
            warehouseFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-5m);

            var warehouseSupportFteCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.WarehouseSupportFte));
            warehouseSupportFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseSupportFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseSupportFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            warehouseSupportFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-2.5m);

            var totalFteCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.TotalFte));
            totalFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            totalFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("total must be >= 0");
            totalFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            totalFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            totalFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-11.5m);
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_validation_dependency_has_invalid_values()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            var salesFteCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.SalesFte));
            var warehouseFteCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.WarehouseFte));

            salesFteCell.SetCellValue(-4m, DateTime.UtcNow);
            warehouseFteCell.SetCellValue(-5m, DateTime.UtcNow);

            // Act
            await reportAgent.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            salesFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            salesFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("sales must be >= 0");
            salesFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            salesFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            salesFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-4m);

            warehouseFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            warehouseFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("warehouse must be >= 0");
            warehouseFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-5m);

            var warehouseSupportFteCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.WarehouseSupportFte));
            warehouseSupportFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseSupportFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseSupportFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            warehouseSupportFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-2.5m);

            var totalFteCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.TotalFte));
            totalFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            totalFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("total must be >= 0");
            totalFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            totalFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            totalFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-11.5m);
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_validation_dependency_has_valid_values()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            var salesFteCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.SalesFte));
            var warehouseFteCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.WarehouseFte));

            salesFteCell.SetCellValue(4m, DateTime.UtcNow);
            warehouseFteCell.SetCellValue(5m, DateTime.UtcNow);

            // Act
            reportAgent.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            salesFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            salesFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            salesFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            salesFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            salesFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)4m);

            warehouseFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)5m);

            var warehouseSupportFteCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.WarehouseSupportFte));
            warehouseSupportFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseSupportFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseSupportFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            warehouseSupportFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)2.5m);

            var totalFteCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.TotalFte));
            totalFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            totalFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            totalFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            totalFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)11.5m);
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_validation_dependency_has_valid_values()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            var salesFteCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.SalesFte));
            var warehouseFteCell = reportAgent.GetCell<IInputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.WarehouseFte));

            salesFteCell.SetCellValue(4m, DateTime.UtcNow);
            warehouseFteCell.SetCellValue(5m, DateTime.UtcNow);

            // Act
            await reportAgent.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            salesFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            salesFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            salesFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            salesFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            salesFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)4m);

            warehouseFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)5m);

            var warehouseSupportFteCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section2, CellIds.WarehouseSupportFte));
            warehouseSupportFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseSupportFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseSupportFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            warehouseSupportFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)2.5m);

            var totalFteCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section1, CellIds.TotalFte));
            totalFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            totalFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            totalFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            totalFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)11.5m);
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_specifying_proprietary_operation_and_protocol()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            NamedDecimalSet expectedValue = new List<NamedValue<decimal>>
            {
                new NamedValue<decimal>("lower-quartile", 4.41666666666667m),
                new NamedValue<decimal>("median", 6.5m),
                new NamedValue<decimal>("upper-quartile", 11.3333333333333m),
            };

            // Act
            reportAgent.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            var quartileCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section3, CellIds.Quartiles));
            quartileCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            quartileCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            quartileCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            quartileCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();

            var actualValue = (NamedDecimalSet)quartileCell.GetCellObjectValue();
            actualValue.GetNames().AsTest().Must().BeEqualTo(expectedValue.GetNames());
            actualValue[0].Value.IsAlmostEqualTo(expectedValue[0].Value).AsTest().Must().BeTrue();
            actualValue[1].Value.IsAlmostEqualTo(expectedValue[1].Value).AsTest().Must().BeTrue();
            actualValue[2].Value.IsAlmostEqualTo(expectedValue[2].Value).AsTest().Must().BeTrue();
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_specifying_proprietary_operation_and_protocol()
        {
            // Arrange
            var report = BuildReport();

            var reportAgent = new ReportAgent(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new TileProtocol(frameworkFactory).ToProtocolFactory(),
                frameworkFactory => new ThisCellGreaterThanZeroProtocol(frameworkFactory).ToProtocolFactory(),
            };

            NamedDecimalSet expectedValue = new List<NamedValue<decimal>>
            {
                new NamedValue<decimal>("lower-quartile", 4.41666666666667m),
                new NamedValue<decimal>("median", 6.5m),
                new NamedValue<decimal>("upper-quartile", 11.3333333333333m),
            };

            // Act
            await reportAgent.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            var quartileCell = reportAgent.GetCell<IOperationOutputCell>(new InReportCellLocator(SectionIds.Section3, CellIds.Quartiles));
            quartileCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            quartileCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            quartileCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            quartileCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();

            var actualValue = (NamedDecimalSet)quartileCell.GetCellObjectValue();
            actualValue.GetNames().AsTest().Must().BeEqualTo(expectedValue.GetNames());
            actualValue[0].Value.IsAlmostEqualTo(expectedValue[0].Value).AsTest().Must().BeTrue();
            actualValue[1].Value.IsAlmostEqualTo(expectedValue[1].Value).AsTest().Must().BeTrue();
            actualValue[2].Value.IsAlmostEqualTo(expectedValue[2].Value).AsTest().Must().BeTrue();
        }

        private static Report BuildReport()
        {
            var isForProfitCell = Cell.CreateEnabledInputCell<bool>(CellIds.IsForProfit);

            var nonProfitAvailabilityCheck = Cell.CreateAvailabilityCheck(
                new[]
                {
                    new AvailabilityCheckStep(Cell.InSameSection(isForProfitCell.Id).HasValue(), Op.Const("got-disabled-1")),
                    new AvailabilityCheckStep(Op.Not(Cell.InSameSection(isForProfitCell.Id).GetValue<bool>()), Op.Const("got-disabled-2")),
                },
                endMessageOp: Op.Const("got-enabled"));

            var restrictedCash = Cell.CreateDisabledInputCell<decimal>(
                CellIds.RestrictedCash,
                availabilityCheck: nonProfitAvailabilityCheck);

            var partiallyRestrictedCash = Cell.CreateDisabledInputCell<decimal>(
                CellIds.PartiallyRestrictedCash,
                availabilityCheck: nonProfitAvailabilityCheck);

            var numberOfSalesFteCell = Cell.CreateEnabledInputCell<decimal>(
                id: CellIds.SalesFte,
                validation:
                    Cell.CreateValidation(
                        new ValidationStepBase[]
                        {
                            new SimpleBooleanValidationStep(Cell.Self().HasValue(), "input required for sales"),
                            new MessageByOpBooleanValidationStep(
                                Op.IsGreaterThanOrEqualTo(
                                    Cell.Self().GetValue<decimal>(),
                                    Op.Const(0m)),
                                Op.Const("sales must be >= 0")),
                        }));

            var numberOfWarehouseFteCell = Cell.CreateEnabledInputCell<decimal>(
                id: CellIds.WarehouseFte,
                validation:
                    Cell.CreateValidation(
                        new ValidationStepBase[]
                        {
                            new MessageByOpBooleanValidationStep(Cell.Self().HasValue(), Op.Const("input required for warehouse")),
                            new SimpleBooleanValidationStep(
                                Op.IsGreaterThanOrEqualTo(
                                    Cell.Self().GetValue<decimal>(),
                                    Op.Const(0m)),
                                "warehouse must be >= 0"),
                        }));

            var numberOfWarehouseSupportFteCell = Cell.CreateOpCell(
                id: CellIds.WarehouseSupportFte,
                operation: Op.IfThenElse(
                    Cell.InSameSection(numberOfWarehouseFteCell.Id).HasValue(),
                    Op.Divide(
                        Cell.InSameSection(numberOfWarehouseFteCell.Id).GetValue<decimal>(),
                        Op.Const(2m)),
                    Op.NotApplicable<decimal>()));

            var numberOfTotalFte = Cell.CreateOpCell(
                    id: CellIds.TotalFte,
                    operation:
                        Op.IfThenElse(
                            Op.AndAlso(
                                Cell.InReport(numberOfSalesFteCell.Id).HasValue(),
                                Cell.InSomeSection(SectionIds.Section2, numberOfWarehouseFteCell.Id).HasValue()),
                            Op.Sum(
                                Cell.InSomeSection(SectionIds.Section2, numberOfSalesFteCell.Id).GetValue<decimal>(),
                                Cell.InReport(numberOfWarehouseFteCell.Id).GetValue<decimal>(),
                                Cell.InSomeSection(SectionIds.Section2, numberOfWarehouseSupportFteCell.Id).GetValue<decimal>()),
                            Op.Abort<decimal>("cannot perform sum")),
                    validation:
                        Cell.CreateValidation(
                            new ValidationStepBase[]
                            {
                                new MessageByOpBooleanValidationStep(
                                    Op.IsEqualTo(
                                        Cell.Self().GetOpExecutionOutcome(),
                                        Op.Const(CellOpExecutionOutcome.Completed)),
                                    falseAction: ValidationStepAction.StopToAbort),
                                new MessageContainedBooleanValidationStep(
                                    new ThisCellGreaterThanZeroOp()),
                            }));

            var scoresCell = Cell.CreateConstCell<NamedDecimalSet>(
                new[]
                {
                    new NamedValue<decimal>("bob", 5),
                    new NamedValue<decimal>("joe", 4),
                    new NamedValue<decimal>("sally", 9),
                    new NamedValue<decimal>("jane", 15),
                    new NamedValue<decimal>("john", 1),
                    new NamedValue<decimal>("ed", 2),
                    new NamedValue<decimal>("lynn", 9),
                    new NamedValue<decimal>("luke", 20),
                    new NamedValue<decimal>("mark", 7),
                    new NamedValue<decimal>("may", 6),
                    new NamedValue<decimal>("april", 13),
                    new NamedValue<decimal>("wally", 5),
                },
                id: CellIds.CoScores);

            var scoresCellCopy = Cell.CreateOpCell(
                Cell.InSameSection(scoresCell.Id).GetValue<NamedDecimalSet>(),
                id: CellIds.CoScoresCopy);

            var intConstCell = new ConstCell<int>(4, id: CellIds.NumberOfTiles);

            var quartileCell = Cell.CreateOpCell(
                new TileOp(
                    Cell.InSameSection(scoresCellCopy.Id).GetValue<NamedDecimalSet>(),
                    Cell.InSameSection(intConstCell.Id).GetValue<int>()),
                id: CellIds.Quartiles);

            var rows1 = new[]
            {
                new Row(new[] { isForProfitCell }),
                new Row(new[] { partiallyRestrictedCash }),
                new Row(new[] { restrictedCash }),
                new Row(new[] { numberOfTotalFte }),
            };

            var rows2 = new[]
            {
                new Row(new[] { numberOfSalesFteCell }),
                new Row(new[] { numberOfWarehouseFteCell }),
                new Row(new[] { numberOfWarehouseSupportFteCell }),
            };

            var rows3 = new[]
            {
                new Row(new[] { intConstCell }),
                new Row(new[] { scoresCellCopy }),
                new Row(new[] { scoresCell }),
                new Row(new[] { quartileCell }),
            };

            var dataRows1 = new DataRows(rows1);
            var dataRows2 = new DataRows(rows2);
            var dataRows3 = new DataRows(rows3);

            var tableRows1 = new TableRows(dataRows: dataRows1);
            var tableRows2 = new TableRows(dataRows: dataRows2);
            var tableRows3 = new TableRows(dataRows: dataRows3);

            var columns1 = new[]
            {
                new Column("the-only-column"),
            };

            var columns2 = new[]
            {
                new Column("the-only-column"),
            };

            var columns3 = new[]
            {
                new Column("the-only-column"),
            };

            var tableColumns1 = new TableColumns(columns1);
            var tableColumns2 = new TableColumns(columns2);
            var tableColumns3 = new TableColumns(columns3);

            var treeTable1 = new TreeTable(tableColumns1, tableRows1);
            var treeTable2 = new TreeTable(tableColumns2, tableRows2);
            var treeTable3 = new TreeTable(tableColumns3, tableRows3);

            var section1 = new Section(SectionIds.Section1, treeTable1);
            var section2 = new Section(SectionIds.Section2, treeTable2);
            var section3 = new Section(SectionIds.Section3, treeTable3);

            var sections = new[]
            {
                section1,
                section2,
                section3,
            };

            var result = new Report("report-id", sections);

            return result;
        }

        private static class SectionIds
        {
            public const string Section1 = "section-1";

            public const string Section2 = "section-2";

            public const string Section3 = "section-3";
        }

        private static class CellIds
        {
            public const string IsForProfit = "is-for-profit";

            public const string RestrictedCash = "restricted-cash";

            public const string PartiallyRestrictedCash = "partially-restricted-cash";

            public const string SalesFte = "sales-fte";

            public const string WarehouseFte = "warehouse-fte";

            public const string WarehouseSupportFte = "warehouse-support-fte";

            public const string TotalFte = "total-fte";

            public const string CoScores = "scores";

            public const string CoScoresCopy = "scores-copy";

            public const string NumberOfTiles = "number-of-tiles";

            public const string Quartiles = "quartiles";
        }

        private class TileOp : IReturningOperation<NamedDecimalSet>
        {
            public TileOp(
                IReturningOperation<NamedDecimalSet> setOp,
                IReturningOperation<int> numberOfTilesOp)
            {
                if (setOp == null)
                {
                    throw new ArgumentNullException(nameof(setOp));
                }

                if (numberOfTilesOp == null)
                {
                    throw new ArgumentNullException(nameof(numberOfTilesOp));
                }

                this.SetOp = setOp;
                this.NumberOfTilesOp = numberOfTilesOp;
            }

            public IReturningOperation<int> NumberOfTilesOp { get; private set; }

            public IReturningOperation<NamedDecimalSet> SetOp { get; private set; }
        }

        private class ThisCellGreaterThanZeroOp : IReturningOperation<ValidationBoolWithMessage>
        {
        }

        private class TileProtocol : SyncSpecificReturningProtocolBase<TileOp, NamedDecimalSet>
        {
            private readonly ISyncReturningProtocol<GetProtocolOp, IProtocol> protocolFactory;

            public TileProtocol(
                IProtocolFactory protocolFactory)
            {
                if (protocolFactory == null)
                {
                    throw new ArgumentNullException(nameof(protocolFactory));
                }

                this.protocolFactory = protocolFactory;
            }

            public override NamedDecimalSet Execute(
                TileOp operation)
            {
                if (operation == null)
                {
                    throw new ArgumentNullException(nameof(operation));
                }

                var set = this.protocolFactory.GetProtocolAndExecuteViaReflection<NamedDecimalSet>(operation.SetOp);

                var tiles = this.protocolFactory.GetProtocolAndExecuteViaReflection<int>(operation.NumberOfTilesOp);

                if (tiles != 4)
                {
                    throw new NotSupportedException();
                }

                var doubleSet = set.GetValues().Select(Convert.ToDouble).ToArray();

                var lowerQuartile = doubleSet.LowerQuartile();

                var upperQuartile = doubleSet.UpperQuartile();

                var median = doubleSet.Median();

                var result = new[]
                {
                    new NamedValue<decimal>("lower-quartile", Convert.ToDecimal(lowerQuartile)),
                    new NamedValue<decimal>("median", Convert.ToDecimal(median)),
                    new NamedValue<decimal>("upper-quartile", Convert.ToDecimal(upperQuartile)),
                };

                return result;
            }
        }

        private class ThisCellGreaterThanZeroProtocol : SyncSpecificReturningProtocolBase<ThisCellGreaterThanZeroOp, ValidationBoolWithMessage>
        {
            private readonly ISyncReturningProtocol<GetProtocolOp, IProtocol> protocolFactory;

            public ThisCellGreaterThanZeroProtocol(
                IProtocolFactory protocolFactory)
            {
                if (protocolFactory == null)
                {
                    throw new ArgumentNullException(nameof(protocolFactory));
                }

                this.protocolFactory = protocolFactory;
            }

            public override ValidationBoolWithMessage Execute(
                ThisCellGreaterThanZeroOp operation)
            {
                if (operation == null)
                {
                    throw new ArgumentNullException(nameof(operation));
                }

                var outcome = this.protocolFactory.GetProtocolAndExecuteViaReflection<bool>(
                    Op.IsGreaterThan(
                        Cell.Self().GetValue<decimal>(),
                        Op.Const(0m)));

                var result = new ValidationBoolWithMessage(outcome, "total must be >= 0");

                return result;
            }
        }
    }
}
