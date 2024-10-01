// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SectionTest.cs" company="OBeautifulCode">
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
    public static partial class SectionTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static SectionTest()
        {
            // Need to do this because SectionFormat is currently empty and so there isn't a way to create
            // two sections having all the same properties except different formats.
            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Section>
                    {
                        Name = "DeepCloneWithId should deep clone object and replace Id with the provided id",
                        WithPropertyName = "Id",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Section>();

                            var referenceObject = A.Dummy<Section>().ThatIs(_ => !systemUnderTest.Id.IsEqualTo(_.Id));

                            var result = new SystemUnderTestDeepCloneWithValue<Section>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Id,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Section>
                    {
                        Name = "DeepCloneWithTreeTable should deep clone object and replace TreeTable with the provided treeTable",
                        WithPropertyName = "TreeTable",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Section>();

                            var referenceObject = A.Dummy<Section>().ThatIs(_ => !systemUnderTest.TreeTable.IsEqualTo(_.TreeTable));

                            var result = new SystemUnderTestDeepCloneWithValue<Section>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.TreeTable,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                new DeepCloneWithTestScenario<Section>
                {
                    Name = "DeepCloneWithName should deep clone object and replace Name with the provided name",
                    WithPropertyName = "Name",
                    SystemUnderTestDeepCloneWithValueFunc = () =>
                    {
                        var systemUnderTest = A.Dummy<Section>();

                        var referenceObject = A.Dummy<Section>().ThatIs(_ => !systemUnderTest.Name.IsEqualTo(_.Name));

                        var result = new SystemUnderTestDeepCloneWithValue<Section>
                        {
                            SystemUnderTest = systemUnderTest,
                            DeepCloneWithValue = referenceObject.Name,
                        };

                        return result;
                    },
                })
            .AddScenario(() =>
                new DeepCloneWithTestScenario<Section>
                {
                    Name = "DeepCloneWithTitle should deep clone object and replace Title with the provided title",
                    WithPropertyName = "Title",
                    SystemUnderTestDeepCloneWithValueFunc = () =>
                    {
                        var systemUnderTest = A.Dummy<Section>();

                        var referenceObject = A.Dummy<Section>().ThatIs(_ => !systemUnderTest.Title.IsEqualTo(_.Title));

                        var result = new SystemUnderTestDeepCloneWithValue<Section>
                        {
                            SystemUnderTest = systemUnderTest,
                            DeepCloneWithValue = referenceObject.Title,
                        };

                        return result;
                    },
                })
            .AddScenario(() =>
                new DeepCloneWithTestScenario<Section>
                {
                    Name = "DeepCloneWithAdditionalInfo should deep clone object and replace AdditionalInfo with the provided additionalInfo",
                    WithPropertyName = "AdditionalInfo",
                    SystemUnderTestDeepCloneWithValueFunc = () =>
                    {
                        var systemUnderTest = A.Dummy<Section>();

                        var referenceObject = A.Dummy<Section>().ThatIs(_ => !systemUnderTest.AdditionalInfo.IsEqualTo(_.AdditionalInfo));

                        var result = new SystemUnderTestDeepCloneWithValue<Section>
                        {
                            SystemUnderTest = systemUnderTest,
                            DeepCloneWithValue = referenceObject.AdditionalInfo,
                        };

                        return result;
                    },
                });

            // Need to do this because SectionFormat is currently empty and so there isn't a way to create
            // two sections having all the same properties except different formats.
            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new EquatableTestScenario<Section>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = ReferenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new Section[]
                        {
                            new Section(
                                    ReferenceObjectForEquatableTestScenarios.Id,
                                    ReferenceObjectForEquatableTestScenarios.TreeTable,
                                    ReferenceObjectForEquatableTestScenarios.Name,
                                    ReferenceObjectForEquatableTestScenarios.Title,
                                    ReferenceObjectForEquatableTestScenarios.AdditionalInfo,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new Section[]
                        {
                            new Section(
                                    A.Dummy<Section>().Whose(_ => !_.Id.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Id)).Id,
                                    ReferenceObjectForEquatableTestScenarios.TreeTable,
                                    ReferenceObjectForEquatableTestScenarios.Name,
                                    ReferenceObjectForEquatableTestScenarios.Title,
                                    ReferenceObjectForEquatableTestScenarios.AdditionalInfo,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new Section(
                                    ReferenceObjectForEquatableTestScenarios.Id,
                                    A.Dummy<Section>().Whose(_ => !_.TreeTable.IsEqualTo(ReferenceObjectForEquatableTestScenarios.TreeTable)).TreeTable,
                                    ReferenceObjectForEquatableTestScenarios.Name,
                                    ReferenceObjectForEquatableTestScenarios.Title,
                                    ReferenceObjectForEquatableTestScenarios.AdditionalInfo,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new Section(
                                    ReferenceObjectForEquatableTestScenarios.Id,
                                    ReferenceObjectForEquatableTestScenarios.TreeTable,
                                    ReferenceObjectForEquatableTestScenarios.Name,
                                    A.Dummy<Section>().Whose(_ => !_.Title.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Title)).Title,
                                    ReferenceObjectForEquatableTestScenarios.AdditionalInfo,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new Section(
                                    ReferenceObjectForEquatableTestScenarios.Id,
                                    ReferenceObjectForEquatableTestScenarios.TreeTable,
                                    A.Dummy<Section>().Whose(_ => !_.Name.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Name)).Name,
                                    ReferenceObjectForEquatableTestScenarios.Title,
                                    ReferenceObjectForEquatableTestScenarios.AdditionalInfo,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new Section(
                                ReferenceObjectForEquatableTestScenarios.Id,
                                ReferenceObjectForEquatableTestScenarios.TreeTable,
                                ReferenceObjectForEquatableTestScenarios.Name,
                                ReferenceObjectForEquatableTestScenarios.Title,
                                A.Dummy<Section>().Whose(_ => !_.AdditionalInfo.IsEqualTo(ReferenceObjectForEquatableTestScenarios.AdditionalInfo)).AdditionalInfo,
                                ReferenceObjectForEquatableTestScenarios.Format),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new object[]
                        {
                            A.Dummy<object>(),
                            A.Dummy<string>(),
                            A.Dummy<int>(),
                            A.Dummy<int?>(),
                            A.Dummy<Guid>(),
                        },
                    });
        }
    }
}