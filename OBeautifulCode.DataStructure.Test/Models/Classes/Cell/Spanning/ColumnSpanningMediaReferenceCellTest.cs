// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnSpanningMediaReferenceCellTest.cs" company="OBeautifulCode">
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
    public static partial class ColumnSpanningMediaReferenceCellTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static ColumnSpanningMediaReferenceCellTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ColumnSpanningMediaReferenceCell>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'mediaReference' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ColumnSpanningMediaReferenceCell>();

                            var result = new ColumnSpanningMediaReferenceCell(
                                                 null,
                                                 referenceObject.ColumnsSpanned,
                                                 referenceObject.Id,
                                                 referenceObject.Format,
                                                 referenceObject.HoverOver,
                                                 referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "mediaReference", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ColumnSpanningMediaReferenceCell>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is 1",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ColumnSpanningMediaReferenceCell>();

                            var result = new ColumnSpanningMediaReferenceCell(
                                referenceObject.MediaReference,
                                1,
                                referenceObject.Id,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned is 1; must be >= 2", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ColumnSpanningMediaReferenceCell>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is 0",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ColumnSpanningMediaReferenceCell>();

                            var result = new ColumnSpanningMediaReferenceCell(
                                referenceObject.MediaReference,
                                0,
                                referenceObject.Id,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned is 0; must be >= 2", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<ColumnSpanningMediaReferenceCell>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is < 0",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<ColumnSpanningMediaReferenceCell>();

                            var result = new ColumnSpanningMediaReferenceCell(
                                referenceObject.MediaReference,
                                A.Dummy<NegativeInteger>(),
                                referenceObject.Id,
                                referenceObject.Format,
                                referenceObject.HoverOver,
                                referenceObject.Link);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned is", "must be >= 2", },
                    });
        }
    }
}