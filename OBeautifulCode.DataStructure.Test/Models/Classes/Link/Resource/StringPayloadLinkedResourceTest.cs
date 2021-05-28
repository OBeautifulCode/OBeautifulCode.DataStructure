// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringPayloadLinkedResourceTest.cs" company="OBeautifulCode">
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
    public static partial class StringPayloadLinkedResourceTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static StringPayloadLinkedResourceTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<StringPayloadLinkedResource>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'resourceKind' is StringPayloadLinkedResourceKind.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<StringPayloadLinkedResource>();

                            var result = new StringPayloadLinkedResource(
                                referenceObject.Value,
                                StringPayloadLinkedResourceKind.Unknown);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "resourceKind", "Unknown", },
                    });
        }
    }
}