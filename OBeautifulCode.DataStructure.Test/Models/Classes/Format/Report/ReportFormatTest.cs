// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportFormatTest.cs" company="OBeautifulCode">
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
    public static partial class ReportFormatTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static ReportFormatTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ReportFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'displayTimestamp' is false and parameter 'timestampFormat' is not null",
                        ConstructionFunc = () =>
                        {
                            var result = new ReportFormat(
                                false,
                                A.Dummy<DateTimeFormat>());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "displayTimestamp is false, but timestampFormat is not null", },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                {
                    var referenceObjectForEquatableTestScenarios = A.Dummy<ReportFormat>().Whose(_ => _.DisplayTimestamp == true);

                    var result = new EquatableTestScenario<ReportFormat>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = referenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new ReportFormat[]
                        {
                            new ReportFormat(
                                referenceObjectForEquatableTestScenarios.DisplayTimestamp,
                                referenceObjectForEquatableTestScenarios.TimestampFormat,
                                referenceObjectForEquatableTestScenarios.Options),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new ReportFormat[]
                        {
                            new ReportFormat(
                                A.Dummy<ReportFormat>().Whose(_ => !_.DisplayTimestamp.IsEqualTo(referenceObjectForEquatableTestScenarios.DisplayTimestamp)).DisplayTimestamp,
                                null),
                            new ReportFormat(
                                referenceObjectForEquatableTestScenarios.DisplayTimestamp,
                                A.Dummy<ReportFormat>().Whose(_ => !_.TimestampFormat.IsEqualTo(referenceObjectForEquatableTestScenarios.TimestampFormat)).TimestampFormat),
                            new ReportFormat(
                                referenceObjectForEquatableTestScenarios.DisplayTimestamp,
                                referenceObjectForEquatableTestScenarios.TimestampFormat,
                                A.Dummy<ReportFormat>().Whose(_ => !_.Options.IsEqualTo(referenceObjectForEquatableTestScenarios.Options)).Options),
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

            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<ReportFormat>
                    {
                        Name = "DeepCloneWithTimestampFormat should deep clone object and replace TimestampFormat with the provided timestampFormat",
                        WithPropertyName = "TimestampFormat",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<ReportFormat>().Whose(_ => _.DisplayTimestamp == true);

                            var referenceObject = A.Dummy<ReportFormat>().ThatIs(_ => !systemUnderTest.TimestampFormat.IsEqualTo(_.TimestampFormat));

                            var result = new SystemUnderTestDeepCloneWithValue<ReportFormat>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.TimestampFormat,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<ReportFormat>
                    {
                        Name = "DeepCloneWithOptions should deep clone object and replace Options with the provided options",
                        WithPropertyName = "Options",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<ReportFormat>();

                            var referenceObject = A.Dummy<ReportFormat>().ThatIs(_ => !systemUnderTest.Options.IsEqualTo(_.Options));

                            var result = new SystemUnderTestDeepCloneWithValue<ReportFormat>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Options,
                            };

                            return result;
                        },
                    });
        }
    }
}