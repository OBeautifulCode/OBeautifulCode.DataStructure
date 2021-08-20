// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportCacheTest.cs" company="OBeautifulCode">
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

    using static System.FormattableString;

    public static class ReportCacheTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_report_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => new ReportCache(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("report");
        }

        [Fact]
        public static void OperationCells___Should_return_all_operation_cells___When_getting()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var systemUnderTest = new ReportCache(report);

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

            var systemUnderTest = new ReportCache(report);

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

            var systemUnderTest = new ReportCache(report);

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

            var systemUnderTest = new ReportCache(report);

            IReadOnlyCollection<IAvailabilityCheckCell> expected = report.Sections.SelectMany(_ => _.TreeTable.GetAllCells()).OfType<IAvailabilityCheckCell>().ToList();

            // Act
            var actual = systemUnderTest.AvailabilityCheckCells;

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetCell_reportCellLocator___Should_throw_ArgumentNullException___When_parameter_reportCellLocator_is_null()
        {
            // Arrange
            var systemUnderTest = new ReportCache(A.Dummy<Report>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("reportCellLocator");
        }

        [Fact]
        public static void GetCell_reportCellLocator___Should_throw_CellNotFoundException___When_section_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var reportCellLocator = new ReportCellLocator(A.Dummy<string>(), report.Sections.First().TreeTable.GetAllCells().First().Id);

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(reportCellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no section with id '{reportCellLocator.SectionId}'."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((CellLocatorBase)reportCellLocator);
        }

        [Fact]
        public static void GetCell_reportCellLocator___Should_throw_CellNotFoundException___When_cell_with_specified_id_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var section = report.Sections.First();

            var reportCellLocator = new ReportCellLocator(section.Id, A.Dummy<string>());

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(reportCellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no cell with id '{reportCellLocator.CellId}' in section '{section.Id}'."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((CellLocatorBase)reportCellLocator);
        }

        [Fact]
        public static void GetCell_reportCellLocator___Should_throw_CellNotFoundException___When_slotId_specified_for_cell_that_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var section = report.Sections.First();

            var cell = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var reportCellLocator = new ReportCellLocator(section.Id, cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(reportCellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{reportCellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' in section '{section.Id}' is not a slotted cell"));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((CellLocatorBase)reportCellLocator);
        }

        [Fact]
        public static void GetCell_reportCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_but_there_is_no_slot_having_slotId()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var reportCellLocator = new ReportCellLocator(section.Id, cell.Id, A.Dummy<string>());

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(reportCellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{reportCellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' in section '{section.Id}' does not contain a slot having that id."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((CellLocatorBase)reportCellLocator);
        }

        [Fact]
        public static void GetCell_reportCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_and_SlotId_not_specified_and_SlotSelectionStrategy_is_ThrowIfSlotIdNotSpecified()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().First(_ => _ is ISlottedCell);

            var reportCellLocator = new ReportCellLocator(section.Id, cell.Id, null, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(reportCellLocator));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"The operation addresses an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((CellLocatorBase)reportCellLocator);
        }

        [Fact]
        public static void GetCell_reportCellLocator___Should_return_cell___When_cell_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var section = report.Sections.First();

            var expected = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var reportCellLocator = new ReportCellLocator(section.Id, expected.Id);

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = systemUnderTest.GetCell(reportCellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_reportCellLocator___Should_return_cell_in_default_slot___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First();

            var expected = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

            var reportCellLocator = new ReportCellLocator(section.Id, cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = systemUnderTest.GetCell(reportCellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_reportCellLocator___Should_return_cell_in_specified_slot___When_addressing_slot_within_slotted_cell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var cell = section.TreeTable.GetAllCells().OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var expected = (ICell)cell.SlotIdToCellMap[slotId];

            var reportCellLocator = new ReportCellLocator(section.Id, cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = systemUnderTest.GetCell(reportCellLocator);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_sectionCellLocator___Should_throw_ArgumentNullException___When_parameter_sectionCellLocator_is_null()
        {
            // Arrange
            var systemUnderTest = new ReportCache(A.Dummy<Report>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(null, A.Dummy<ICell>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("sectionCellLocator");
        }

        [Fact]
        public static void GetCell_sectionCellLocator___Should_throw_ArgumentNullException___When_parameter_currentCell_is_null()
        {
            // Arrange
            var systemUnderTest = new ReportCache(A.Dummy<Report>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(A.Dummy<SectionCellLocator>(), null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("currentCell");
        }

        [Fact]
        public static void GetCell_sectionCellLocator___Should_throw_ArgumentNullException___When_currentCell_is_not_a_cell_in_the_report()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var systemUnderTest = new ReportCache(report);

            var sectionCellLocator = new SectionCellLocator(report.Sections.First().TreeTable.GetAllCells().First().Id);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(sectionCellLocator, A.Dummy<ICell>()));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString("currentCell is not a cell in the report.");
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((CellLocatorBase)sectionCellLocator);
        }

        [Fact]
        public static void GetCell_sectionCellLocator___Should_throw_CellNotFoundException___When_cell_with_specified_id_is_not_found()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var section = report.Sections.First();

            var currentCell = section.TreeTable.GetAllCells().First();

            var sectionCellLocator = new SectionCellLocator(A.Dummy<string>());

            var systemUnderTest = new ReportCache(report);

            var expectedReportCellLocator = new ReportCellLocator(section.Id, sectionCellLocator.CellId);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(sectionCellLocator, currentCell));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no cell with id '{sectionCellLocator.CellId}' in section '{section.Id}'."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((CellLocatorBase)expectedReportCellLocator);
        }

        [Fact]
        public static void GetCell_sectionCellLocator___Should_throw_CellNotFoundException___When_slotId_specified_for_cell_that_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var section = report.Sections.First();

            var allCells = section.TreeTable.GetAllCells();

            var cell = allCells.First(_ => _ is INotSlottedCell);

            var currentCell = allCells.First(_ => !_.Equals(cell));

            var sectionCellLocator = new SectionCellLocator(cell.Id, A.Dummy<string>());

            var expectedReportCellLocator = new ReportCellLocator(section.Id, sectionCellLocator.CellId, sectionCellLocator.SlotId);

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(sectionCellLocator, currentCell));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{sectionCellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' in section '{section.Id}' is not a slotted cell"));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((CellLocatorBase)expectedReportCellLocator);
        }

        [Fact]
        public static void GetCell_sectionCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_but_there_is_no_slot_having_slotId()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var allCells = section.TreeTable.GetAllCells();

            var cell = allCells.First(_ => _ is ISlottedCell);

            var currentCell = allCells.First(_ => !_.Equals(cell));

            var sectionCellLocator = new SectionCellLocator(cell.Id, A.Dummy<string>());

            var expectedReportCellLocator = new ReportCellLocator(section.Id, sectionCellLocator.CellId, sectionCellLocator.SlotId);

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(sectionCellLocator, currentCell));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Slot id '{sectionCellLocator.SlotId}' was specified, but the addressed cell '{cell.Id}' in section '{section.Id}' does not contain a slot having that id."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((CellLocatorBase)expectedReportCellLocator);
        }

        [Fact]
        public static void GetCell_sectionCellLocator___Should_throw_CellNotFoundException___When_addressing_slotted_cell_and_SlotId_not_specified_and_SlotSelectionStrategy_is_ThrowIfSlotIdNotSpecified()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var allCells = section.TreeTable.GetAllCells();

            var cell = allCells.First(_ => _ is ISlottedCell);

            var currentCell = allCells.First(_ => !_.Equals(cell));

            var sectionCellLocator = new SectionCellLocator(cell.Id, null, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var expectedReportCellLocator = new ReportCellLocator(section.Id, sectionCellLocator.CellId, sectionCellLocator.SlotId);

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCell(sectionCellLocator, currentCell));

            // Assert
            actual.AsTest().Must().BeOfType<CellNotFoundException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"The operation addresses an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."));
            ((CellNotFoundException)actual).CellLocator.AsTest().Must().BeEqualTo((CellLocatorBase)expectedReportCellLocator);
        }

        [Fact]
        public static void GetCell_sectionCellLocator___Should_return_cell___When_cell_is_not_slotted()
        {
            // Arrange
            var report = A.Dummy<Report>();

            var section = report.Sections.First();

            var allCells = section.TreeTable.GetAllCells();

            var expected = section.TreeTable.GetAllCells().First(_ => _ is INotSlottedCell);

            var currentCell = allCells.First(_ => !_.Equals(expected));

            var sectionCellLocator = new SectionCellLocator(expected.Id);

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = systemUnderTest.GetCell(sectionCellLocator, currentCell);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_sectionCellLocator___Should_return_cell_in_default_slot___When_addressing_slotted_cell_and_SlotSelectionStrategy_is_DefaultSlot()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().Any(c => c is ISlottedCell)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var allCells = section.TreeTable.GetAllCells();

            var cell = allCells.OfType<ISlottedCell>().First();

            var currentCell = allCells.First(_ => !_.Equals(cell));

            var expected = (ICell)cell.SlotIdToCellMap[cell.DefaultSlotId];

            var sectionCellLocator = new SectionCellLocator(cell.Id, null, SlotSelectionStrategy.DefaultSlot);

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = systemUnderTest.GetCell(sectionCellLocator, currentCell);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void GetCell_sectionCellLocator___Should_return_cell_in_specified_slot___When_addressing_slot_within_slotted_cell()
        {
            // Arrange
            var report = A.Dummy<Report>().Whose(_ => _.Sections.Any(s => s.TreeTable.GetAllCells().OfType<ISlottedCell>().Any(c => c.SlotIdToCellMap.Count > 1)));

            var section = report.Sections.First(_ => _.TreeTable.GetAllCells().Any(c => c is ISlottedCell));

            var allCells = section.TreeTable.GetAllCells();

            var cell = allCells.OfType<ISlottedCell>().First(_ => _.SlotIdToCellMap.Count > 1);

            var currentCell = allCells.First(_ => !_.Equals(cell));

            var slotId = cell.SlotIdToCellMap.First(_ => _.Key != cell.DefaultSlotId).Key;

            var expected = (ICell)cell.SlotIdToCellMap[slotId];

            var sectionCellLocator = new SectionCellLocator(cell.Id, slotId, SlotSelectionStrategy.ThrowIfSlotIdNotSpecified);

            var systemUnderTest = new ReportCache(report);

            // Act
            var actual = systemUnderTest.GetCell(sectionCellLocator, currentCell);

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }
    }
}
