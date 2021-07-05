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

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
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
                                referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "sections", "contains two or more elements with the same Id", },
                    });
        }
    }
}