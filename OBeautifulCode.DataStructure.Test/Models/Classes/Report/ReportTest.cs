// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportTest.cs" company="OBeautifulCode">
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

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class ReportTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static ReportTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Report>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'sections' contains two or more elements with the same Id",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Report>();

                            var result = new Report(
                                referenceObject.Id,
                                new[]
                                {
                                    new Section("duplicate", A.Dummy<TreeTable>()),
                                    new Section("unique-1", A.Dummy<TreeTable>()),
                                    new Section("unique-2", A.Dummy<TreeTable>()),
                                    new Section("duplicate", A.Dummy<TreeTable>()),
                                },
                                referenceObject.Title,
                                referenceObject.TimestampUtc,
                                referenceObject.DownloadLinks,
                                referenceObject.AdditionalInfo,
                                referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "sections", "contains two or more elements with the same Id", },
                    });

            // Need to do this because ReportFormat is currently empty and so there isn't a way to create
            // two reports having all the same properties except different report formats.
            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new EquatableTestScenario<Report>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = ReferenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new Report[]
                        {
                            new Report(
                                    ReferenceObjectForEquatableTestScenarios.Id,
                                    ReferenceObjectForEquatableTestScenarios.Sections,
                                    ReferenceObjectForEquatableTestScenarios.Title,
                                    ReferenceObjectForEquatableTestScenarios.TimestampUtc,
                                    ReferenceObjectForEquatableTestScenarios.DownloadLinks,
                                    ReferenceObjectForEquatableTestScenarios.AdditionalInfo,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new Report[]
                        {
                            new Report(
                                    A.Dummy<Report>().Whose(_ => !_.Id.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Id)).Id,
                                    ReferenceObjectForEquatableTestScenarios.Sections,
                                    ReferenceObjectForEquatableTestScenarios.Title,
                                    ReferenceObjectForEquatableTestScenarios.TimestampUtc,
                                    ReferenceObjectForEquatableTestScenarios.DownloadLinks,
                                    ReferenceObjectForEquatableTestScenarios.AdditionalInfo,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new Report(
                                    ReferenceObjectForEquatableTestScenarios.Id,
                                    A.Dummy<Report>().Whose(_ => !_.Sections.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Sections)).Sections,
                                    ReferenceObjectForEquatableTestScenarios.Title,
                                    ReferenceObjectForEquatableTestScenarios.TimestampUtc,
                                    ReferenceObjectForEquatableTestScenarios.DownloadLinks,
                                    ReferenceObjectForEquatableTestScenarios.AdditionalInfo,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new Report(
                                    ReferenceObjectForEquatableTestScenarios.Id,
                                    ReferenceObjectForEquatableTestScenarios.Sections,
                                    A.Dummy<Report>().Whose(_ => !_.Title.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Title)).Title,
                                    ReferenceObjectForEquatableTestScenarios.TimestampUtc,
                                    ReferenceObjectForEquatableTestScenarios.DownloadLinks,
                                    ReferenceObjectForEquatableTestScenarios.AdditionalInfo,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new Report(
                                ReferenceObjectForEquatableTestScenarios.Id,
                                ReferenceObjectForEquatableTestScenarios.Sections,
                                ReferenceObjectForEquatableTestScenarios.Title,
                                ReferenceObjectForEquatableTestScenarios.TimestampUtc,
                                A.Dummy<Report>().Whose(_ => !_.DownloadLinks.IsEqualTo(ReferenceObjectForEquatableTestScenarios.DownloadLinks)).DownloadLinks,
                                ReferenceObjectForEquatableTestScenarios.AdditionalInfo,
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

            // Need to do this because ReportFormat is currently empty and so there isn't a way to create
            // two reports having all the same properties except different report formats.
            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Report>
                    {
                        Name = "DeepCloneWithId should deep clone object and replace Id with the provided id",
                        WithPropertyName = "Id",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Report>();

                            var referenceObject = A.Dummy<Report>().ThatIs(_ => !systemUnderTest.Id.IsEqualTo(_.Id));

                            var result = new SystemUnderTestDeepCloneWithValue<Report>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Id,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Report>
                    {
                        Name = "DeepCloneWithSections should deep clone object and replace Sections with the provided sections",
                        WithPropertyName = "Sections",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Report>();

                            var referenceObject = A.Dummy<Report>().ThatIs(_ => !systemUnderTest.Sections.IsEqualTo(_.Sections));

                            var result = new SystemUnderTestDeepCloneWithValue<Report>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Sections,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Report>
                    {
                        Name = "DeepCloneWithTitle should deep clone object and replace Title with the provided title",
                        WithPropertyName = "Title",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Report>();

                            var referenceObject = A.Dummy<Report>().ThatIs(_ => !systemUnderTest.Title.IsEqualTo(_.Title));

                            var result = new SystemUnderTestDeepCloneWithValue<Report>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Title,
                            };

                            return result;
                        },
                    });
        }
    }
}