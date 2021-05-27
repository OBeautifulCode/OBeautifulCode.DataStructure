// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableFormatTest.cs" company="OBeautifulCode">
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
    public static partial class TableFormatTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static TableFormatTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TableFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'outerBorders' is an empty enumerable scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<TableFormat>();

                            var result = new TableFormat(
                                                 referenceObject.CellsFormat,
                                                 new List<OuterBorder>(),
                                                 referenceObject.InnerBorders);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "outerBorders", "is an empty enumerable", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TableFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'outerBorders' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<TableFormat>();

                            var result = new TableFormat(
                                                 referenceObject.CellsFormat,
                                                 new OuterBorder[0].Concat(referenceObject.OuterBorders).Concat(new OuterBorder[] { null }).Concat(referenceObject.OuterBorders).ToList(),
                                                 referenceObject.InnerBorders);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "outerBorders", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TableFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'innerBorders' is an empty enumerable scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<TableFormat>();

                            var result = new TableFormat(
                                                 referenceObject.CellsFormat,
                                                 referenceObject.OuterBorders,
                                                 new List<InnerBorder>());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "innerBorders", "is an empty enumerable", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<TableFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'innerBorders' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<TableFormat>();

                            var result = new TableFormat(
                                                 referenceObject.CellsFormat,
                                                 referenceObject.OuterBorders,
                                                 new InnerBorder[0].Concat(referenceObject.InnerBorders).Concat(new InnerBorder[] { null }).Concat(referenceObject.InnerBorders).ToList());

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "innerBorders", "contains at least one null element", },
                    });
        }
    }
}