// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DropdownSelectorTest.cs" company="OBeautifulCode">
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
    using OBeautifulCode.Math.Recipes;
    using OBeautifulCode.Type;
    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class DropdownSelectorTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static DropdownSelectorTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<DropdownSelector>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'selectedItem' is not an item in 'items'",
                        ConstructionFunc = () =>
                        {
                            var referenceObject1 = A.Dummy<DropdownSelector>();
                            var referenceObject2 = A.Dummy<DropdownSelector>();

                            var result = new DropdownSelector(
                                referenceObject1.Items,
                                referenceObject2.SelectedItemName);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "selectedItemName", "does not exist in items", },
                    });

            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<DropdownSelector>
                    {
                        Name = "DeepCloneWithItems should deep clone object and replace Items with the provided items",
                        WithPropertyName = "Items",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<DropdownSelector>();

                            var items = systemUnderTest.Items.ToList();

                            items.Add(A.Dummy<NamedValue<ILink>>());

                            var result = new SystemUnderTestDeepCloneWithValue<DropdownSelector>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = items,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<DropdownSelector>
                    {
                        Name = "DeepCloneWithSelectedItemName should deep clone object and replace SelectedItemName with the provided selectedItemName",
                        WithPropertyName = "SelectedItemName",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<DropdownSelector>().Whose(_ => _.Items.Count > 1);

                            var selectedItemNameCandidates = systemUnderTest.Items.Select(_ => _.Name).Where(_ => _ != systemUnderTest.SelectedItemName).ToList();

                            var selectedItemName = selectedItemNameCandidates[ThreadSafeRandom.Next(0, selectedItemNameCandidates.Count)];

                            var result = new SystemUnderTestDeepCloneWithValue<DropdownSelector>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = selectedItemName,
                            };

                            return result;
                        },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                {
                    var referenceObject = A.Dummy<DropdownSelector>().Whose(_ => _.Items.Count > 1);

                    var referenceObjectItemsPlusOne = referenceObject.Items.ToList();
                    referenceObjectItemsPlusOne.Add(A.Dummy<NamedValue<ILink>>());

                    var differentSelectedItemCandidates = referenceObject.Items.Where(_ => _.Name != referenceObject.SelectedItemName).Select(_ => _.Name).ToList();
                    var differentSelectedItem = differentSelectedItemCandidates[ThreadSafeRandom.Next(0, differentSelectedItemCandidates.Count)];

                    var result = new EquatableTestScenario<DropdownSelector>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = referenceObject,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new DropdownSelector[]
                        {
                            new DropdownSelector(
                                referenceObject.Items,
                                referenceObject.SelectedItemName),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new DropdownSelector[]
                        {
                            new DropdownSelector(
                                referenceObjectItemsPlusOne,
                                referenceObject.SelectedItemName),
                            new DropdownSelector(
                                referenceObject.Items,
                                differentSelectedItem),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new object[]
                        {
                            A.Dummy<object>(),
                            A.Dummy<string>(),
                            A.Dummy<int>(),
                            A.Dummy<int?>(),
                            A.Dummy<Guid>(),
                        },
                    };

                    return result;
                });
        }
    }
}