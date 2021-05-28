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
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Row>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'cells' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Row>();

                            var result = new Row(
                                                 null,
                                                 referenceObject.Id,
                                                 referenceObject.Format,
                                                 referenceObject.ChildRows,
                                                 referenceObject.ExpandedSummaryRow,
                                                 referenceObject.CollapsedSummaryRow);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "cells", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Row>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'cells' is an empty enumerable scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Row>();

                            var result = new Row(
                                                 new List<ICell>(),
                                                 referenceObject.Id,
                                                 referenceObject.Format,
                                                 referenceObject.ChildRows,
                                                 referenceObject.ExpandedSummaryRow,
                                                 referenceObject.CollapsedSummaryRow);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "cells", "is an empty enumerable", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Row>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'cells' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Row>();

                            var result = new Row(
                                                 new ICell[0].Concat(referenceObject.Cells).Concat(new ICell[] { null }).Concat(referenceObject.Cells).ToList(),
                                                 referenceObject.Id,
                                                 referenceObject.Format,
                                                 referenceObject.ChildRows,
                                                 referenceObject.ExpandedSummaryRow,
                                                 referenceObject.CollapsedSummaryRow);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "cells", "contains at least one null element", },
                    })
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
                                referenceObject.ExpandedSummaryRow,
                                referenceObject.CollapsedSummaryRow);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "id", "white space", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Row>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'childRows' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Row>();

                            var result = new Row(
                                referenceObject.Cells,
                                referenceObject.Id,
                                referenceObject.Format,
                                new Row[0].Concat(referenceObject.ChildRows).Concat(new Row[] { null }).Concat(referenceObject.ChildRows).ToList(),
                                referenceObject.ExpandedSummaryRow,
                                referenceObject.CollapsedSummaryRow);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "childRows", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Row>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'expandedSummaryRow' is not null, but childRows is null.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Row>();

                            var result = new Row(
                                referenceObject.Cells,
                                referenceObject.Id,
                                referenceObject.Format,
                                null,
                                A.Dummy<FlatRow>(),
                                null);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "expandedSummaryRow is specified when there are no rows in childRows", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Row>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'expandedSummaryRow' is not null, but childRows is empty.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Row>();

                            var result = new Row(
                                referenceObject.Cells,
                                referenceObject.Id,
                                referenceObject.Format,
                                new Row[0],
                                A.Dummy<FlatRow>(),
                                null);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "expandedSummaryRow is specified when there are no rows in childRows", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<Row>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'collapsedSummaryRow' is not null, but childRows is null.",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<Row>();

                            var result = new Row(
                                referenceObject.Cells,
                                referenceObject.Id,
                                referenceObject.Format,
                                null,
                                null,
                                A.Dummy<FlatRow>());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "collapsedSummaryRow is specified when there are no rows in childRows", },
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
                                A.Dummy<FlatRow>());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "collapsedSummaryRow is specified when there are no rows in childRows", },
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
                        Name = "DeepCloneWithExpandedSummaryRow should deep clone object and replace ExpandedSummaryRow with the provided expandedSummaryRow",
                        WithPropertyName = "ExpandedSummaryRow",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Row>().Whose(_ => _.ChildRows.Any());

                            var referenceObject = A.Dummy<Row>().Whose(_ => _.ChildRows.Any() && (!_.ExpandedSummaryRow.IsEqualTo(systemUnderTest.ExpandedSummaryRow)));

                            var result = new SystemUnderTestDeepCloneWithValue<Row>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.ExpandedSummaryRow,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Row>
                    {
                        Name = "DeepCloneWithCollapsedSummaryRow should deep clone object and replace CollapsedSummaryRow with the provided collapsedSummaryRow",
                        WithPropertyName = "CollapsedSummaryRow",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Row>().Whose(_ => _.ChildRows.Any());

                            var referenceObject = A.Dummy<Row>().Whose(_ => _.ChildRows.Any() && (!_.CollapsedSummaryRow.IsEqualTo(systemUnderTest.CollapsedSummaryRow)));

                            var result = new SystemUnderTestDeepCloneWithValue<Row>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.CollapsedSummaryRow,
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
                                    referenceObject.ExpandedSummaryRow,
                                    referenceObject.CollapsedSummaryRow),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new Row[]
                        {
                            new Row(
                                    referenceObject.Cells,
                                    A.Dummy<Row>().Whose(_ => !_.Id.IsEqualTo(referenceObject.Id)).Id,
                                    referenceObject.Format,
                                    referenceObject.ChildRows,
                                    referenceObject.ExpandedSummaryRow,
                                    referenceObject.CollapsedSummaryRow),
                            new Row(
                                    A.Dummy<Row>().Whose(_ => !_.Cells.IsEqualTo(referenceObject.Cells)).Cells,
                                    referenceObject.Id,
                                    referenceObject.Format,
                                    referenceObject.ChildRows,
                                    referenceObject.ExpandedSummaryRow,
                                    referenceObject.CollapsedSummaryRow),
                            new Row(
                                    referenceObject.Cells,
                                    referenceObject.Id,
                                    A.Dummy<Row>().Whose(_ => !_.Format.IsEqualTo(referenceObject.Format)).Format,
                                    referenceObject.ChildRows,
                                    referenceObject.ExpandedSummaryRow,
                                    referenceObject.CollapsedSummaryRow),
                            new Row(
                                    referenceObject.Cells,
                                    referenceObject.Id,
                                    referenceObject.Format,
                                    A.Dummy<Row>().Whose(_ => (!_.ChildRows.IsEqualTo(referenceObject.ChildRows)) && _.ChildRows.Any()).ChildRows,
                                    referenceObject.ExpandedSummaryRow,
                                    referenceObject.CollapsedSummaryRow),
                            new Row(
                                    referenceObject.Cells,
                                    referenceObject.Id,
                                    referenceObject.Format,
                                    referenceObject.ChildRows,
                                    A.Dummy<Row>().Whose(_ => (!_.ExpandedSummaryRow.IsEqualTo(referenceObject.ExpandedSummaryRow)) && _.ChildRows.Any()).ExpandedSummaryRow,
                                    referenceObject.CollapsedSummaryRow),
                            new Row(
                                    referenceObject.Cells,
                                    referenceObject.Id,
                                    referenceObject.Format,
                                    referenceObject.ChildRows,
                                    referenceObject.ExpandedSummaryRow,
                                    A.Dummy<Row>().Whose(_ => (!_.CollapsedSummaryRow.IsEqualTo(ReferenceObjectForEquatableTestScenarios.CollapsedSummaryRow)) && _.ChildRows.Any()).CollapsedSummaryRow),
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