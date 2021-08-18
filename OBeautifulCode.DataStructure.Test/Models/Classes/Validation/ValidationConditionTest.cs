// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationConditionTest.cs" company="OBeautifulCode">
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
    public static partial class ValidationConditionTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static ValidationConditionTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ValidationCondition>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'operation' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ValidationCondition>();

                            var result = new ValidationCondition(
                                                 null,
                                                 referenceObject.FailureMessageOp,
                                                 referenceObject.Kind,
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "operation", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ValidationCondition>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'failureMessageOp' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ValidationCondition>();

                            var result = new ValidationCondition(
                                                 referenceObject.Operation,
                                                 null,
                                                 referenceObject.Kind,
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "failureMessageOp", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ValidationCondition>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'kind' ValidationConditionKind.Unknown is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ValidationCondition>();

                            var result = new ValidationCondition(
                                                 referenceObject.Operation,
                                                 referenceObject.FailureMessageOp,
                                                 ValidationConditionKind.Unknown,
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "kind", "Unknown" },
                    });
        }
    }
}