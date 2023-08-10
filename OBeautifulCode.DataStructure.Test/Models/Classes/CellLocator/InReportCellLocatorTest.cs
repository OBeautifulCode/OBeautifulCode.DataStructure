// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InReportCellLocatorTest.cs" company="OBeautifulCode">
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
    public static partial class InReportCellLocatorTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static InReportCellLocatorTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InReportCellLocator>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'sectionId' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InReportCellLocator>();

                            var result = new InReportCellLocator(
                                                 null,
                                                 referenceObject.CellId,
                                                 referenceObject.SlotId,
                                                 referenceObject.SlotSelectionStrategy);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "sectionId", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InReportCellLocator>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'sectionId' is white space scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InReportCellLocator>();

                            var result = new InReportCellLocator(
                                                 Invariant($"  {Environment.NewLine}  "),
                                                 referenceObject.CellId,
                                                 referenceObject.SlotId,
                                                 referenceObject.SlotSelectionStrategy);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "sectionId", "white space", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InReportCellLocator>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'cellId' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InReportCellLocator>();

                            var result = new InReportCellLocator(
                                                 referenceObject.SectionId,
                                                 null,
                                                 referenceObject.SlotId,
                                                 referenceObject.SlotSelectionStrategy);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "cellId", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InReportCellLocator>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'cellId' is white space scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InReportCellLocator>();

                            var result = new InReportCellLocator(
                                                 referenceObject.SectionId,
                                                 Invariant($"  {Environment.NewLine}  "),
                                                 referenceObject.SlotId,
                                                 referenceObject.SlotSelectionStrategy);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "cellId", "white space", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<InReportCellLocator>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'slotSelectionStrategy' is SlotSelectionStrategy.Unknown",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<InReportCellLocator>();

                            var result = new InReportCellLocator(
                                                 referenceObject.SectionId,
                                                 referenceObject.CellId,
                                                 referenceObject.SlotId,
                                                 SlotSelectionStrategy.Unknown);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "slotSelectionStrategy", "Unknown", },
                    });
        }
    }
}