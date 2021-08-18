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
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Execution.Recipes;
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
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = ObcSuppressBecause.CA2104_DoNotDeclareReadOnlyMutableReferenceTypes_TypeIsImmutable)]
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
            typeof(CellLocatorBase),
            typeof(AvailabilityCheckResult),
            typeof(ValidationResult),
            typeof(CellOpExecutionOutcome),
            typeof(Validity),
            typeof(Availability),
            typeof(CompareOperator),
        };

        private static readonly ConcurrentDictionary<Type, ConstructorInfo> CachedTypeToExecuteOperationCellIfNecessaryOpConstructorInfoMap =
            new ConcurrentDictionary<Type, ConstructorInfo>();

        private static readonly ConcurrentDictionary<Type, ConstructorInfo> CachedTypeToCellProtocolsConstructorInfoMap =
            new ConcurrentDictionary<Type, ConstructorInfo>();

        private static readonly ConcurrentDictionary<Type, ConstructorInfo> CachedTypeToConvenienceProtocolsConstructorInfoMap =
            new ConcurrentDictionary<Type, ConstructorInfo>();

        private static readonly Synchronizer RecalcSynchronizer = new Synchronizer();

        /// <summary>
        /// Executes all cell operations, validations, and availability checks, and records the results.
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
        public static void Recalc(
            this Report report,
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs = null,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps = null)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            if (timestampUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(Invariant($"{nameof(timestampUtc)} is not in UTC."));
            }

            RecalcSynchronizer.Run(() => report.ReCalcInternal(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps));
        }

        /// <summary>
        /// Executes all cell operations, validations, and availability checks and records the results.
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
        public static async Task RecalcAsync(
            this Report report,
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs = null,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps = null)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            if (timestampUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(Invariant($"{nameof(timestampUtc)} is not in UTC."));
            }

            await RecalcSynchronizer.RunAsync(() => report.ReCalcInternalAsync(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps));
        }

        private static void ReCalcInternal(
            this Report report,
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs = null,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps = null)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            var recalcPhase = RecalcPhase.Unknown;

            // ReSharper disable once AccessToModifiedClosure
            RecalcPhase GetRecalcPhaseFunc() => recalcPhase;

            var reportCache = new ReportCache(report);

            var protocolFactory = reportCache.BuildProtocolFactoryToExecuteAllOperations(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps, GetRecalcPhaseFunc);

            reportCache.ClearCells(timestampUtc);

            recalcPhase = RecalcPhase.CellOpExecution;

            foreach (var cell in reportCache.OperationCells)
            {
                var executeOperationCellIfNecessaryOp = cell.BuildExecuteOperationCellIfNecessaryOp();

                protocolFactory.GetProtocolAndExecuteViaReflection(executeOperationCellIfNecessaryOp);
            }

            recalcPhase = RecalcPhase.Validation;

            foreach (var cell in reportCache.ValidationCells)
            {
                var validateCellIfNecessaryOp = new ValidateCellIfNecessaryOp(cell);

                protocolFactory.GetProtocolAndExecuteViaReflection(validateCellIfNecessaryOp);
            }

            recalcPhase = RecalcPhase.AvailabilityCheck;

            foreach (var cell in reportCache.AvailabilityCheckCells)
            {
                var checkAvailabilityOfCellIfNecessaryOp = new CheckAvailabilityOfCellIfNecessaryOp(cell);

                protocolFactory.GetProtocolAndExecuteViaReflection(checkAvailabilityOfCellIfNecessaryOp);
            }

            if (reportCache.PrepareToRerunRecalc(timestampUtc))
            {
                report.ReCalcInternal(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps);
            }
        }

        private static async Task ReCalcInternalAsync(
            this Report report,
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs = null,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps = null)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            var recalcPhase = RecalcPhase.Unknown;

            // ReSharper disable once AccessToModifiedClosure
            RecalcPhase GetRecalcPhaseFunc() => recalcPhase;

            var reportCache = new ReportCache(report);

            var protocolFactory = reportCache.BuildProtocolFactoryToExecuteAllOperations(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps, GetRecalcPhaseFunc);

            reportCache.ClearCells(timestampUtc);

            recalcPhase = RecalcPhase.CellOpExecution;

            foreach (var cell in reportCache.OperationCells)
            {
                var executeOperationCellIfNecessaryOp = cell.BuildExecuteOperationCellIfNecessaryOp();

                await protocolFactory.GetProtocolAndExecuteViaReflectionAsync(executeOperationCellIfNecessaryOp);
            }

            recalcPhase = RecalcPhase.Validation;

            foreach (var cell in reportCache.ValidationCells)
            {
                var validateCellIfNecessaryOp = new ValidateCellIfNecessaryOp(cell);

                await protocolFactory.GetProtocolAndExecuteViaReflectionAsync(validateCellIfNecessaryOp);
            }

            recalcPhase = RecalcPhase.AvailabilityCheck;

            foreach (var cell in reportCache.AvailabilityCheckCells)
            {
                var checkAvailabilityOfCellIfNecessaryOp = new CheckAvailabilityOfCellIfNecessaryOp(cell);

                await protocolFactory.GetProtocolAndExecuteViaReflectionAsync(checkAvailabilityOfCellIfNecessaryOp);
            }

            if (reportCache.PrepareToRerunRecalc(timestampUtc))
            {
                await report.ReCalcInternalAsync(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps);
            }
        }

        private static ChainOfResponsibilityProtocolFactory BuildProtocolFactoryToExecuteAllOperations(
            this ReportCache reportCache,
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps,
            Func<RecalcPhase> getRecalcPhaseFunc)
        {
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

            var cellProtocolsConstructorInfoParams = new object[] { reportCache, result, timestampUtc, getRecalcPhaseFunc };
            var convenienceProtocolsConstructorInfoParams = new object[] { result };

            foreach (var typeForCoreCellOps in typesForCoreCellOps)
            {
                RegisterProtocols(typeForCoreCellOps, CachedTypeToCellProtocolsConstructorInfoMap, coreProtocolsFactory, GetCellProtocolsFunc, cellProtocolsConstructorInfoParams);

                RegisterProtocols(typeForCoreCellOps, CachedTypeToConvenienceProtocolsConstructorInfoMap, coreProtocolsFactory, GetConvenienceProtocolsFunc, convenienceProtocolsConstructorInfoParams);
            }

            result.AddToEndOfChain(coreProtocolsFactory);

            return result;
        }

        private static void ClearCells(
            this ReportCache reportCache,
            DateTime timestampUtc)
        {
            var details = Invariant($"Value cleared by {nameof(ReportExtensions)}.{nameof(Recalc)} or async overload.");

            foreach (var operationCell in reportCache.OperationCells)
            {
                operationCell.ClearCellValue(timestampUtc, details);
            }

            details = Invariant($"Validation cleared by {nameof(ReportExtensions)}.{nameof(Recalc)} or async overload.");

            foreach (var cell in reportCache.ValidationCells)
            {
                cell.ClearValidation(timestampUtc, details);
            }

            details = Invariant($"Availability check cleared by {nameof(ReportExtensions)}.{nameof(Recalc)} or async overload.");

            foreach (var cell in reportCache.AvailabilityCheckCells)
            {
                cell.ClearAvailabilityCheck(timestampUtc, details);
            }
        }

        private static bool PrepareToRerunRecalc(
            this ReportCache reportCache,
            DateTime timestampUtc)
        {
            var validityIsUnknown = reportCache.ValidationCells.Any(_ => _.GetValidity() == Validity.Unknown);

            var availabilityIsUnknown = reportCache.AvailabilityCheckCells.Any(_ => _.GetAvailability() == Availability.Unknown);

            bool result;

            if (validityIsUnknown || availabilityIsUnknown)
            {
                result = false;
            }
            else
            {
                var disabledInputCellsWithValues = reportCache
                    .InputCells
                    .Where(_ => _.GetAvailability() == Availability.Disabled)
                    .Where(_ => _.HasCellValue())
                    .ToList();

                if (disabledInputCellsWithValues.Any())
                {
                    foreach (var disabledInputCellWithValue in disabledInputCellsWithValues)
                    {
                        disabledInputCellWithValue.ClearCellValue(timestampUtc, Invariant($"{nameof(ReportExtensions)}.{nameof(Recalc)} found this disabled input cell having a value.  Clearing the value and will re-run recalc."));
                    }

                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        private static IOperation BuildExecuteOperationCellIfNecessaryOp(
            this IOperationOutputCell operationCell)
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

            // ReSharper disable once CoVariantArrayConversion
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
