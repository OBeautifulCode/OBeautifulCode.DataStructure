// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="Report"/>.
    /// </summary>
    public static class ReportExtensions
    {
        /// <summary>
        /// Gets the default types supported for core operations (e.g. <see cref="GetCellValueOp{TValue}"/>).
        /// </summary>
        public static readonly IReadOnlyCollection<Type> DefaultTypesSupportedForCoreCellOps = new[]
        {
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(string),
            typeof(bool),
            typeof(DateTime),
            typeof(Guid),
            typeof(IReadOnlyList<NamedValue<sbyte>>),
            typeof(IReadOnlyList<NamedValue<byte>>),
            typeof(IReadOnlyList<NamedValue<short>>),
            typeof(IReadOnlyList<NamedValue<ushort>>),
            typeof(IReadOnlyList<NamedValue<int>>),
            typeof(IReadOnlyList<NamedValue<uint>>),
            typeof(IReadOnlyList<NamedValue<long>>),
            typeof(IReadOnlyList<NamedValue<ulong>>),
            typeof(IReadOnlyList<NamedValue<float>>),
            typeof(IReadOnlyList<NamedValue<double>>),
            typeof(IReadOnlyList<NamedValue<decimal>>),
            typeof(IReadOnlyList<NamedValue<string>>),
            typeof(IReadOnlyList<NamedValue<bool>>),
            typeof(IReadOnlyList<NamedValue<DateTime>>),
            typeof(IReadOnlyList<NamedValue<Guid>>),
            typeof(sbyte?),
            typeof(byte?),
            typeof(short?),
            typeof(ushort?),
            typeof(int?),
            typeof(uint?),
            typeof(long?),
            typeof(ulong?),
            typeof(float?),
            typeof(double?),
            typeof(decimal?),
            typeof(string),
            typeof(bool?),
            typeof(DateTime?),
            typeof(Guid?),
            typeof(IReadOnlyList<NamedValue<sbyte?>>),
            typeof(IReadOnlyList<NamedValue<byte?>>),
            typeof(IReadOnlyList<NamedValue<short?>>),
            typeof(IReadOnlyList<NamedValue<ushort?>>),
            typeof(IReadOnlyList<NamedValue<int?>>),
            typeof(IReadOnlyList<NamedValue<uint?>>),
            typeof(IReadOnlyList<NamedValue<long?>>),
            typeof(IReadOnlyList<NamedValue<ulong?>>),
            typeof(IReadOnlyList<NamedValue<float?>>),
            typeof(IReadOnlyList<NamedValue<double?>>),
            typeof(IReadOnlyList<NamedValue<decimal?>>),
            typeof(IReadOnlyList<NamedValue<string>>),
            typeof(IReadOnlyList<NamedValue<bool?>>),
            typeof(IReadOnlyList<NamedValue<DateTime?>>),
            typeof(IReadOnlyList<NamedValue<Guid?>>),
        };

        private static readonly ConcurrentDictionary<Type, ConstructorInfo> CachedTypeToExecuteOperationCellIfNecessaryOpConstructorInfoMap =
            new ConcurrentDictionary<Type, ConstructorInfo>();

        private static readonly ConcurrentDictionary<Type, ConstructorInfo> CachedTypeToCellProtocolsConstructorInfoMap =
            new ConcurrentDictionary<Type, ConstructorInfo>();

        private static readonly ConcurrentDictionary<Type, ConstructorInfo> CachedTypeToConvenienceProtocolsConstructorInfoMap =
            new ConcurrentDictionary<Type, ConstructorInfo>();

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="sectionId">The id of the section that contains the cell.</param>
        /// <param name="cellId">The id of the cell.</param>
        /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public static ICell GetCell(
            this Report report,
            string sectionId,
            string cellId,
            string slotId = null)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            if (sectionId == null)
            {
                throw new ArgumentNullException(nameof(sectionId));
            }

            if (string.IsNullOrWhiteSpace(sectionId))
            {
                throw new ArgumentException(Invariant($"{nameof(sectionId)} is white space."));
            }

            if (!report.GetSectionIdToSectionMap().TryGetValue(sectionId, out var section))
            {
                throw new InvalidOperationException(Invariant($"There is no section with id {sectionId}."));
            }

            var result = section.TreeTable.GetCell(cellId, slotId);

            return result;
        }

        /// <summary>
        /// Executes all cell operations and validations and records the results.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="timestampUtc">The timestamp (in UTC) to use when recording a <see cref="CellOpExecutionEventBase"/> with an <see cref="IOperationOutputCell{TValue}"/>.</param>
        /// <param name="protocolFactoryFuncs">
        /// OPTIONAL protocol factory chain-of-responsibility for protocols needed to execute the operations for all <see cref="IOperationOutputCell{TValue}"/>.
        /// Each func takes, as input, the protocol factory that should be used when these protocols need to execute other operations
        /// (e.g. MyOperation requires an int and declares it as an IReturningOperation{int} to enable others to "plug-in"
        /// any source of an int - perhaps the value of another cell or the output of some other calculation).
        /// Each func should return a protocol factory that gets protocols for the operations in-use by the <see cref="IOperationOutputCell{TValue}"/>s.
        /// DEFAULT is not to "plug-in" any additional protocols.
        /// </param>
        /// <param name="additionalTypesForCoreCellOps">
        /// OPTIONAL types in addition to <see cref="DefaultTypesSupportedForCoreCellOps"/> that should be supported
        /// when executing the core cell operations (e.g. <see cref="GetCellValueOp{TValue}"/>) .
        /// </param>
        public static void ReCalc(
            this Report report,
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs = null,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps = null)
        {
            report.ExecuteAllOperationsAndValidationsAndRecordResults(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps);
        }

        /// <summary>
        /// Executes all cell operations and validations and records the results.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="timestampUtc">The timestamp (in UTC) to use when recording a <see cref="CellOpExecutionEventBase"/> with an <see cref="IOperationOutputCell{TValue}"/>.</param>
        /// <param name="protocolFactoryFuncs">
        /// OPTIONAL protocol factory chain-of-responsibility for protocols needed to execute the operations for all <see cref="IOperationOutputCell{TValue}"/>.
        /// Each func takes, as input, the protocol factory that should be used when these protocols need to execute other operations
        /// (e.g. MyOperation requires an int and declares it as an IReturningOperation{int} to enable others to "plug-in"
        /// any source of an int - perhaps the value of another cell or the output of some other calculation).
        /// Each func should return a protocol factory that gets protocols for the operations in-use by the <see cref="IOperationOutputCell{TValue}"/>s.
        /// DEFAULT is not to "plug-in" any additional protocols.
        /// </param>
        /// <param name="additionalTypesForCoreCellOps">
        /// OPTIONAL types in addition to <see cref="DefaultTypesSupportedForCoreCellOps"/> that should be supported
        /// when executing the core cell operations (e.g. <see cref="GetCellValueOp{TValue}"/>) .
        /// </param>
        /// <returns>
        /// A task.
        /// </returns>
        public static async Task ReCalcAsync(
            this Report report,
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs = null,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps = null)
        {
            await report.ExecuteAllOperationsAndValidationsAndRecordResultsAsync(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps);
        }

        /// <summary>
        /// Executes all cell operations and validations and records the results.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="timestampUtc">The timestamp (in UTC) to use when recording a <see cref="CellOpExecutionEventBase"/> with an <see cref="IOperationOutputCell{TValue}"/>.</param>
        /// <param name="protocolFactoryFuncs">
        /// OPTIONAL protocol factory chain-of-responsibility for protocols needed to execute the operations for all <see cref="IOperationOutputCell{TValue}"/>.
        /// Each func takes, as input, the protocol factory that should be used when these protocols need to execute other operations
        /// (e.g. MyOperation requires an int and declares it as an IReturningOperation{int} to enable others to "plug-in"
        /// any source of an int - perhaps the value of another cell or the output of some other calculation).
        /// Each func should return a protocol factory that gets protocols for the operations in-use by the <see cref="IOperationOutputCell{TValue}"/>s.
        /// DEFAULT is not to "plug-in" any additional protocols.
        /// </param>
        /// <param name="additionalTypesForCoreCellOps">
        /// OPTIONAL types in addition to <see cref="DefaultTypesSupportedForCoreCellOps"/> that should be supported
        /// when executing the core cell operations (e.g. <see cref="GetCellValueOp{TValue}"/>) .
        /// </param>
        public static void ExecuteAllOperationsAndValidationsAndRecordResults(
            this Report report,
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs = null,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps = null)
        {
            var protocolFactory = report.BuildProtocolFactoryToExecuteAllOperations(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps);

            var operationCells = report.GetClearedOperationCells(timestampUtc);

            foreach (var operationCell in operationCells)
            {
                var executeOperationCellIfNecessaryOp = operationCell.BuildExecuteOperationCellIfNecessaryOp();

                protocolFactory.GetProtocolAndExecuteViaReflection(executeOperationCellIfNecessaryOp);
            }

            var validateableCells = report.GetClearedValidateableCells(timestampUtc);

            foreach (var validateableCell in validateableCells)
            {
                var validateCellOp = new ValidateCellOp(validateableCell);

                protocolFactory.GetProtocolAndExecuteViaReflection(validateCellOp);
            }
        }

        /// <summary>
        /// Executes all cell operations and validations and records the results.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="timestampUtc">The timestamp (in UTC) to use when recording a <see cref="CellOpExecutionEventBase"/> with an <see cref="IOperationOutputCell{TValue}"/>.</param>
        /// <param name="protocolFactoryFuncs">
        /// OPTIONAL protocol factory chain-of-responsibility for protocols needed to execute the operations for all <see cref="IOperationOutputCell{TValue}"/>.
        /// Each func takes, as input, the protocol factory that should be used when these protocols need to execute other operations
        /// (e.g. MyOperation requires an int and declares it as an IReturningOperation{int} to enable others to "plug-in"
        /// any source of an int - perhaps the value of another cell or the output of some other calculation).
        /// Each func should return a protocol factory that gets protocols for the operations in-use by the <see cref="IOperationOutputCell{TValue}"/>s.
        /// DEFAULT is not to "plug-in" any additional protocols.
        /// </param>
        /// <param name="additionalTypesForCoreCellOps">
        /// OPTIONAL types in addition to <see cref="DefaultTypesSupportedForCoreCellOps"/> that should be supported
        /// when executing the core cell operations (e.g. <see cref="GetCellValueOp{TValue}"/>) .
        /// </param>
        /// <returns>
        /// A task.
        /// </returns>
        public static async Task ExecuteAllOperationsAndValidationsAndRecordResultsAsync(
            this Report report,
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs = null,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps = null)
        {
            var protocolFactory = report.BuildProtocolFactoryToExecuteAllOperations(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps);

            var operationCells = report.GetClearedOperationCells(timestampUtc);

            foreach (var operationCell in operationCells)
            {
                var executeOperationCellIfNecessaryOp = operationCell.BuildExecuteOperationCellIfNecessaryOp();

                await protocolFactory.GetProtocolAndExecuteViaReflectionAsync(executeOperationCellIfNecessaryOp);
            }

            var validateableCells = report.GetClearedValidateableCells(timestampUtc);

            foreach (var validateableCell in validateableCells)
            {
                var validateCellOp = new ValidateCellOp(validateableCell);

                await protocolFactory.GetProtocolAndExecuteViaReflectionAsync(validateCellOp);
            }
        }

        /// <summary>
        /// Sets the value of an input cell.
        /// </summary>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="report">The report.</param>
        /// <param name="value">The value to set the cell to.</param>
        /// <param name="timestampUtc">The timestamp, in UTC, to record when the input is applied to the cell.</param>
        /// <param name="sectionId">The id of the section that contains the cell.</param>
        /// <param name="cellId">The id of the cell.</param>
        /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
        /// <param name="details">OPTIONAL details about inputting a value.  DEFAULT is to omit any details.</param>
        public static void SetInputCellValue<TValue>(
            this Report report,
            TValue value,
            DateTime timestampUtc,
            string sectionId,
            string cellId,
            string slotId = null,
            string details = null)
        {
            var cell = report.GetCell(sectionId, cellId, slotId);

            if (!(cell is IInputCell<TValue> inputCell))
            {
                throw new ArgumentException("The specified cell is not an input cell.");
            }

            var inputAppliedToCellEvent = new CellInputAppliedEvent<TValue>(timestampUtc, value, details);

            inputCell.Record(inputAppliedToCellEvent);
        }

        private static ChainOfResponsibilityProtocolFactory BuildProtocolFactoryToExecuteAllOperations(
            this Report report,
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            if (timestampUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(Invariant($"{nameof(timestampUtc)} is not in UTC."));
            }

            protocolFactoryFuncs = protocolFactoryFuncs ?? new List<Func<IProtocolFactory, IProtocolFactory>>();

            if (protocolFactoryFuncs.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(protocolFactoryFuncs)} contains a null element."));
            }

            additionalTypesForCoreCellOps = additionalTypesForCoreCellOps ?? new List<Type>();

            if (additionalTypesForCoreCellOps.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(additionalTypesForCoreCellOps)} contains a null element."));
            }

            var result = new ChainOfResponsibilityProtocolFactory();

            // Add caller's protocols to the chain of responsibility.
            foreach (var protocolFactoryFunc in protocolFactoryFuncs)
            {
                result.AddToEndOfChain(protocolFactoryFunc(result));
            }

            // Add DataStructureCellProtocols{TValue} and DataStructureConvenienceProtocols{TResult} to chain of responsibility.
            var typesForCoreCellOps = DefaultTypesSupportedForCoreCellOps
                .Concat(additionalTypesForCoreCellOps)
                .ToList();

            var coreProtocolsFactory = new ProtocolFactory();

            ConstructorInfo GetCellProtocolsFunc(Type type) => typeof(DataStructureCellProtocols<>).MakeGenericType(type).GetConstructors().Single();
            ConstructorInfo GetConvenienceProtocolsFunc(Type type) => typeof(DataStructureConvenienceProtocols<>).MakeGenericType(type).GetConstructors().Single();

            var cellProtocolsConstructorInfoParams = new object[] { report, result, timestampUtc };
            var convenienceProtocolsConstructorInfoParams = new object[] { result };

            foreach (var typeForCoreCellOps in typesForCoreCellOps)
            {
                RegisterProtocols(typeForCoreCellOps, CachedTypeToCellProtocolsConstructorInfoMap, coreProtocolsFactory, GetCellProtocolsFunc, cellProtocolsConstructorInfoParams);

                RegisterProtocols(typeForCoreCellOps, CachedTypeToConvenienceProtocolsConstructorInfoMap, coreProtocolsFactory, GetConvenienceProtocolsFunc, convenienceProtocolsConstructorInfoParams);
            }

            result.AddToEndOfChain(coreProtocolsFactory);

            return result;
        }

        private static IReadOnlyCollection<ICell> GetClearedOperationCells(
            this Report report,
            DateTime timestampUtc)
        {
            var result = report.Sections.SelectMany(_ => _.TreeTable.GetOperationCells()).ToList();

            var details = Invariant($"Value cleared by {nameof(ReportExtensions)}.{nameof(ExecuteAllOperationsAndValidationsAndRecordResults)} or async overload.");

            foreach (var operationCell in result)
            {
                ((IClearCellValue)operationCell).ClearCellValue(timestampUtc, details);
            }

            return result;
        }

        private static IReadOnlyCollection<ICanBeValidated> GetClearedValidateableCells(
            this Report report,
            DateTime timestampUtc)
        {
            var result = report.Sections.SelectMany(_ => _.TreeTable.GetCellsNeedingValidation()).ToList();

            var details = Invariant($"Validation cleared by {nameof(ReportExtensions)}.{nameof(ExecuteAllOperationsAndValidationsAndRecordResults)} or async overload.");

            foreach (var cell in result)
            {
                cell.ClearValidation(timestampUtc, details);
            }

            return result;
        }

        private static IOperation BuildExecuteOperationCellIfNecessaryOp(
            this ICell operationCell)
        {
            var valueType = operationCell.GetValueTypeOrNull();

            if (valueType == null)
            {
                throw new InvalidOperationException(Invariant($"This kind of cell is supposed to have a value type: {operationCell.GetType().ToStringReadable()}."));
            }

            if (!CachedTypeToExecuteOperationCellIfNecessaryOpConstructorInfoMap.TryGetValue(valueType, out var executeOperationCellIfNecessaryOpConstructorInfo))
            {
                executeOperationCellIfNecessaryOpConstructorInfo = typeof(ExecuteOperationCellIfNecessaryOp<>).MakeGenericType(valueType).GetConstructors().Single();

                CachedTypeToExecuteOperationCellIfNecessaryOpConstructorInfoMap.TryAdd(valueType, executeOperationCellIfNecessaryOpConstructorInfo);
            }

            var result = (IOperation)executeOperationCellIfNecessaryOpConstructorInfo.Invoke(new[] { operationCell });

            return result;
        }

        private static void RegisterProtocols(
            Type typeForCoreCellOps,
            ConcurrentDictionary<Type, ConstructorInfo> typeToConstructorInfoCache,
            ProtocolFactory protocolFactory,
            Func<Type, ConstructorInfo> getConstructorInfoFunc,
            object[] constructorInfoParamsToInvoke)
        {
            if (!typeToConstructorInfoCache.TryGetValue(typeForCoreCellOps, out var constructorInfo))
            {
                constructorInfo = getConstructorInfoFunc(typeForCoreCellOps);

                typeToConstructorInfoCache.TryAdd(typeForCoreCellOps, constructorInfo);
            }

            var protocol = (IProtocol)constructorInfo.Invoke(constructorInfoParamsToInvoke);

            protocolFactory.RegisterProtocolForSupportedOperations(protocol.GetType(), () => protocol, ProtocolAlreadyRegisteredForOperationStrategy.Skip);
        }
    }
}
