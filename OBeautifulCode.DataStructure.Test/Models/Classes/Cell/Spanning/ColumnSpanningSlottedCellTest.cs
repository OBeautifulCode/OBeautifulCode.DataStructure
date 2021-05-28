// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnSpanningSlottedCellTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class ColumnSpanningSlottedCellTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static ColumnSpanningSlottedCellTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ColumnSpanningSlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'slotIdToCellMap' contains a white space key",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ColumnSpanningSlottedCell>();

                            var slotIdToCellMap = referenceObject.SlotIdToCellMap.ToDictionary(_ => _.Key, _ => _.Value);

                            slotIdToCellMap.Add(" \r\n ", A.Dummy<IHaveValueCell>());

                            var result = new ColumnSpanningSlottedCell(
                                slotIdToCellMap,
                                referenceObject.DefaultSlotName,
                                referenceObject.ColumnsSpanned);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap", "contains at least one key-value pair with a white space key" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ColumnSpanningSlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'slotIdToCellMap' does not contain the specified defaultSlotName",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ColumnSpanningSlottedCell>();

                            var result = new ColumnSpanningSlottedCell(
                                referenceObject.SlotIdToCellMap,
                                A.Dummy<string>(),
                                referenceObject.ColumnsSpanned);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap does not contain the specified defaultSlotName" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ColumnSpanningSlottedCell>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is 1",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ColumnSpanningSlottedCell>();

                            var result = new ColumnSpanningSlottedCell(
                                referenceObject.SlotIdToCellMap,
                                referenceObject.DefaultSlotName,
                                1);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned is 1; must be >= 2", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ColumnSpanningSlottedCell>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is 0",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ColumnSpanningSlottedCell>();

                            var result = new ColumnSpanningSlottedCell(
                                referenceObject.SlotIdToCellMap,
                                referenceObject.DefaultSlotName,
                                0);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned is 0; must be >= 2", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ColumnSpanningSlottedCell>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is < 0",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ColumnSpanningSlottedCell>();

                            var result = new ColumnSpanningSlottedCell(
                                referenceObject.SlotIdToCellMap,
                                referenceObject.DefaultSlotName,
                                A.Dummy<NegativeInteger>());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned is", "must be >= 2", },
                    });

            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<ColumnSpanningSlottedCell>
                    {
                        Name = "DeepCloneWithSlotIdToCellMap should deep clone object and replace SlotIdToCellMap with the provided slotIdToCellMap",
                        WithPropertyName = "SlotIdToCellMap",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<ColumnSpanningSlottedCell>();

                            var referenceObject = A.Dummy<ColumnSpanningSlottedCell>().ThatIs(_ => !systemUnderTest.SlotIdToCellMap.IsEqualTo(_.SlotIdToCellMap));

                            var slotIdToCellMap = referenceObject.SlotIdToCellMap.ToDictionary(_ => _.Key, _ => _.Value);

                            slotIdToCellMap.Add(systemUnderTest.DefaultSlotName, A.Dummy<IHaveValueCell>());

                            var result = new SystemUnderTestDeepCloneWithValue<ColumnSpanningSlottedCell>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = slotIdToCellMap,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<ColumnSpanningSlottedCell>
                    {
                        Name = "DeepCloneWithDefaultSlotName should deep clone object and replace DefaultSlotName with the provided defaultSlotName",
                        WithPropertyName = "DefaultSlotName",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<ColumnSpanningSlottedCell>();

                            var defaultSlotName = systemUnderTest.SlotIdToCellMap.Keys.First(_ => _ != systemUnderTest.DefaultSlotName);

                            var result = new SystemUnderTestDeepCloneWithValue<ColumnSpanningSlottedCell>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = defaultSlotName,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<ColumnSpanningSlottedCell>
                    {
                        Name = "DeepCloneWithColumnsSpanned should deep clone object and replace ColumnsSpanned with the provided columnsSpanned",
                        WithPropertyName = "ColumnsSpanned",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<ColumnSpanningSlottedCell>();

                            var referenceObject = A.Dummy<ColumnSpanningSlottedCell>().ThatIs(_ => !systemUnderTest.ColumnsSpanned.IsEqualTo(_.ColumnsSpanned));

                            var result = new SystemUnderTestDeepCloneWithValue<ColumnSpanningSlottedCell>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.ColumnsSpanned,
                            };

                            return result;
                        },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    {
                        var slotIdToCellMap = A.Dummy<ColumnSpanningSlottedCell>()
                            .Whose(_ => !_.SlotIdToCellMap.IsEqualTo(ReferenceObjectForEquatableTestScenarios.SlotIdToCellMap))
                            .SlotIdToCellMap
                            .ToDictionary(_ => _.Key, _ => _.Value);

                        slotIdToCellMap.Add(ReferenceObjectForEquatableTestScenarios.DefaultSlotName, A.Dummy<IHaveValueCell>());

                        var result = new EquatableTestScenario<ColumnSpanningSlottedCell>
                        {
                            Name = "Default Code Generated Scenario",
                            ReferenceObject = ReferenceObjectForEquatableTestScenarios,
                            ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new ColumnSpanningSlottedCell[]
                            {
                                new ColumnSpanningSlottedCell(
                                    ReferenceObjectForEquatableTestScenarios.SlotIdToCellMap,
                                    ReferenceObjectForEquatableTestScenarios.DefaultSlotName,
                                    ReferenceObjectForEquatableTestScenarios.ColumnsSpanned),
                            },
                            ObjectsThatAreNotEqualToReferenceObject = new ColumnSpanningSlottedCell[]
                            {
                                new ColumnSpanningSlottedCell(
                                    slotIdToCellMap,
                                    ReferenceObjectForEquatableTestScenarios.DefaultSlotName,
                                    ReferenceObjectForEquatableTestScenarios.ColumnsSpanned),
                                new ColumnSpanningSlottedCell(
                                    ReferenceObjectForEquatableTestScenarios.SlotIdToCellMap,
                                    ReferenceObjectForEquatableTestScenarios.SlotIdToCellMap.Keys.First(_ => _ != ReferenceObjectForEquatableTestScenarios.DefaultSlotName),
                                    ReferenceObjectForEquatableTestScenarios.ColumnsSpanned),
                                new ColumnSpanningSlottedCell(
                                    ReferenceObjectForEquatableTestScenarios.SlotIdToCellMap,
                                    ReferenceObjectForEquatableTestScenarios.DefaultSlotName,
                                    A.Dummy<ColumnSpanningSlottedCell>().Whose(_ => !_.ColumnsSpanned.IsEqualTo(ReferenceObjectForEquatableTestScenarios.ColumnsSpanned)).ColumnsSpanned),
                            },
                            ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new object[]
                            {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                                A.Dummy<MediaReferenceCell>(),
                                A.Dummy<ColumnSpanningMediaReferenceCell>(),
                                A.Dummy<HtmlCell>(),
                                A.Dummy<ColumnSpanningDecimalCell>(),
                                A.Dummy<ColumnSpanningNullCell>(),
                                A.Dummy<SlottedCell>(),
                                A.Dummy<ColumnSpanningHtmlCell>(),
                                A.Dummy<ColumnSpanningStringCell>(),
                                A.Dummy<NullCell>(),
                                A.Dummy<DecimalCell>(),
                                A.Dummy<StringCell>(),
                            },
                        };

                        return result;
                    });
        }
    }
}