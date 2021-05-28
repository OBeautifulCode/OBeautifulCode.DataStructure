// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SlottedCellTest.cs" company="OBeautifulCode">
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
    public static partial class SlottedCellTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static SlottedCellTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'slotIdToCellMap' contains a white space key",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var slotIdToCellMap = referenceObject.SlotIdToCellMap.ToDictionary(_ => _.Key, _ => _.Value);

                            slotIdToCellMap.Add(" \r\n ", A.Dummy<IHaveValueCell>());

                            var result = new SlottedCell(
                                slotIdToCellMap,
                                referenceObject.DefaultSlotName);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap", "contains at least one key-value pair with a white space key" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'slotIdToCellMap' does not contain the specified defaultSlotName",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                referenceObject.SlotIdToCellMap,
                                A.Dummy<string>());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap does not contain the specified defaultSlotName" },
                    });

            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<SlottedCell>
                    {
                        Name = "DeepCloneWithSlotIdToCellMap should deep clone object and replace SlotIdToCellMap with the provided slotIdToCellMap",
                        WithPropertyName = "SlotIdToCellMap",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<SlottedCell>();

                            var referenceObject = A.Dummy<SlottedCell>().ThatIs(_ => !systemUnderTest.SlotIdToCellMap.IsEqualTo(_.SlotIdToCellMap));

                            var slotIdToCellMap = referenceObject.SlotIdToCellMap.ToDictionary(_ => _.Key, _ => _.Value);

                            slotIdToCellMap.Add(systemUnderTest.DefaultSlotName, A.Dummy<IHaveValueCell>());

                            var result = new SystemUnderTestDeepCloneWithValue<SlottedCell>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = slotIdToCellMap,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<SlottedCell>
                    {
                        Name = "DeepCloneWithDefaultSlotName should deep clone object and replace DefaultSlotName with the provided defaultSlotName",
                        WithPropertyName = "DefaultSlotName",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<SlottedCell>();

                            var defaultSlotName = systemUnderTest.SlotIdToCellMap.Keys.First(_ => _ != systemUnderTest.DefaultSlotName);

                            var result = new SystemUnderTestDeepCloneWithValue<SlottedCell>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = defaultSlotName,
                            };

                            return result;
                        },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                {
                    var slotIdToCellMap = A.Dummy<SlottedCell>()
                        .Whose(_ => !_.SlotIdToCellMap.IsEqualTo(ReferenceObjectForEquatableTestScenarios.SlotIdToCellMap))
                        .SlotIdToCellMap
                        .ToDictionary(_ => _.Key, _ => _.Value);

                    slotIdToCellMap.Add(ReferenceObjectForEquatableTestScenarios.DefaultSlotName, A.Dummy<IHaveValueCell>());

                    var result = new EquatableTestScenario<SlottedCell>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = ReferenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new SlottedCell[]
                        {
                                new SlottedCell(
                                    ReferenceObjectForEquatableTestScenarios.SlotIdToCellMap,
                                    ReferenceObjectForEquatableTestScenarios.DefaultSlotName),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new SlottedCell[]
                        {
                                new SlottedCell(
                                    slotIdToCellMap,
                                    ReferenceObjectForEquatableTestScenarios.DefaultSlotName),
                                new SlottedCell(
                                    ReferenceObjectForEquatableTestScenarios.SlotIdToCellMap,
                                    ReferenceObjectForEquatableTestScenarios.SlotIdToCellMap.Keys.First(_ => _ != ReferenceObjectForEquatableTestScenarios.DefaultSlotName)),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new object[]
                        {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                                A.Dummy<MediaReferenceCell>(),
                                A.Dummy<MediaReferenceCell>(),
                                A.Dummy<HtmlCell>(),
                                A.Dummy<DecimalCell>(),
                                A.Dummy<NullCell>(),
                                A.Dummy<ColumnSpanningSlottedCell>(),
                                A.Dummy<HtmlCell>(),
                                A.Dummy<StringCell>(),
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