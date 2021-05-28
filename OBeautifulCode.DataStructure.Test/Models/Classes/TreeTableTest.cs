// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeTableTest.cs" company="OBeautifulCode">
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
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class TreeTableTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static TreeTableTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TreeTable>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'tableColumns' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<TreeTable>();

                            var result = new TreeTable(
                                                 null,
                                                 referenceObject.TableRows,
                                                 referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "tableColumns", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TreeTable>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'tableRows' contains a header row that does not span all of the columns in tableColumns",
                        ConstructionFunc = () =>
                        {
                            var tableColumns = new TableColumns(Some.ReadOnlyDummies<Column>(2).ToList());

                            var rows = new[]
                            {
                                new FlatRow(Some.ReadOnlyDummies<DecimalCell>(2).ToList()),
                                new FlatRow(Some.ReadOnlyDummies<StringCell>(3).ToList()),
                                new FlatRow(Some.ReadOnlyDummies<HtmlCell>(2).ToList()),
                            };

                            var headerRows = new HeaderRows(rows, null);

                            var tableRows = new TableRows(headerRows, null);

                            var result = new TreeTable(
                                tableColumns,
                                tableRows);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "tableRows contains a row or descendant row that does not span all 2 of the defined columns", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TreeTable>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'tableRows' contains a data row that does not span all of the columns in tableColumns",
                        ConstructionFunc = () =>
                        {
                            var tableColumns = new TableColumns(Some.ReadOnlyDummies<Column>(2).ToList());

                            var rows = new[]
                            {
                                new Row(Some.ReadOnlyDummies<DecimalCell>(2).ToList()),
                                new Row(
                                    Some.ReadOnlyDummies<HtmlCell>(2).ToList(),
                                    childRows: new[]
                                    {
                                        new Row(Some.ReadOnlyDummies<MediaReferenceCell>(2).ToList()),
                                        new Row(Some.ReadOnlyDummies<StringCell>(3).ToList()),
                                        new Row(Some.ReadOnlyDummies<SlottedCell>(2).ToList()),
                                    }),
                                new Row(Some.ReadOnlyDummies<DecimalCell>(2).ToList()),
                            };

                            var dataRows = new DataRows(rows, null);

                            var tableRows = new TableRows(null, dataRows);

                            var result = new TreeTable(
                                tableColumns,
                                tableRows);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "tableRows contains a row or descendant row that does not span all 2 of the defined columns", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TreeTable>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'tableRows' contains header rows where the last header row does not contain one cell for all defined columns",
                        ConstructionFunc = () =>
                        {
                            var tableColumns = new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList());

                            var rows = new[]
                            {
                                new FlatRow(Some.ReadOnlyDummies<MediaReferenceCell>(3).ToList()),
                                new FlatRow(Some.ReadOnlyDummies<StringCell>(3).ToList()),
                                new FlatRow(
                                    new[]
                                    {
                                        new ColumnSpanningDecimalCell(A.Dummy<decimal>(), 3),
                                    }),
                            };

                            var headerRows = new HeaderRows(rows, null);

                            var tableRows = new TableRows(headerRows, null);

                            var result = new TreeTable(
                                tableColumns,
                                tableRows);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "The last row in tableRows.HeaderRows does not contain one cell for all 3 of the defined columns.  Spanning is disallowed for the last header row.", },
                    });

            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<TreeTable>
                    {
                        Name = "DeepCloneWithTableColumns should deep clone object and replace TableColumns with the provided tableColumns",
                        WithPropertyName = "TableColumns",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<TreeTable>();

                            var referenceObject = A.Dummy<TreeTable>().ThatIs(_ => !systemUnderTest.TableColumns.IsEqualTo(_.TableColumns) && (_.TableColumns.Columns.Count == systemUnderTest.TableColumns.Columns.Count));

                            var result = new SystemUnderTestDeepCloneWithValue<TreeTable>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.TableColumns,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<TreeTable>
                    {
                        Name = "DeepCloneWithTableRows should deep clone object and replace TableRows with the provided tableRows",
                        WithPropertyName = "TableRows",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<TreeTable>();

                            var referenceObject = A.Dummy<TreeTable>().ThatIs(_ =>
                                !systemUnderTest.TableRows.IsEqualTo(_.TableRows) &&
                                ((!_.TableRows.GetAllRowsInOrder().Any()) || (_.TableRows.GetAllRowsInOrder().First().GetNumberOfColumnsSpanned() == systemUnderTest.TableColumns.Columns.Count)));

                            var result = new SystemUnderTestDeepCloneWithValue<TreeTable>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.TableRows,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<TreeTable>
                    {
                        Name = "DeepCloneWithFormat should deep clone object and replace Format with the provided format",
                        WithPropertyName = "Format",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<TreeTable>();

                            var referenceObject = A.Dummy<TreeTable>().ThatIs(_ => !systemUnderTest.Format.IsEqualTo(_.Format));

                            var result = new SystemUnderTestDeepCloneWithValue<TreeTable>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Format,
                            };

                            return result;
                        },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new EquatableTestScenario<TreeTable>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = ReferenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new TreeTable[]
                        {
                            new TreeTable(
                                    ReferenceObjectForEquatableTestScenarios.TableColumns,
                                    ReferenceObjectForEquatableTestScenarios.TableRows,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new TreeTable[]
                        {
                            new TreeTable(
                                    A.Dummy<TreeTable>().Whose(_ => !_.TableColumns.IsEqualTo(ReferenceObjectForEquatableTestScenarios.TableColumns) && (_.TableColumns.Columns.Count == ReferenceObjectForEquatableTestScenarios.TableColumns.Columns.Count)).TableColumns,
                                    ReferenceObjectForEquatableTestScenarios.TableRows,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new TreeTable(
                                    ReferenceObjectForEquatableTestScenarios.TableColumns,
                                    A.Dummy<TreeTable>().Whose(_ => (!_.TableRows.IsEqualTo(ReferenceObjectForEquatableTestScenarios.TableRows) && ((!_.TableRows.GetAllRowsInOrder().Any()) || (_.TableRows.GetAllRowsInOrder().First().GetNumberOfColumnsSpanned() == ReferenceObjectForEquatableTestScenarios.TableColumns.Columns.Count)))).TableRows,
                                    ReferenceObjectForEquatableTestScenarios.Format),
                            new TreeTable(
                                    ReferenceObjectForEquatableTestScenarios.TableColumns,
                                    ReferenceObjectForEquatableTestScenarios.TableRows,
                                    A.Dummy<TreeTable>().Whose(_ => !_.Format.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Format)).Format),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new object[]
                        {
                            A.Dummy<object>(),
                            A.Dummy<string>(),
                            A.Dummy<int>(),
                            A.Dummy<int?>(),
                            A.Dummy<Guid>(),
                        },
                    });
        }

        [Fact]
        public static void Constructor___Does_not_throw___When_all_rows_span_all_columns()
        {
            // Arrange
            var tableColumns = new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList());

            var allHeaderRows = new[]
            {
                new FlatRow(
                    new ICell[]
                    {
                        new DecimalCell(A.Dummy<decimal>()),
                        new ColumnSpanningHtmlCell(A.Dummy<string>(), 2),
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        new ColumnSpanningStringCell(A.Dummy<string>(), 2),
                        new HtmlCell(A.Dummy<string>()),
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        new ColumnSpanningMediaReferenceCell(A.Dummy<MediaReference>(), 3),
                    }),
                new FlatRow(
                    new ICell[]
                    {
                        new DecimalCell(A.Dummy<decimal>()),
                        new StringCell(A.Dummy<string>()),
                        new MediaReferenceCell(A.Dummy<MediaReference>()),
                    }),
            };

            var headerRows = new HeaderRows(allHeaderRows);

            var allDataRows = new[]
            {
                new Row(Some.ReadOnlyDummies<DecimalCell>(3).ToList()),
                new Row(
                    Some.ReadOnlyDummies<HtmlCell>(3).ToList(),
                    childRows: new[]
                    {
                        new Row(Some.ReadOnlyDummies<MediaReferenceCell>(3).ToList()),
                        new Row(Some.ReadOnlyDummies<StringCell>(3).ToList()),
                        new Row(
                            new ICell[]
                            {
                                new DecimalCell(A.Dummy<decimal>()),
                                new ColumnSpanningHtmlCell(A.Dummy<string>(), 2),
                            }),
                    }),
                new Row(
                    Some.ReadOnlyDummies<DecimalCell>(3).ToList(),
                    childRows: new[]
                    {
                        new Row(Some.ReadOnlyDummies<StringCell>(3).ToList()),
                        new Row(
                            new ICell[]
                            {
                                new ColumnSpanningStringCell(A.Dummy<string>(), 2),
                                new HtmlCell(A.Dummy<string>()),
                            }),
                        new Row(
                            new ICell[]
                            {
                                new ColumnSpanningMediaReferenceCell(A.Dummy<MediaReference>(), 3),
                            }),
                    }),
            };

            var dataRows = new DataRows(allDataRows, null);

            var tableRows = new TableRows(headerRows, dataRows);

            // Act
            var actual = Record.Exception(() => new TreeTable(tableColumns, tableRows));

            // Assert
            actual.AsTest().Must().BeNull();
        }
    }
}