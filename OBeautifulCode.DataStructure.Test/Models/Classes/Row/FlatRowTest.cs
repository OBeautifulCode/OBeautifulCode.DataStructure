// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlatRowTest.cs" company="OBeautifulCode">
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
    public static partial class FlatRowTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static FlatRowTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FlatRow>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'cells' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<FlatRow>();

                            var result = new FlatRow(
                                null,
                                referenceObject.Id,
                                referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "cells", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FlatRow>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'cells' is an empty enumerable scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<FlatRow>();

                            var result = new FlatRow(
                                new List<ICell>(),
                                referenceObject.Id,
                                referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "cells", "is an empty enumerable", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<FlatRow>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'cells' contains a null element scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<FlatRow>();

                            var result = new FlatRow(
                                new ICell[0].Concat(referenceObject.Cells).Concat(new ICell[] { null }).Concat(referenceObject.Cells).ToList(),
                                referenceObject.Id,
                                referenceObject.Format);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "cells", "contains at least one null element", },
                    });
        }
    }
}