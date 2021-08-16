// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoricalCellValueFormat{TValue}Test.cs" company="OBeautifulCode">
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
    public static partial class CategoricalCellValueFormatTValueTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CategoricalCellValueFormatTValueTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CategoricalCellValueFormat<Version>>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'valueToTextMap' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CategoricalCellValueFormat<Version>>();

                            var result = new CategoricalCellValueFormat<Version>(
                                                 null,
                                                 referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "valueToTextMap", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CategoricalCellValueFormat<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'valueToTextMap' is an empty dictionary scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CategoricalCellValueFormat<Version>>();

                            var result = new CategoricalCellValueFormat<Version>(
                                                 new Dictionary<Version, string>(),
                                                 referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "valueToTextMap", "is an empty dictionary", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CategoricalCellValueFormat<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'valueToTextMap' contains a key-value pair with a null value scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CategoricalCellValueFormat<Version>>();

                            var dictionaryWithNullValue = referenceObject.ValueToTextMap.ToDictionary(_ => _.Key, _ => _.Value);

                            var randomKey = dictionaryWithNullValue.Keys.ElementAt(ThreadSafeRandom.Next(0, dictionaryWithNullValue.Count));

                            dictionaryWithNullValue[randomKey] = null;

                            var result = new CategoricalCellValueFormat<Version>(
                                                 dictionaryWithNullValue,
                                                 referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "valueToTextMap", "contains at least one key-value pair with a null value", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CategoricalCellValueFormat<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'valueToTextMap' contains a key-value pair with a white space value scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CategoricalCellValueFormat<Version>>();

                            var dictionaryWithNullValue = referenceObject.ValueToTextMap.ToDictionary(_ => _.Key, _ => _.Value);

                            var randomKey = dictionaryWithNullValue.Keys.ElementAt(ThreadSafeRandom.Next(0, dictionaryWithNullValue.Count));

                            dictionaryWithNullValue[randomKey] = "  \r\n  ";

                            var result = new CategoricalCellValueFormat<Version>(
                                dictionaryWithNullValue,
                                referenceObject.MissingValueText);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "valueToTextMap", "contains at least one key-value pair with a null value", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CategoricalCellValueFormat<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'missingValueText' is white space scenario 1",
                        ConstructionFunc = () =>
                        {
                            var result = new CategoricalCellValueFormat<Version>(
                                                 new Dictionary<Version, string>
                                                 {
                                                     { A.Dummy<Version>(), "abc" },
                                                     { A.Dummy<Version>(), "def" },
                                                     { A.Dummy<Version>(), "abc" },
                                                 },
                                                 Invariant($"  {Environment.NewLine}  "));

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "valueToTextMap Values and missingValueText are not distinct", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CategoricalCellValueFormat<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'missingValueText' is white space scenario 2",
                        ConstructionFunc = () =>
                        {
                            var result = new CategoricalCellValueFormat<Version>(
                                new Dictionary<Version, string>
                                {
                                    { A.Dummy<Version>(), "abc" },
                                    { A.Dummy<Version>(), "def" },
                                    { A.Dummy<Version>(), "ghi" },
                                },
                                Invariant($"def"));

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "valueToTextMap Values and missingValueText are not distinct", },
                    });
        }
    }
}