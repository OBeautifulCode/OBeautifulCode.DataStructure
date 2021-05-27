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
    }
}