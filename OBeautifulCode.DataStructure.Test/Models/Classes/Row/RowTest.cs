// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowTest.cs" company="OBeautifulCode">
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
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class RowTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static RowTest()
        {
            ConstructorArgumentValidationTestScenarios
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Row>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'id' is white space scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Row>();

                            var result = new Row(
                                referenceObject.Cells,
                                Invariant($"  {Environment.NewLine}  "),
                                referenceObject.Format,
                                referenceObject.ChildRows,
                                referenceObject.ExpandedSummaryRows,
                                referenceObject.CollapsedSummaryRows);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "id", "white space", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Row>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'expandedSummaryRows' contains rows, but childRows is null.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Row>();

                            var result = new Row(
                                referenceObject.Cells,
                                referenceObject.Id,
                                referenceObject.Format,
                                null,
                                new[] { A.Dummy<FlatRow>() },
                                null);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "expandedSummaryRows is specified when there are no rows in childRows", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Row>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'expandedSummaryRows' contains rows, but childRows is empty.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Row>();

                            var result = new Row(
                                referenceObject.Cells,
                                referenceObject.Id,
                                referenceObject.Format,
                                new Row[0],
                                new[] { A.Dummy<FlatRow>() },
                                null);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "expandedSummaryRows is specified when there are no rows in childRows", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Row>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'collapsedSummaryRows' contains rows, but childRows is null.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Row>();

                            var result = new Row(
                                referenceObject.Cells,
                                referenceObject.Id,
                                referenceObject.Format,
                                null,
                                null,
                                new[] { A.Dummy<FlatRow>() });

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "collapsedSummaryRows is specified when there are no rows in childRows", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Row>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'collapsedSummaryRow' is not null, but childRows is empty.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Row>();

                            var result = new Row(
                                referenceObject.Cells,
                                referenceObject.Id,
                                referenceObject.Format,
                                new Row[0],
                                null,
                                new[] { A.Dummy<FlatRow>() });

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "collapsedSummaryRows is specified when there are no rows in childRows", },
                    });

            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Row>
                    {
                        Name = "DeepCloneWithId should deep clone object and replace Id with the provided id",
                        WithPropertyName = "Id",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Row>();

                            var referenceObject = A.Dummy<Row>().ThatIs(_ => !systemUnderTest.Id.IsEqualTo(_.Id));

                            var result = new SystemUnderTestDeepCloneWithValue<Row>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Id,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Row>
                    {
                        Name = "DeepCloneWithCells should deep clone object and replace Cells with the provided cells",
                        WithPropertyName = "Cells",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Row>();

                            var referenceObject = A.Dummy<Row>().ThatIs(_ => !systemUnderTest.Cells.IsEqualTo(_.Cells));

                            var result = new SystemUnderTestDeepCloneWithValue<Row>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Cells,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Row>
                    {
                        Name = "DeepCloneWithFormat should deep clone object and replace Format with the provided format",
                        WithPropertyName = "Format",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Row>();

                            var referenceObject = A.Dummy<Row>().ThatIs(_ => !systemUnderTest.Format.IsEqualTo(_.Format));

                            var result = new SystemUnderTestDeepCloneWithValue<Row>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Format,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Row>
                    {
                        Name = "DeepCloneWithChildRows should deep clone object and replace ChildRows with the provided childRows",
                        WithPropertyName = "ChildRows",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Row>().Whose(_ => _.ChildRows.Any());

                            var referenceObject = A.Dummy<Row>().Whose(_ => _.ChildRows.Any() && (!_.ChildRows.IsEqualTo(systemUnderTest.ChildRows)));

                            var result = new SystemUnderTestDeepCloneWithValue<Row>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.ChildRows,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Row>
                    {
                        Name = "DeepCloneWithExpandedSummaryRows should deep clone object and replace ExpandedSummaryRows with the provided expandedSummaryRows",
                        WithPropertyName = "ExpandedSummaryRows",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Row>().Whose(_ => _.ChildRows.Any());

                            var referenceObject = A.Dummy<Row>().Whose(_ => _.ChildRows.Any() && (!_.ExpandedSummaryRows.IsEqualTo(systemUnderTest.ExpandedSummaryRows)));

                            var result = new SystemUnderTestDeepCloneWithValue<Row>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.ExpandedSummaryRows,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Row>
                    {
                        Name = "DeepCloneWithCollapsedSummaryRows should deep clone object and replace CollapsedSummaryRows with the provided collapsedSummaryRows",
                        WithPropertyName = "CollapsedSummaryRows",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Row>().Whose(_ => _.ChildRows.Any());

                            var referenceObject = A.Dummy<Row>().Whose(_ => _.ChildRows.Any() && (!_.CollapsedSummaryRows.IsEqualTo(systemUnderTest.CollapsedSummaryRows)));

                            var result = new SystemUnderTestDeepCloneWithValue<Row>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.CollapsedSummaryRows,
                            };

                            return result;
                        },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                {
                    var referenceObject = A.Dummy<Row>().Whose(_ => _.ChildRows.Any());

                    var result = new EquatableTestScenario<Row>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = referenceObject,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new Row[]
                        {
                            new Row(
                                    referenceObject.Cells,
                                    referenceObject.Id,
                                    referenceObject.Format,
                                    referenceObject.ChildRows,
                                    referenceObject.ExpandedSummaryRows,
                                    referenceObject.CollapsedSummaryRows),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new Row[]
                        {
                            new Row(
                                    referenceObject.Cells,
                                    A.Dummy<Row>().Whose(_ => !_.Id.IsEqualTo(referenceObject.Id)).Id,
                                    referenceObject.Format,
                                    referenceObject.ChildRows,
                                    referenceObject.ExpandedSummaryRows,
                                    referenceObject.CollapsedSummaryRows),
                            new Row(
                                    A.Dummy<Row>().Whose(_ => !_.Cells.IsEqualTo(referenceObject.Cells)).Cells,
                                    referenceObject.Id,
                                    referenceObject.Format,
                                    referenceObject.ChildRows,
                                    referenceObject.ExpandedSummaryRows,
                                    referenceObject.CollapsedSummaryRows),
                            new Row(
                                    referenceObject.Cells,
                                    referenceObject.Id,
                                    A.Dummy<Row>().Whose(_ => !_.Format.IsEqualTo(referenceObject.Format)).Format,
                                    referenceObject.ChildRows,
                                    referenceObject.ExpandedSummaryRows,
                                    referenceObject.CollapsedSummaryRows),
                            new Row(
                                    referenceObject.Cells,
                                    referenceObject.Id,
                                    referenceObject.Format,
                                    A.Dummy<Row>().Whose(_ => (!_.ChildRows.IsEqualTo(referenceObject.ChildRows)) && _.ChildRows.Any()).ChildRows,
                                    referenceObject.ExpandedSummaryRows,
                                    referenceObject.CollapsedSummaryRows),
                            new Row(
                                    referenceObject.Cells,
                                    referenceObject.Id,
                                    referenceObject.Format,
                                    referenceObject.ChildRows,
                                    A.Dummy<Row>().Whose(_ => (!_.ExpandedSummaryRows.IsEqualTo(referenceObject.ExpandedSummaryRows)) && _.ChildRows.Any()).ExpandedSummaryRows,
                                    referenceObject.CollapsedSummaryRows),
                            new Row(
                                    referenceObject.Cells,
                                    referenceObject.Id,
                                    referenceObject.Format,
                                    referenceObject.ChildRows,
                                    referenceObject.ExpandedSummaryRows,
                                    A.Dummy<Row>().Whose(_ => (!_.CollapsedSummaryRows.IsEqualTo(ReferenceObjectForEquatableTestScenarios.CollapsedSummaryRows)) && _.ChildRows.Any()).CollapsedSummaryRows),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new object[]
                        {
                            A.Dummy<object>(),
                            A.Dummy<string>(),
                            A.Dummy<int>(),
                            A.Dummy<int?>(),
                            A.Dummy<Guid>(),
                            A.Dummy<FlatRow>(),
                        },
                    };

                    return result;
                });
        }
    }
}