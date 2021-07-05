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
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'slotIdToCellMap' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                                 null,
                                                 referenceObject.DefaultSlotId,
                                                 referenceObject.Id);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'slotIdToCellMap' is an empty dictionary scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                                 new Dictionary<string, IHaveValueCell>(),
                                                 referenceObject.DefaultSlotId,
                                                 referenceObject.Id);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap", "is an empty dictionary", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'slotIdToCellMap' contains a key-value pair with a null value scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var dictionaryWithNullValue = referenceObject.SlotIdToCellMap.ToDictionary(_ => _.Key, _ => _.Value);

                            var randomKey = dictionaryWithNullValue.Keys.ElementAt(ThreadSafeRandom.Next(0, dictionaryWithNullValue.Count));

                            dictionaryWithNullValue[randomKey] = null;

                            var result = new SlottedCell(
                                                 dictionaryWithNullValue,
                                                 referenceObject.DefaultSlotId,
                                                 referenceObject.Id);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap", "contains at least one key-value pair with a null value", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'defaultSlotId' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                                 referenceObject.SlotIdToCellMap,
                                                 null,
                                                 referenceObject.Id);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "defaultSlotId", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'defaultSlotId' is white space scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                                 referenceObject.SlotIdToCellMap,
                                                 Invariant($"  {Environment.NewLine}  "),
                                                 referenceObject.Id);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "defaultSlotId", "white space", },
                    })
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
                                referenceObject.DefaultSlotId);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap", "contains at least one key-value pair with a white space key" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'slotIdToCellMap' does not contain the specified defaultSlotId",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                referenceObject.SlotIdToCellMap,
                                A.Dummy<string>());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap does not contain the specified defaultSlotId" },
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

                            slotIdToCellMap.Add(systemUnderTest.DefaultSlotId, A.Dummy<IHaveValueCell>());

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
                        Name = "DeepCloneWithDefaultSlotId should deep clone object and replace DefaultSlotId with the provided defaultSlotId",
                        WithPropertyName = "DefaultSlotId",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<SlottedCell>();

                            var defaultSlotId = systemUnderTest.SlotIdToCellMap.Keys.First(_ => _ != systemUnderTest.DefaultSlotId);

                            var result = new SystemUnderTestDeepCloneWithValue<SlottedCell>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = defaultSlotId,
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

                    slotIdToCellMap.Add(ReferenceObjectForEquatableTestScenarios.DefaultSlotId, A.Dummy<IHaveValueCell>());

                    var result = new EquatableTestScenario<SlottedCell>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = ReferenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new SlottedCell[]
                        {
                                new SlottedCell(
                                    ReferenceObjectForEquatableTestScenarios.SlotIdToCellMap,
                                    ReferenceObjectForEquatableTestScenarios.DefaultSlotId),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new SlottedCell[]
                        {
                                new SlottedCell(
                                    slotIdToCellMap,
                                    ReferenceObjectForEquatableTestScenarios.DefaultSlotId),
                                new SlottedCell(
                                    ReferenceObjectForEquatableTestScenarios.SlotIdToCellMap,
                                    ReferenceObjectForEquatableTestScenarios.SlotIdToCellMap.Keys.First(_ => _ != ReferenceObjectForEquatableTestScenarios.DefaultSlotId)),
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