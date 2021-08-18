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
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Section>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'id' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Section>();

                            var result = new Section(
                                                 null,
                                                 referenceObject.TreeTable,
                                                 referenceObject.Title,
                                                 referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "id", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Section>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'id' is white space scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Section>();

                            var result = new Section(
                                                 Invariant($"  {Environment.NewLine}  "),
                                                 referenceObject.TreeTable,
                                                 referenceObject.Title,
                                                 referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "id", "white space", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Section>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'treeTable' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Section>();

                            var result = new Section(
                                                 referenceObject.Id,
                                                 null,
                                                 referenceObject.Title,
                                                 referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "treeTable", },
                    });

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
                                    ReferenceObjectForEquatableTestScenarios.Title,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new Section[]
                        {
                            new Section(
                                    A.Dummy<Section>().Whose(_ => !_.Id.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Id)).Id,
                                    ReferenceObjectForEquatableTestScenarios.TreeTable,
                                    ReferenceObjectForEquatableTestScenarios.Title,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new Section(
                                    ReferenceObjectForEquatableTestScenarios.Id,
                                    A.Dummy<Section>().Whose(_ => !_.TreeTable.IsEqualTo(ReferenceObjectForEquatableTestScenarios.TreeTable)).TreeTable,
                                    ReferenceObjectForEquatableTestScenarios.Title,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new Section(
                                    ReferenceObjectForEquatableTestScenarios.Id,
                                    ReferenceObjectForEquatableTestScenarios.TreeTable,
                                    A.Dummy<Section>().Whose(_ => !_.Title.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Title)).Title,
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