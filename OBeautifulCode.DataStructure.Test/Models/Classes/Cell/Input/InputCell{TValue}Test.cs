// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputCell{TValue}Test.cs" company="OBeautifulCode">
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
    public static partial class InputCellTValueTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static InputCellTValueTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InputCell<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is 0",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InputCell<Version>>();

                            var result = new InputCell<Version>(
                                                 referenceObject.Id,
                                                 0,
                                                 referenceObject.Details,
                                                 referenceObject.Validation,
                                                 referenceObject.ValidationEvents,
                                                 referenceObject.DefaultAvailability,
                                                 referenceObject.AvailabilityCheck,
                                                 referenceObject.AvailabilityCheckEvents,
                                                 referenceObject.InputEvents,
                                                 referenceObject.ValueFormat,
                                                 referenceObject.Format,
                                                 referenceObject.HoverOver);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned is 0; must be null or >= 1.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InputCell<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is -1",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InputCell<Version>>();

                            var result = new InputCell<Version>(
                                referenceObject.Id,
                                -1,
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                referenceObject.InputEvents,
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned is -1; must be null or >= 1.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InputCell<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is negative number",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InputCell<Version>>();

                            var result = new InputCell<Version>(
                                referenceObject.Id,
                                A.Dummy<NegativeInteger>(),
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                referenceObject.InputEvents,
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned", "must be null or >= 1.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InputCell<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'validation' is is null but parameter 'validationEvents' is not null nor empty.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InputCell<Version>>();

                            var result = new InputCell<Version>(
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details,
                                null,
                                Some.ReadOnlyDummies<CellValidationEventBase>().ToList(),
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                referenceObject.InputEvents,
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "There is no validation specified, however one or more validationEvents exists.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InputCell<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'availabilityCheck' is is null but parameter 'availabilityCheckEvents' is not null nor empty.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InputCell<Version>>();

                            var result = new InputCell<Version>(
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                null,
                                Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>().ToList(),
                                referenceObject.InputEvents,
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver);

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
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithAvailabilityCheckEvents(null).DeepCloneWithAvailabilityCheck(null);

            // Act
            systemUnderTest.ClearAvailabilityCheck(A.Dummy<UtcDateTime>(), A.Dummy<string>());

            // Assert
            systemUnderTest.AvailabilityCheckEvents.Must().BeNull();
        }

        [Fact]
        public static void ClearAvailabilityCheck___Should_add_CellAvailabilityCheckClearedEvent_to_the_end_of_AvailabilityCheckEvents___When_AvailabilityCheck_is_not_null_and_AvailabilityCheckEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithAvailabilityCheckEvents(null);

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
            var systemUnderTest = A.Dummy<InputCell<Version>>();

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
            var systemUnderTest = A.Dummy<InputCell<Version>>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.Record((CellAvailabilityCheckEventBase)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Record_CellAvailabilityCheckEventBase___Should_throw_InvalidOperationException___When_AvailabilityCheck_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithAvailabilityCheckEvents(null).DeepCloneWithAvailabilityCheck(null);

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
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithAvailabilityCheckEvents(null);

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
            var systemUnderTest = A.Dummy<InputCell<Version>>();

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
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithValidationEvents(null).DeepCloneWithValidation(null);

            // Act
            systemUnderTest.ClearValidation(A.Dummy<UtcDateTime>(), A.Dummy<string>());

            // Assert
            systemUnderTest.ValidationEvents.Must().BeNull();
        }

        [Fact]
        public static void ClearValidation___Should_add_CellValidationClearedEvent_to_the_end_of_ValidationEvents___When_Validation_is_not_null_and_ValidationEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithValidationEvents(null);

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
            var systemUnderTest = A.Dummy<InputCell<Version>>();

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
            var systemUnderTest = A.Dummy<InputCell<Version>>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.Record((CellValidationEventBase)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Record_CellValidationEventBase___Should_throw_InvalidOperationException___When_Validation_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithValidationEvents(null).DeepCloneWithValidation(null);

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
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithValidationEvents(null);

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
            var systemUnderTest = A.Dummy<InputCell<Version>>();

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
            var systemUnderTest = A.Dummy<InputCell<Version>>();

            // Act
            var actual = systemUnderTest.GetValueTypeOrNull();

            // Assert
            actual.AsTest().Must().BeEqualTo(typeof(Version));
        }

        [Fact]
        public static void ClearCellValue___Should_add_CellInputClearedEvent_to_the_end_of_InputEvents___When_InputEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(null);

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            IReadOnlyList<CellInputEventBase> expected = new CellInputEventBase[]
                {
                    new CellInputClearedEvent(timestampUtc, details),
                }
                .ToList();

            // Act
            systemUnderTest.ClearCellValue(timestampUtc, details);

            // Assert
            systemUnderTest.InputEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ClearCellValue___Should_add_CellInputClearedEvent_to_the_end_of_InputEvents___When_InputEvents_is_not_empty()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>();

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            IReadOnlyList<CellInputEventBase> expected = new CellInputEventBase[0]
                .Concat(systemUnderTest.InputEvents)
                .Concat(new[]
                {
                    new CellInputClearedEvent(timestampUtc, details),
                })
                .ToList();

            // Act
            systemUnderTest.ClearCellValue(timestampUtc, details);

            // Assert
            systemUnderTest.InputEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void SetCellValue_TValue___Should_add_CellInputAppliedEvent_to_the_end_of_InputEvents___When_InputEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(null);

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            var version = A.Dummy<Version>();

            IReadOnlyList<CellInputEventBase> expected = new CellInputEventBase[]
                {
                    new CellInputAppliedEvent<Version>(version, timestampUtc, details),
                }
                .ToList();

            // Act
            systemUnderTest.SetCellValue(version, timestampUtc, details);

            // Assert
            systemUnderTest.InputEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void SetCellValue_TValue___Should_add_CellInputAppliedEvent_to_the_end_of_InputEvents___When_InputEvents_is_not_empty()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>();

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            var version = A.Dummy<Version>();

            IReadOnlyList<CellInputEventBase> expected = new CellInputEventBase[0]
                .Concat(systemUnderTest.InputEvents)
                .Concat(new[]
                {
                    new CellInputAppliedEvent<Version>(version, timestampUtc, details),
                })
                .ToList();

            // Act
            systemUnderTest.SetCellValue(version, timestampUtc, details);

            // Assert
            systemUnderTest.InputEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void SetCellValue_object___Should_throw_ArgumentException___When_value_is_null_but_TValue_is_not_assignable_to_null()
        {
            // Arrange
            var systemUnderTest = new InputCell<int>();

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.SetCellValue(null, timestampUtc, details));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().BeEqualTo("value is null, which is not assignable to a value of type int.");
        }

        [Fact]
        public static void SetCellValue_object___Should_throw_ArgumentException___When_value_is_not_null_and_not_assignable_to_TValue()
        {
            // Arrange
            var systemUnderTest = new InputCell<int>();

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.SetCellValue(A.Dummy<Version>(), timestampUtc, details));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().BeEqualTo("value is not of type int.");
        }

        [Fact]
        public static void SetCellValue_object___Should_add_CellInputAppliedEvent_to_the_end_of_InputEvents___When_value_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(null);

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            IReadOnlyList<CellInputEventBase> expected = new CellInputEventBase[]
                {
                    new CellInputAppliedEvent<Version>(null, timestampUtc, details),
                }
                .ToList();

            // Act
            systemUnderTest.SetCellValue(null, timestampUtc, details);

            // Assert
            systemUnderTest.InputEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void SetCellValue_object___Should_add_CellInputAppliedEvent_to_the_end_of_InputEvents___When_InputEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(null);

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            var version = A.Dummy<Version>();

            IReadOnlyList<CellInputEventBase> expected = new CellInputEventBase[]
                {
                    new CellInputAppliedEvent<Version>(version, timestampUtc, details),
                }
                .ToList();

            // Act
            systemUnderTest.SetCellValue((object)version, timestampUtc, details);

            // Assert
            systemUnderTest.InputEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void SetCellValue_object___Should_add_CellInputAppliedEvent_to_the_end_of_InputEvents___When_InputEvents_is_not_empty()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>();

            var timestampUtc = A.Dummy<UtcDateTime>();

            var details = A.Dummy<string>();

            var version = A.Dummy<Version>();

            IReadOnlyList<CellInputEventBase> expected = new CellInputEventBase[0]
                .Concat(systemUnderTest.InputEvents)
                .Concat(new[]
                {
                    new CellInputAppliedEvent<Version>(version, timestampUtc, details),
                })
                .ToList();

            // Act
            systemUnderTest.SetCellValue(version, timestampUtc, details);

            // Assert
            systemUnderTest.InputEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Record_CellInputEventBase___Should_throw_ArgumentNullException___When_inputEvent_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.Record((CellInputEventBase)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Record_CellInputEventBase___Should_throw_ArgumentException___When_inputEvent_cannot_be_applied_to_cell()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>();

            var inputEvent = new CellInputAppliedEvent<string>(A.Dummy<string>(), DateTime.UtcNow, A.Dummy<string>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.Record(inputEvent));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().BeEqualTo("inputEvent is of type 'CellInputAppliedEvent<string>', which is not applicable to this cell, which is of type 'InputCell<Version>'.");
        }

        [Fact]
        public static void Record_CellInputEventBase___Should_add_inputEvent_to_the_end_of_InputEvents___When_InputEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(null);

            var inputEvent = A.Dummy<CellInputEventBase>();

            IReadOnlyList<CellInputEventBase> expected = new CellInputEventBase[]
                {
                    inputEvent,
                }
                .ToList();

            // Act
            systemUnderTest.Record(inputEvent);

            // Assert
            systemUnderTest.InputEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Record_CellInputEventBase___Should_add_inputEvent_to_the_end_of_InputEvents___When_InputEvents_is_not_empty()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>();

            var inputEvent = A.Dummy<CellInputEventBase>();

            IReadOnlyList<CellInputEventBase> expected = new CellInputEventBase[0]
                .Concat(systemUnderTest.InputEvents)
                .Concat(new[]
                {
                    inputEvent,
                })
                .ToList();

            // Act
            systemUnderTest.Record(inputEvent);

            // Assert
            systemUnderTest.InputEvents.Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetCellObjectValue___Should_throw_InvalidOperationException___When_InputEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(null);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCellObjectValue());

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().BeEqualTo("No input has been applied to the cell.");
        }

        [Fact]
        public static void GetCellObjectValue___Should_throw_InvalidOperationException___When_InputEvents_is_empty()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(new CellInputEventBase[0]);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCellObjectValue());

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().BeEqualTo("No input has been applied to the cell.");
        }

        [Fact]
        public static void GetCellObjectValue___Should_throw_InvalidOperationException___When_InputEvents_does_not_contain_CellInputAppliedEvent()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(new CellInputEventBase[0]);

            systemUnderTest.ClearCellValue(A.Dummy<UtcDateTime>(), A.Dummy<string>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCellObjectValue());

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().BeEqualTo("No input has been applied to the cell.");
        }

        [Fact]
        public static void GetCellObjectValue___Should_return_last_inputted_value___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>();

            var expected = A.Dummy<Version>();

            systemUnderTest.SetCellValue(A.Dummy<Version>(), A.Dummy<UtcDateTime>());

            systemUnderTest.SetCellValue(expected, A.Dummy<UtcDateTime>());

            // Act
            var actual = systemUnderTest.GetCellObjectValue();

            // Assert
            actual.AsTest().Must().BeEqualTo((object)expected);
        }

        [Fact]
        public static void GetCellValue___Should_throw_InvalidOperationException___When_InputEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(null);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCellValue());

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().BeEqualTo("No input has been applied to the cell.");
        }

        [Fact]
        public static void GetCellValue___Should_throw_InvalidOperationException___When_InputEvents_is_empty()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(new CellInputEventBase[0]);

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCellValue());

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().BeEqualTo("No input has been applied to the cell.");
        }

        [Fact]
        public static void GetCellValue___Should_throw_InvalidOperationException___When_InputEvents_does_not_contain_CellInputAppliedEvent()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(new CellInputEventBase[0]);

            systemUnderTest.ClearCellValue(A.Dummy<UtcDateTime>(), A.Dummy<string>());

            // Act
            var actual = Record.Exception(() => systemUnderTest.GetCellValue());

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().BeEqualTo("No input has been applied to the cell.");
        }

        [Fact]
        public static void GetCellValue___Should_return_last_inputted_value___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>();

            var expected = A.Dummy<Version>();

            systemUnderTest.SetCellValue(A.Dummy<Version>(), A.Dummy<UtcDateTime>());

            systemUnderTest.SetCellValue(expected, A.Dummy<UtcDateTime>());

            // Act
            var actual = systemUnderTest.GetCellValue();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void HasCellValue___Should_return_false___When_InputEvents_is_null()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(null);

            // Act
            var actual = systemUnderTest.HasCellValue();

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void HasCellValue___Should_return_false___When_InputEvents_is_empty()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(new CellInputEventBase[0]);

            // Act
            var actual = systemUnderTest.HasCellValue();

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void HasCellValue___Should_return_false___When_InputEvents_does_not_contain_CellInputAppliedEvent()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>().DeepCloneWithInputEvents(null);

            systemUnderTest.ClearCellValue(A.Dummy<UtcDateTime>(), A.Dummy<string>());

            // Act
            var actual = systemUnderTest.HasCellValue();

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void HasCellValue___Should_return_true___When_there_is_a_last_inputted_value()
        {
            // Arrange
            var systemUnderTest = A.Dummy<InputCell<Version>>();

            systemUnderTest.SetCellValue(A.Dummy<Version>(), A.Dummy<UtcDateTime>());

            // Act
            var actual = systemUnderTest.HasCellValue();

            // Assert
            actual.AsTest().Must().BeTrue();
        }
    }
}