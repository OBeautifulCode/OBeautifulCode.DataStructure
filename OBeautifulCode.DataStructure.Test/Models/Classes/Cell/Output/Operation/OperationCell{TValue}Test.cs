// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationCell{TValue}Test.cs" company="OBeautifulCode">
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
    public static partial class OperationCellTValueTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static OperationCellTValueTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<OperationCell<Version>>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'operation' is null",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<OperationCell<Version>>();

                            var result = new OperationCell<Version>(
                                null,
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                referenceObject.OperationExecutionEvents,
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "operation", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<OperationCell<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is 0",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<OperationCell<Version>>();

                            var result = new OperationCell<Version>(
                                                 referenceObject.Operation,
                                                 referenceObject.Id,
                                                 0,
                                                 referenceObject.Details,
                                                 referenceObject.Validation,
                                                 referenceObject.ValidationEvents,
                                                 referenceObject.DefaultAvailability,
                                                 referenceObject.AvailabilityCheck,
                                                 referenceObject.AvailabilityCheckEvents,
                                                 referenceObject.OperationExecutionEvents,
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
                    new ConstructorArgumentValidationTestScenario<OperationCell<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is -1",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<OperationCell<Version>>();

                            var result = new OperationCell<Version>(
                                referenceObject.Operation,
                                referenceObject.Id,
                                -1,
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                referenceObject.OperationExecutionEvents,
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
                    new ConstructorArgumentValidationTestScenario<OperationCell<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is negative number",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<OperationCell<Version>>();

                            var result = new OperationCell<Version>(
                                referenceObject.Operation,
                                referenceObject.Id,
                                A.Dummy<NegativeInteger>(),
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                referenceObject.OperationExecutionEvents,
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
                    new ConstructorArgumentValidationTestScenario<OperationCell<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'validationEvents' contains a null element",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<OperationCell<Version>>();

                            var result = new OperationCell<Version>(
                                referenceObject.Operation,
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details,
                                referenceObject.Validation,
                                new[] { A.Dummy<CellValidationEventBase>(), null, A.Dummy<CellValidationEventBase>() },
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                referenceObject.OperationExecutionEvents,
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "validationEvents contains a null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<OperationCell<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'validation' is is null but parameter 'validationEvents' is not null nor empty.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<OperationCell<Version>>();

                            var result = new OperationCell<Version>(
                                referenceObject.Operation,
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details,
                                null,
                                Some.ReadOnlyDummies<CellValidationEventBase>().ToList(),
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                referenceObject.OperationExecutionEvents,
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
                    new ConstructorArgumentValidationTestScenario<OperationCell<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'defaultAvailability' is neither Availability.Enabled nor Availabilty.Disabled",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<OperationCell<Version>>();

                            var result = new OperationCell<Version>(
                                referenceObject.Operation,
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                Availability.Unknown,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                referenceObject.OperationExecutionEvents,
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "defaultAvailability is neither Availability.Enabled nor Availability.Disabled", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<OperationCell<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'availabilityCheckEvents' contains a null element",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<OperationCell<Version>>();

                            var result = new OperationCell<Version>(
                                referenceObject.Operation,
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                new[] { A.Dummy<CellAvailabilityCheckEventBase>(), null, A.Dummy<CellAvailabilityCheckEventBase>() },
                                referenceObject.OperationExecutionEvents,
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "availabilityCheckEvents contains a null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<OperationCell<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'availabilityCheck' is is null but parameter 'availabilityCheckEvents' is not null nor empty.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<OperationCell<Version>>();

                            var result = new OperationCell<Version>(
                                referenceObject.Operation,
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                null,
                                Some.ReadOnlyDummies<CellAvailabilityCheckEventBase>().ToList(),
                                referenceObject.OperationExecutionEvents,
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "There is no availabilityCheck specified, however one or more availabilityCheckEvents exists", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<OperationCell<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'operationExecutionEvents' contains a null element",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<OperationCell<Version>>();

                            var result = new OperationCell<Version>(
                                referenceObject.Operation,
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details,
                                referenceObject.Validation,
                                referenceObject.ValidationEvents,
                                referenceObject.DefaultAvailability,
                                referenceObject.AvailabilityCheck,
                                referenceObject.AvailabilityCheckEvents,
                                new[] { A.Dummy<CellOpExecutionEventBase>(), null, A.Dummy<CellOpExecutionEventBase>() },
                                referenceObject.ValueFormat,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "operationExecutionEvents contains a null element", },
                    });
        }
    }
}