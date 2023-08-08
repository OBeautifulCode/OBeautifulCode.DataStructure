﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureDummyFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.DataStructure.Test source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Math.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// A Dummy Factory for types in <see cref="OBeautifulCode.DataStructure"/>.
    /// </summary>
#if !OBeautifulCodeDataStructureSolution
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.DataStructure.Test", "See package version number")]
    internal
#else
    public
#endif
    class DataStructureDummyFactory : DefaultDataStructureDummyFactory
    {
        public DataStructureDummyFactory()
        {
            // <------------------- ENUMS ------------------------>
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(Availability.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(AvailabilityCheckStatus.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(AvailabilityCheckStepAction.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(BorderStyle.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(BorderWeight.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(BytesPayloadLinkedResourceKind.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(CellOpExecutionOutcome.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(CellOpExecutionStatus.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(CompareOperator.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(FillPatternStyle.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(HorizontalAlignment.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(InnerBorderEdges.None);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(LinkTarget.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MediaKind.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MessageFormatKind.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(NumberFormatDigitGroupKind.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(NumberFormatNegativeDisplayKind.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(NumberFormatPercentDisplayKind.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(OuterBorderSides.None);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(SlotSelectionStrategy.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(StringPayloadLinkedResourceKind.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(UrlLinkedResourceKind.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(ValidationStatus.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(ValidationStepAction.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(Validity.Unknown);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(VerticalAlignment.Unknown);

            // <------------------- INTERFACES ------------------------>
            AutoFixtureBackedDummyFactory.AddDummyCreator<IAvailabilityCheckCell>(A.Dummy<NotSlottedCellBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<ICell>(A.Dummy<CellBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<INotSlottedCell>(A.Dummy<NotSlottedCellBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<IValidationCell>(A.Dummy<NotSlottedCellBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<ICellValueFormat<Version>>(A.Dummy<CellValueFormatBase<Version>>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<IHoverOver>(A.Dummy<HoverOverBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<ILink>(A.Dummy<SimpleLink>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<ILinkedResource>(A.Dummy<LinkedResourceBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<IMedia>(A.Dummy<MediaBase>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<IOperationOutputCell<Version>>(A.Dummy<OperationCell<Version>>);
            AutoFixtureBackedDummyFactory.AddDummyCreator<IDetails>(A.Dummy<DetailsBase>);

            // <------------------- OPERATIONS ------------------------>
            RegisterReturningOperation<bool>();
            RegisterReturningOperation<decimal>();
            RegisterReturningOperation<string>();
            RegisterReturningOperation<ValidationResult>();
            RegisterReturningOperation<AvailabilityCheckResult>();
            RegisterReturningOperation<Availability>();
            RegisterReturningOperation<Validity>();
            RegisterReturningOperation<CellLocatorBase>();
            RegisterReturningOperation<CompareOperator>();

            // <------------------- MODELS ------------------------>

            // Report
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var numberOfSections = ThreadSafeRandom.Next(1, 4);

                var result = new Report(A.Dummy<string>(), Some.ReadOnlyDummies<Section>(numberOfSections).ToList(), A.Dummy<string>(), A.Dummy<UtcDateTime>(), Some.ReadOnlyDummies<SimpleLink>().ToList(), A.Dummy<AdditionalReportInfo>(), A.Dummy<ReportFormat>());

                return result;
            });

            // TreeTable
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var numberOfColumns = GetRandomNumberOfColumns();

                var columns = Some.ReadOnlyDummies<Column>(numberOfColumns).ToList();

                var tableColumns = new TableColumns(columns, A.Dummy<ColumnFormat>());

                var tableRows = BuildTableRows(columns.Count);

                var result = new TreeTable(tableColumns, tableRows, A.Dummy<TableFormat>());

                return result;
            });

            // TableRows
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = BuildTableRows(GetRandomNumberOfColumns());

                return result;
            });

            // HeaderRows
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var headerRows = BuildFlatRows(GetRandomNumberOfColumns());

                var result = new HeaderRows(headerRows, A.Dummy<HeaderRowsFormat>());

                return result;
            });

            // DataRows
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var allDataRows = BuildRowBases(GetRandomNumberOfColumns());

                var result = new DataRows(allDataRows, A.Dummy<DataRowsFormat>());

                return result;
            });

            // FooterRows
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var footerRows = BuildFlatRows(GetRandomNumberOfColumns());

                var result = new FooterRows(footerRows, A.Dummy<FooterRowsFormat>());

                return result;
            });

            // Row
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = BuildRow(GetRandomNumberOfColumns());

                return result;
            });

            // FlatRow
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = BuildFlatRow(GetRandomNumberOfColumns(), allowSpanningCells: true);

                return result;
            });

            // SlottedCell
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var columnsSpanned = GetRandomColumnsSpannedByCell();

                var cells = Some.ReadOnlyDummies<NotSlottedCellBase>().Select(_ => _.DeepCloneWithColumnsSpanned(columnsSpanned)).Cast<NotSlottedCellBase>().ToList();

                var slotIdToCellMap = cells.ToDictionary(_ => A.Dummy<string>(), _ => (INotSlottedCell)_);

                var defaultSlotId = slotIdToCellMap.ElementAt(ThreadSafeRandom.Next(0, slotIdToCellMap.Count)).Key;

                var result = new SlottedCell(slotIdToCellMap, defaultSlotId, A.Dummy<string>(), columnsSpanned, A.Dummy<string>());

                return result;
            });

            // InputCell<Version>
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new InputCell<Version>(
                    A.Dummy<string>(),
                    GetRandomColumnsSpannedByCell(),
                    A.Dummy<string>(),
                    A.Dummy<Validation>(),
                    Some.ReadOnlyDummies<CellValidationEventBase>().ToList(),
                    A.Dummy<Availability>(),
                    A.Dummy<AvailabilityCheck>(),
                    Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>().ToList(),
                    Some.ReadOnlyDummies<CellInputEventBase>().ToList(),
                    A.Dummy<ICellValueFormat<Version>>(),
                    A.Dummy<CellFormat>(), A.Dummy<IHoverOver>());

                return result;
            });

            // NullCell
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new NullCell(
                    A.Dummy<string>(),
                    GetRandomColumnsSpannedByCell(),
                    A.Dummy<string>(),
                    A.Dummy<Validation>(),
                    Some.ReadOnlyDummies<CellValidationEventBase>().ToList(),
                    A.Dummy<Availability>(),
                    A.Dummy<AvailabilityCheck>(),
                    Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>().ToList(),
                    A.Dummy<CellFormat>(),
                    A.Dummy<IHoverOver>(),
                    A.Dummy<ILink>());

                return result;
            });

            // ConstCell<Version>
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new ConstCell<Version>(
                    A.Dummy<Version>(),
                    A.Dummy<string>(),
                    GetRandomColumnsSpannedByCell(),
                    A.Dummy<string>(),
                    A.Dummy<Validation>(),
                    Some.ReadOnlyDummies<CellValidationEventBase>().ToList(),
                    A.Dummy<Availability>(),
                    A.Dummy<AvailabilityCheck>(),
                    Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>().ToList(),
                    A.Dummy<ICellValueFormat<Version>>(),
                    A.Dummy<CellFormat>(),
                    A.Dummy<IHoverOver>(),
                    A.Dummy<ILink>());

                return result;
            });

            // OperationOutputCell<Version>
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new OperationCell<Version>(
                    A.Dummy<IReturningOperation<Version>>(),
                    A.Dummy<string>(),
                    GetRandomColumnsSpannedByCell(),
                    A.Dummy<string>(),
                    A.Dummy<Validation>(),
                    Some.ReadOnlyDummies<CellValidationEventBase>().ToList(),
                    A.Dummy<Availability>(),
                    A.Dummy<AvailabilityCheck>(),
                    Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>().ToList(),
                    Some.ReadOnlyDummies<CellOpExecutionEventBase>().ToList(),
                    A.Dummy<ICellValueFormat<Version>>(),
                    A.Dummy<CellFormat>(),
                    A.Dummy<IHoverOver>(),
                    A.Dummy<ILink>());

                return result;
            });

            // DateTimeFormat
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var localize = A.Dummy<bool>();

                var result = new DateTimeFormat(A.Dummy<DateTimeFormatKind>(), A.Dummy<CultureKind>(), localize, localize ? A.Dummy<StandardTimeZone>() : (StandardTimeZone?)null);

                return result;
            });

            // ReportFormat
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var displayTimestamp = A.Dummy<bool>();

                var result = new ReportFormat(displayTimestamp, displayTimestamp ? A.Dummy<DateTimeFormat>() : null, A.Dummy<ReportFormatOptions>());

                return result;
            });

            // Color
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = Color.FromArgb(A.Dummy<byte>(), A.Dummy<byte>(), A.Dummy<byte>());

                return result;
            });

            // CellValueFormat
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CurrencyCellValueFormat<Version>(A.Dummy<CurrencyCode>(), A.Dummy<ZeroOrPositiveInteger>(), A.Dummy<MidpointRounding>(), A.Dummy<char>(), A.Dummy<NumberFormatDigitGroupKind>(), A.Dummy<char>(), A.Dummy<NumberFormatNegativeDisplayKind>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new NumberCellValueFormat<Version>(A.Dummy<string>(), A.Dummy<string>(), A.Dummy<ZeroOrPositiveInteger>(), A.Dummy<MidpointRounding>(), A.Dummy<char>(), A.Dummy<NumberFormatDigitGroupKind>(), A.Dummy<char>(), A.Dummy<NumberFormatNegativeDisplayKind>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new NullNumberCellFormat<decimal>(A.Dummy<ZeroOrPositiveInteger>(), A.Dummy<MidpointRounding>(), A.Dummy<char>(), A.Dummy<NumberFormatDigitGroupKind>(), A.Dummy<char>(), A.Dummy<NumberFormatNegativeDisplayKind>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new NullNumberCellFormat<double>(A.Dummy<ZeroOrPositiveInteger>(), A.Dummy<MidpointRounding>(), A.Dummy<char>(), A.Dummy<NumberFormatDigitGroupKind>(), A.Dummy<char>(), A.Dummy<NumberFormatNegativeDisplayKind>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new NullNumberCellFormat<int>(A.Dummy<ZeroOrPositiveInteger>(), A.Dummy<MidpointRounding>(), A.Dummy<char>(), A.Dummy<NumberFormatDigitGroupKind>(), A.Dummy<char>(), A.Dummy<NumberFormatNegativeDisplayKind>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new NullNumberCellFormat<Version>(A.Dummy<ZeroOrPositiveInteger>(), A.Dummy<MidpointRounding>(), A.Dummy<char>(), A.Dummy<NumberFormatDigitGroupKind>(), A.Dummy<char>(), A.Dummy<NumberFormatNegativeDisplayKind>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new PercentCellValueFormat<Version>(A.Dummy<NumberFormatPercentDisplayKind>(), A.Dummy<ZeroOrPositiveInteger>(), A.Dummy<MidpointRounding>(), A.Dummy<char>(), A.Dummy<NumberFormatDigitGroupKind>(), A.Dummy<char>(), A.Dummy<NumberFormatNegativeDisplayKind>(), A.Dummy<string>());

                return result;
            });

            // Events
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellAvailabilityCheckClearedEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellAvailabilityCheckDeterminedCellDisabledEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellAvailabilityCheckDeterminedCellEnabledEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellAvailabilityCheckFailedEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellInputAppliedEvent<Version>(A.Dummy<UtcDateTime>(), A.Dummy<Version>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellInputClearedEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellOpExecutionAbortedEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellOpExecutionClearedEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellOpExecutionCompletedEvent<Version>(A.Dummy<UtcDateTime>(), A.Dummy<string>(), A.Dummy<Version>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellOpExecutionDeemedNotApplicableEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellOpExecutionFailedEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellValidationAbortedEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellValidationClearedEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellValidationDeemedNotApplicableEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellValidationDeterminedCellInvalidEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellValidationDeterminedCellValidEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>(), A.Dummy<string>());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new CellValidationFailedEvent(A.Dummy<UtcDateTime>(), A.Dummy<string>());

                return result;
            });

            // Details
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var result = new LogoDetails(A.Dummy<MediaBase>().Whose(_ => _.MediaKind == MediaKind.Image));

                return result;
            });
        }

        private static TableRows BuildTableRows(
            int numberOfColumns)
        {
            var allHeaderRows = BuildFlatRows(numberOfColumns);

            var headerRows = new HeaderRows(allHeaderRows, A.Dummy<HeaderRowsFormat>());

            var allFooterRows = BuildFlatRows(numberOfColumns);

            var footerRows = new FooterRows(allFooterRows, A.Dummy<FooterRowsFormat>());

            var allDataRows = BuildRowBases(numberOfColumns);

            var dataRows = new DataRows(allDataRows, A.Dummy<DataRowsFormat>());

            var result = new TableRows(headerRows, dataRows, footerRows, A.Dummy<RowFormat>());

            return result;
        }

        private static IReadOnlyList<FlatRow> BuildFlatRows(
            int numberOfColumns)
        {
            var numberOfRows = GetRandomNumberOfRows();

            var result = new List<FlatRow>();

            for (var x = 0; x < numberOfRows; x++)
            {
                var allowSpanningCells = (x + 1) != numberOfRows;

                var headerRow = BuildFlatRow(numberOfColumns, allowSpanningCells);

                result.Add(headerRow);
            }

            return result;
        }

        private static FlatRow BuildFlatRow(
            int numberOfColumns,
            bool allowSpanningCells)
        {
            var cells = BuildRowCells(numberOfColumns, allowSpanningCells);

            var result = new FlatRow(cells, A.Dummy<string>(), A.Dummy<RowFormat>());

            return result;

        }

        private static IReadOnlyList<RowBase> BuildRowBases(
            int numberOfColumns,
            int depth = 0)
        {
            var numberOfRows = GetRandomNumberOfRows();

            var result = new List<RowBase>();

            for (var x = 0; x < numberOfRows; x++)
            {
                var dataRow = BuildRowBase(numberOfColumns, depth);

                result.Add(dataRow);
            }

            return result;
        }

        private static RowBase BuildRowBase(
            int numberOfColumns,
            int depth = 0)
        {
            var hasChildren = A.Dummy<bool>();

            RowBase result;
            
            if (hasChildren)
            {
                result = BuildRow(numberOfColumns, depth);
            }
            else
            {
                result = BuildFlatRow(numberOfColumns, allowSpanningCells: true);
            }

            return result;
        }

        private static Row BuildRow(
            int numberOfColumns,
            int depth = 0)
        {
            var cells = BuildRowCells(numberOfColumns, allowSpanningCells: true);

            var childRows = depth == 2
                ? new RowBase[0]
                : BuildRowBases(numberOfColumns, depth + 1);

            var expandedSummaryRow = childRows.Any()
                ? new[] { BuildFlatRow(numberOfColumns, allowSpanningCells: true) }
                : null;

            var collapsedSummaryRow = childRows.Any()
                ? new[] { BuildFlatRow(numberOfColumns, allowSpanningCells: true) }
                : null;

            var result = new Row(cells, A.Dummy<string>(), A.Dummy<RowFormat>(), childRows, expandedSummaryRow, collapsedSummaryRow);

            return result;
        }

        private static IReadOnlyList<ICell> BuildRowCells(
            int numberOfColumns,
            bool allowSpanningCells)
        {
            var columnsSpanned = 0;

            var result = new List<ICell>();

            while (columnsSpanned != numberOfColumns)
            {
                int columnsSpannedByThisCell;

                if (allowSpanningCells && ShouldCellSpanColumns() && ((columnsSpanned + 1) != numberOfColumns))
                {
                    columnsSpannedByThisCell = ThreadSafeRandom.Next(2, (numberOfColumns - columnsSpanned + 1));
                }
                else
                {
                    columnsSpannedByThisCell = 1;
                }

                var cell = InternalDeepCloneWithColumnsSpanned(A.Dummy<CellBase>(), columnsSpannedByThisCell);

                columnsSpanned += columnsSpannedByThisCell;

                result.Add(cell);
            }

            return result;
        }

        private static int GetRandomNumberOfColumns()
        {
            var result = ThreadSafeRandom.Next(1, 4);

            return result;
        }

        private static int GetRandomNumberOfRows()
        {
            var result = ThreadSafeRandom.Next(0, 4);

            return result;
        }

        private static bool ShouldCellSpanColumns()
        {
            var result = ThreadSafeRandom.Next(0, 2) == 0;

            return result;
        }

        private static int? GetRandomColumnsSpannedByCell()
        {
            var result = ShouldCellSpanColumns()
                ? GetRandomNumberOfColumns()
                : (int?)null;

            return result;
        }

        private static void RegisterReturningOperation<T>()
        {
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(ThrowOpExecutionAbortedExceptionOp<T>),
                        typeof(NullReturningOp<T>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (ReturningOperationBase<T>)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator<IReturningOperation<T>>(A.Dummy<ReturningOperationBase<T>>);
        }

        private static CellBase InternalDeepCloneWithColumnsSpanned(
            CellBase cell,
            int columnsSpanned)
        {
            CellBase result;

            if (cell is SlottedCell slottedCell)
            {
                result = new SlottedCell(
                    slottedCell.SlotIdToCellMap.ToDictionary(_ => _.Key, _ => (INotSlottedCell)((NotSlottedCellBase)_.Value).DeepCloneWithColumnsSpanned(columnsSpanned)),
                    slottedCell.DefaultSlotId,
                    slottedCell.Id,
                    columnsSpanned,
                    slottedCell.Details);
            }
            else
            {
                result = cell.DeepCloneWithColumnsSpanned(columnsSpanned);
            }

            return result;
        }
    }
}