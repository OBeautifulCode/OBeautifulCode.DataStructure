// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Linq;
    using FakeItEasy;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using Xunit;

    public static class CellExtensionsTest
    {
        [Fact]
        public static void GetCellOpExecutionStatus___Should_throw_ArgumentNullException___When_parameter_cell_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => CellExtensions.GetCellOpExecutionStatus(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetCellOpExecutionStatus___Should_return_CellOpExecutionStatus_NotExecuted___When_OperationExecutionEvents_is_null()
        {
            // Arrange
            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(null);

            // Act
            var actual = cell.GetCellOpExecutionStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionStatus.NotExecuted);
        }

        [Fact]
        public static void GetCellOpExecutionStatus___Should_return_CellOpExecutionStatus_NotExecuted___When_OperationExecutionEvents_is_empty()
        {
            // Arrange
            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(new CellOpExecutionEventBase[0]);

            // Act
            var actual = cell.GetCellOpExecutionStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionStatus.NotExecuted);
        }

        [Fact]
        public static void GetCellOpExecutionStatus___Should_return_CellOpExecutionStatus_NotExecuted___When_last_event_in_OperationExecutionEvents_is_CellOpExecutionClearedEvent()
        {
            // Arrange
            var events = new CellOpExecutionEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellOpExecutionEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellOpExecutionClearedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(events);

            // Act
            var actual = cell.GetCellOpExecutionStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionStatus.NotExecuted);
        }

        [Fact]
        public static void GetCellOpExecutionStatus___Should_return_CellOpExecutionStatus_Aborted___When_last_event_in_OperationExecutionEvents_is_CellOpExecutionAbortedEvent()
        {
            // Arrange
            var events = new CellOpExecutionEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellOpExecutionEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellOpExecutionAbortedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(events);

            // Act
            var actual = cell.GetCellOpExecutionStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionStatus.Aborted);
        }

        [Fact]
        public static void GetCellOpExecutionStatus___Should_return_CellOpExecutionStatus_Completed___When_last_event_in_OperationExecutionEvents_is_CellOpExecutionCompletedEvent()
        {
            // Arrange
            var events = new CellOpExecutionEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellOpExecutionEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellOpExecutionCompletedEvent<Version>>(),
                })
                .ToList();

            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(events);

            // Act
            var actual = cell.GetCellOpExecutionStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionStatus.Completed);
        }

        [Fact]
        public static void GetCellOpExecutionStatus___Should_return_CellOpExecutionStatus_DeemedNotApplicable___When_last_event_in_OperationExecutionEvents_is_CellOpExecutionDeemedNotApplicableEvent()
        {
            // Arrange
            var events = new CellOpExecutionEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellOpExecutionEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellOpExecutionDeemedNotApplicableEvent>(),
                })
                .ToList();

            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(events);

            // Act
            var actual = cell.GetCellOpExecutionStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionStatus.DeemedNotApplicable);
        }

        [Fact]
        public static void GetCellOpExecutionStatus___Should_return_CellOpExecutionStatus_Failed___When_last_event_in_OperationExecutionEvents_is_CellOpExecutionFailedEvent()
        {
            // Arrange
            var events = new CellOpExecutionEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellOpExecutionEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellOpExecutionFailedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(events);

            // Act
            var actual = cell.GetCellOpExecutionStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionStatus.Failed);
        }

        [Fact]
        public static void GetCellOpExecutionOutcome___Should_return_CellOpExecutionOutcome_Unknown___When_OperationExecutionEvents_is_null()
        {
            // Arrange
            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(null);

            // Act
            var actual = cell.GetCellOpExecutionOutcome();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Unknown);
        }

        [Fact]
        public static void GetCellOpExecutionOutcome___Should_return_CellOpExecutionOutcome_Unknown___When_OperationExecutionEvents_is_empty()
        {
            // Arrange
            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(new CellOpExecutionEventBase[0]);

            // Act
            var actual = cell.GetCellOpExecutionOutcome();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Unknown);
        }

        [Fact]
        public static void GetCellOpExecutionOutcome___Should_return_CellOpExecutionOutcome_Unknown___When_last_event_in_OperationExecutionEvents_is_CellOpExecutionClearedEvent()
        {
            // Arrange
            var events = new CellOpExecutionEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellOpExecutionEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellOpExecutionClearedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(events);

            // Act
            var actual = cell.GetCellOpExecutionOutcome();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Unknown);
        }

        [Fact]
        public static void GetCellOpExecutionOutcome___Should_return_CellOpExecutionOutcome_Aborted___When_last_event_in_OperationExecutionEvents_is_CellOpExecutionAbortedEvent()
        {
            // Arrange
            var events = new CellOpExecutionEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellOpExecutionEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellOpExecutionAbortedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(events);

            // Act
            var actual = cell.GetCellOpExecutionOutcome();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Aborted);
        }

        [Fact]
        public static void GetCellOpExecutionOutcome___Should_return_CellOpExecutionOutcome_Completed___When_last_event_in_OperationExecutionEvents_is_CellOpExecutionCompletedEvent()
        {
            // Arrange
            var events = new CellOpExecutionEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellOpExecutionEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellOpExecutionCompletedEvent<Version>>(),
                })
                .ToList();

            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(events);

            // Act
            var actual = cell.GetCellOpExecutionOutcome();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
        }

        [Fact]
        public static void GetCellOpExecutionOutcome___Should_return_CellOpExecutionOutcome_NotApplicable___When_last_event_in_OperationExecutionEvents_is_CellOpExecutionDeemedNotApplicableEvent()
        {
            // Arrange
            var events = new CellOpExecutionEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellOpExecutionEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellOpExecutionDeemedNotApplicableEvent>(),
                })
                .ToList();

            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(events);

            // Act
            var actual = cell.GetCellOpExecutionOutcome();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionOutcome.NotApplicable);
        }

        [Fact]
        public static void GetCellOpExecutionOutcome___Should_return_CellOpExecutionOutcome_Failed___When_last_event_in_OperationExecutionEvents_is_CellOpExecutionFailedEvent()
        {
            // Arrange
            var events = new CellOpExecutionEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellOpExecutionEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellOpExecutionFailedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<OperationCell<Version>>().DeepCloneWithOperationExecutionEvents(events);

            // Act
            var actual = cell.GetCellOpExecutionOutcome();

            // Assert
            actual.AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Failed);
        }

        [Fact]
        public static void GetValidationStatus___Should_throw_ArgumentNullException___When_parameter_cell_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => CellExtensions.GetValidationStatus(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetValidationStatus___Should_return_ValidationStatus_ValidationMissing___When_Validation_is_null()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(null).DeepCloneWithValidation(null);

            // Act
            var actual = cell.GetValidationStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(ValidationStatus.ValidationMissing);
        }

        [Fact]
        public static void GetValidationStatus___Should_return_ValidationStatus_Unvalidated___When_ValidationEvents_is_null()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(null);

            // Act
            var actual = cell.GetValidationStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(ValidationStatus.Unvalidated);
        }

        [Fact]
        public static void GetValidationStatus___Should_return_ValidationStatus_Unvalidated___When_ValidationEvents_is_empty()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(new CellValidationEventBase[0]);

            // Act
            var actual = cell.GetValidationStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(ValidationStatus.Unvalidated);
        }

        [Fact]
        public static void GetValidationStatus___Should_return_ValidationStatus_Unvalidated___When_last_event_of_ValidationEvents_is_CellValidationClearedEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationClearedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidationStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(ValidationStatus.Unvalidated);
        }

        [Fact]
        public static void GetValidationStatus___Should_return_ValidationStatus_Aborted___When_last_event_of_ValidationEvents_is_CellValidationAbortedEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationAbortedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidationStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(ValidationStatus.Aborted);
        }

        [Fact]
        public static void GetValidationStatus___Should_return_ValidationStatus_DeemedNotApplicable___When_last_event_of_ValidationEvents_is_CellValidationDeemedNotApplicableEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationDeemedNotApplicableEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidationStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(ValidationStatus.DeemedNotApplicable);
        }

        [Fact]
        public static void GetValidationStatus___Should_return_ValidationStatus_DeterminedSubjectIsInvalid___When_last_event_of_ValidationEvents_is_CellValidationDeterminedCellInvalidEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationDeterminedCellInvalidEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidationStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(ValidationStatus.DeterminedSubjectIsInvalid);
        }

        [Fact]
        public static void GetValidationStatus___Should_return_ValidationStatus_DeterminedSubjectIsValid___When_last_event_of_ValidationEvents_is_CellValidationDeterminedCellValidEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationDeterminedCellValidEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidationStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(ValidationStatus.DeterminedSubjectIsValid);
        }

        [Fact]
        public static void GetValidationStatus___Should_return_ValidationStatus_Failed___When_last_event_of_ValidationEvents_is_CellValidationFailedEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationFailedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidationStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(ValidationStatus.Failed);
        }

        [Fact]
        public static void GetValidationMessageOrNull___Should_throw_ArgumentNullException___When_parameter_cell_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => CellExtensions.GetValidationMessageOrNull(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetValidationMessageOrNull___Should_return_null___When_Validation_is_null()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(null).DeepCloneWithValidation(null);

            // Act
            var actual = cell.GetValidationMessageOrNull();

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void GetValidationMessageOrNull___Should_return_null___When_ValidationEvents_is_null()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(null);

            // Act
            var actual = cell.GetValidationMessageOrNull();

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void GetValidationMessageOrNull___Should_return_null___When_ValidationEvents_is_empty()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(new CellValidationEventBase[0]);

            // Act
            var actual = cell.GetValidationMessageOrNull();

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void GetValidationMessageOrNull___Should_return_null___When_last_event_of_ValidationEvents_is_CellValidationClearedEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationClearedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidationMessageOrNull();

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void GetValidationMessageOrNull___Should_return_message___When_last_event_of_ValidationEvents_is_CellValidationAbortedEvent()
        {
            // Arrange
            var cellValidationAbortedEvent = A.Dummy<CellValidationAbortedEvent>();

            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    cellValidationAbortedEvent,
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidationMessageOrNull();

            // Assert
            actual.AsTest().Must().BeEqualTo(cellValidationAbortedEvent.Message);
        }

        [Fact]
        public static void GetValidationMessageOrNull___Should_return_message___When_last_event_of_ValidationEvents_is_CellValidationDeemedNotApplicableEvent()
        {
            // Arrange
            var cellValidationDeemedNotApplicableEvent = A.Dummy<CellValidationDeemedNotApplicableEvent>();

            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    cellValidationDeemedNotApplicableEvent,
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidationMessageOrNull();

            // Assert
            actual.AsTest().Must().BeEqualTo(cellValidationDeemedNotApplicableEvent.Message);
        }

        [Fact]
        public static void GetValidationMessageOrNull___Should_return_message___When_last_event_of_ValidationEvents_is_CellValidationDeterminedCellInvalidEvent()
        {
            // Arrange
            var cellValidationDeterminedCellInvalidEvent = A.Dummy<CellValidationDeterminedCellInvalidEvent>();

            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    cellValidationDeterminedCellInvalidEvent,
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidationMessageOrNull();

            // Assert
            actual.AsTest().Must().BeEqualTo(cellValidationDeterminedCellInvalidEvent.Message);
        }

        [Fact]
        public static void GetValidationMessageOrNull___Should_return_message___When_last_event_of_ValidationEvents_is_CellValidationDeterminedCellValidEvent()
        {
            // Arrange
            var cellValidationDeterminedCellValidEvent = A.Dummy<CellValidationDeterminedCellValidEvent>();

            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    cellValidationDeterminedCellValidEvent,
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidationMessageOrNull();

            // Assert
            actual.AsTest().Must().BeEqualTo(cellValidationDeterminedCellValidEvent.Message);
        }

        [Fact]
        public static void GetValidationMessageOrNull___Should_return_null___When_last_event_of_ValidationEvents_is_CellValidationFailedEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationFailedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidationMessageOrNull();

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void GetValidity___Should_return_Validity_Valid___When_Validation_is_null()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(null).DeepCloneWithValidation(null);

            // Act
            var actual = cell.GetValidity();

            // Assert
            actual.AsTest().Must().BeEqualTo(Validity.Valid);
        }

        [Fact]
        public static void GetValidity___Should_return_Validity_Unknown___When_ValidationEvents_is_null()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(null);

            // Act
            var actual = cell.GetValidity();

            // Assert
            actual.AsTest().Must().BeEqualTo(Validity.Unknown);
        }

        [Fact]
        public static void GetValidity___Should_return_Validity_Unknown___When_ValidationEvents_is_empty()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(new CellValidationEventBase[0]);

            // Act
            var actual = cell.GetValidity();

            // Assert
            actual.AsTest().Must().BeEqualTo(Validity.Unknown);
        }

        [Fact]
        public static void GetValidity___Should_return_Validity_Unknown___When_last_event_of_ValidationEvents_is_CellValidationClearedEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationClearedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidity();

            // Assert
            actual.AsTest().Must().BeEqualTo(Validity.Unknown);
        }

        [Fact]
        public static void GetValidity___Should_return_Validity_Aborted___When_last_event_of_ValidationEvents_is_CellValidationAbortedEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationAbortedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidity();

            // Assert
            actual.AsTest().Must().BeEqualTo(Validity.Aborted);
        }

        [Fact]
        public static void GetValidity___Should_return_Validity_NotApplicable___When_last_event_of_ValidationEvents_is_CellValidationDeemedNotApplicableEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationDeemedNotApplicableEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidity();

            // Assert
            actual.AsTest().Must().BeEqualTo(Validity.NotApplicable);
        }

        [Fact]
        public static void GetValidity___Should_return_Validity_Invalid___When_last_event_of_ValidationEvents_is_CellValidationDeterminedCellInvalidEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationDeterminedCellInvalidEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidity();

            // Assert
            actual.AsTest().Must().BeEqualTo(Validity.Invalid);
        }

        [Fact]
        public static void GetValidity___Should_return_Validity_Valid___When_last_event_of_ValidationEvents_is_CellValidationDeterminedCellValidEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationDeterminedCellValidEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidity();

            // Assert
            actual.AsTest().Must().BeEqualTo(Validity.Valid);
        }

        [Fact]
        public static void GetValidity___Should_return_Validity_Unknown___When_last_event_of_ValidationEvents_is_CellValidationFailedEvent()
        {
            // Arrange
            var events = new CellValidationEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellValidationEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellValidationFailedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithValidationEvents(events);

            // Act
            var actual = cell.GetValidity();

            // Assert
            actual.AsTest().Must().BeEqualTo(Validity.Unknown);
        }

        [Fact]
        public static void GetAvailabilityCheckStatus___Should_throw_ArgumentNullException___When_parameter_cell_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => CellExtensions.GetAvailabilityCheckStatus(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetAvailabilityCheckStatus___Should_return_AvailabilityCheckStatus_AvailabilityCheckMissing___When_AvailabilityCheck_is_null()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(null).DeepCloneWithAvailabilityCheck(null);

            // Act
            var actual = cell.GetAvailabilityCheckStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(AvailabilityCheckStatus.AvailabilityCheckMissing);
        }

        [Fact]
        public static void GetAvailabilityCheckStatus___Should_return_AvailabilityCheckStatus_Unchecked___When_AvailabilityCheckEvents_is_null()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(null);

            // Act
            var actual = cell.GetAvailabilityCheckStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(AvailabilityCheckStatus.Unchecked);
        }

        [Fact]
        public static void GetAvailabilityCheckStatus___Should_return_AvailabilityCheckStatus_Unchecked___When_AvailabilityCheckEvents_is_empty()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(new CellAvailabilityCheckEventBase[0]);

            // Act
            var actual = cell.GetAvailabilityCheckStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(AvailabilityCheckStatus.Unchecked);
        }

        [Fact]
        public static void GetAvailabilityCheckStatus___Should_return_AvailabilityCheckStatus_Unchecked___When_last_event_in_AvailabilityCheckEvents_is_CellAvailabilityCheckClearedEvent()
        {
            // Arrange
            var events = new CellAvailabilityCheckEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellAvailabilityCheckClearedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(events);

            // Act
            var actual = cell.GetAvailabilityCheckStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(AvailabilityCheckStatus.Unchecked);
        }

        [Fact]
        public static void GetAvailabilityCheckStatus___Should_return_AvailabilityCheckStatus_DeterminedSubjectIsDisabled___When_last_event_in_AvailabilityCheckEvents_is_CellAvailabilityCheckDeterminedCellDisabledEvent()
        {
            // Arrange
            var events = new CellAvailabilityCheckEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellAvailabilityCheckDeterminedCellDisabledEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(events);

            // Act
            var actual = cell.GetAvailabilityCheckStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(AvailabilityCheckStatus.DeterminedSubjectIsDisabled);
        }

        [Fact]
        public static void GetAvailabilityCheckStatus___Should_return_AvailabilityCheckStatus_DeterminedSubjectIsEnabled___When_last_event_in_AvailabilityCheckEvents_is_CellAvailabilityCheckDeterminedCellEnabledEvent()
        {
            // Arrange
            var events = new CellAvailabilityCheckEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellAvailabilityCheckDeterminedCellEnabledEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(events);

            // Act
            var actual = cell.GetAvailabilityCheckStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(AvailabilityCheckStatus.DeterminedSubjectIsEnabled);
        }

        [Fact]
        public static void GetAvailabilityCheckStatus___Should_return_AvailabilityCheckStatus_Failed___When_last_event_in_AvailabilityCheckEvents_is_CellAvailabilityCheckFailedEvent()
        {
            // Arrange
            var events = new CellAvailabilityCheckEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellAvailabilityCheckFailedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(events);

            // Act
            var actual = cell.GetAvailabilityCheckStatus();

            // Assert
            actual.AsTest().Must().BeEqualTo(AvailabilityCheckStatus.Failed);
        }

        [Fact]
        public static void GetAvailabilityCheckMessageOrNull___Should_throw_ArgumentNullException___When_parameter_cell_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => CellExtensions.GetAvailabilityCheckMessageOrNull(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetAvailabilityCheckMessageOrNull___Should_return_null___When_AvailabilityCheck_is_null()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(null).DeepCloneWithAvailabilityCheck(null);

            // Act
            var actual = cell.GetAvailabilityCheckMessageOrNull();

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void GetAvailabilityCheckMessageOrNull___Should_return_null___When_AvailabilityCheckEvents_is_null()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(null);

            // Act
            var actual = cell.GetAvailabilityCheckMessageOrNull();

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void GetAvailabilityCheckMessageOrNull___Should_return_null___When_AvailabilityCheckEvents_is_empty()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(new CellAvailabilityCheckEventBase[0]);

            // Act
            var actual = cell.GetAvailabilityCheckMessageOrNull();

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void GetAvailabilityCheckMessageOrNull___Should_return_null___When_last_event_in_AvailabilityCheckEvents_is_CellAvailabilityCheckClearedEvent()
        {
            // Arrange
            var events = new CellAvailabilityCheckEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellAvailabilityCheckClearedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(events);

            // Act
            var actual = cell.GetAvailabilityCheckMessageOrNull();

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void GetAvailabilityCheckMessageOrNull___Should_return_message___When_last_event_in_AvailabilityCheckEvents_is_CellAvailabilityCheckDeterminedCellDisabledEvent()
        {
            // Arrange
            var cellAvailabilityCheckDeterminedCellDisabledEvent = A.Dummy<CellAvailabilityCheckDeterminedCellDisabledEvent>();

            var events = new CellAvailabilityCheckEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>())
                .Concat(new[]
                {
                    cellAvailabilityCheckDeterminedCellDisabledEvent,
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(events);

            // Act
            var actual = cell.GetAvailabilityCheckMessageOrNull();

            // Assert
            actual.AsTest().Must().BeEqualTo(cellAvailabilityCheckDeterminedCellDisabledEvent.Message);
        }

        [Fact]
        public static void GetAvailabilityCheckMessageOrNull___Should_return_message___When_last_event_in_AvailabilityCheckEvents_is_CellAvailabilityCheckDeterminedCellEnabledEvent()
        {
            // Arrange
            var cellAvailabilityCheckDeterminedCellEnabledEvent = A.Dummy<CellAvailabilityCheckDeterminedCellEnabledEvent>();

            var events = new CellAvailabilityCheckEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>())
                .Concat(new[]
                {
                    cellAvailabilityCheckDeterminedCellEnabledEvent,
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(events);

            // Act
            var actual = cell.GetAvailabilityCheckMessageOrNull();

            // Assert
            actual.AsTest().Must().BeEqualTo(cellAvailabilityCheckDeterminedCellEnabledEvent.Message);
        }

        [Fact]
        public static void GetAvailabilityCheckMessageOrNull___Should_return_null___When_last_event_in_AvailabilityCheckEvents_is_CellAvailabilityCheckFailedEvent()
        {
            // Arrange
            var events = new CellAvailabilityCheckEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellAvailabilityCheckFailedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(events);

            // Act
            var actual = cell.GetAvailabilityCheckMessageOrNull();

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void GetAvailability___Should_throw_ArgumentNullException___When_parameter_cell_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => CellExtensions.GetAvailability(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetAvailability___Should_return_DefaultAvailability___When_AvailabilityCheck_is_null()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(null).DeepCloneWithAvailabilityCheck(null);

            // Act
            var actual = cell.GetAvailability();

            // Assert
            actual.AsTest().Must().BeEqualTo(cell.DefaultAvailability);
        }

        [Fact]
        public static void GetAvailability___Should_return_DefaultAvailability___When_AvailabilityCheckEvents_is_null()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(null);

            // Act
            var actual = cell.GetAvailability();

            // Assert
            actual.AsTest().Must().BeEqualTo(cell.DefaultAvailability);
        }

        [Fact]
        public static void GetAvailability___Should_return_DefaultAvailability___When_AvailabilityCheckEvents_is_empty()
        {
            // Arrange
            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(new CellAvailabilityCheckEventBase[0]);

            // Act
            var actual = cell.GetAvailability();

            // Assert
            actual.AsTest().Must().BeEqualTo(cell.DefaultAvailability);
        }

        [Fact]
        public static void GetAvailability___Should_return_DefaultAvailability___When_last_event_in_AvailabilityCheckEvents_is_CellAvailabilityCheckClearedEvent()
        {
            // Arrange
            var events = new CellAvailabilityCheckEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellAvailabilityCheckClearedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(events);

            // Act
            var actual = cell.GetAvailability();

            // Assert
            actual.AsTest().Must().BeEqualTo(cell.DefaultAvailability);
        }

        [Fact]
        public static void GetAvailability___Should_return_Availability_Disabled___When_last_event_in_AvailabilityCheckEvents_is_CellAvailabilityCheckDeterminedCellDisabledEvent()
        {
            // Arrange
            var events = new CellAvailabilityCheckEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellAvailabilityCheckDeterminedCellDisabledEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(events);

            // Act
            var actual = cell.GetAvailability();

            // Assert
            actual.AsTest().Must().BeEqualTo(Availability.Disabled);
        }

        [Fact]
        public static void GetAvailability___Should_return_Availability_Enabled___When_last_event_in_AvailabilityCheckEvents_is_CellAvailabilityCheckDeterminedCellEnabledEvent()
        {
            // Arrange
            var events = new CellAvailabilityCheckEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellAvailabilityCheckDeterminedCellEnabledEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(events);

            // Act
            var actual = cell.GetAvailability();

            // Assert
            actual.AsTest().Must().BeEqualTo(Availability.Enabled);
        }

        [Fact]
        public static void GetAvailability___Should_return_Availability_Unknown___When_last_event_in_AvailabilityCheckEvents_is_CellAvailabilityCheckFailedEvent()
        {
            // Arrange
            var events = new CellAvailabilityCheckEventBase[0]
                .Concat(Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>())
                .Concat(new[]
                {
                    A.Dummy<CellAvailabilityCheckFailedEvent>(),
                })
                .ToList();

            var cell = A.Dummy<NotSlottedCellBase>().DeepCloneWithAvailabilityCheckEvents(events);

            // Act
            var actual = cell.GetAvailability();

            // Assert
            actual.AsTest().Must().BeEqualTo(Availability.Unknown);
        }
    }
}
