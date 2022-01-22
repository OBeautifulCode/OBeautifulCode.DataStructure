// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeFormatTest.cs" company="OBeautifulCode">
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
    using OBeautifulCode.Type;
    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class DateTimeFormatTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static DateTimeFormatTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<DateTimeFormat>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'formatKind' is DateTimeFormatKind.Unknown scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<DateTimeFormat>();

                            var result = new DateTimeFormat(
                                DateTimeFormatKind.Unknown,
                                referenceObject.CultureKind,
                                referenceObject.Localize,
                                referenceObject.LocalTimeZone);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "formatKind is Unknown", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<DateTimeFormat>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'cultureKind' is CultureKind.Unknown scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<DateTimeFormat>();

                            var result = new DateTimeFormat(
                                referenceObject.FormatKind,
                                CultureKind.Unknown,
                                referenceObject.Localize,
                                referenceObject.LocalTimeZone);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "cultureKind is Unknown", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<DateTimeFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'localize' is false and parameter 'localTimeZone' is not null",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<DateTimeFormat>();

                            var result = new DateTimeFormat(
                                referenceObject.FormatKind,
                                referenceObject.CultureKind,
                                false,
                                A.Dummy<StandardTimeZone>());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "localize is false, but localTimeZone is not null", },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                {
                    var referenceObjectForEquatableTestScenarios = A.Dummy<DateTimeFormat>().Whose(_ => _.Localize == true);

                    var result = new EquatableTestScenario<DateTimeFormat>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = referenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new DateTimeFormat[]
                        {
                            new DateTimeFormat(
                                    referenceObjectForEquatableTestScenarios.FormatKind,
                                    referenceObjectForEquatableTestScenarios.CultureKind,
                                    referenceObjectForEquatableTestScenarios.Localize,
                                    referenceObjectForEquatableTestScenarios.LocalTimeZone),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new DateTimeFormat[]
                        {
                            new DateTimeFormat(
                                    A.Dummy<DateTimeFormat>().Whose(_ => !_.FormatKind.IsEqualTo(referenceObjectForEquatableTestScenarios.FormatKind)).FormatKind,
                                    referenceObjectForEquatableTestScenarios.CultureKind,
                                    referenceObjectForEquatableTestScenarios.Localize,
                                    referenceObjectForEquatableTestScenarios.LocalTimeZone),
                            new DateTimeFormat(
                                    referenceObjectForEquatableTestScenarios.FormatKind,
                                    A.Dummy<DateTimeFormat>().Whose(_ => !_.CultureKind.IsEqualTo(referenceObjectForEquatableTestScenarios.CultureKind)).CultureKind,
                                    referenceObjectForEquatableTestScenarios.Localize,
                                    referenceObjectForEquatableTestScenarios.LocalTimeZone),
                            new DateTimeFormat(
                                    referenceObjectForEquatableTestScenarios.FormatKind,
                                    referenceObjectForEquatableTestScenarios.CultureKind,
                                    A.Dummy<DateTimeFormat>().Whose(_ => !_.Localize.IsEqualTo(referenceObjectForEquatableTestScenarios.Localize)).Localize,
                                    null),
                            new DateTimeFormat(
                                    referenceObjectForEquatableTestScenarios.FormatKind,
                                    referenceObjectForEquatableTestScenarios.CultureKind,
                                    referenceObjectForEquatableTestScenarios.Localize,
                                    A.Dummy<DateTimeFormat>().Whose(_ => !_.LocalTimeZone.IsEqualTo(referenceObjectForEquatableTestScenarios.LocalTimeZone)).LocalTimeZone),
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
                new DeepCloneWithTestScenario<DateTimeFormat>
                {
                    Name = "DeepCloneWithFormatKind should deep clone object and replace FormatKind with the provided formatKind",
                    WithPropertyName = "FormatKind",
                    SystemUnderTestDeepCloneWithValueFunc = () =>
                    {
                        var systemUnderTest = A.Dummy<DateTimeFormat>();

                        var referenceObject = A.Dummy<DateTimeFormat>().ThatIs(_ => !systemUnderTest.FormatKind.IsEqualTo(_.FormatKind));

                        var result = new SystemUnderTestDeepCloneWithValue<DateTimeFormat>
                        {
                            SystemUnderTest = systemUnderTest,
                            DeepCloneWithValue = referenceObject.FormatKind,
                        };

                        return result;
                    },
                })
            .AddScenario(() =>
                new DeepCloneWithTestScenario<DateTimeFormat>
                {
                    Name = "DeepCloneWithCultureKind should deep clone object and replace CultureKind with the provided cultureKind",
                    WithPropertyName = "CultureKind",
                    SystemUnderTestDeepCloneWithValueFunc = () =>
                    {
                        var systemUnderTest = A.Dummy<DateTimeFormat>();

                        var referenceObject = A.Dummy<DateTimeFormat>().ThatIs(_ => !systemUnderTest.CultureKind.IsEqualTo(_.CultureKind));

                        var result = new SystemUnderTestDeepCloneWithValue<DateTimeFormat>
                        {
                            SystemUnderTest = systemUnderTest,
                            DeepCloneWithValue = referenceObject.CultureKind,
                        };

                        return result;
                    },
                });

        }
    }
}