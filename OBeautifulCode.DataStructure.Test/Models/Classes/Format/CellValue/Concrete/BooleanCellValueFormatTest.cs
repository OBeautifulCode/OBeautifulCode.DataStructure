// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanCellValueFormatTest.cs" company="OBeautifulCode">
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
    public static partial class BooleanCellValueFormatTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static BooleanCellValueFormatTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<BooleanCellValueFormat>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'trueText' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<BooleanCellValueFormat>();

                            var result = new BooleanCellValueFormat(
                                                 null,
                                                 referenceObject.FalseText,
                                                 referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "trueText", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<BooleanCellValueFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'trueText' is white space scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<BooleanCellValueFormat>();

                            var result = new BooleanCellValueFormat(
                                                 Invariant($"  {Environment.NewLine}  "),
                                                 referenceObject.FalseText,
                                                 referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "trueText", "white space", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<BooleanCellValueFormat>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'falseText' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<BooleanCellValueFormat>();

                            var result = new BooleanCellValueFormat(
                                                 referenceObject.TrueText,
                                                 null,
                                                 referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "falseText", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<BooleanCellValueFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'falseText' is white space scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<BooleanCellValueFormat>();

                            var result = new BooleanCellValueFormat(
                                                 referenceObject.TrueText,
                                                 Invariant($"  {Environment.NewLine}  "),
                                                 referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "falseText", "white space", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<BooleanCellValueFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameters 'trueText', 'falseText', and 'missingValueText' are not unique scenario 1",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<BooleanCellValueFormat>();

                            var result = new BooleanCellValueFormat(
                                                 referenceObject.TrueText,
                                                 referenceObject.FalseText,
                                                 referenceObject.TrueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "trueText, falseText, and missingValueText are not distinct.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<BooleanCellValueFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameters 'trueText', 'falseText', and 'missingValueText' are not unique scenario 2",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<BooleanCellValueFormat>();

                            var result = new BooleanCellValueFormat(
                                referenceObject.FalseText,
                                referenceObject.FalseText,
                                null);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "trueText, falseText, and missingValueText are not distinct.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<BooleanCellValueFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameters 'trueText', 'falseText', and 'missingValueText' are not unique scenario 3",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<BooleanCellValueFormat>();

                            var result = new BooleanCellValueFormat(
                                referenceObject.TrueText,
                                referenceObject.FalseText,
                                referenceObject.FalseText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "trueText, falseText, and missingValueText are not distinct.", },
                    });
        }
    }
}