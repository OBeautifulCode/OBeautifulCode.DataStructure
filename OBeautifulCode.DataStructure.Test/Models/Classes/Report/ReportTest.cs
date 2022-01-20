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
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Report>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'id' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Report>();

                            var result = new Report(
                                                 null,
                                                 referenceObject.Sections,
                                                 referenceObject.Title,
                                                 referenceObject.TimestampUtc,
                                                 referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "id", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Report>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'id' is white space scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Report>();

                            var result = new Report(
                                                 Invariant($"  {Environment.NewLine}  "),
                                                 referenceObject.Sections,
                                                 referenceObject.Title,
                                                 referenceObject.TimestampUtc,
                                                 referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "id", "white space", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Report>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'sections' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Report>();

                            var result = new Report(
                                                 referenceObject.Id,
                                                 null,
                                                 referenceObject.Title,
                                                 referenceObject.TimestampUtc,
                                                 referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "sections", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Report>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'sections' is an empty enumerable scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Report>();

                            var result = new Report(
                                                 referenceObject.Id,
                                                 new List<Section>(),
                                                 referenceObject.Title,
                                                 referenceObject.TimestampUtc,
                                                 referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "sections", "is an empty enumerable", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Report>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'sections' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Report>();

                            var result = new Report(
                                                 referenceObject.Id,
                                                 new Section[0].Concat(referenceObject.Sections).Concat(new Section[] { null }).Concat(referenceObject.Sections).ToList(),
                                                 referenceObject.Title,
                                                 referenceObject.TimestampUtc,
                                                 referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "sections", "contains at least one null element", },
                    })
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
                                referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "sections", "contains two or more elements with the same Id", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Report>
                    {
                        Name = "constructor should throw ArgumentException when the same cell object is used multiple times",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Report>();

                            var treeTable = A.Dummy<TreeTable>();

                            var result = new Report(
                                referenceObject.Id,
                                new[]
                                {
                                    new Section("id1", treeTable),
                                    new Section("id2", A.Dummy<TreeTable>()),
                                    new Section("id3", treeTable),
                                },
                                referenceObject.Title,
                                referenceObject.TimestampUtc,
                                referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "One or more ICell objects are used multiple times in the report", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Report>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'timestampUtc' is a UTC DateTime",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Report>();

                            var result = new Report(
                                referenceObject.Id,
                                referenceObject.Sections,
                                referenceObject.Title,
                                A.Dummy<DateTime>().Whose(_ => _.Kind != DateTimeKind.Utc),
                                referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "timestampUtc is not in UTC" },
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
                                    ReferenceObjectForEquatableTestScenarios.Format),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new Report[]
                        {
                            new Report(
                                    A.Dummy<Report>().Whose(_ => !_.Id.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Id)).Id,
                                    ReferenceObjectForEquatableTestScenarios.Sections,
                                    ReferenceObjectForEquatableTestScenarios.Title,
                                    ReferenceObjectForEquatableTestScenarios.TimestampUtc,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new Report(
                                    ReferenceObjectForEquatableTestScenarios.Id,
                                    A.Dummy<Report>().Whose(_ => !_.Sections.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Sections)).Sections,
                                    ReferenceObjectForEquatableTestScenarios.Title,
                                    ReferenceObjectForEquatableTestScenarios.TimestampUtc,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new Report(
                                    ReferenceObjectForEquatableTestScenarios.Id,
                                    ReferenceObjectForEquatableTestScenarios.Sections,
                                    A.Dummy<Report>().Whose(_ => !_.Title.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Title)).Title,
                                    ReferenceObjectForEquatableTestScenarios.TimestampUtc,
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