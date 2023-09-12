// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardLinkTest.cs" company="OBeautifulCode">
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
    public static partial class StandardLinkTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static StandardLinkTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<StandardLink>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'resource' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<StandardLink>();

                            var result = new StandardLink(
                                null,
                                referenceObject.Target,
                                referenceObject.FormatsToApplyWhenActivated);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "resource", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<StandardLink>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'target' is LinkTarget.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<StandardLink>();

                            var result = new StandardLink(
                                referenceObject.Resource,
                                LinkTarget.Unknown,
                                referenceObject.FormatsToApplyWhenActivated);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "target", "Unknown" },
                    })

                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<StandardLink>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'formatsToApplyWhenActivated' is an empty enumerable scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<StandardLink>();

                            var result = new StandardLink(
                                                 referenceObject.Resource,
                                                 referenceObject.Target,
                                                 new List<RegionFormatBase>());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "formatsToApplyWhenActivated", "is an empty enumerable", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<StandardLink>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'formatsToApplyWhenActivated' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<StandardLink>();

                            var result = new StandardLink(
                                                 referenceObject.Resource,
                                                 referenceObject.Target,
                                                 new RegionFormatBase[0].Concat(referenceObject.FormatsToApplyWhenActivated).Concat(new RegionFormatBase[] { null }).Concat(referenceObject.FormatsToApplyWhenActivated).ToList());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "formatsToApplyWhenActivated", "contains at least one null element", },
                    });
        }
    }
}