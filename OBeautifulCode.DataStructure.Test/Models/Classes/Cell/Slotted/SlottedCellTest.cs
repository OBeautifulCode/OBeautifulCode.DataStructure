// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SlottedCellTest.cs" company="OBeautifulCode">
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
    public static partial class SlottedCellTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static SlottedCellTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'slotIdToCellMap' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                                 null,
                                                 referenceObject.DefaultSlotId,
                                                 referenceObject.Id,
                                                 referenceObject.ColumnsSpanned,
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'slotIdToCellMap' is an empty dictionary scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                                 new Dictionary<string, INotSlottedCell>(),
                                                 referenceObject.DefaultSlotId,
                                                 referenceObject.Id,
                                                 referenceObject.ColumnsSpanned,
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap", "is an empty dictionary", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'slotIdToCellMap' contains a key-value pair with a null value scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var dictionaryWithNullValue = referenceObject.SlotIdToCellMap.ToDictionary(_ => _.Key, _ => _.Value);

                            var randomKey = dictionaryWithNullValue.Keys.ElementAt(ThreadSafeRandom.Next(0, dictionaryWithNullValue.Count));

                            dictionaryWithNullValue[randomKey] = null;

                            var result = new SlottedCell(
                                                 dictionaryWithNullValue,
                                                 referenceObject.DefaultSlotId,
                                                 referenceObject.Id,
                                                 referenceObject.ColumnsSpanned,
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap", "contains at least one key-value pair with a null value", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'slotIdToCellMap' contains a white space key",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var slotIdToCellMap = referenceObject.SlotIdToCellMap.ToDictionary(_ => _.Key, _ => _.Value);

                            slotIdToCellMap.Add(" \r\n ", A.Dummy<INotSlottedCell>());

                            var result = new SlottedCell(
                                slotIdToCellMap,
                                referenceObject.DefaultSlotId,
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap", "contains at least one key-value pair with a white space key" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'defaultSlotId' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                                 referenceObject.SlotIdToCellMap,
                                                 null,
                                                 referenceObject.Id,
                                                 referenceObject.ColumnsSpanned,
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "defaultSlotId", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'defaultSlotId' is white space scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                                 referenceObject.SlotIdToCellMap,
                                                 Invariant($"  {Environment.NewLine}  "),
                                                 referenceObject.Id,
                                                 referenceObject.ColumnsSpanned,
                                                 referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "defaultSlotId", "white space", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'slotIdToCellMap' does not contain the specified defaultSlotId",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                referenceObject.SlotIdToCellMap,
                                A.Dummy<string>(),
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap does not contain the specified defaultSlotId" },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is 0",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                referenceObject.SlotIdToCellMap,
                                referenceObject.DefaultSlotId,
                                referenceObject.Id,
                                0,
                                referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned is 0; must be null or >= 1.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is -1",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                referenceObject.SlotIdToCellMap,
                                referenceObject.DefaultSlotId,
                                referenceObject.Id,
                                -1,
                                referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "columnsSpanned is -1; must be null or >= 1.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when parameter 'columnsSpanned' is negative",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var result = new SlottedCell(
                                referenceObject.SlotIdToCellMap,
                                referenceObject.DefaultSlotId,
                                referenceObject.Id,
                                A.Dummy<NegativeInteger>(),
                                referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "must be null or >= 1.", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<SlottedCell>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'slotIdToCellMap' contains a value whose ColumnsSpanned does not equal columnsSpanned (with null imputed to 1)",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<SlottedCell>();

                            var slotIdToCellMap = new Dictionary<string, INotSlottedCell>
                            {
                                { A.Dummy<string>(), new NullCell(columnsSpanned: referenceObject.ColumnsSpanned) },
                                { A.Dummy<string>(), new NullCell(columnsSpanned: referenceObject.ColumnsSpanned == null ? 2 : referenceObject.ColumnsSpanned + 1) },
                                { A.Dummy<string>(), new NullCell(columnsSpanned: referenceObject.ColumnsSpanned) },
                            };

                            var result = new SlottedCell(
                                slotIdToCellMap,
                                slotIdToCellMap.Keys.First(),
                                referenceObject.Id,
                                referenceObject.ColumnsSpanned,
                                referenceObject.Details);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "slotIdToCellMap contains a cell that does not span the same number of columns as this cell." },
                    });

            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<SlottedCell>
                    {
                        Name = "DeepCloneWithId should deep clone object and replace Id with the provided id",
                        WithPropertyName = "Id",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<SlottedCell>();

                            var referenceObject = A.Dummy<SlottedCell>().ThatIs(_ => !systemUnderTest.Id.IsEqualTo(_.Id));

                            var result = new SystemUnderTestDeepCloneWithValue<SlottedCell>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Id,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<SlottedCell>
                    {
                        Name = "DeepCloneWithColumnsSpanned should deep clone object and replace ColumnsSpanned with the provided columnsSpanned",
                        WithPropertyName = "ColumnsSpanned",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var slotIdToCellMap = new Dictionary<string, INotSlottedCell>
                            {
                                { A.Dummy<string>(), new NullCell() },
                                { A.Dummy<string>(), new NullCell() },
                                { A.Dummy<string>(), new NullCell() },
                            };

                            var systemUnderTest = new SlottedCell(
                                slotIdToCellMap,
                                slotIdToCellMap.Keys.First());

                            var result = new SystemUnderTestDeepCloneWithValue<SlottedCell>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = 1,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<SlottedCell>
                    {
                        Name = "DeepCloneWithDetails should deep clone object and replace Details with the provided details",
                        WithPropertyName = "Details",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<SlottedCell>();

                            var referenceObject = A.Dummy<SlottedCell>().ThatIs(_ => !systemUnderTest.Details.IsEqualTo(_.Details));

                            var result = new SystemUnderTestDeepCloneWithValue<SlottedCell>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Details,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<SlottedCell>
                    {
                        Name = "DeepCloneWithSlotIdToCellMap should deep clone object and replace SlotIdToCellMap with the provided slotIdToCellMap",
                        WithPropertyName = "SlotIdToCellMap",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var slotIdToCellMap = new Dictionary<string, INotSlottedCell>
                            {
                                { "abc", new NullCell() },
                                { A.Dummy<string>(), new NullCell() },
                                { A.Dummy<string>(), new NullCell() },
                            };

                            var systemUnderTest = new SlottedCell(
                                slotIdToCellMap,
                                slotIdToCellMap.Keys.First());

                            var result = new SystemUnderTestDeepCloneWithValue<SlottedCell>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = new Dictionary<string, INotSlottedCell>
                                {
                                    { A.Dummy<string>(), new NullCell() },
                                    { "abc", new NullCell() },
                                    { A.Dummy<string>(), new NullCell() },
                                },
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<SlottedCell>
                    {
                        Name = "DeepCloneWithDefaultSlotId should deep clone object and replace DefaultSlotId with the provided defaultSlotId",
                        WithPropertyName = "DefaultSlotId",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<SlottedCell>().Whose(_ => _.SlotIdToCellMap.Count() > 1);

                            var defaultSlotId = systemUnderTest.SlotIdToCellMap.Keys.First(_ => _ != systemUnderTest.DefaultSlotId);

                            var result = new SystemUnderTestDeepCloneWithValue<SlottedCell>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = defaultSlotId,
                            };

                            return result;
                        },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                {
                    var referenceObjectForEquatableTestScenarios = new SlottedCell(
                        new Dictionary<string, INotSlottedCell>
                        {
                            { "abc", new NullCell(columnsSpanned: 1) },
                            { "def", new NullCell(columnsSpanned: 1) },
                            { "ghi", new NullCell(columnsSpanned: 1) },
                        },
                        "def",
                        A.Dummy<string>(),
                        1,
                        A.Dummy<string>());

                    return new EquatableTestScenario<SlottedCell>
                    {
                        Name = "Default Code Generated Scenario",
                        ReferenceObject = referenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new SlottedCell[]
                        {
                                new SlottedCell(
                                    referenceObjectForEquatableTestScenarios.SlotIdToCellMap,
                                    referenceObjectForEquatableTestScenarios.DefaultSlotId,
                                    referenceObjectForEquatableTestScenarios.Id,
                                    referenceObjectForEquatableTestScenarios.ColumnsSpanned,
                                    referenceObjectForEquatableTestScenarios.Details),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new SlottedCell[]
                        {
                                new SlottedCell(
                                        referenceObjectForEquatableTestScenarios.SlotIdToCellMap,
                                        referenceObjectForEquatableTestScenarios.DefaultSlotId,
                                        A.Dummy<SlottedCell>().Whose(_ => !_.Id.IsEqualTo(referenceObjectForEquatableTestScenarios.Id)).Id,
                                        referenceObjectForEquatableTestScenarios.ColumnsSpanned,
                                        referenceObjectForEquatableTestScenarios.Details),
                                new SlottedCell(
                                        referenceObjectForEquatableTestScenarios.SlotIdToCellMap,
                                        referenceObjectForEquatableTestScenarios.DefaultSlotId,
                                        referenceObjectForEquatableTestScenarios.Id,
                                        referenceObjectForEquatableTestScenarios.ColumnsSpanned,
                                        A.Dummy<SlottedCell>().Whose(_ => !_.Details.IsEqualTo(referenceObjectForEquatableTestScenarios.Details)).Details),
                                new SlottedCell(
                                        referenceObjectForEquatableTestScenarios.SlotIdToCellMap,
                                        referenceObjectForEquatableTestScenarios.DefaultSlotId,
                                        referenceObjectForEquatableTestScenarios.Id,
                                        null,
                                        referenceObjectForEquatableTestScenarios.Details),
                                new SlottedCell(
                                        new Dictionary<string, INotSlottedCell>
                                        {
                                            { "abc", new NullCell(columnsSpanned: 1) },
                                            { "def", new NullCell(columnsSpanned: 1) },
                                        },
                                        referenceObjectForEquatableTestScenarios.DefaultSlotId,
                                        referenceObjectForEquatableTestScenarios.Id,
                                        referenceObjectForEquatableTestScenarios.ColumnsSpanned,
                                        referenceObjectForEquatableTestScenarios.Details),
                                new SlottedCell(
                                        referenceObjectForEquatableTestScenarios.SlotIdToCellMap,
                                        "abc",
                                        referenceObjectForEquatableTestScenarios.Id,
                                        referenceObjectForEquatableTestScenarios.ColumnsSpanned,
                                        referenceObjectForEquatableTestScenarios.Details),
                        },
                        ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new object[]
                        {
                                A.Dummy<object>(),
                                A.Dummy<string>(),
                                A.Dummy<int>(),
                                A.Dummy<int?>(),
                                A.Dummy<Guid>(),
                                A.Dummy<ConstCell<Version>>(),
                                A.Dummy<InputCell<Version>>(),
                                A.Dummy<NullCell>(),
                                A.Dummy<OperationCell<Version>>(),
                        },
                    };
                });
        }
    }
}