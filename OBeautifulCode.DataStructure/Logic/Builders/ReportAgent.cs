// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportAgent.cs" company="OBeautifulCode">
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
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Execution.Recipes;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Assists in working with a <see cref="Report"/>.
    /// </summary>
    public class ReportAgent
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

        private readonly Dictionary<string, Dictionary<string, ICell>> sectionIdToCellIdToCellMap;

        private readonly Dictionary<ICell, string> cellToSectionIdMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportAgent"/> class.
        /// </summary>
        /// <param name="report">The report.</param>
        public ReportAgent(
            Report report)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            var sectionIdToAllCellsMap = report.Sections.ToDictionary(_ => _.Id, _ => _.TreeTable.GetAllCells());

            var allCells = sectionIdToAllCellsMap.Values.SelectMany(_ => _).ToList();

            var distinctCells = allCells.Distinct(new ReferenceEqualityComparer<ICell>()).ToList();

            if (allCells.Count != distinctCells.Count)
            {
                throw new ArgumentException(Invariant($"One or more {nameof(ICell)} objects are used multiple times in the report."));
            }

            var operationCells = allCells.OfType<IOperationOutputCell>().ToList();

            var inputCells = allCells.OfType<IInputCell>().ToList();

            var validationCells = allCells.OfType<IValidationCell>().ToList();

            var availabilityCheckCells = allCells.OfType<IAvailabilityCheckCell>().ToList();

            var localSectionIdToCellIdToCellMap = sectionIdToAllCellsMap.ToDictionary(
                _ => _.Key,
                _ => _.Value
                    .Where(cell => !string.IsNullOrWhiteSpace(cell.Id))
                    .ToDictionary(cell => cell.Id, cell => cell));

            var localCellToSectionIdMap = sectionIdToAllCellsMap
                .SelectMany(_ => _.Value.Select(cell => new { SectionId = _.Key, Cell = cell }))
                .ToDictionary(_ => _.Cell, _ => _.SectionId, new ReferenceEqualityComparer<ICell>());

            this.sectionIdToCellIdToCellMap = localSectionIdToCellIdToCellMap;
            this.cellToSectionIdMap = localCellToSectionIdMap;

            this.OperationCells = operationCells;
            this.InputCells = inputCells;
            this.ValidationCells = validationCells;
            this.AvailabilityCheckCells = availabilityCheckCells;
            this.Report = report;
        }

        /// <summary>
        /// Gets the report.
        /// </summary>
        public Report Report { get; }

        /// <summary>
        /// Gets all operation cells.
        /// </summary>
        public IReadOnlyCollection<IOperationOutputCell> OperationCells { get; }

        /// <summary>
        /// Gets all input cells.
        /// </summary>
        public IReadOnlyCollection<IInputCell> InputCells { get; }

        /// <summary>
        /// Gets all validation cells.
        /// </summary>
        public IReadOnlyCollection<IValidationCell> ValidationCells { get; }

        /// <summary>
        /// Gets all availability check cells.
        /// </summary>
        public IReadOnlyCollection<IAvailabilityCheckCell> AvailabilityCheckCells { get; }

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <param name="reportCellLocator">The report cell locator.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public ICell GetCell(
            ReportCellLocator reportCellLocator)
        {
            if (reportCellLocator == null)
            {
                throw new ArgumentNullException(nameof(reportCellLocator));
            }

            var sectionId = reportCellLocator.SectionId;

            if (!this.sectionIdToCellIdToCellMap.ContainsKey(sectionId))
            {
                throw new CellNotFoundException(Invariant($"There is no section with id '{sectionId}'."), reportCellLocator);
            }

            var cellIdToCellMap = this.sectionIdToCellIdToCellMap[sectionId];

            var cellId = reportCellLocator.CellId;

            if (!cellIdToCellMap.TryGetValue(cellId, out var cell))
            {
                throw new CellNotFoundException(Invariant($"There is no cell with id '{cellId}' in section '{sectionId}'."), reportCellLocator);
            }

            ICell result;

            var slotId = reportCellLocator.SlotId;

            if (string.IsNullOrWhiteSpace(slotId))
            {
                result = cell;
            }
            else
            {
                if (cell is ISlottedCell slottedCell)
                {
                    if (slottedCell.SlotIdToCellMap.ContainsKey(slotId))
                    {
                        result = slottedCell.SlotIdToCellMap[slotId];
                    }
                    else
                    {
                        throw new CellNotFoundException(Invariant($"Slot id '{slotId}' was specified, but the addressed cell '{cellId}' in section '{sectionId}' does not contain a slot having that id."), reportCellLocator);
                    }
                }
                else
                {
                    throw new CellNotFoundException(Invariant($"Slot id '{slotId}' was specified, but the addressed cell '{cellId}' in section '{sectionId}' is not a slotted cell."), reportCellLocator);
                }
            }

            if (result is ISlottedCell addressedSlottedCell)
            {
                if (reportCellLocator.SlotSelectionStrategy == SlotSelectionStrategy.DefaultSlot)
                {
                    result = addressedSlottedCell.SlotIdToCellMap[addressedSlottedCell.DefaultSlotId];
                }
                else if (reportCellLocator.SlotSelectionStrategy == SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
                {
                    throw new CellNotFoundException(Invariant($"The operation addresses an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."), reportCellLocator);
                }
                else
                {
                    throw new NotSupportedException(Invariant($"This {nameof(SlotSelectionStrategy)} is not supported: {reportCellLocator.SlotSelectionStrategy}."));
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <typeparam name="TCell">The type of cell to return.</typeparam>
        /// <param name="reportCellLocator">The report cell locator.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public TCell GetCell<TCell>(
            ReportCellLocator reportCellLocator)
            where TCell : ICell
        {
            var cell = this.GetCell(reportCellLocator);

            if (!(cell is TCell result))
            {
                throw new CellNotFoundException(Invariant($"The operation addresses a cell of type {cell.GetType().ToStringReadable()}, which is not assignable to the specified {nameof(TCell)}."), reportCellLocator);
            }

            return result;
        }

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <param name="sectionCellLocator">The section cell locator.</param>
        /// <param name="currentCell">The current cell.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public ICell GetCell(
            SectionCellLocator sectionCellLocator,
            ICell currentCell)
        {
            if (sectionCellLocator == null)
            {
                throw new ArgumentNullException(nameof(sectionCellLocator));
            }

            if (currentCell == null)
            {
                throw new ArgumentNullException(nameof(currentCell));
            }

            if (!this.cellToSectionIdMap.TryGetValue(currentCell, out var sectionId))
            {
                throw new CellNotFoundException(Invariant($"{nameof(currentCell)} is not a cell in the report."), sectionCellLocator);
            }

            var reportCellLocator = new ReportCellLocator(sectionId, sectionCellLocator.CellId, sectionCellLocator.SlotId, sectionCellLocator.SlotSelectionStrategy);

            var result = this.GetCell(reportCellLocator);

            return result;
        }

        /// <summary>
        /// Executes all cell operations, validations, and availability checks, and records the results.
        /// </summary>
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
        public void Recalc(
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs = null,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps = null)
        {
            if (timestampUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(Invariant($"{nameof(timestampUtc)} is not in UTC."));
            }

            RecalcSynchronizer.Run(() => this.ReCalcInternal(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps));
        }

        /// <summary>
        /// Executes all cell operations, validations, and availability checks and records the results.
        /// </summary>
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
        public async Task RecalcAsync(
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs = null,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps = null)
        {
            if (timestampUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(Invariant($"{nameof(timestampUtc)} is not in UTC."));
            }

            await RecalcSynchronizer.RunAsync(() => this.ReCalcInternalAsync(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps));
        }

        private static IOperation BuildExecuteOperationCellIfNecessaryOp(
            IOperationOutputCell operationCell)
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

        private void ReCalcInternal(
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs = null,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps = null)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            var recalcPhase = RecalcPhase.Unknown;

            // ReSharper disable once AccessToModifiedClosure
            RecalcPhase GetRecalcPhaseFunc() => recalcPhase;

            var protocolFactory = this.BuildProtocolFactoryToExecuteAllOperations(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps, GetRecalcPhaseFunc);

            this.ClearCells(timestampUtc);

            recalcPhase = RecalcPhase.CellOpExecution;

            foreach (var cell in this.OperationCells)
            {
                var executeOperationCellIfNecessaryOp = BuildExecuteOperationCellIfNecessaryOp(cell);

                protocolFactory.GetProtocolAndExecuteViaReflection(executeOperationCellIfNecessaryOp);
            }

            recalcPhase = RecalcPhase.Validation;

            foreach (var cell in this.ValidationCells)
            {
                var validateCellIfNecessaryOp = new ValidateCellIfNecessaryOp(cell);

                protocolFactory.GetProtocolAndExecuteViaReflection(validateCellIfNecessaryOp);
            }

            recalcPhase = RecalcPhase.AvailabilityCheck;

            foreach (var cell in this.AvailabilityCheckCells)
            {
                var checkAvailabilityOfCellIfNecessaryOp = new CheckAvailabilityOfCellIfNecessaryOp(cell);

                protocolFactory.GetProtocolAndExecuteViaReflection(checkAvailabilityOfCellIfNecessaryOp);
            }

            if (this.PrepareToRerunRecalc(timestampUtc))
            {
                this.ReCalcInternal(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps);
            }
        }

        private async Task ReCalcInternalAsync(
            DateTime timestampUtc,
            IReadOnlyCollection<Func<IProtocolFactory, IProtocolFactory>> protocolFactoryFuncs = null,
            IReadOnlyCollection<Type> additionalTypesForCoreCellOps = null)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            var recalcPhase = RecalcPhase.Unknown;

            // ReSharper disable once AccessToModifiedClosure
            RecalcPhase GetRecalcPhaseFunc() => recalcPhase;

            var protocolFactory = this.BuildProtocolFactoryToExecuteAllOperations(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps, GetRecalcPhaseFunc);

            this.ClearCells(timestampUtc);

            recalcPhase = RecalcPhase.CellOpExecution;

            foreach (var cell in this.OperationCells)
            {
                var executeOperationCellIfNecessaryOp = BuildExecuteOperationCellIfNecessaryOp(cell);

                await protocolFactory.GetProtocolAndExecuteViaReflectionAsync(executeOperationCellIfNecessaryOp);
            }

            recalcPhase = RecalcPhase.Validation;

            foreach (var cell in this.ValidationCells)
            {
                var validateCellIfNecessaryOp = new ValidateCellIfNecessaryOp(cell);

                await protocolFactory.GetProtocolAndExecuteViaReflectionAsync(validateCellIfNecessaryOp);
            }

            recalcPhase = RecalcPhase.AvailabilityCheck;

            foreach (var cell in this.AvailabilityCheckCells)
            {
                var checkAvailabilityOfCellIfNecessaryOp = new CheckAvailabilityOfCellIfNecessaryOp(cell);

                await protocolFactory.GetProtocolAndExecuteViaReflectionAsync(checkAvailabilityOfCellIfNecessaryOp);
            }

            if (this.PrepareToRerunRecalc(timestampUtc))
            {
                await this.ReCalcInternalAsync(timestampUtc, protocolFactoryFuncs, additionalTypesForCoreCellOps);
            }
        }

        private ChainOfResponsibilityProtocolFactory BuildProtocolFactoryToExecuteAllOperations(
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

            var cellProtocolsConstructorInfoParams = new object[] { this, result, timestampUtc, getRecalcPhaseFunc };
            var convenienceProtocolsConstructorInfoParams = new object[] { result };

            foreach (var typeForCoreCellOps in typesForCoreCellOps)
            {
                RegisterProtocols(typeForCoreCellOps, CachedTypeToCellProtocolsConstructorInfoMap, coreProtocolsFactory, GetCellProtocolsFunc, cellProtocolsConstructorInfoParams);

                RegisterProtocols(typeForCoreCellOps, CachedTypeToConvenienceProtocolsConstructorInfoMap, coreProtocolsFactory, GetConvenienceProtocolsFunc, convenienceProtocolsConstructorInfoParams);
            }

            result.AddToEndOfChain(coreProtocolsFactory);

            return result;
        }

        private void ClearCells(
            DateTime timestampUtc)
        {
            var details = Invariant($"Value cleared by {nameof(ReportAgent)}.{nameof(this.Recalc)} or async overload.");

            foreach (var operationCell in this.OperationCells)
            {
                operationCell.ClearCellValue(timestampUtc, details);
            }

            details = Invariant($"Validation cleared by {nameof(ReportAgent)}.{nameof(this.Recalc)} or async overload.");

            foreach (var cell in this.ValidationCells)
            {
                cell.ClearValidation(timestampUtc, details);
            }

            details = Invariant($"Availability check cleared by {nameof(ReportAgent)}.{nameof(this.Recalc)} or async overload.");

            foreach (var cell in this.AvailabilityCheckCells)
            {
                cell.ClearAvailabilityCheck(timestampUtc, details);
            }
        }

        private bool PrepareToRerunRecalc(
            DateTime timestampUtc)
        {
            var validityIsUnknown = this.ValidationCells.Any(_ => _.GetValidity() == Validity.Unknown);

            var availabilityIsUnknown = this.AvailabilityCheckCells.Any(_ => _.GetAvailability() == Availability.Unknown);

            bool result;

            if (validityIsUnknown || availabilityIsUnknown)
            {
                result = false;
            }
            else
            {
                var disabledInputCellsWithValues =
                    this.InputCells
                    .Where(_ => _.GetAvailability() == Availability.Disabled)
                    .Where(_ => _.HasCellValue())
                    .ToList();

                if (disabledInputCellsWithValues.Any())
                {
                    foreach (var disabledInputCellWithValue in disabledInputCellsWithValues)
                    {
                        disabledInputCellWithValue.ClearCellValue(timestampUtc, Invariant($"{nameof(ReportAgent)}.{nameof(this.Recalc)} found this disabled input cell having a value.  Clearing the value and will re-run recalc."));
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
    }
}
