// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataRowsTest.cs" company="OBeautifulCode">
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
    public static partial class DataRowsTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static DataRowsTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<DataRows>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'rows' is null",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<DataRows>();

                            var result = new DataRows(
                                null,
                                referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "rows", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<DataRows>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'rows' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<DataRows>();

                            var result = new DataRows(
                                                 new Row[0].Concat(referenceObject.Rows).Concat(new Row[] { null }).Concat(referenceObject.Rows).ToList(),
                                                 referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "rows", "contains at least one null element", },
                    });
        }
    }
}