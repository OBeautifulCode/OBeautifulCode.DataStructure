// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SectionCellLocatorTest.cs" company="OBeautifulCode">
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
    public static partial class SectionCellLocatorTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static SectionCellLocatorTest()
        {
            ConstructorArgumentValidationTestScenarios
               .RemoveAllScenarios()
               .AddScenario(() =>
                   new ConstructorArgumentValidationTestScenario<SectionCellLocator>
                   {
                       Name = "constructor should throw ArgumentNullException when parameter 'cellId' is null scenario",
                       ConstructionFunc = () =>
                       {
                           var referenceObject = A.Dummy<SectionCellLocator>();

                           var result = new SectionCellLocator(
                                                null,
                                                referenceObject.SlotId,
                                                referenceObject.SlotSelectionStrategy);

                           return result;
                       },
                       ExpectedExceptionType = typeof(ArgumentNullException),
                       ExpectedExceptionMessageContains = new[] { "cellId", },
                   })
               .AddScenario(() =>
                   new ConstructorArgumentValidationTestScenario<SectionCellLocator>
                   {
                       Name = "constructor should throw ArgumentException when parameter 'cellId' is white space scenario",
                       ConstructionFunc = () =>
                       {
                           var referenceObject = A.Dummy<SectionCellLocator>();

                           var result = new SectionCellLocator(
                                                Invariant($"  {Environment.NewLine}  "),
                                                referenceObject.SlotId,
                                                referenceObject.SlotSelectionStrategy);

                           return result;
                       },
                       ExpectedExceptionType = typeof(ArgumentException),
                       ExpectedExceptionMessageContains = new[] { "cellId", "white space", },
                   })
               .AddScenario(() =>
                   new ConstructorArgumentValidationTestScenario<SectionCellLocator>
                   {
                       Name = "constructor should throw ArgumentOutOfRangeException when parameter 'slotSelectionStrategy' is SlotSelectionStrategy.Unknown",
                       ConstructionFunc = () =>
                       {
                           var referenceObject = A.Dummy<SectionCellLocator>();

                           var result = new SectionCellLocator(
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