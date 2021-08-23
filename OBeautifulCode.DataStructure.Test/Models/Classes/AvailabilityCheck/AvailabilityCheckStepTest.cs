// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AvailabilityCheckStepTest.cs" company="OBeautifulCode">
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
    public static partial class AvailabilityCheckStepTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static AvailabilityCheckStepTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<AvailabilityCheckStep>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'operation' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<AvailabilityCheckStep>();

                            var result = new AvailabilityCheckStep(
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
                    new ConstructorArgumentValidationTestScenario<AvailabilityCheckStep>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'trueAction' is AvailabilityCheckStepAction.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<AvailabilityCheckStep>();

                            var result = new AvailabilityCheckStep(
                                                 referenceObject.Operation,
                                                 referenceObject.StopMessageOp,
                                                 AvailabilityCheckStepAction.Unknown,
                                                 referenceObject.FalseAction,
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "trueAction", "Unknown" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<AvailabilityCheckStep>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'falseAction' is AvailabilityCheckStepAction.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<AvailabilityCheckStep>();

                            var result = new AvailabilityCheckStep(
                                referenceObject.Operation,
                                referenceObject.StopMessageOp,
                                referenceObject.TrueAction,
                                AvailabilityCheckStepAction.Unknown,
                                referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "falseAction", "Unknown" },
                    });
        }
    }
}