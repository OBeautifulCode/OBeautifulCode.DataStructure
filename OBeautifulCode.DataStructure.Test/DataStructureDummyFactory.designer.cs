﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.156.0)
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

    /// <summary>
    /// The default (code generated) Dummy Factory.
    /// Derive from this class to add any overriding or custom registrations.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [GeneratedCode("OBeautifulCode.CodeGen.ModelObject", "1.0.156.0")]
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
                () => new MediaReference(
                                 A.Dummy<string>(),
                                 A.Dummy<MediaReferenceKind>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(DecimalCell),
                        typeof(HtmlCell),
                        typeof(MediaReferenceCell),
                        typeof(NullCell),
                        typeof(SlottedCell),
                        typeof(StringCell),
                        typeof(ColumnSpanningDecimalCell),
                        typeof(ColumnSpanningHtmlCell),
                        typeof(ColumnSpanningMediaReferenceCell),
                        typeof(ColumnSpanningNullCell),
                        typeof(ColumnSpanningSlottedCell),
                        typeof(ColumnSpanningStringCell)
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
                        typeof(ColumnSpanningDecimalCell),
                        typeof(ColumnSpanningHtmlCell),
                        typeof(ColumnSpanningMediaReferenceCell),
                        typeof(ColumnSpanningNullCell),
                        typeof(ColumnSpanningStringCell)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (ColumnSpanningStandardCellBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new DecimalCell(
                                 A.Dummy<decimal>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new HtmlCell(
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new MediaReferenceCell(
                                 A.Dummy<MediaReference>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new NullCell(
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new SlottedCell(
                                 A.Dummy<IReadOnlyDictionary<string, IHaveValueCell>>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new StringCell(
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ColumnSpanningDecimalCell(
                                 A.Dummy<decimal>(),
                                 A.Dummy<int>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ColumnSpanningHtmlCell(
                                 A.Dummy<string>(),
                                 A.Dummy<int>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ColumnSpanningMediaReferenceCell(
                                 A.Dummy<MediaReference>(),
                                 A.Dummy<int>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ColumnSpanningNullCell(
                                 A.Dummy<int>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ColumnSpanningSlottedCell(
                                 A.Dummy<IReadOnlyDictionary<string, IHaveValueCell>>(),
                                 A.Dummy<string>(),
                                 A.Dummy<int>(),
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ColumnSpanningStringCell(
                                 A.Dummy<string>(),
                                 A.Dummy<int>(),
                                 A.Dummy<string>(),
                                 A.Dummy<string>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<IHoverOver>(),
                                 A.Dummy<ILink>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(DecimalCell),
                        typeof(HtmlCell),
                        typeof(MediaReferenceCell),
                        typeof(NullCell),
                        typeof(StringCell),
                        typeof(ColumnSpanningDecimalCell),
                        typeof(ColumnSpanningHtmlCell),
                        typeof(ColumnSpanningMediaReferenceCell),
                        typeof(ColumnSpanningNullCell),
                        typeof(ColumnSpanningStringCell)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (StandardCellBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new Column(
                                 A.Dummy<string>(),
                                 A.Dummy<ColumnFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new TableColumns(
                                 A.Dummy<IReadOnlyList<Column>>(),
                                 A.Dummy<ColumnFormat>()));

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
                () => new FillPattern(
                                 A.Dummy<FillPatternStyle>(),
                                 A.Dummy<Color>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FontFormat(
                                 A.Dummy<Color?>(),
                                 A.Dummy<IReadOnlyList<string>>(),
                                 A.Dummy<decimal?>(),
                                 A.Dummy<FontFormatOptions?>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(ColumnFormat),
                        typeof(DataRowsFormat),
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
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(CellFormat),
                        typeof(ColumnFormat),
                        typeof(DataRowsFormat),
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
                () => new ColumnFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<bool?>(),
                                 A.Dummy<ColumnFormatOptions?>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new DataRowsFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<RowFormat>(),
                                 A.Dummy<IReadOnlyList<RowFormat>>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new HeaderRowsFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<RowFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new RowFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<CellFormat>(),
                                 A.Dummy<int?>(),
                                 A.Dummy<RowFormatOptions?>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new TableFormat(
                                 A.Dummy<IReadOnlyList<OuterBorder>>(),
                                 A.Dummy<IReadOnlyList<InnerBorder>>(),
                                 A.Dummy<CellFormat>()));

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
                () => new HtmlHoverOver(
                                 A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new StringHoverOver(
                                 A.Dummy<string>()));

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
                () => new DataRows(
                                 A.Dummy<IReadOnlyList<Row>>(),
                                 A.Dummy<DataRowsFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new FlatRow(
                                 A.Dummy<IReadOnlyList<ICell>>(),
                                 A.Dummy<string>(),
                                 A.Dummy<RowFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new HeaderRows(
                                 A.Dummy<IReadOnlyList<FlatRow>>(),
                                 A.Dummy<HeaderRowsFormat>()));

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
                () => new TableRows(
                                 A.Dummy<HeaderRows>(),
                                 A.Dummy<DataRows>(),
                                 A.Dummy<RowFormat>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new TreeTable(
                                 A.Dummy<TableColumns>(),
                                 A.Dummy<TableRows>(),
                                 A.Dummy<TableFormat>()));
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