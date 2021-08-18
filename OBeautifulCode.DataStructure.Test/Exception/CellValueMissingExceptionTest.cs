// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellValueMissingExceptionTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using FakeItEasy;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.CodeAnalysis.Recipes;

    using Xunit;

    using static System.FormattableString;

    public static class CellValueMissingExceptionTest
    {
        [Fact]
        public static void CellLocator___Should_return_same_cellLocator_passed_to_constructor___When_getting()
        {
            // Arrange
            var expected = A.Dummy<CellLocatorBase>();

            var systemUnderTest1 = new CellValueMissingException(expected);
            var systemUnderTest2 = new CellValueMissingException(A.Dummy<string>(), expected);
            var systemUnderTest3 = new CellValueMissingException(A.Dummy<string>(), A.Dummy<Exception>(), expected);

            // Act
            var actual1 = systemUnderTest1.CellLocator;
            var actual2 = systemUnderTest2.CellLocator;
            var actual3 = systemUnderTest3.CellLocator;

            // Assert
            actual1.AsTest().Must().BeSameReferenceAs(expected);
            actual2.AsTest().Must().BeSameReferenceAs(expected);
            actual3.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void Message___Should_return_same_message_passed_to_constructor___When_getting()
        {
            // Arrange
            var expected = A.Dummy<string>();

            var systemUnderTest1 = new CellValueMissingException(expected, A.Dummy<CellLocatorBase>());
            var systemUnderTest2 = new CellValueMissingException(expected, A.Dummy<Exception>(), A.Dummy<CellLocatorBase>());

            // Act
            var actual1 = systemUnderTest1.Message;
            var actual2 = systemUnderTest2.Message;

            // Assert
            actual1.AsTest().Must().BeEqualTo(expected);
            actual2.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void InnerException___Should_return_same_innerException_passed_to_constructor___When_getting()
        {
            // Arrange
            var expected = A.Dummy<Exception>();

            var systemUnderTest = new CellValueMissingException(A.Dummy<string>(), expected, A.Dummy<CellLocatorBase>());

            // Act
            var actual = systemUnderTest.InnerException;

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void ToString___Should_return_string_representation_of_exception___When_cellLocator_is_null()
        {
            // Arrange
            var systemUnderTest = new CellValueMissingException("my-message", new Exception("my-inner-exception"), null);

            var expected = Invariant($"OBeautifulCode.DataStructure.CellValueMissingException: my-message ---> System.Exception: my-inner-exception{Environment.NewLine}   --- End of inner exception stack trace ---");

            // Act
            var actual = systemUnderTest.ToString();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = ObcSuppressBecause.CA2201_DoNotRaiseReservedExceptionTypes_UsedForUnitTesting)]
        public static void ToString___Should_return_string_representation_of_exception___When_cellLocator_is_not_null()
        {
            // Arrange
            var cellLocator = new SectionCellLocator("cell-id");

            var systemUnderTest = new CellValueMissingException("my-message", new Exception("my-inner-exception"), cellLocator);

            var expected = Invariant($"The cell that was found using the following locator does not yet have a value: OBeautifulCode.DataStructure.SectionCellLocator: CellId = cell-id, SlotId = <null>, SlotSelectionStrategy = ThrowIfSlotIdNotSpecified.{Environment.NewLine}OBeautifulCode.DataStructure.CellValueMissingException: my-message ---> System.Exception: my-inner-exception{Environment.NewLine}   --- End of inner exception stack trace ---");

            // Act
            var actual = systemUnderTest.ToString();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }
    }
}
