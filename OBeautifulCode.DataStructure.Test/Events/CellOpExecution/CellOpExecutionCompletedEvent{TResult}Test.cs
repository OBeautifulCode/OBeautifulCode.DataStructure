// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellOpExecutionCompletedEvent{TResult}Test.cs" company="OBeautifulCode">
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
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.DateTime.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class CellOpExecutionCompletedEventTResultTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CellOpExecutionCompletedEventTResultTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CellOpExecutionCompletedEvent<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'timestampUtc' is not a UTC DateTime (it's Local)",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CellOpExecutionCompletedEvent<Version>>();

                            var result = new CellOpExecutionCompletedEvent<Version>(
                                                    referenceObject.ExecutionResult,
                                                    DateTime.Now,
                                                    referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "timestampUtc", "Kind that is not DateTimeKind.Utc", "DateTimeKind.Local" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CellOpExecutionCompletedEvent<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'timestampUtc' is not a UTC DateTime (it's Unspecified)",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CellOpExecutionCompletedEvent<Version>>();

                            var result = new CellOpExecutionCompletedEvent<Version>(
                                                    referenceObject.ExecutionResult,
                                                    DateTime.UtcNow.ToUnspecified(),
                                                    referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "timestampUtc", "Kind that is not DateTimeKind.Utc", "DateTimeKind.Unspecified" },
                    });
        }

        [Fact]
        public static void GetExecutionResultObjectValue___Returns_ExecutionResult___When_called()
        {
            // Arrange
            var expected = A.Dummy<string>();

            var systemUnderTest = new CellOpExecutionCompletedEvent<string>(expected, A.Dummy<UtcDateTime>());

            // Act
            var actual = systemUnderTest.GetExecutionResultObjectValue();

            // Assert
            actual.AsTest().Must().BeEqualTo((object)expected);
        }

        [Fact]
        public static void GetExecutionResultType___Returns_type_of_TResult___When_called()
        {
            // Arrange
            var systemUnderTest = new CellOpExecutionCompletedEvent<string>(A.Dummy<string>(), A.Dummy<UtcDateTime>());

            // Act
            var actual = systemUnderTest.GetExecutionResultType();

            // Assert
            actual.AsTest().Must().BeEqualTo(typeof(string));
        }
    }
}