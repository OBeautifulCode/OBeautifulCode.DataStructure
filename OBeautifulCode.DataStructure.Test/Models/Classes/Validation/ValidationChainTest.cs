// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationChainTest.cs" company="OBeautifulCode">
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
    public static partial class ValidationChainTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static ValidationChainTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ValidationChain>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'steps' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ValidationChain>();

                            var result = new ValidationChain(
                                                 null,
                                                 referenceObject.EndMessageOp,
                                                 referenceObject.EndValidity);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "steps", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ValidationChain>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'steps' is an empty enumerable scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ValidationChain>();

                            var result = new ValidationChain(
                                                 new List<ValidationStep>(),
                                                 referenceObject.EndMessageOp,
                                                 referenceObject.EndValidity);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "steps", "is an empty enumerable", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ValidationChain>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'steps' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ValidationChain>();

                            var result = new ValidationChain(
                                                 new ValidationStep[0].Concat(referenceObject.Steps).Concat(new ValidationStep[] { null }).Concat(referenceObject.Steps).ToList(),
                                                 referenceObject.EndMessageOp,
                                                 referenceObject.EndValidity);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "steps", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ValidationChain>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'endValidity' is Validity.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ValidationChain>();

                            var result = new ValidationChain(
                                                 referenceObject.Steps,
                                                 referenceObject.EndMessageOp,
                                                 Validity.Unknown);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "endValidity", "Unknown" },
                    });
        }
    }
}