﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.157.0)
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
    [GeneratedCode("OBeautifulCode.CodeGen.ModelObject", "1.0.157.0")]
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
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(CellOpExecutionFailedWithCellNotFoundEvent),
                        typeof(CellOpExecutionFailedWithMissingCellValueEvent),
                        typeof(CellOpExecutionAbortedEvent),
                        typeof(CellOpExecutionFailedWithExceptionEvent),
                        typeof(CellOpExecutionCompletedEvent<Version>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (CellOpExecutionEventBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellOpExecutionFailedWithCellNotFoundEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<CellLocator>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellOpExecutionFailedWithMissingCellValueEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<CellLocator>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellOpExecutionAbortedEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new InputAppliedToCellEvent<Version>(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<Version>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellOpExecutionFailedWithExceptionEvent(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellOpExecutionCompletedEvent<Version>(
                                 A.Dummy<DateTime>(),
                                 A.Dummy<string>(),
                                 A.Dummy<Version>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new MediaReference(
                                 A.Dummy<string>(),
                                 A.Dummy<MediaReferenceKind>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new InputCell<Version>(
                                 A.Dummy<InputAppliedToCellEvent<Version>>(),
                                 A.Dummy<ValidationConditions>(),
                                 A.Dummy<CellValidationEventBase>(),
                                 A.Dummy<string>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

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
                        typeof(OperationCell<Version>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (OperationOutputCellBase<Version>)AD.ummy(randomType);

                    return result;
                });

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
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(OperationCell<Version>),
                        typeof(ConstCell<Version>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (OutputCellBase<Version>)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new OperationCell<Version>(
                                 A.Dummy<IReturningOperation<Version>>(),
                                 A.Dummy<CellOpExecutionEventBase>(),
                                 A.Dummy<ValidationConditions>(),
                                 A.Dummy<CellValidationEventBase>(),
                                 A.Dummy<string>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ConstCell<Version>(
                                 A.Dummy<Version>(),
                                 A.Dummy<string>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(InputCell<Version>),
                        typeof(OperationCell<Version>),
                        typeof(ConstCell<Version>),
                        typeof(SlottedCell),
                        typeof(NullCell)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (CellBase)AD.ummy(randomType);

                    return result;
                });

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
                () => new SlottedCell(
                                 A.Dummy<IReadOnlyDictionary<string, INotSlottedCell>>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new NullCell(
                                 A.Dummy<string>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new InnerBorder(
                                 A.Dummy<BorderWeight>(),
                                 A.Dummy<BorderStyle>(),
                                 A.Dummy<Color>(),
                                 A.Dummy<InnerBorderEdges>()));

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
                        typeof(InnerBorder),
                        typeof(OuterBorder)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (BorderBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FillPattern(
                                 A.Dummy<FillPatternStyle>(),
                                 A.Dummy<Color>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(HeaderRowsFormat),
                        typeof(DataRowsFormat),
                        typeof(TableFormat),
                        typeof(RowFormat),
                        typeof(ColumnFormat)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (MultiCellRegionFormatBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(HeaderRowsFormat),
                        typeof(CellFormat),
                        typeof(DataRowsFormat),
                        typeof(TableFormat),
                        typeof(RowFormat),
                        typeof(ColumnFormat)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (RegionFormatBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FontFormat(
                                 A.Dummy<Color?>(),
                                 A.Dummy<IReadOnlyList<string>>(),
                                 A.Dummy<decimal?>(),
                                 A.Dummy<FontFormatOptions?>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new HeaderRowsFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<RowFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new SectionFormat());

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ReportFormat());

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
                () => new BytesPayloadLinkedResource(
                                 A.Dummy<byte[]>(),
                                 A.Dummy<BytesPayloadLinkedResourceKind>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new StringPayloadLinkedResource(
                                 A.Dummy<string>(),
                                 A.Dummy<StringPayloadLinkedResourceKind>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new UrlLinkedResource(
                                 A.Dummy<string>(),
                                 A.Dummy<UrlLinkedResourceKind>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new SimpleLink(
                                 A.Dummy<LinkTarget>(),
                                 A.Dummy<ILinkedResource>(),
                                 A.Dummy<IReadOnlyList<RegionFormatBase>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new CellLocator(
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<SlotSelectionStrategy>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new Section(
                                 A.Dummy<string>(),
                                 A.Dummy<TreeTable>(),
                                 A.Dummy<string>(),
                                 A.Dummy<SectionFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new TableRows(
                                 A.Dummy<HeaderRows>(),
                                 A.Dummy<DataRows>(),
                                 A.Dummy<RowFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new HeaderRows(
                                 A.Dummy<IReadOnlyList<FlatRow>>(),
                                 A.Dummy<HeaderRowsFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new Report(
                                 A.Dummy<string>(),
                                 A.Dummy<IReadOnlyCollection<Section>>(),
                                 A.Dummy<string>(),
                                 A.Dummy<ReportFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ValidationConditions(
                                 A.Dummy<IReadOnlyList<ValidationCondition>>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ValidationCondition(
                                 A.Dummy<IReturningOperation<bool>>(),
                                 A.Dummy<IReturningOperation<string>>(),
                                 A.Dummy<ValidationConditionKind>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new DataRowsFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<RowFormat>(),
                                 A.Dummy<IReadOnlyList<RowFormat>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new TableFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<CellFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new RowFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<RowFormatOptions?>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ColumnFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<bool?>(),
                                 A.Dummy<ColumnFormatOptions?>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new HtmlHoverOver(
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new StringHoverOver(
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new AndAlsoOp(
                                 A.Dummy<IReadOnlyCollection<IReturningOperation<bool>>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new OrElseOp(
                                 A.Dummy<IReadOnlyCollection<IReturningOperation<bool>>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new NotOp(
                                 A.Dummy<IReturningOperation<bool>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new SumOp(
                                 A.Dummy<IReadOnlyCollection<IReturningOperation<decimal>>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ExecuteOperationCellIfNecessaryOp<Version>(
                                 A.Dummy<IOperationOutputCell<Version>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new IfThenElseOp<Version>(
                                 A.Dummy<IReturningOperation<bool>>(),
                                 A.Dummy<IReturningOperation<Version>>(),
                                 A.Dummy<IReturningOperation<Version>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new HasCellValueOp(
                                 A.Dummy<CellLocator>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new GetConstOp<Version>(
                                 A.Dummy<Version>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new GetCellValueOp<Version>(
                                 A.Dummy<CellLocator>(),
                                 A.Dummy<IReturningOperation<Version>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FlatRow(
                                 A.Dummy<IReadOnlyList<ICell>>(),
                                 A.Dummy<string>(),
                                 A.Dummy<RowFormat>()));

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
                () => new TreeTable(
                                 A.Dummy<TableColumns>(),
                                 A.Dummy<TableRows>(),
                                 A.Dummy<TableFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new Column(
                                 A.Dummy<string>(),
                                 A.Dummy<ColumnFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new TableColumns(
                                 A.Dummy<IReadOnlyList<Column>>(),
                                 A.Dummy<ColumnFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new DataRows(
                                 A.Dummy<IReadOnlyList<Row>>(),
                                 A.Dummy<DataRowsFormat>()));
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