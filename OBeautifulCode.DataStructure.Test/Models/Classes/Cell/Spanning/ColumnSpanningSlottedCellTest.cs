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