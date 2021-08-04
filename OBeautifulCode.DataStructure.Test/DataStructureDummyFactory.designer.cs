﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.160.0)
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using global::System;
    using global::System.CodeDom.Compiler;
    using global::System.Collections.Concurrent;
    using global::System.Collections.Generic;
    using global::System.Collections.ObjectModel;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Drawing;

    using global::FakeItEasy;

    using global::OBeautifulCode.AutoFakeItEasy;
    using global::OBeautifulCode.DataStructure;
    using global::OBeautifulCode.Math.Recipes;
    using global::OBeautifulCode.Type;

    /// <summary>
    /// The default (code generated) Dummy Factory.
    /// Derive from this class to add any overriding or custom registrations.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [GeneratedCode("OBeautifulCode.CodeGen.ModelObject", "1.0.160.0")]
#if !OBeautifulCodeDataStructureSolution
    internal
#else
    public
#endif
    abstract class DefaultDataStructureDummyFactory : IDummyFactory
    {
        public DefaultDataStructureDummyFactory()
        {
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new AndAlsoOp(
                                 A.Dummy<IReadOnlyCollection<IReturningOperation<bool>>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new BooleanCellValueFormat(
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(InnerBorder),
                        typeof(OuterBorder)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (BorderBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new BytesPayloadLinkedResource(
                                 A.Dummy<byte[]>(),
                                 A.Dummy<BytesPayloadLinkedResourceKind>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CategoricalCellValueFormat<Version>(
                                 A.Dummy<IReadOnlyDictionary<Version, string>>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(ConstCell<Version>),
                        typeof(InputCell<Version>),
                        typeof(NullCell),
                        typeof(OperationCell<Version>),
                        typeof(SlottedCell)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (CellBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<FontFormat>(),
                                 A.Dummy<Color?>(),
                                 A.Dummy<VerticalAlignment?>(),
                                 A.Dummy<HorizontalAlignment?>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<FillPattern>(),
                                 A.Dummy<CellFormatOptions?>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellInputAppliedEvent<Version>(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<Version>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellInputClearedEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(CellInputAppliedEvent<Version>),
                        typeof(CellInputClearedEvent)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (CellInputEventBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellLocator(
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<SlotSelectionStrategy>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellOpExecutionAbortedEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellOpExecutionClearedEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellOpExecutionCompletedEvent<Version>(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>(),
                                 A.Dummy<Version>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellOpExecutionDeemedNotApplicableEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(CellOpExecutionAbortedEvent),
                        typeof(CellOpExecutionClearedEvent),
                        typeof(CellOpExecutionCompletedEvent<Version>),
                        typeof(CellOpExecutionDeemedNotApplicableEvent),
                        typeof(CellOpExecutionFailedEvent)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (CellOpExecutionEventBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellOpExecutionFailedEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellValidationAbortedEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellValidationClearedEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellValidationDeemedNotApplicableEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellValidationDeterminedCellInvalidEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellValidationDeterminedCellValidEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(CellValidationAbortedEvent),
                        typeof(CellValidationClearedEvent),
                        typeof(CellValidationDeemedNotApplicableEvent),
                        typeof(CellValidationDeterminedCellInvalidEvent),
                        typeof(CellValidationDeterminedCellValidEvent),
                        typeof(CellValidationFailedEvent)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (CellValidationEventBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellValidationFailedEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(CategoricalCellValueFormat<Version>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (CellValueFormatBase<Version>)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new Column(
                                 A.Dummy<string>(),
                                 A.Dummy<ColumnFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ColumnFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<bool?>(),
                                 A.Dummy<ColumnFormatOptions?>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CompareOp(
                                 A.Dummy<IReturningOperation<decimal>>(),
                                 A.Dummy<CompareOperator>(),
                                 A.Dummy<IReturningOperation<decimal>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ConstCell<Version>(
                                 A.Dummy<Version>(),
                                 A.Dummy<string>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<string>(),
                                 A.Dummy<Validation>(),
                                 A.Dummy<IReadOnlyList<CellValidationEventBase>>(),
                                 A.Dummy<ICellValueFormat<Version>>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(ConstCell<Version>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (ConstOutputCellBase<Version>)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new DataRows(
                                 A.Dummy<IReadOnlyList<Row>>(),
                                 A.Dummy<DataRowsFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new DataRowsFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<RowFormat>(),
                                 A.Dummy<IReadOnlyList<RowFormat>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new DecimalCellValueFormat(
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<char?>(),
                                 A.Dummy<NumberFormatDigitGroupKind?>(),
                                 A.Dummy<char?>(),
                                 A.Dummy<NumberFormatNegativeDisplayKind?>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ExecuteOperationCellIfNecessaryOp<Version>(
                                 A.Dummy<IOperationOutputCell<Version>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FillPattern(
                                 A.Dummy<FillPatternStyle>(),
                                 A.Dummy<Color>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FlatRow(
                                 A.Dummy<IReadOnlyList<ICell>>(),
                                 A.Dummy<string>(),
                                 A.Dummy<RowFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FontFormat(
                                 A.Dummy<Color?>(),
                                 A.Dummy<IReadOnlyList<string>>(),
                                 A.Dummy<decimal?>(),
                                 A.Dummy<FontFormatOptions?>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FooterRows(
                                 A.Dummy<IReadOnlyList<FlatRow>>(),
                                 A.Dummy<FooterRowsFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FooterRowsFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<RowFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new GetCellValueOp<Version>(
                                 A.Dummy<CellLocator>(),
                                 A.Dummy<IReturningOperation<Version>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new GetConstOp<Version>(
                                 A.Dummy<Version>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new GetNumberOfSignificantDigitsOp(
                                 A.Dummy<IReturningOperation<decimal>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new HasCellValueOp(
                                 A.Dummy<CellLocator>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new HeaderRows(
                                 A.Dummy<IReadOnlyList<FlatRow>>(),
                                 A.Dummy<HeaderRowsFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new HeaderRowsFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<RowFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(HtmlHoverOver),
                        typeof(StringHoverOver)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (HoverOverBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new HtmlCellValueFormat(
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new HtmlHoverOver(
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new IfThenElseOp<Version>(
                                 A.Dummy<IReturningOperation<bool>>(),
                                 A.Dummy<IReturningOperation<Version>>(),
                                 A.Dummy<IReturningOperation<Version>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new InnerBorder(
                                 A.Dummy<BorderWeight>(),
                                 A.Dummy<BorderStyle>(),
                                 A.Dummy<Color>(),
                                 A.Dummy<InnerBorderEdges>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new InputCell<Version>(
                                 A.Dummy<string>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<string>(),
                                 A.Dummy<Validation>(),
                                 A.Dummy<IReadOnlyList<CellValidationEventBase>>(),
                                 A.Dummy<IReadOnlyList<CellInputEventBase>>(),
                                 A.Dummy<ICellValueFormat<Version>>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(InputCell<Version>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (InputCellBase<Version>)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(SimpleLink)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (LinkBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(BytesPayloadLinkedResource),
                        typeof(StringPayloadLinkedResource),
                        typeof(UrlLinkedResource)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (LinkedResourceBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new MediaReference(
                                 A.Dummy<string>(),
                                 A.Dummy<MediaReferenceKind>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(ColumnFormat),
                        typeof(DataRowsFormat),
                        typeof(FooterRowsFormat),
                        typeof(HeaderRowsFormat),
                        typeof(RowFormat),
                        typeof(TableFormat)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (MultiCellRegionFormatBase)AD.ummy(randomType);

                    return result;
                });


            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new NotOp(
                                 A.Dummy<IReturningOperation<bool>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(ConstCell<Version>),
                        typeof(InputCell<Version>),
                        typeof(NullCell),
                        typeof(OperationCell<Version>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (NotSlottedCellBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new NullCell(
                                 A.Dummy<string>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<string>(),
                                 A.Dummy<Validation>(),
                                 A.Dummy<IReadOnlyList<CellValidationEventBase>>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(NullCell)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (NullCellBase)AD.ummy(randomType);

                    return result;
                });


            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new OperationCell<Version>(
                                 A.Dummy<IReturningOperation<Version>>(),
                                 A.Dummy<string>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<string>(),
                                 A.Dummy<Validation>(),
                                 A.Dummy<IReadOnlyList<CellValidationEventBase>>(),
                                 A.Dummy<IReadOnlyList<CellOpExecutionEventBase>>(),
                                 A.Dummy<ICellValueFormat<Version>>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(OperationCell<Version>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (OperationOutputCellBase<Version>)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new OrElseOp(
                                 A.Dummy<IReadOnlyCollection<IReturningOperation<bool>>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new OuterBorder(
                                 A.Dummy<BorderWeight>(),
                                 A.Dummy<BorderStyle>(),
                                 A.Dummy<Color>(),
                                 A.Dummy<OuterBorderSides>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(ConstCell<Version>),
                        typeof(OperationCell<Version>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (OutputCellBase<Version>)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new PercentCellValueFormat(
                                 A.Dummy<NumberFormatPercentDisplayKind?>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<char?>(),
                                 A.Dummy<NumberFormatDigitGroupKind?>(),
                                 A.Dummy<char?>(),
                                 A.Dummy<NumberFormatNegativeDisplayKind?>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(CellFormat),
                        typeof(ColumnFormat),
                        typeof(DataRowsFormat),
                        typeof(FooterRowsFormat),
                        typeof(HeaderRowsFormat),
                        typeof(RowFormat),
                        typeof(TableFormat)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (RegionFormatBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new Report(
                                 A.Dummy<string>(),
                                 A.Dummy<IReadOnlyCollection<Section>>(),
                                 A.Dummy<string>(),
                                 A.Dummy<ReportFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ReportFormat());

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new Row(
                                 A.Dummy<IReadOnlyList<ICell>>(),
                                 A.Dummy<string>(),
                                 A.Dummy<RowFormat>(),
                                 A.Dummy<IReadOnlyList<Row>>(),
                                 A.Dummy<FlatRow>(),
                                 A.Dummy<FlatRow>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(FlatRow),
                        typeof(Row)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (RowBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new RowFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<RowFormatOptions?>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new Section(
                                 A.Dummy<string>(),
                                 A.Dummy<TreeTable>(),
                                 A.Dummy<string>(),
                                 A.Dummy<SectionFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new SectionFormat());

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new SimpleLink(
                                 A.Dummy<LinkTarget>(),
                                 A.Dummy<ILinkedResource>(),
                                 A.Dummy<IReadOnlyList<RegionFormatBase>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new SlottedCell(
                                 A.Dummy<IReadOnlyDictionary<string, INotSlottedCell>>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(CategoricalCellValueFormat<Version>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (StandardCellValueFormatBase<Version>)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new StringHoverOver(
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new StringPayloadLinkedResource(
                                 A.Dummy<string>(),
                                 A.Dummy<StringPayloadLinkedResourceKind>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new SumOp(
                                 A.Dummy<IReadOnlyCollection<IReturningOperation<decimal>>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new TableColumns(
                                 A.Dummy<IReadOnlyList<Column>>(),
                                 A.Dummy<ColumnFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new TableFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<CellFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new TableRows(
                                 A.Dummy<HeaderRows>(),
                                 A.Dummy<DataRows>(),
                                 A.Dummy<FooterRows>(),
                                 A.Dummy<RowFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new TreeTable(
                                 A.Dummy<TableColumns>(),
                                 A.Dummy<TableRows>(),
                                 A.Dummy<TableFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new UrlLinkedResource(
                                 A.Dummy<string>(),
                                 A.Dummy<UrlLinkedResourceKind>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ValidateCellOp(
                                 A.Dummy<IValidationCell>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ValidateUsingConditionsOp(
                                 A.Dummy<IReadOnlyList<ValidationCondition>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new Validation(
                                 A.Dummy<IReturningOperation<ValidationResult>>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ValidationCondition(
                                 A.Dummy<IReturningOperation<bool>>(),
                                 A.Dummy<IReturningOperation<string>>(),
                                 A.Dummy<ValidationConditionKind>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ValidationResult(
                                 A.Dummy<Validity>(),
                                 A.Dummy<IReturningOperation<string>>()));
        }

        /// <inheritdoc />
        public Priority Priority => new FakeItEasy.Priority(1);

        /// <inheritdoc />
        public bool CanCreate(Type type)
        {
            return false;
        }

        /// <inheritdoc />
        public object Create(Type type)
        {
            return null;
        }
    }
}