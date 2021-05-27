// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableColumnsTest.cs" company="OBeautifulCode">
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
    public static partial class TableColumnsTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static TableColumnsTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TableColumns>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'columns' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<TableColumns>();

                            var result = new TableColumns(
                                                 null,
                                                 referenceObject.ColumnsFormat);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "columns", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TableColumns>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'columns' is an empty enumerable scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<TableColumns>();

                            var result = new TableColumns(
                                                 new List<Column>(),
                                                 referenceObject.ColumnsFormat);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "columns", "is an empty enumerable", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TableColumns>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'columns' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<TableColumns>();

                            var result = new TableColumns(
                                                 new Column[0].Concat(referenceObject.Columns).Concat(new Column[] { null }).Concat(referenceObject.Columns).ToList(),
                                                 referenceObject.ColumnsFormat);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "columns", "contains at least one null element", },
                    });
        }
    }
}