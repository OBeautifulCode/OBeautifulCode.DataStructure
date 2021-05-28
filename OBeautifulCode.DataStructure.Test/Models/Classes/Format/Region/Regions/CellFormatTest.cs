// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellFormatTest.cs" company="OBeautifulCode">
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
    public static partial class CellFormatTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CellFormatTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CellFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'fontNamesInFallbackOrder' is an empty enumerable scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CellFormat>();

                            var result = new CellFormat(
                                                 referenceObject.OuterBorders,
                                                 referenceObject.BackgroundColor,
                                                 referenceObject.FontColor,
                                                 new List<string>(),
                                                 referenceObject.FontSizeInPoints,
                                                 referenceObject.VerticalAlignment,
                                                 referenceObject.HorizontalAlignment,
                                                 referenceObject.FontRotationAngle,
                                                 referenceObject.FillPattern,
                                                 referenceObject.Options);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "fontNamesInFallbackOrder", "is an empty enumerable", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CellFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'fontNamesInFallbackOrder' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CellFormat>();

                            var result = new CellFormat(
                                                 referenceObject.OuterBorders,
                                                 referenceObject.BackgroundColor,
                                                 referenceObject.FontColor,
                                                 new string[0].Concat(referenceObject.FontNamesInFallbackOrder).Concat(new string[] { null }).Concat(referenceObject.FontNamesInFallbackOrder).ToList(),
                                                 referenceObject.FontSizeInPoints,
                                                 referenceObject.VerticalAlignment,
                                                 referenceObject.HorizontalAlignment,
                                                 referenceObject.FontRotationAngle,
                                                 referenceObject.FillPattern,
                                                 referenceObject.Options);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "fontNamesInFallbackOrder", "contains at least one null element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CellFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'fontNamesInFallbackOrder' contains a white space element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CellFormat>();

                            var result = new CellFormat(
                                referenceObject.OuterBorders,
                                referenceObject.BackgroundColor,
                                referenceObject.FontColor,
                                new string[0].Concat(referenceObject.FontNamesInFallbackOrder).Concat(new string[] { " \r\n " }).Concat(referenceObject.FontNamesInFallbackOrder).ToList(),
                                referenceObject.FontSizeInPoints,
                                referenceObject.VerticalAlignment,
                                referenceObject.HorizontalAlignment,
                                referenceObject.FontRotationAngle,
                                referenceObject.FillPattern,
                                referenceObject.Options);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "fontNamesInFallbackOrder", "contains a white space element", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CellFormat>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'verticalAlignment' is VerticalAlignment.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CellFormat>();

                            var result = new CellFormat(
                                referenceObject.OuterBorders,
                                referenceObject.BackgroundColor,
                                referenceObject.FontColor,
                                referenceObject.FontNamesInFallbackOrder,
                                referenceObject.FontSizeInPoints,
                                VerticalAlignment.Unknown,
                                referenceObject.HorizontalAlignment,
                                referenceObject.FontRotationAngle,
                                referenceObject.FillPattern,
                                referenceObject.Options);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "verticalAlignment", "Unknown", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CellFormat>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'horizontalAlignment' is HorizontalAlignment.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CellFormat>();

                            var result = new CellFormat(
                                referenceObject.OuterBorders,
                                referenceObject.BackgroundColor,
                                referenceObject.FontColor,
                                referenceObject.FontNamesInFallbackOrder,
                                referenceObject.FontSizeInPoints,
                                referenceObject.VerticalAlignment,
                                HorizontalAlignment.Unknown,
                                referenceObject.FontRotationAngle,
                                referenceObject.FillPattern,
                                referenceObject.Options);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "horizontalAlignment", "Unknown", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CellFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'outerBorders' is an empty enumerable scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CellFormat>();

                            var result = new CellFormat(
                                new List<OuterBorder>(),
                                referenceObject.BackgroundColor,
                                referenceObject.FontColor,
                                referenceObject.FontNamesInFallbackOrder,
                                referenceObject.FontSizeInPoints,
                                referenceObject.VerticalAlignment,
                                referenceObject.HorizontalAlignment,
                                referenceObject.FontRotationAngle,
                                referenceObject.FillPattern,
                                referenceObject.Options);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "outerBorders", "is an empty enumerable", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<CellFormat>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'outerBorders' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<CellFormat>();

                            var result = new CellFormat(
                                new OuterBorder[0].Concat(referenceObject.OuterBorders).Concat(new OuterBorder[] { null }).Concat(referenceObject.OuterBorders).ToList(),
                                referenceObject.BackgroundColor,
                                referenceObject.FontColor,
                                referenceObject.FontNamesInFallbackOrder,
                                referenceObject.FontSizeInPoints,
                                referenceObject.VerticalAlignment,
                                referenceObject.HorizontalAlignment,
                                referenceObject.FontRotationAngle,
                                referenceObject.FillPattern,
                                referenceObject.Options);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "outerBorders", "contains at least one null element", },
                    });
        }
    }
}