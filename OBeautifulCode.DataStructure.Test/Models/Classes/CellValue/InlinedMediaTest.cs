// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InlinedMediaTest.cs" company="OBeautifulCode">
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
    public static partial class InlinedMediaTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static InlinedMediaTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InlinedMedia>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'bytes' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InlinedMedia>();

                            var result = new InlinedMedia(
                                                 null,
                                                 referenceObject.MediaKind,
                                                 referenceObject.Name);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "bytes", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InlinedMedia>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'bytes' is empty",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InlinedMedia>();

                            var result = new InlinedMedia(
                                                 new byte[0],
                                                 referenceObject.MediaKind,
                                                 referenceObject.Name);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "bytes", "empty", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InlinedMedia>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'mediaKind' is Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InlinedMedia>();

                            var result = new InlinedMedia(
                                Some.ReadOnlyDummies<byte>().ToArray(),
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