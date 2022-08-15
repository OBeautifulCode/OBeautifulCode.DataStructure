// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PercentCellValueFormatTest.cs" company="OBeautifulCode">
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
    public static partial class PercentCellValueFormatTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static PercentCellValueFormatTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PercentCellValueFormat>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'numberOfDecimalPlaces' is negative",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PercentCellValueFormat>();

                            var result = new PercentCellValueFormat(
                                A.Dummy<NumberFormatPercentDisplayKind>(),
                                A.Dummy<NegativeInteger>(),
                                referenceObject.MidpointRounding,
                                referenceObject.DecimalSeparator,
                                referenceObject.DigitGroupKind,
                                referenceObject.DigitGroupSeparator,
                                referenceObject.NegativeNumberDisplayKind,
                                referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "numberOfDecimalPlaces", "is negative" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PercentCellValueFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'midpointRounding' is not null, but 'numberOfDecimalPlaces' is null.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PercentCellValueFormat>();

                            var result = new PercentCellValueFormat(
                                A.Dummy<NumberFormatPercentDisplayKind>(),
                                null,
                                A.Dummy<MidpointRounding>(),
                                referenceObject.DecimalSeparator,
                                referenceObject.DigitGroupKind,
                                referenceObject.DigitGroupSeparator,
                                referenceObject.NegativeNumberDisplayKind,
                                referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "midpointRounding is not null, but numberOfDecimalPlaces is null." },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<PercentCellValueFormat>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'percentDisplayKind' is NumberFormatPercentDisplayKind.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<PercentCellValueFormat>();

                            var result = new PercentCellValueFormat(
                                NumberFormatPercentDisplayKind.Unknown,
                                referenceObject.NumberOfDecimalPlaces,
                                referenceObject.MidpointRounding,
                                referenceObject.DecimalSeparator,
                                referenceObject.DigitGroupKind,
                                referenceObject.DigitGroupSeparator,
                                referenceObject.NegativeNumberDisplayKind,
                                referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "percentDisplayKind", "Unknown" },
                    });
        }
    }
}