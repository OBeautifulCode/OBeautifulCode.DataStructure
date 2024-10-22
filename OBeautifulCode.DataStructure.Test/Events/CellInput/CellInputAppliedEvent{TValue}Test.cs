// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellInputAppliedEvent{TValue}Test.cs" company="OBeautifulCode">
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
    public static partial class CellInputAppliedEventTValueTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CellInputAppliedEventTValueTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CellInputAppliedEvent<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'timestampUtc' is not a UTC DateTime (it's Local)",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CellInputAppliedEvent<Version>>();

                            var result = new CellInputAppliedEvent<Version>(
                                                 referenceObject.Value,
                                                 DateTime.Now,
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "timestampUtc", "Kind that is not DateTimeKind.Utc", "DateTimeKind.Local" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CellInputAppliedEvent<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'timestampUtc' is not a UTC DateTime (it's Unspecified)",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CellInputAppliedEvent<Version>>();

                            var result = new CellInputAppliedEvent<Version>(
                                                 referenceObject.Value,
                                                 DateTime.UtcNow.ToUnspecified(),
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "timestampUtc", "Kind that is not DateTimeKind.Utc", "DateTimeKind.Unspecified" },
                    });
        }

        [Fact]
        public static void GetObjectValue___Returns_Value___When_called()
        {
            // Arrange
            var expected = A.Dummy<string>();

            var systemUnderTest = new CellInputAppliedEvent<string>(expected, A.Dummy<UtcDateTime>());

            // Act
            var actual = systemUnderTest.GetObjectValue();

            // Assert
            actual.AsTest().Must().BeEqualTo((object)expected);
        }
    }
}