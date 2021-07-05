// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FontFormatTest.cs" company="OBeautifulCode">
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
    public static partial class FontFormatTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static FontFormatTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FontFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'fontNamesInFallbackOrder' is an empty enumerable scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<FontFormat>();

                            var result = new FontFormat(
                                                 referenceObject.FontColor,
                                                 new List<string>(),
                                                 referenceObject.FontSizeInPoints,
                                                 referenceObject.Options);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "fontNamesInFallbackOrder", "is an empty enumerable", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FontFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'fontNamesInFallbackOrder' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<FontFormat>();

                            var result = new FontFormat(
                                                 referenceObject.FontColor,
                                                 new string[0].Concat(referenceObject.FontNamesInFallbackOrder).Concat(new string[] { null }).Concat(referenceObject.FontNamesInFallbackOrder).ToList(),
                                                 referenceObject.FontSizeInPoints,
                                                 referenceObject.Options);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "fontNamesInFallbackOrder", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FontFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'fontNamesInFallbackOrder' contains a white space element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<FontFormat>();

                            var result = new FontFormat(
                                referenceObject.FontColor,
                                new string[0].Concat(referenceObject.FontNamesInFallbackOrder).Concat(new string[] { " \r\n " }).Concat(referenceObject.FontNamesInFallbackOrder).ToList(),
                                referenceObject.FontSizeInPoints,
                                referenceObject.Options);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "fontNamesInFallbackOrder", "contains a white space element", },
                    });
        }
    }
}