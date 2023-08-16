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
    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = ObcSuppressBecause.CA1506_AvoidExcessiveClassCoupling_DisagreeWithAssessment)]
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
            typeof(ICellLocator),
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

        private readonly Dictionary<string, IReadOnlyCollection<ICell>> cellIdToCellsMap;

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

            var sectionToCellsMap = report.Sections.ToDictionary(_ => _, _ => _.TreeTable.GetAllCells());

            var allCells = sectionToCellsMap.Values.SelectMany(_ => _).ToList();

            var distinctCells = allCells.Distinct(new ReferenceEqualityComparer<ICell>()).ToList();

            if (allCells.Count != distinctCells.Count)
            {
                throw new ArgumentException(Invariant($"One or more {nameof(ICell)} objects are used multiple times in the report."));
            }

            var operationCells = allCells.OfType<IOperationOutputCell>().ToList();

            var inputCells = allCells.OfType<IInputCell>().ToList();

            var validationCells = allCells.OfType<IValidationCell>().ToList();

            var availabilityCheckCells = allCells.OfType<IAvailabilityCheckCell>().ToList();

            var localSectionIdToCellIdToCellMap = new Dictionary<string, Dictionary<string, ICell>>();

            var localCellToSectionIdMap = new Dictionary<ICell, string>(new ReferenceEqualityComparer<ICell>());

            var localCellIdToCellsMap = new Dictionary<string, IReadOnlyCollection<ICell>>();

            foreach (var sectionToCellsMapKvp in sectionToCellsMap)
            {
                var section = sectionToCellsMapKvp.Key;
                var thisSectionCellIdToCellMap = new Dictionary<string, ICell>();

                foreach (var cell in sectionToCellsMapKvp.Value)
                {
                    if (!string.IsNullOrWhiteSpace(cell.Id))
                    {
                        thisSectionCellIdToCellMap.Add(cell.Id, cell);

                        if (!localCellIdToCellsMap.TryGetValue(cell.Id, out var thisIdCells))
                        {
                            thisIdCells = new List<ICell>();
                            localCellIdToCellsMap.Add(cell.Id, thisIdCells);
                        }

                        ((List<ICell>)thisIdCells).Add(cell);
                    }

                    localCellToSectionIdMap.Add(cell, section.Id);
                }

                localSectionIdToCellIdToCellMap.Add(section.Id, thisSectionCellIdToCellMap);
            }

            this.sectionIdToCellIdToCellMap = localSectionIdToCellIdToCellMap;
            this.cellToSectionIdMap = localCellToSectionIdMap;
            this.cellIdToCellsMap = localCellIdToCellsMap;

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
        /// <param name="cellLocator">The cell locator.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public ICell GetCell(
            StandardCellLocator cellLocator)
        {
            if (cellLocator == null)
            {
                throw new ArgumentNullException(nameof(cellLocator));
            }

            if (!this.cellIdToCellsMap.TryGetValue(cellLocator.CellId, out var cells))
            {
                throw new CellNotFoundException(Invariant($"There is no cell with id '{cellLocator.CellId}' in the report."), cellLocator);
            }

            if (cells.Count > 1)
            {
                throw new CellNotFoundException(Invariant($"There are multiple cells with id '{cellLocator.CellId}' in the report."), cellLocator);
            }

            var cell = cells.Single();

            var result = GetCellResolvingSlotting(cell, cellLocator);

            return result;
        }

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <typeparam name="TCell">The type of cell to return.</typeparam>
        /// <param name="cellLocator">The cell locator.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public TCell GetCell<TCell>(
            StandardCellLocator cellLocator)
            where TCell : ICell
        {
            var cell = this.GetCell(cellLocator);

            if (!(cell is TCell result))
            {
                throw new CellNotFoundException(Invariant($"Addressing a cell of type {cell.GetType().ToStringReadable()}, which is not assignable to the specified {nameof(TCell)}."), cellLocator);
            }

            return result;
        }

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        /// <param name="cell">When this method returns, contains the cell if the cell is found; otherwise null.
        /// </param>
        /// <returns>
        /// true if the cell was found; otherwise, false.
        /// </returns>
        public bool TryGetCell(
            StandardCellLocator cellLocator,
            out ICell cell)
        {
            if (cellLocator == null)
            {
                throw new ArgumentNullException(nameof(cellLocator));
            }

            cell = null;

            if (!this.cellIdToCellsMap.TryGetValue(cellLocator.CellId, out var cells))
            {
                return false;
            }

            if (cells.Count > 1)
            {
                return false;
            }

            cell = cells.Single();

            cell = GetCellResolvingSlottingOrNull(cell, cellLocator);

            var result = cell != null;

            return result;
        }

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <typeparam name="TCell">The type of cell to return.</typeparam>
        /// <param name="cellLocator">The cell locator.</param>
        /// <param name="cell">When this method returns, contains the cell if the cell is found and is of type <typeparamref name="TCell"/>; otherwise null.</param>
        /// <returns>
        /// true if the cell was found; otherwise, false.
        /// </returns>
        public bool TryGetCell<TCell>(
            StandardCellLocator cellLocator,
            out TCell cell)
            where TCell : ICell
        {
            cell = default;

            var result = false;

            if (this.TryGetCell(cellLocator, out var untypedCell))
            {
                if (untypedCell is TCell typedCell)
                {
                    cell = typedCell;

                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public ICell GetCell(
            InReportCellLocator cellLocator)
        {
            if (cellLocator == null)
            {
                throw new ArgumentNullException(nameof(cellLocator));
            }

            var sectionId = cellLocator.SectionId;

            if (!this.sectionIdToCellIdToCellMap.ContainsKey(sectionId))
            {
                throw new CellNotFoundException(Invariant($"There is no section with id '{sectionId}'."), cellLocator);
            }

            var cellIdToCellMap = this.sectionIdToCellIdToCellMap[sectionId];

            if (!cellIdToCellMap.TryGetValue(cellLocator.CellId, out var cell))
            {
                throw new CellNotFoundException(Invariant($"There is no cell with id '{cellLocator.CellId}' in section '{sectionId}'."), cellLocator);
            }

            var result = GetCellResolvingSlotting(cell, cellLocator, sectionId);

            return result;
        }

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <typeparam name="TCell">The type of cell to return.</typeparam>
        /// <param name="cellLocator">The cell locator.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        public TCell GetCell<TCell>(
            InReportCellLocator cellLocator)
            where TCell : ICell
        {
            var cell = this.GetCell(cellLocator);

            if (!(cell is TCell result))
            {
                throw new CellNotFoundException(Invariant($"Addressing a cell of type {cell.GetType().ToStringReadable()}, which is not assignable to the specified {nameof(TCell)}."), cellLocator);
            }

            return result;
        }

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        /// <param name="cell">When this method returns, contains the cell if the cell is found; otherwise null.
        /// </param>
        /// <returns>
        /// true if the cell was found; otherwise, false.
        /// </returns>
        public bool TryGetCell(
            InReportCellLocator cellLocator,
            out ICell cell)
        {
            if (cellLocator == null)
            {
                throw new ArgumentNullException(nameof(cellLocator));
            }

            cell = null;

            var sectionId = cellLocator.SectionId;

            if (!this.sectionIdToCellIdToCellMap.ContainsKey(sectionId))
            {
                return false;
            }

            var cellIdToCellMap = this.sectionIdToCellIdToCellMap[sectionId];

            if (!cellIdToCellMap.TryGetValue(cellLocator.CellId, out cell))
            {
                return false;
            }

            cell = GetCellResolvingSlottingOrNull(cell, cellLocator);

            var result = cell != null;

            return result;
        }

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <typeparam name="TCell">The type of cell to return.</typeparam>
        /// <param name="cellLocator">The cell locator.</param>
        /// <param name="cell">When this method returns, contains the cell if the cell is found and is of type <typeparamref name="TCell"/>; otherwise null.</param>
        /// <returns>
        /// true if the cell was found; otherwise, false.
        /// </returns>
        public bool TryGetCell<TCell>(
            InReportCellLocator cellLocator,
            out TCell cell)
            where TCell : ICell
        {
            cell = default;

            var result = false;

            if (this.TryGetCell(cellLocator, out var untypedCell))
            {
                if (untypedCell is TCell typedCell)
                {
                    cell = typedCell;

                    result = true;
                }
            }

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

        /// <summary>
        /// Gets a cell.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        /// <param name="referenceCell">A reference cell; any cell in the section containing the cell targeted by <paramref name="cellLocator"/>.</param>
        /// <returns>
        /// The cell.
        /// </returns>
        internal ICell GetCell(
            InSectionCellLocator cellLocator,
            ICell referenceCell)
        {
            if (cellLocator == null)
            {
                throw new ArgumentNullException(nameof(cellLocator));
            }

            if (referenceCell == null)
            {
                throw new ArgumentNullException(nameof(referenceCell));
            }

            if (!this.cellToSectionIdMap.TryGetValue(referenceCell, out var sectionId))
            {
                throw new CellNotFoundException(Invariant($"{nameof(referenceCell)} is not a cell in the report."), cellLocator);
            }

            var reportCellLocator = new InReportCellLocator(sectionId, cellLocator.CellId, cellLocator.SlotId, cellLocator.SlotSelectionStrategy);

            var result = this.GetCell(reportCellLocator);

            return result;
        }

        private static ICell GetCellResolvingSlotting(
            ICell cell,
            CellLocatorBase cellLocator,
            string sectionId = null)
        {
            ICell result;

            var slotId = cellLocator.SlotId;

            if (string.IsNullOrWhiteSpace(slotId))
            {
                result = cell;
            }
            else
            {
                var exceptionMessageSectionQualifier = sectionId == null
                ? string.Empty
                : Invariant($"in section '{sectionId}' ");

                if (cell is ISlottedCell slottedCell)
                {
                    if (slottedCell.SlotIdToCellMap.ContainsKey(slotId))
                    {
                        result = slottedCell.SlotIdToCellMap[slotId];
                    }
                    else
                    {
                        throw new CellNotFoundException(Invariant($"Slot id '{slotId}' was specified, but the addressed cell '{cellLocator.CellId}' {exceptionMessageSectionQualifier}does not contain a slot having that id."), cellLocator);
                    }
                }
                else
                {
                    throw new CellNotFoundException(Invariant($"Slot id '{slotId}' was specified, but the addressed cell '{cellLocator.CellId}' {exceptionMessageSectionQualifier}is not a slotted cell."), cellLocator);
                }
            }

            if (result is ISlottedCell addressedSlottedCell)
            {
                if (cellLocator.SlotSelectionStrategy == SlotSelectionStrategy.DefaultSlot)
                {
                    result = addressedSlottedCell.SlotIdToCellMap[addressedSlottedCell.DefaultSlotId];
                }
                else if (cellLocator.SlotSelectionStrategy == SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
                {
                    throw new CellNotFoundException(Invariant($"Located an {nameof(ISlottedCell)} (and not a slot within that cell) and {nameof(SlotSelectionStrategy)} is {nameof(SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)}."), cellLocator);
                }
                else
                {
                    throw new NotSupportedException(Invariant($"This {nameof(SlotSelectionStrategy)} is not supported: {cellLocator.SlotSelectionStrategy}."));
                }
            }

            return result;
        }

        private static ICell GetCellResolvingSlottingOrNull(
            ICell cell,
            CellLocatorBase cellLocator)
        {
            ICell result;

            var slotId = cellLocator.SlotId;

            if (string.IsNullOrWhiteSpace(slotId))
            {
                result = cell;
            }
            else
            {
                if (cell is ISlottedCell slottedCell)
                {
                    result = slottedCell.SlotIdToCellMap.ContainsKey(slotId)
                        ? slottedCell.SlotIdToCellMap[slotId]
                        : null;
                }
                else
                {
                    result = null;
                }
            }

            if (result is ISlottedCell addressedSlottedCell)
            {
                if (cellLocator.SlotSelectionStrategy == SlotSelectionStrategy.DefaultSlot)
                {
                    result = addressedSlottedCell.SlotIdToCellMap[addressedSlottedCell.DefaultSlotId];
                }
                else if (cellLocator.SlotSelectionStrategy == SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
                {
                    result = null;
                }
                else
                {
                    throw new NotSupportedException(Invariant($"This {nameof(SlotSelectionStrategy)} is not supported: {cellLocator.SlotSelectionStrategy}."));
                }
            }

            return result;
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
