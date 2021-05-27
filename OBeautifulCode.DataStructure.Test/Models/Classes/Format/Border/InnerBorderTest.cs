// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InnerBorderTest.cs" company="OBeautifulCode">
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
    public static partial class InnerBorderTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static InnerBorderTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InnerBorder>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'weight' is BorderWeight.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InnerBorder>();

                            var result = new InnerBorder(BorderWeight.Unknown, referenceObject.Style, referenceObject.Color, referenceObject.Edges);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "weight", "Unknown", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InnerBorder>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'style' is BorderStyle.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InnerBorder>();

                            var result = new InnerBorder(referenceObject.Weight, BorderStyle.Unknown, referenceObject.Color, referenceObject.Edges);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "style", "Unknown", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InnerBorder>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'edges' is InnerBorderEdges.None",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InnerBorder>();

                            var result = new InnerBorder(referenceObject.Weight, referenceObject.Style, referenceObject.Color, InnerBorderEdges.None);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "edges", "None", },
                    });
        }
    }
}