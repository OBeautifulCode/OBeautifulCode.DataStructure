// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkedMediaTest.cs" company="OBeautifulCode">
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
    public static partial class LinkedMediaTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static LinkedMediaTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<LinkedMedia>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'url' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<LinkedMedia>();

                            var result = new LinkedMedia(
                                                 null,
                                                 referenceObject.MediaKind,
                                                 referenceObject.Name);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "url", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<LinkedMedia>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'url' is white space scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<LinkedMedia>();

                            var result = new LinkedMedia(
                                                 Invariant($"  {Environment.NewLine}  "),
                                                 referenceObject.MediaKind,
                                                 referenceObject.Name);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "url", "white space", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<LinkedMedia>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'mediaKind' is Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<LinkedMedia>();

                            var result = new LinkedMedia(
                                referenceObject.Url,
                                MediaKind.Unknown,
                                referenceObject.Name);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "mediaKind", "Unknown", },
                    });
        }
    }
}