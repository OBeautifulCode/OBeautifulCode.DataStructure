// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullNumberCellFormat{TValue}Test.cs" company="OBeautifulCode">
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
    public static partial class NullNumberCellFormatTValueTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static NullNumberCellFormatTValueTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<NullNumberCellFormat<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'numberOfDecimalPlaces' is negative",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<NullNumberCellFormat<Version>>();

                            var result = new NullNumberCellFormat<Version>(
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
                    new ConstructorArgumentValidationTestScenario<NullNumberCellFormat<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'midpointRounding' is not null, but 'numberOfDecimalPlaces' is null.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<NullNumberCellFormat<Version>>();

                            var result = new NullNumberCellFormat<Version>(
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
                    new ConstructorArgumentValidationTestScenario<NullNumberCellFormat<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'digitGroupKind' is NumberFormatDigitGroupKind.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<NullNumberCellFormat<Version>>();

                            var result = new NullNumberCellFormat<Version>(
                                                 referenceObject.NumberOfDecimalPlaces,
                                                 referenceObject.MidpointRounding,
                                                 referenceObject.DecimalSeparator,
                                                 NumberFormatDigitGroupKind.Unknown,
                                                 referenceObject.DigitGroupSeparator,
                                                 referenceObject.NegativeNumberDisplayKind,
                                                 referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "digitGroupKind", "Unknown" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<NullNumberCellFormat<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'negativeNumberDisplayKind' is NumberFormatNegativeDisplayKind.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<NullNumberCellFormat<Version>>();

                            var result = new NullNumberCellFormat<Version>(
                                referenceObject.NumberOfDecimalPlaces,
                                referenceObject.MidpointRounding,
                                referenceObject.DecimalSeparator,
                                referenceObject.DigitGroupKind,
                                referenceObject.DigitGroupSeparator,
                                NumberFormatNegativeDisplayKind.Unknown,
                                referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "negativeNumberDisplayKind", "Unknown" },
                    });
        }
    }
}