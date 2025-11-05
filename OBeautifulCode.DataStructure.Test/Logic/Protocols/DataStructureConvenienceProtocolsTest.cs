// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureConvenienceProtocolsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FakeItEasy;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.CoreOperation;
    using OBeautifulCode.Type;

    using Xunit;

    public static class DataStructureConvenienceProtocolsTest
    {
        private static readonly IProtocolFactory ProtocolFactory = new ProtocolFactory(
            new Dictionary<Type, Func<IProtocol>>
            {
                { typeof(GetConstValueProtocol<int>), () => new GetConstValueProtocol<int>() },
                { typeof(GetConstValueProtocol<bool>), () => new GetConstValueProtocol<bool>() },
                { typeof(GetConstValueProtocol<decimal>), () => new GetConstValueProtocol<decimal>() },
                { typeof(GetConstValueProtocol<CompareOperator>), () => new GetConstValueProtocol<CompareOperator>() },
                { typeof(GetConstValueProtocol<ValidationBoolWithMessage>), () => new GetConstValueProtocol<ValidationBoolWithMessage>() },
                { typeof(ThrowProtocol<int>), () => new ThrowProtocol<int>() },
                { typeof(ThrowProtocol<bool>), () => new ThrowProtocol<bool>() },
                { typeof(ThrowProtocol<decimal>), () => new ThrowProtocol<decimal>() },
            });

        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_protocolFactory_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => new DataStructureConvenienceProtocols(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_ValidateOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((ValidateOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopAsInvalid),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopAsInvalid, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Valid_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsValid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopAsValid),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Valid), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Valid_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsValid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopAsValid, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Valid), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Aborted_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopToAbort()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopToAbort),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Aborted), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Aborted_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopToAbort()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopToAbort, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Aborted), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_NotApplicable_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsNotApplicable()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopAsNotApplicable),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.NotApplicable), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_NotApplicable_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsNotApplicable()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopAsNotApplicable, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.NotApplicable), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_EndValidity_and_MessageOp_is_EndMessageOp___When_all_SimpleBooleanValidationSteps_result_in_NextStep_actions()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var validity = A.Dummy<Validity>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(A.Dummy<bool>()), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate(Op.Const(endMessage), validity);

            var expected = new ValidationResult(Op.Const(validity), Op.Const(endMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopAsInvalid),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopAsInvalid, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Valid_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsValid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopAsValid),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Valid), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Valid_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsValid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopAsValid, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Valid), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Aborted_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopToAbort()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopToAbort),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Aborted), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Aborted_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopToAbort()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopToAbort, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Aborted), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_NotApplicable_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsNotApplicable()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopAsNotApplicable),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.NotApplicable), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_NotApplicable_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsNotApplicable()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopAsNotApplicable, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.NotApplicable), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_EndValidity_and_MessageOp_is_EndMessageOp___When_all_MessageByOpBooleanValidationSteps_result_in_NextStep_actions()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var validity = A.Dummy<Validity>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(A.Dummy<bool>()), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate(Op.Const(endMessage), validity);

            var expected = new ValidationResult(Op.Const(validity), Op.Const(endMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopAsInvalid),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopAsInvalid, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Valid_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsValid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopAsValid),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Valid), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Valid_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsValid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopAsValid, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Valid), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Aborted_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopToAbort()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopToAbort),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Aborted), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Aborted_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopToAbort()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopToAbort, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Aborted), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_NotApplicable_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsNotApplicable()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopAsNotApplicable),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.NotApplicable), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_NotApplicable_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsNotApplicable()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopAsNotApplicable, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.NotApplicable), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_EndValidity_and_MessageOp_is_EndMessageOp___When_all_MessageContainedBooleanValidationSteps_result_in_NextStep_actions()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var validity = A.Dummy<Validity>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(A.Dummy<bool>(), A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate(Op.Const(endMessage), validity);

            var expected = new ValidationResult(Op.Const(validity), Op.Const(endMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync((ValidateOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = StepExtensions.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopAsInvalid),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopAsInvalid, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Valid_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsValid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopAsValid),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Valid), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Valid_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsValid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopAsValid, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Valid), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Aborted_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopToAbort()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopToAbort),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Aborted), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Aborted_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopToAbort()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopToAbort, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Aborted), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_NotApplicable_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsNotApplicable()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopAsNotApplicable),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.NotApplicable), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_NotApplicable_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsNotApplicable()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopAsNotApplicable, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.NotApplicable), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_EndValidity_and_MessageOp_is_EndMessageOp___When_all_SimpleBooleanValidationSteps_result_in_NextStep_actions()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var validity = A.Dummy<Validity>();

            var operation = new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(A.Dummy<bool>()), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate(Op.Const(endMessage), validity);

            var expected = new ValidationResult(Op.Const(validity), Op.Const(endMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopAsInvalid),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopAsInvalid, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Valid_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsValid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopAsValid),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Valid), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Valid_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsValid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopAsValid, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Valid), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Aborted_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopToAbort()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopToAbort),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Aborted), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Aborted_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopToAbort()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopToAbort, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Aborted), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_NotApplicable_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsNotApplicable()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopAsNotApplicable),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.NotApplicable), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_NotApplicable_and_MessageOp_is_StopMessageOp___When_executing_MessageByOpBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsNotApplicable()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopAsNotApplicable, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.NotApplicable), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_EndValidity_and_MessageOp_is_EndMessageOp___When_all_MessageByOpBooleanValidationSteps_result_in_NextStep_actions()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var validity = A.Dummy<Validity>();

            var operation = new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(A.Dummy<bool>()), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate(Op.Const(endMessage), validity);

            var expected = new ValidationResult(Op.Const(validity), Op.Const(endMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopAsInvalid),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopAsInvalid, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Valid_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsValid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopAsValid),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Valid), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Valid_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsValid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopAsValid, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Valid), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Aborted_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopToAbort()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopToAbort),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Aborted), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Aborted_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopToAbort()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopToAbort, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.Aborted), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_NotApplicable_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsNotApplicable()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopAsNotApplicable),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.NotApplicable), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_NotApplicable_and_MessageOp_is_StopMessageOp___When_executing_MessageContainedBooleanValidationStep_operation_returns_true_and_TrueAction_is_StopAsNotApplicable()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopAsNotApplicable, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate();

            var expected = new ValidationResult(Op.Const(Validity.NotApplicable), Op.Const(stopMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_EndValidity_and_MessageOp_is_EndMessageOp___When_all_MessageContainedBooleanValidationSteps_result_in_NextStep_actions()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var validity = A.Dummy<Validity>();

            var operation = new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(A.Dummy<bool>(), A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            }.Validate(Op.Const(endMessage), validity);

            var expected = new ValidationResult(Op.Const(validity), Op.Const(endMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_CheckAvailabilityOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((CheckAvailabilityOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_CheckAvailabilityOp___Should_stop_executing_Steps_and_return_AvailabilityCheckResult_where_AvailabilityOp_is_ConstOp_with_Value_Disabled_and_MessageOp_is_StopMessageOp___When_executing_AvailabilityCheckStep_operation_returns_false_and_FalseAction_is_StopAsDisabled()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new AvailabilityCheckStep(Op.Const(true), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Const(false), Op.Const(stopMessage), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.StopAsDisabled),
                new AvailabilityCheckStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
            }.CheckAvailability();

            var expected = new AvailabilityCheckResult(Op.Const(Availability.Disabled), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_CheckAvailabilityOp___Should_stop_executing_Steps_and_return_AvailabilityCheckResult_where_AvailabilityOp_is_ConstOp_with_Value_Disabled_and_MessageOp_is_StopMessageOp___When_executing_AvailabilityCheckStep_operation_returns_true_and_TrueAction_is_StopAsDisabled()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new AvailabilityCheckStep(Op.Const(false), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Const(true), Op.Const(stopMessage), AvailabilityCheckStepAction.StopAsDisabled, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
            }.CheckAvailability();

            var expected = new AvailabilityCheckResult(Op.Const(Availability.Disabled), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_CheckAvailabilityOp___Should_stop_executing_Steps_and_return_AvailabilityCheckResult_where_AvailabilityOp_is_ConstOp_with_Value_Enabled_and_MessageOp_is_StopMessageOp___When_executing_AvailabilityCheckStep_operation_returns_false_and_FalseAction_is_StopAsEnabled()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new AvailabilityCheckStep(Op.Const(true), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Const(false), Op.Const(stopMessage), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.StopAsEnabled),
                new AvailabilityCheckStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
            }.CheckAvailability();

            var expected = new AvailabilityCheckResult(Op.Const(Availability.Enabled), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_CheckAvailabilityOp___Should_stop_executing_Steps_and_return_AvailabilityCheckResult_where_AvailabilityOp_is_ConstOp_with_Value_Enabled_and_MessageOp_is_StopMessageOp___When_executing_AvailabilityCheckStep_operation_returns_true_and_TrueAction_is_StopAsEnabled()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = new[]
            {
                new AvailabilityCheckStep(Op.Const(false), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Const(true), Op.Const(stopMessage), AvailabilityCheckStepAction.StopAsEnabled, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
            }.CheckAvailability();

            var expected = new AvailabilityCheckResult(Op.Const(Availability.Enabled), Op.Const(stopMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_CheckAvailabilityOp___Should_return_AvailabilityCheckResult_where_AvailabilityOp_is_ConstOp_with_Value_EndAvailability_and_MessageOp_is_EndMessageOp___When_all_AvailabilityCheckSteps_result_in_NextStep_actions()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var availability = A.Dummy<Availability>();

            var operation = new[]
            {
                new AvailabilityCheckStep(Op.Const(true), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Const(false), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Const(A.Dummy<bool>()), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
            }.CheckAvailability(Op.Const(endMessage), availability);

            var expected = new AvailabilityCheckResult(Op.Const(availability), Op.Const(endMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        private class ThrowProtocol<TValue> : SyncSpecificReturningProtocolBase<ThrowOpExecutionAbortedExceptionOp<TValue>, TValue>
        {
            public override TValue Execute(
                ThrowOpExecutionAbortedExceptionOp<TValue> operation)
            {
                if (operation == null)
                {
                    throw new ArgumentNullException(nameof(operation));
                }

                throw new OpExecutionAbortedException();
            }
        }
    }
}
