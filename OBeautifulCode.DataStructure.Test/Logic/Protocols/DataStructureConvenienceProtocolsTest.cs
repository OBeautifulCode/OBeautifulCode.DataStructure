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
            var actual = Record.Exception(() => new DataStructureConvenienceProtocols<int>(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_IfThenElseOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((IfThenElseOp<int>)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_IfThenElseOp___Should_execute_Statement_and_return_result___When_executing_Condition_returns_true()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IfThenElse(Op.Const(true), Op.Const(1), Op.Const(2));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(1);
        }

        [Fact]
        public static void Execute_IfThenElseOp___Should_execute_ElseStatement_and_return_result___When_executing_Condition_returns_false()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IfThenElse(Op.Const(false), Op.Const(1), Op.Const(2));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(2);
        }

        [Fact]
        public static async Task ExecuteAsync_IfThenElseOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync((IfThenElseOp<int>)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static async Task ExecuteAsync_IfThenElseOp___Should_execute_Statement_and_return_result___When_executing_Condition_returns_true()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IfThenElse(Op.Const(true), Op.Const(1), Op.Const(2));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(1);
        }

        [Fact]
        public static async Task ExecuteAsync_IfThenElseOp___Should_execute_ElseStatement_and_return_result___When_executing_Condition_returns_false()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IfThenElse(Op.Const(false), Op.Const(1), Op.Const(2));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(2);
        }

        [Fact]
        public static void Execute_IsEqualToOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((IsEqualToOp<int>)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_IsEqualToOp___Should_return_false___When_the_result_of_executing_Statement1_is_not_equal_to_the_result_of_executing_Statement2()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsEqualTo(Op.Const(1), Op.Const(2));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void Execute_IsEqualToOp___Should_return_true___When_the_result_of_executing_Statement1_is_equal_to_the_result_of_executing_Statement2()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsEqualTo(Op.Const(2), Op.Const(2));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static async Task ExecuteAsync_IsEqualToOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync((IsEqualToOp<int>)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static async Task ExecuteAsync_IsEqualToOp___Should_return_false___When_the_result_of_executing_Statement1_is_not_equal_to_the_result_of_executing_Statement2()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsEqualTo(Op.Const(1), Op.Const(2));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static async Task ExecuteAsync_IsEqualToOp___Should_return_true___When_the_result_of_executing_Statement1_is_equal_to_the_result_of_executing_Statement2()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsEqualTo(Op.Const(2), Op.Const(2));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static void Execute_AndAlsoOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((AndAlsoOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_AndAlsoOp___Should_stop_executing_Statements_and_return_false___When_executing_Statements_in_order_and_one_returns_false()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.AndAlso(Op.Const(true), Op.Const(true), Op.Const(false), Op.Abort<bool>());

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void Execute_AndAlsoOp__Should_return_true___When_executing_Statements_and_none_return_false()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.AndAlso(Op.Const(true), Op.Const(true), Op.Const(true));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static async Task ExecuteAsync_AndAlsoOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync((AndAlsoOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static async Task ExecuteAsync_AndAlsoOp___Should_stop_executing_Statements_and_return_false___When_executing_Statements_in_order_and_one_returns_false()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.AndAlso(Op.Const(true), Op.Const(true), Op.Const(false), Op.Abort<bool>());

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static async Task ExecuteAsync_AndAlsoOp__Should_return_true___When_executing_Statements_and_none_return_false()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.AndAlso(Op.Const(true), Op.Const(true), Op.Const(true));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static void Execute_OrElseOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((OrElseOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_OrElseOp___Should_stop_executing_Statements_and_return_true___When_executing_Statements_in_order_and_one_returns_true()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.OrElse(Op.Const(false), Op.Const(false), Op.Const(true), Op.Abort<bool>());

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static void Execute_OrElseOp__Should_return_false___When_executing_Statements_and_none_return_true()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.OrElse(Op.Const(false), Op.Const(false), Op.Const(false));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static async Task ExecuteAsync_OrElseOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync((OrElseOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static async Task ExecuteAsync_OrElseOp___Should_stop_executing_Statements_and_return_true___When_executing_Statements_in_order_and_one_returns_true()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.OrElse(Op.Const(false), Op.Const(false), Op.Const(true), Op.Abort<bool>());

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static async Task ExecuteAsync_OrElseOp__Should_return_true___When_executing_Statements_and_none_return_true()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.OrElse(Op.Const(false), Op.Const(false), Op.Const(false));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void Execute_NotOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((NotOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_NotOp___Should_return_true___When_the_result_of_executing_Statement_is_false()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.Not(Op.Const(false));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static void Execute_NotOp___Should_return_false___When_the_result_of_executing_Statement_is_true()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.Not(Op.Const(true));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static async Task ExecuteAsync_NotOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync((NotOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static async Task ExecuteAsync_NotOp___Should_return_true___When_the_result_of_executing_Statement_is_false()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.Not(Op.Const(false));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static async Task ExecuteAsync_NotOp___Should_return_false___When_the_result_of_executing_Statement_is_true()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.Not(Op.Const(true));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void Execute_SumOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((SumOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_SumOp___Should_return_sum_of_the_results_of_executing_Statements___When_called()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.Sum(Op.Const(1m), Op.Const(2m), Op.Const(3m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(6m);
        }

        [Fact]
        public static async Task ExecuteAsync_SumOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync((SumOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static async Task ExecuteAsync_SumOp___Should_return_sum_of_the_results_of_executing_Statements___When_called()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.Sum(Op.Const(1m), Op.Const(2m), Op.Const(3m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(6m);
        }

        [Fact]
        public static void Execute_CompareOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((CompareOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_CompareOp___Should_return_false___When_executing_CompareOperator_returns_GreaterThan_and_the_result_of_executing_Left_is_less_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsGreaterThan(Op.Const(1m), Op.Const(2m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void Execute_CompareOp___Should_return_false___When_executing_CompareOperator_returns_GreaterThan_and_the_result_of_executing_Left_is_equal_to_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsGreaterThan(Op.Const(1m), Op.Const(1m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void Execute_CompareOp___Should_return_true___When_executing_CompareOperator_returns_GreaterThan_and_the_result_of_executing_Left_is_greater_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsGreaterThan(Op.Const(2m), Op.Const(1m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static void Execute_CompareOp___Should_return_false___When_executing_CompareOperator_returns_GreaterThanOrEqualTo_and_the_result_of_executing_Left_is_less_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsGreaterThanOrEqualTo(Op.Const(1m), Op.Const(2m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void Execute_CompareOp___Should_return_true___When_executing_CompareOperator_returns_GreaterThanOrEqualTo_and_the_result_of_executing_Left_is_equal_to_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsGreaterThanOrEqualTo(Op.Const(1m), Op.Const(1m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static void Execute_CompareOp___Should_return_true___When_executing_CompareOperator_returns_GreaterThanOrEqualTo_and_the_result_of_executing_Left_is_greater_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsGreaterThanOrEqualTo(Op.Const(2m), Op.Const(1m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static void Execute_CompareOp___Should_return_true___When_executing_CompareOperator_returns_LessThan_and_the_result_of_executing_Left_is_less_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsLessThan(Op.Const(1m), Op.Const(2m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static void Execute_CompareOp___Should_return_false___When_executing_CompareOperator_returns_LessThan_and_the_result_of_executing_Left_is_equal_to_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsLessThan(Op.Const(1m), Op.Const(1m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void Execute_CompareOp___Should_return_false___When_executing_CompareOperator_returns_LessThan_and_the_result_of_executing_Left_is_greater_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsLessThan(Op.Const(2m), Op.Const(1m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void Execute_CompareOp___Should_return_true___When_executing_CompareOperator_returns_LessThanOrEqualTo_and_the_result_of_executing_Left_is_less_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsLessThanOrEqualTo(Op.Const(1m), Op.Const(2m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static void Execute_CompareOp___Should_return_true___When_executing_CompareOperator_returns_LessThanOrEqualTo_and_the_result_of_executing_Left_is_equal_to_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsLessThanOrEqualTo(Op.Const(1m), Op.Const(1m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static void Execute_CompareOp___Should_return_false___When_executing_CompareOperator_returns_LessThanOrEqualTo_and_the_result_of_executing_Left_is_greater_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsLessThanOrEqualTo(Op.Const(2m), Op.Const(1m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync((CompareOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_return_false___When_executing_CompareOperator_returns_GreaterThan_and_the_result_of_executing_Left_is_less_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsGreaterThan(Op.Const(1m), Op.Const(2m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_return_false___When_executing_CompareOperator_returns_GreaterThan_and_the_result_of_executing_Left_is_equal_to_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsGreaterThan(Op.Const(1m), Op.Const(1m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_return_true___When_executing_CompareOperator_returns_GreaterThan_and_the_result_of_executing_Left_is_greater_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsGreaterThan(Op.Const(2m), Op.Const(1m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_return_false___When_executing_CompareOperator_returns_GreaterThanOrEqualTo_and_the_result_of_executing_Left_is_less_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsGreaterThanOrEqualTo(Op.Const(1m), Op.Const(2m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_return_true___When_executing_CompareOperator_returns_GreaterThanOrEqualTo_and_the_result_of_executing_Left_is_equal_to_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsGreaterThanOrEqualTo(Op.Const(1m), Op.Const(1m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_return_true___When_executing_CompareOperator_returns_GreaterThanOrEqualTo_and_the_result_of_executing_Left_is_greater_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsGreaterThanOrEqualTo(Op.Const(2m), Op.Const(1m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_return_true___When_executing_CompareOperator_returns_LessThan_and_the_result_of_executing_Left_is_less_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsLessThan(Op.Const(1m), Op.Const(2m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_return_false___When_executing_CompareOperator_returns_LessThan_and_the_result_of_executing_Left_is_equal_to_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsLessThan(Op.Const(1m), Op.Const(1m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_return_false___When_executing_CompareOperator_returns_LessThan_and_the_result_of_executing_Left_is_greater_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsLessThan(Op.Const(2m), Op.Const(1m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_return_true___When_executing_CompareOperator_returns_LessThanOrEqualTo_and_the_result_of_executing_Left_is_less_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsLessThanOrEqualTo(Op.Const(1m), Op.Const(2m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_return_true___When_executing_CompareOperator_returns_LessThanOrEqualTo_and_the_result_of_executing_Left_is_equal_to_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsLessThanOrEqualTo(Op.Const(1m), Op.Const(1m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static async Task ExecuteAsync_CompareOp___Should_return_false___When_executing_CompareOperator_returns_LessThanOrEqualTo_and_the_result_of_executing_Left_is_greater_than_the_result_of_executing_Right()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.IsLessThanOrEqualTo(Op.Const(2m), Op.Const(1m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void Execute_GetNumberOfSignificantDigitsOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((GetNumberOfSignificantDigitsOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_GetNumberOfSignificantDigitsOp___Should_return_number_of_significant_digits_of_the_result_of_executing_Statement___When_called()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.GetNumberOfSignificantDigits(Op.Const(1.12340m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(4);
        }

        [Fact]
        public static async Task ExecuteAsync_GetNumberOfSignificantDigitsOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync((GetNumberOfSignificantDigitsOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static async Task ExecuteAsync_GetNumberOfSignificantDigitsOp___Should_return_number_of_significant_digits_of_the_result_of_executing_Statement___When_called()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.GetNumberOfSignificantDigits(Op.Const(1.12340m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(4);
        }

        [Fact]
        public static void Execute_ValidateOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((ValidateOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopAsInvalid),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopAsInvalid, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopAsValid),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopAsValid, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopToAbort),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopToAbort, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopAsNotApplicable),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopAsNotApplicable, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var validity = A.Dummy<Validity>();

            var operation = Op.Validate(
                new[]
                {
                    new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                    new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                    new SimpleBooleanValidationStep(Op.Const(A.Dummy<bool>()), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                },
                Op.Const(endMessage),
                validity);

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopAsInvalid),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopAsInvalid, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopAsValid),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopAsValid, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopToAbort),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopToAbort, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopAsNotApplicable),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopAsNotApplicable, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var validity = A.Dummy<Validity>();

            var operation = Op.Validate(
                new[]
                {
                    new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                    new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                    new MessageByOpBooleanValidationStep(Op.Const(A.Dummy<bool>()), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                },
                Op.Const(endMessage),
                validity);

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopAsInvalid),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopAsInvalid, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopAsValid),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopAsValid, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopToAbort),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopToAbort, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopAsNotApplicable),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopAsNotApplicable, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var validity = A.Dummy<Validity>();

            var operation = Op.Validate(
                new[]
                {
                    new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                    new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                    new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(A.Dummy<bool>(), A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                },
                Op.Const(endMessage),
                validity);

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync((ValidateOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateOp___Should_stop_executing_Steps_and_return_ValidationResult_where_ValidityOp_is_ConstOp_with_Value_Invalid_and_MessageOp_is_StopMessageOp___When_executing_SimpleBooleanValidationStep_operation_returns_false_and_FalseAction_is_StopAsInvalid()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopAsInvalid, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopAsValid),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopAsValid, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopToAbort),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopToAbort, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(false), stopMessage, ValidationStepAction.NextStep, ValidationStepAction.StopAsNotApplicable),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Const(true), stopMessage, ValidationStepAction.StopAsNotApplicable, ValidationStepAction.NextStep),
                new SimpleBooleanValidationStep(Op.Abort<bool>(), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var validity = A.Dummy<Validity>();

            var operation = Op.Validate(
                new[]
                {
                    new SimpleBooleanValidationStep(Op.Const(true), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                    new SimpleBooleanValidationStep(Op.Const(false), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                    new SimpleBooleanValidationStep(Op.Const(A.Dummy<bool>()), A.Dummy<string>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                },
                Op.Const(endMessage),
                validity);

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopAsInvalid),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopAsInvalid, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopAsValid),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopAsValid, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopToAbort),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopToAbort, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(stopMessage), ValidationStepAction.NextStep, ValidationStepAction.StopAsNotApplicable),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(stopMessage), ValidationStepAction.StopAsNotApplicable, ValidationStepAction.NextStep),
                new MessageByOpBooleanValidationStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var validity = A.Dummy<Validity>();

            var operation = Op.Validate(
                new[]
                {
                    new MessageByOpBooleanValidationStep(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                    new MessageByOpBooleanValidationStep(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                    new MessageByOpBooleanValidationStep(Op.Const(A.Dummy<bool>()), Op.Const(A.Dummy<string>()), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                },
                Op.Const(endMessage),
                validity);

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopAsInvalid),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopAsInvalid, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopAsValid),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopAsValid, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopToAbort),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopToAbort, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, stopMessage)), ValidationStepAction.NextStep, ValidationStepAction.StopAsNotApplicable),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.Validate(new[]
            {
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, stopMessage)), ValidationStepAction.StopAsNotApplicable, ValidationStepAction.NextStep),
                new MessageContainedBooleanValidationStep(Op.Abort<ValidationBoolWithMessage>(), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var validity = A.Dummy<Validity>();

            var operation = Op.Validate(
                new[]
                {
                    new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(true, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                    new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(false, A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                    new MessageContainedBooleanValidationStep(Op.Const(new ValidationBoolWithMessage(A.Dummy<bool>(), A.Dummy<string>())), ValidationStepAction.NextStep, ValidationStepAction.NextStep),
                },
                Op.Const(endMessage),
                validity);

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((CheckAvailabilityOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_CheckAvailabilityOp___Should_stop_executing_Steps_and_return_AvailabilityCheckResult_where_AvailabilityOp_is_ConstOp_with_Value_Disabled_and_MessageOp_is_StopMessageOp___When_executing_AvailabilityCheckStep_operation_returns_false_and_FalseAction_is_StopAsDisabled()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.CheckAvailability(new[]
            {
                new AvailabilityCheckStep(Op.Const(true), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Const(false), Op.Const(stopMessage), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.StopAsDisabled),
                new AvailabilityCheckStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.CheckAvailability(new[]
            {
                new AvailabilityCheckStep(Op.Const(false), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Const(true), Op.Const(stopMessage), AvailabilityCheckStepAction.StopAsDisabled, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.CheckAvailability(new[]
            {
                new AvailabilityCheckStep(Op.Const(true), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Const(false), Op.Const(stopMessage), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.StopAsEnabled),
                new AvailabilityCheckStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var stopMessage = A.Dummy<string>();

            var operation = Op.CheckAvailability(new[]
            {
                new AvailabilityCheckStep(Op.Const(false), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Const(true), Op.Const(stopMessage), AvailabilityCheckStepAction.StopAsEnabled, AvailabilityCheckStepAction.NextStep),
                new AvailabilityCheckStep(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
            });

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
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var endMessage = A.Dummy<string>();

            var availability = A.Dummy<Availability>();

            var operation = Op.CheckAvailability(
                new[]
                {
                    new AvailabilityCheckStep(Op.Const(true), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                    new AvailabilityCheckStep(Op.Const(false), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                    new AvailabilityCheckStep(Op.Const(A.Dummy<bool>()), Op.Const(A.Dummy<string>()), AvailabilityCheckStepAction.NextStep, AvailabilityCheckStepAction.NextStep),
                },
                Op.Const(endMessage),
                availability);

            var expected = new AvailabilityCheckResult(Op.Const(availability), Op.Const(endMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_DivideOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((DivideOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_DivideOp___Should_throw_DivideByZeroException___When_executing_Numerator_returns_0()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.Divide(Op.Const(1m), Op.Const(0m));

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute(operation));

            // Assert
            actual.AsTest().Must().BeOfType<DivideByZeroException>();
        }

        [Fact]
        public static void Execute_DivideOp___Should_execute_Numerator_and_divide_the_result_by_the_result_of_executing_Denominator___When_called()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.Divide(Op.Const(1m), Op.Const(2m));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(.5m);
        }

        [Fact]
        public static async Task ExecuteAsync_DivideOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync((DivideOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static async Task ExecuteAsync_DivideOp___Should_throw_DivideByZeroException___When_executing_Numerator_returns_0()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.Divide(Op.Const(1m), Op.Const(0m));

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync(operation));

            // Assert
            actual.AsTest().Must().BeOfType<DivideByZeroException>();
        }

        [Fact]
        public static async Task ExecuteAsync_DivideOp___Should_execute_Numerator_and_divide_the_result_by_the_result_of_executing_Denominator___When_called()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.Divide(Op.Const(1m), Op.Const(2m));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(.5m);
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
