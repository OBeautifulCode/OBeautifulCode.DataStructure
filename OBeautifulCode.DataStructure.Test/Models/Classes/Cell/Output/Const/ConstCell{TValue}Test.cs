// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstCell{TValue}Test.cs" company="OBeautifulCode">
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
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class ConstCellTValueTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static ConstCellTValueTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ConstCell<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'validationEvents' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ConstCell<Version>>();

                            var result = new ConstCell<Version>(
                                                 referenceObject.Value,
                                                 referenceObject.Id,
                                                 referenceObject.ColumnsSpanned,
                                                 referenceObject.Details,
                                                 referenceObject.Validation,
                                                 new CellValidationEventBase[0].Concat(referenceObject.ValidationEvents).Concat(new CellValidationEventBase[] { null }).Concat(referenceObject.ValidationEvents).ToList(),
                                                 referenceObject.DefaultAvailability,
                                                 referenceObject.AvailabilityCheck,
                                                 referenceObject.AvailabilityCheckEvents,
                                                 referenceObject.ValueFormat,
                                                 referenceObject.Format,
                                                 referenceObject.HoverOver,
                                                 referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "validationEvents", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ConstCell<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'defaultAvailability' is Availability.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ConstCell<Version>>();

                            var result = new ConstCell<Version>(
                                                 referenceObject.Value,
                                                 referenceObject.Id,
                                                 referenceObject.ColumnsSpanned,
                                                 referenceObject.Details,
                                                 referenceObject.Validation,
                                                 referenceObject.ValidationEvents,
                                                 Availability.Unknown,
                                                 referenceObject.AvailabilityCheck,
                                                 referenceObject.AvailabilityCheckEvents,
                                                 referenceObject.ValueFormat,
                                                 referenceObject.Format,
                                                 referenceObject.HoverOver,
                                                 referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "defaultAvailability", "Unknown", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ConstCell<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'availabilityCheckEvents' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ConstCell<Version>>();

                            var result = new ConstCell<Version>(
                                                 referenceObject.Value,
                                                 referenceObject.Id,
                                                 referenceObject.ColumnsSpanned,
                                                 referenceObject.Details,
                                                 referenceObject.Validation,
                                                 referenceObject.ValidationEvents,
                                                 referenceObject.DefaultAvailability,
                                                 referenceObject.AvailabilityCheck,
                                                 new CellAvailabilityCheckEventBase[0].Concat(referenceObject.AvailabilityCheckEvents).Concat(new CellAvailabilityCheckEventBase[] { null }).Concat(referenceObject.AvailabilityCheckEvents).ToList(),
                                                 referenceObject.ValueFormat,
                                                 referenceObject.Format,
                                                 referenceObject.HoverOver,
                                                 referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "availabilityCheckEvents", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ConstCell<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is 0",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ConstCell<Version>>();

                            var result = new ConstCell<Version>(
                                                 referenceObject.Value,
                                                 referenceObject.Id,
                                                 0,
                                                 referenceObject.Details,
                                                 referenceObject.Validation,
                                                 referenceObject.ValidationEvents,
                                                 referenceObject.DefaultAvailability,
                                                 referenceObject.AvailabilityCheck,
                                                 referenceObject.AvailabilityCheckEvents,
                                                 referenceObject.ValueFormat,
                                                 referenceObject.Format,
                                                 referenceObject.HoverOver,
                                                 referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned is 0; must be null or >= 1.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ConstCell<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is -1",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ConstCell<Version>>();

                            var result = new ConstCell<Version>(
                                referenceObject.Value,
                                referenceObject.Id,
                                -1,
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned is -1; must be null or >= 1.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ConstCell<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is negative number",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ConstCell<Version>>();

                            var result = new ConstCell<Version>(
                                referenceObject.Value,
                                referenceObject.Id,
                                A.Dummy<NegativeInteger>(),
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned", "must be null or >= 1.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ConstCell<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'validation' is is null but parameter 'validationEvents' is not null nor empty.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ConstCell<Version>>();

                            var result = new ConstCell<Version>(
                                referenceObject.Value,
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details,
                                null,
                                Some.ReadOnlyDummies<CellValidationEventBase>().ToList(),
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "There is no validation specified, however one or more validationEvents exists.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ConstCell<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'availabilityCheckEvents' contains a null element",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ConstCell<Version>>();

                            var result = new ConstCell<Version>(
                                referenceObject.Value,
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                new[] { A.Dummy<CellAvailabilityCheckEventBase>(), null, A.Dummy<CellAvailabilityCheckEventBase>() },
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "availabilityCheckEvents contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ConstCell<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'availabilityCheck' is is null but parameter 'availabilityCheckEvents' is not null nor empty.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ConstCell<Version>>();

                            var result = new ConstCell<Version>(
                                referenceObject.Value,
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                null,
                                Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>().ToList(),
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "There is no availabilityCheck specified, however one or more availabilityCheckEvents exists", },
                    });
        }

        [Fact]
        public static void ClearAvailabilityCheck___Should_do_nothing___When_AvailabilityCheck_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>().DeepCloneWithAvailabilityCheckEvents(null).DeepCloneWithAvailabilityCheck(null);

            // Act
            systemUnderTest.ClearAvailabilityCheck(A.Dummy<UtcDateTime>(), A.Dummy<string>());

            // Assert
            systemUnderTest.AvailabilityCheckEvents.Must().BeNull();
        }

        [Fact]
        public static void ClearAvailabilityCheck___Should_add_CellAvailabilityCheckClearedEvent_to_the_end_of_AvailabilityCheckEvents___When_AvailabilityCheck_is_not_null_and_AvailabilityCheckEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>().DeepCloneWithAvailabilityCheckEvents(null);

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            IReadOnlyList<CellAvailabilityCheckEventBase> expected = new CellAvailabilityCheckEventBase[]
                {
                    new CellAvailabilityCheckClearedEvent(timestampUtc, details),
                }
                .ToList();

            // Act
            systemUnderTest.ClearAvailabilityCheck(timestampUtc, details);

            // Assert
            systemUnderTest.AvailabilityCheckEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ClearAvailabilityCheck___Should_add_CellAvailabilityCheckClearedEvent_to_the_end_of_AvailabilityCheckEvents___When_AvailabilityCheck_is_not_null_and_AvailabilityCheckEvents_is_not_empty()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>();

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            IReadOnlyList<CellAvailabilityCheckEventBase> expected = new CellAvailabilityCheckEventBase[0]
                .Concat(systemUnderTest.AvailabilityCheckEvents)
                .Concat(new[]
                {
                    new CellAvailabilityCheckClearedEvent(timestampUtc, details),
                })
                .ToList();

            // Act
            systemUnderTest.ClearAvailabilityCheck(timestampUtc, details);

            // Assert
            systemUnderTest.AvailabilityCheckEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Record_CellAvailabilityCheckEventBase___Should_throw_ArgumentNullException___When_availabilityCheckEvent_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.Record((CellAvailabilityCheckEventBase)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Record_CellAvailabilityCheckEventBase___Should_throw_InvalidOperationException___When_AvailabilityCheck_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>().DeepCloneWithAvailabilityCheckEvents(null).DeepCloneWithAvailabilityCheck(null);

            var availabilityCheckEvent = A.Dummy<CellAvailabilityCheckEventBase>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.Record(availabilityCheckEvent));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().BeEqualTo("Cannot record availabilityCheckEvent when there is no AvailabilityCheck.");
        }

        [Fact]
        public static void Record_CellAvailabilityCheckEventBase___Should_add_availabilityCheckEvent_to_the_end_of_AvailabilityCheckEvents___When_AvailabilityCheck_is_not_null_and_AvailabilityCheckEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>().DeepCloneWithAvailabilityCheckEvents(null);

            var availabilityCheckEvent = A.Dummy<CellAvailabilityCheckEventBase>();

            IReadOnlyList<CellAvailabilityCheckEventBase> expected = new CellAvailabilityCheckEventBase[]
                {
                    availabilityCheckEvent,
                }
                .ToList();

            // Act
            systemUnderTest.Record(availabilityCheckEvent);

            // Assert
            systemUnderTest.AvailabilityCheckEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Record_CellAvailabilityCheckEventBase___Should_add_availabilityCheckEvent_to_the_end_of_AvailabilityCheckEvents___When_AvailabilityCheck_is_not_null_and_AvailabilityCheckEvents_is_not_empty()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>();

            var availabilityCheckEvent = A.Dummy<CellAvailabilityCheckEventBase>();

            IReadOnlyList<CellAvailabilityCheckEventBase> expected = new CellAvailabilityCheckEventBase[0]
                .Concat(systemUnderTest.AvailabilityCheckEvents)
                .Concat(new[]
                {
                    availabilityCheckEvent,
                })
                .ToList();

            // Act
            systemUnderTest.Record(availabilityCheckEvent);

            // Assert
            systemUnderTest.AvailabilityCheckEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ClearValidation___Should_do_nothing___When_Validation_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>().DeepCloneWithValidationEvents(null).DeepCloneWithValidation(null);

            // Act
            systemUnderTest.ClearValidation(A.Dummy<UtcDateTime>(), A.Dummy<string>());

            // Assert
            systemUnderTest.ValidationEvents.Must().BeNull();
        }

        [Fact]
        public static void ClearValidation___Should_add_CellValidationClearedEvent_to_the_end_of_ValidationEvents___When_Validation_is_not_null_and_ValidationEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>().DeepCloneWithValidationEvents(null);

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            IReadOnlyList<CellValidationEventBase> expected = new CellValidationEventBase[]
                {
                    new CellValidationClearedEvent(timestampUtc, details),
                }
                .ToList();

            // Act
            systemUnderTest.ClearValidation(timestampUtc, details);

            // Assert
            systemUnderTest.ValidationEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ClearValidation___Should_add_CellValidationClearedEvent_to_the_end_of_ValidationEvents___When_Validation_is_not_null_and_ValidationEvents_is_not_empty()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>();

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            IReadOnlyList<CellValidationEventBase> expected = new CellValidationEventBase[0]
                .Concat(systemUnderTest.ValidationEvents)
                .Concat(new[]
                {
                    new CellValidationClearedEvent(timestampUtc, details),
                })
                .ToList();

            // Act
            systemUnderTest.ClearValidation(timestampUtc, details);

            // Assert
            systemUnderTest.ValidationEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Record_CellValidationEventBase___Should_throw_ArgumentNullException___When_validationEvent_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.Record((CellValidationEventBase)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Record_CellValidationEventBase___Should_throw_InvalidOperationException___When_Validation_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>().DeepCloneWithValidationEvents(null).DeepCloneWithValidation(null);

            var validationEvent = A.Dummy<CellValidationEventBase>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.Record(validationEvent));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().BeEqualTo("Cannot record validationEvent when there is no Validation.");
        }

        [Fact]
        public static void Record_CellValidationEventBase___Should_add_validationEvent_to_the_end_of_ValidationEvents___When_Validation_is_not_null_and_ValidationEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>().DeepCloneWithValidationEvents(null);

            var validationEvent = A.Dummy<CellValidationEventBase>();

            IReadOnlyList<CellValidationEventBase> expected = new CellValidationEventBase[]
                {
                    validationEvent,
                }
                .ToList();

            // Act
            systemUnderTest.Record(validationEvent);

            // Assert
            systemUnderTest.ValidationEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Record_CellValidationEventBase___Should_add_validationEvent_to_the_end_of_ValidationEvents___When_Validation_is_not_null_and_ValidationEvents_is_not_empty()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>();

            var validationEvent = A.Dummy<CellValidationEventBase>();

            IReadOnlyList<CellValidationEventBase> expected = new CellValidationEventBase[0]
                .Concat(systemUnderTest.ValidationEvents)
                .Concat(new[]
                {
                    validationEvent,
                })
                .ToList();

            // Act
            systemUnderTest.Record(validationEvent);

            // Assert
            systemUnderTest.ValidationEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetValueTypeOfNull___Should_return_TValue___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>();

            // Act
            var actual = systemUnderTest.GetValueTypeOrNull();

            // Assert
            actual.AsTest().Must().BeEqualTo(typeof(Version));
        }

        [Fact]
        public static void GetCellObjectValue___Should_return_constant_value___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>();

            // Act
            var actual = systemUnderTest.GetCellObjectValue();

            // Assert
            actual.AsTest().Must().BeEqualTo((object)systemUnderTest.Value);
        }

        [Fact]
        public static void GetCellValue___Should_return_constant_value___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>();

            // Act
            var actual = systemUnderTest.GetCellValue();

            // Assert
            actual.AsTest().Must().BeEqualTo(systemUnderTest.Value);
        }

        [Fact]
        public static void HasCellValue___Should_return_true___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<ConstCell<Version>>();

            // Act
            var actual = systemUnderTest.HasCellValue();

            // Assert
            actual.AsTest().Must().BeTrue();
        }
    }
}