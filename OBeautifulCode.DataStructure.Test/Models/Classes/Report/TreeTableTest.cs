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
                                new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(2).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                                new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(3).ToList()),
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
                                new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(2).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                                new Row(
                                    Some.ReadOnlyDummies<NotSlottedCellBase>(2).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList(),
                                    childRows: new[]
                                    {
                                        new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(2).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                                        new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(3)),
                                        new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(2).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                                    }),
                                new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(2).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
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
                        Name = "constructor should throw ArgumentException when parameter 'footerRows' contains a footer row that does not span all of the columns in tableColumns",
                        ConstructionFunc = () =>
                        {
                            var tableColumns = new TableColumns(Some.ReadOnlyDummies<Column>(2).ToList());

                            var rows = new[]
                            {
                                new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(2).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                                new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(3).ToList()),
                            };

                            var footerRows = new FooterRows(rows, null);

                            var tableRows = new TableRows(footerRows: footerRows);

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
                                new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                                new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(1).Select(_ => _.DeepCloneWithColumnsSpanned(3)).ToList()),
                                new FlatRow(
                                    new[]
                                    {
                                        A.Dummy<NotSlottedCellBase>().DeepCloneWithColumnsSpanned(3),
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
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TreeTable>
                    {
                        Name = "constructor should throw ArgumentException when two columns have the same identifier",
                        ConstructionFunc = () =>
                        {
                            var tableColumns = new TableColumns(new[]
                            {
                                new Column("column-1"),
                                new Column("column-2"),
                                new Column("column-1"),
                            });

                            var headerRows = new HeaderRows(
                                new[]
                                {
                                    new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                                    new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                                },
                                null);

                            var dataRows = new DataRows(
                                new[]
                                {
                                    new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                                    new Row(
                                        Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList(),
                                        childRows:
                                            new[]
                                            {
                                                new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                                            }),
                                });

                            var tableRows = new TableRows(headerRows, dataRows);

                            var result = new TreeTable(
                                tableColumns,
                                tableRows);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "Two or more elements (i.e. columns, rows, cells) have the same identifier.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TreeTable>
                    {
                        Name = "constructor should throw ArgumentException when two rows have the same identifier",
                        ConstructionFunc = () =>
                        {
                            var tableColumns = new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList());

                            var headerRows = new HeaderRows(
                                new[]
                                {
                                    new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList(), "row-1"),
                                    new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList(), "row-2"),
                                },
                                null);

                            var dataRows = new DataRows(
                                new[]
                                {
                                    new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList(), "row-3"),
                                    new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList(), childRows:
                                        new[]
                                        {
                                            new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList(), "row-1"),
                                        }),
                                });

                            var tableRows = new TableRows(headerRows, dataRows);

                            var result = new TreeTable(
                                tableColumns,
                                tableRows);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "Two or more elements (i.e. columns, rows, cells) have the same identifier.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TreeTable>
                    {
                        Name = "constructor should throw ArgumentException when two cells have the same identifier",
                        ConstructionFunc = () =>
                        {
                            var tableColumns = new TableColumns(Some.ReadOnlyDummies<Column>(3).ToList());

                            var headerRows = new HeaderRows(
                                new[]
                                {
                                    new FlatRow(
                                        new ICell[]
                                        {
                                            A.Dummy<NotSlottedCellBase>().DeepCloneWithId(null),
                                            A.Dummy<NotSlottedCellBase>().DeepCloneWithId("cell-1"),
                                            A.Dummy<NotSlottedCellBase>().DeepCloneWithId(null),
                                        }),
                                    new FlatRow(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                                },
                                null);

                            var dataRows = new DataRows(
                                new[]
                                {
                                    new Row(Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList()),
                                    new Row(
                                        Some.ReadOnlyDummies<NotSlottedCellBase>(3).Select(_ => _.DeepCloneWithColumnsSpanned(1)).ToList(),
                                        childRows:
                                            new[]
                                            {
                                                new Row(
                                                    new ICell[]
                                                    {
                                                        A.Dummy<NotSlottedCellBase>().DeepCloneWithId(null),
                                                        A.Dummy<NotSlottedCellBase>().DeepCloneWithId(null),
                                                        A.Dummy<NotSlottedCellBase>().DeepCloneWithId("cell-1"),
                                                    }),
                                            }),
                                });

                            var tableRows = new TableRows(headerRows, dataRows);

                            var result = new TreeTable(
                                tableColumns,
                                tableRows);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "Two or more elements (i.e. columns, rows, cells) have the same identifier.", },
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

        [Fact]
        public static void GetCellIdToCellMap___Should_return_map_of_Cell_Id_to_Cell___When_called()
        {
            // Arrange
            var tableColumns = new TableColumns(Some.ReadOnlyDummies<Column>(2).ToList());

            var cell1 = new MediaReferenceCell(A.Dummy<MediaReference>(), "id-1");
            var cell2 = new ColumnSpanningDecimalCell(A.Dummy<decimal>(), 2, "id-2");
            var cell3 = new DecimalCell(A.Dummy<decimal>(), null, "id-3");
            var cell4 = A.Dummy<IHaveValueCell>().ThatIs(_ => !(_ is IColumnSpanningCell));
            var cell5 = A.Dummy<IHaveValueCell>().ThatIs(_ => !(_ is IColumnSpanningCell));
            var cell6 = A.Dummy<IHaveValueCell>().ThatIs(_ => !(_ is IColumnSpanningCell));
            var cell7 = A.Dummy<IHaveValueCell>().ThatIs(_ => !(_ is IColumnSpanningCell));
            var cell8 = new SlottedCell(
                new Dictionary<string, IHaveValueCell>
                {
                    { "slot-1-id", new NullCell() },
                    { "slot-2-id", cell6 },
                    { "slot-3-id", cell7 },
                },
                "slot-2-id",
                A.Dummy<string>());

            var cell9 = A.Dummy<IHaveValueCell>().ThatIs(_ => !(_ is IColumnSpanningCell));
            var cell10 = new SlottedCell(
                new Dictionary<string, IHaveValueCell>
                {
                    { "slot-1-id", new NullCell() },
                    { "slot-2-id", new NullCell() },
                    { "slot-3-id", cell9 },
                },
                "slot-2-id");

            IReadOnlyDictionary<string, ICell> expected = new Dictionary<string, ICell>
            {
                { cell1.Id, cell1 },
                { cell2.Id, cell2 },
                { cell3.Id, cell3 },
                { cell4.Id, cell4 },
                { cell5.Id, cell5 },
                { cell6.Id, cell6 },
                { cell7.Id, cell7 },
                { cell8.Id, cell8 },
                { cell9.Id, cell9 },
            };

            var headerRows = new HeaderRows(
                new[]
                {
                    new FlatRow(
                        new ICell[]
                        {
                            cell2,
                        }),
                    new FlatRow(Some.ReadOnlyDummies<StringCell>(2).Select(_ => _.DeepCloneWithId(null)).ToList()),
                    new FlatRow(
                        new ICell[]
                        {
                            new NullCell(),
                            cell1,
                        }),
                },
                null);

            var dataRows = new DataRows(
                new[]
                {
                    new Row(
                        new[]
                        {
                            cell3,
                            cell4,
                        }),
                    new Row(
                        Some.ReadOnlyDummies<NullCell>(2).Select(_ => _.DeepCloneWithId(null)).ToList(),
                        childRows:
                        new[]
                        {
                            new Row(
                                new ICell[]
                                {
                                    cell5,
                                    cell8,
                                },
                                childRows:
                                new[]
                                {
                                    new Row(
                                        new ICell[]
                                        {
                                            cell10,
                                            new NullCell(),
                                        }),
                                }),
                        }),
                });

            var tableRows = new TableRows(headerRows, dataRows);

            var systemUnderTest = new TreeTable(
                tableColumns,
                tableRows);

            // Act
            var actual = systemUnderTest.GetCellIdToCellMap();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }
    }
}