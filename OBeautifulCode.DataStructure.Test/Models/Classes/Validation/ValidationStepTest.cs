// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationStepTest.cs" company="OBeautifulCode">
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
    public static partial class ValidationStepTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static ValidationStepTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ValidationStep>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'operation' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ValidationStep>();

                            var result = new ValidationStep(
                                                 null,
                                                 referenceObject.StopMessageOp,
                                                 referenceObject.TrueAction,
                                                 referenceObject.FalseAction,
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "operation", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ValidationStep>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'trueAction' is ValidationStepAction.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ValidationStep>();

                            var result = new ValidationStep(
                                                 referenceObject.Operation,
                                                 referenceObject.StopMessageOp,
                                                 ValidationStepAction.Unknown,
                                                 referenceObject.FalseAction,
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "trueAction", "Unknown" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ValidationStep>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'falseAction' is ValidationStepAction.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ValidationStep>();

                            var result = new ValidationStep(
                                referenceObject.Operation,
                                referenceObject.StopMessageOp,
                                referenceObject.TrueAction,
                                ValidationStepAction.Unknown,
                                referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "falseAction", "Unknown" },
                    });
        }
    }
}