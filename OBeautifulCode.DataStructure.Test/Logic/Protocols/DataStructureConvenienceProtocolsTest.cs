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
                { typeof(ConstProtocol<int>), () => new ConstProtocol<int>() },
                { typeof(ConstProtocol<bool>), () => new ConstProtocol<bool>() },
                { typeof(ConstProtocol<decimal>), () => new ConstProtocol<decimal>() },
                { typeof(ConstProtocol<CompareOperator>), () => new ConstProtocol<CompareOperator>() },
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
        public static void Execute_ValidateUsingConditionsOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute((ValidateUsingConditionsOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Execute_ValidateUsingConditionsOp___Should_stop_executing_Conditions_and_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_executing_MessageOp_returns_the_result_of_executing_FailureMessageOp___When_executing_validation_conditions_operation_and_one_returns_false_and_ValidationConditionKind_is_PassWhenTrue()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var failureMessage = A.Dummy<string>();

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenTrue),
                new ValidationCondition(Op.Const(false), Op.Const(failureMessage), ValidationConditionKind.PassWhenTrue),
                new ValidationCondition(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenTrue),
            });

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(failureMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateUsingConditionsOp___Should_stop_executing_Conditions_and_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_executing_MessageOp_returns_the_result_of_executing_FailureMessageOp___When_executing_validation_conditions_operation_and_one_returns_false_and_ValidationConditionKind_is_FailWhenFalse()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var failureMessage = A.Dummy<string>();

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenFalse),
                new ValidationCondition(Op.Const(false), Op.Const(failureMessage), ValidationConditionKind.FailWhenFalse),
                new ValidationCondition(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenFalse),
            });

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(failureMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateUsingConditionsOp___Should_stop_executing_Conditions_and_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_executing_MessageOp_returns_the_result_of_executing_FailureMessageOp___When_executing_validation_conditions_operation_and_one_returns_true_and_ValidationConditionKind_is_PassWhenFalse()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var failureMessage = A.Dummy<string>();

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenFalse),
                new ValidationCondition(Op.Const(true), Op.Const(failureMessage), ValidationConditionKind.PassWhenFalse),
                new ValidationCondition(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenFalse),
            });

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(failureMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateUsingConditionsOp___Should_stop_executing_Conditions_and_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_executing_MessageOp_returns_the_result_of_executing_FailureMessageOp___When_executing_validation_conditions_operation_and_one_returns_true_and_ValidationConditionKind_is_FailWhenTrue()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var failureMessage = A.Dummy<string>();

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenTrue),
                new ValidationCondition(Op.Const(true), Op.Const(failureMessage), ValidationConditionKind.FailWhenTrue),
                new ValidationCondition(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenTrue),
            });

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(failureMessage));

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateUsingConditionsOp___Should_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_MessageOp_is_null___When_executing_all_validation_conditions_returns_true_for_each_and_ValidationConditionKind_is_PassWhenTrue()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenTrue),
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenTrue),
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenTrue),
            });

            var expected = new ValidationResult(Op.Const(Validity.Valid), null);

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateUsingConditionsOp___Should_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_MessageOp_is_null___When_executing_all_validation_conditions_returns_true_for_each_and_ValidationConditionKind_is_FailWhenFalse()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenFalse),
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenFalse),
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenFalse),
            });

            var expected = new ValidationResult(Op.Const(Validity.Valid), null);

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateUsingConditionsOp___Should_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_MessageOp_is_null___When_executing_all_validation_conditions_returns_false_for_each_and_ValidationConditionKind_is_PassWhenFalse()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenFalse),
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenFalse),
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenFalse),
            });

            var expected = new ValidationResult(Op.Const(Validity.Valid), null);

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void Execute_ValidateUsingConditionsOp___Should_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_MessageOp_is_null___When_executing_all_validation_conditions_returns_false_for_each_and_ValidationConditionKind_is_FailWhenTrue()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenTrue),
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenTrue),
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenTrue),
            });

            var expected = new ValidationResult(Op.Const(Validity.Valid), null);

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateUsingConditionsOp___Should_throw_ArgumentNullException___When_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync((ValidateUsingConditionsOp)null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateUsingConditionsOp___Should_stop_executing_Conditions_and_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_executing_MessageOp_returns_the_result_of_executing_FailureMessageOp___When_executing_validation_conditions_operation_and_one_returns_false_and_ValidationConditionKind_is_PassWhenTrue()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var failureMessage = A.Dummy<string>();

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenTrue),
                new ValidationCondition(Op.Const(false), Op.Const(failureMessage), ValidationConditionKind.PassWhenTrue),
                new ValidationCondition(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenTrue),
            });

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(failureMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateUsingConditionsOp___Should_stop_executing_Conditions_and_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_executing_MessageOp_returns_the_result_of_executing_FailureMessageOp___When_executing_validation_conditions_operation_and_one_returns_false_and_ValidationConditionKind_is_FailWhenFalse()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var failureMessage = A.Dummy<string>();

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenFalse),
                new ValidationCondition(Op.Const(false), Op.Const(failureMessage), ValidationConditionKind.FailWhenFalse),
                new ValidationCondition(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenFalse),
            });

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(failureMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateUsingConditionsOp___Should_stop_executing_Conditions_and_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_executing_MessageOp_returns_the_result_of_executing_FailureMessageOp___When_executing_validation_conditions_operation_and_one_returns_true_and_ValidationConditionKind_is_PassWhenFalse()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var failureMessage = A.Dummy<string>();

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenFalse),
                new ValidationCondition(Op.Const(true), Op.Const(failureMessage), ValidationConditionKind.PassWhenFalse),
                new ValidationCondition(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenFalse),
            });

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(failureMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateUsingConditionsOp___Should_stop_executing_Conditions_and_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_executing_MessageOp_returns_the_result_of_executing_FailureMessageOp___When_executing_validation_conditions_operation_and_one_returns_true_and_ValidationConditionKind_is_FailWhenTrue()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var failureMessage = A.Dummy<string>();

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenTrue),
                new ValidationCondition(Op.Const(true), Op.Const(failureMessage), ValidationConditionKind.FailWhenTrue),
                new ValidationCondition(Op.Abort<bool>(), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenTrue),
            });

            var expected = new ValidationResult(Op.Const(Validity.Invalid), Op.Const(failureMessage));

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateUsingConditionsOp___Should_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_MessageOp_is_null___When_executing_all_validation_conditions_returns_true_for_each_and_ValidationConditionKind_is_PassWhenTrue()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenTrue),
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenTrue),
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenTrue),
            });

            var expected = new ValidationResult(Op.Const(Validity.Valid), null);

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateUsingConditionsOp___Should_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_MessageOp_is_null___When_executing_all_validation_conditions_returns_true_for_each_and_ValidationConditionKind_is_FailWhenFalse()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenFalse),
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenFalse),
                new ValidationCondition(Op.Const(true), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenFalse),
            });

            var expected = new ValidationResult(Op.Const(Validity.Valid), null);

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateUsingConditionsOp___Should_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_MessageOp_is_null___When_executing_all_validation_conditions_returns_false_for_each_and_ValidationConditionKind_is_PassWhenFalse()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenFalse),
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenFalse),
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.PassWhenFalse),
            });

            var expected = new ValidationResult(Op.Const(Validity.Valid), null);

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static async Task ExecuteAsync_ValidateUsingConditionsOp___Should_return_ValidationResult_where_executing_ValidityOp_returns_Invalid_and_MessageOp_is_null___When_executing_all_validation_conditions_returns_false_for_each_and_ValidationConditionKind_is_FailWhenTrue()
        {
            // Arrange
            var systemUnderTest = new DataStructureConvenienceProtocols<int>(ProtocolFactory);

            var operation = Op.ValidateUsingConditions(new[]
            {
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenTrue),
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenTrue),
                new ValidationCondition(Op.Const(false), Op.Const(A.Dummy<string>()), ValidationConditionKind.FailWhenTrue),
            });

            var expected = new ValidationResult(Op.Const(Validity.Valid), null);

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        public class ConstProtocol<TValue> : SyncSpecificReturningProtocolBase<GetConstOp<TValue>, TValue>
        {
            public override TValue Execute(
                GetConstOp<TValue> operation)
            {
                if (operation == null)
                {
                    throw new ArgumentNullException(nameof(operation));
                }

                var result = operation.Value;

                return result;
            }
        }

        public class ThrowProtocol<TValue> : SyncSpecificReturningProtocolBase<ThrowOpExecutionAbortedExceptionOp<TValue>, TValue>
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
