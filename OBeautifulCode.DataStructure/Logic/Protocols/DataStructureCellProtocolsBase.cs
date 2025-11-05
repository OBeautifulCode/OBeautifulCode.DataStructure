// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureCellProtocolsBase.cs" company="OBeautifulCode">
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
    /// Base class for the core <see cref="ICell"/>-related protocols.
    /// </summary>
    public class DataStructureCellProtocolsBase
    {
        private static readonly ConcurrentDictionary<Type, ConstructorInfo> CachedTypeToExecuteOperationCellIfNecessaryOpConstructorInfoMap =
            new ConcurrentDictionary<Type, ConstructorInfo>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStructureCellProtocolsBase"/> class.
        /// </summary>
        /// <param name="reportAgent">The report cache in-context.</param>
        /// <param name="protocolFactory">The protocol factory to use when executing an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.</param>
        /// <param name="timestampUtc">The timestamp (in UTC) to use when recording a <see cref="CellOpExecutionEventBase"/> with an <see cref="IOperationOutputCell{TValue}"/>.</param>
        /// <param name="getRecalcPhaseFunc">Func that gets the <see cref="RecalcPhase"/>.</param>
        /// <param name="currentCellStack">Gets a stack of cells that are the "current" cell.</param>
        protected DataStructureCellProtocolsBase(
            ReportAgent reportAgent,
            IProtocolFactory protocolFactory,
            DateTime timestampUtc,
            Func<RecalcPhase> getRecalcPhaseFunc,
            Stack<ICell> currentCellStack)
        {
            if (reportAgent == null)
            {
                throw new ArgumentNullException(nameof(reportAgent));
            }

            // ReSharper disable once JoinNullCheckWithUsage
            if (protocolFactory == null)
            {
                throw new ArgumentNullException(nameof(protocolFactory));
            }

            if (timestampUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(Invariant($"{nameof(timestampUtc)} is not in UTC time."));
            }

            if (getRecalcPhaseFunc == null)
            {
                throw new ArgumentNullException(nameof(getRecalcPhaseFunc));
            }

            this.ReportAgent = reportAgent;
            this.ProtocolFactory = protocolFactory;
            this.TimestampUtc = timestampUtc;
            this.GetRecalcPhaseFunc = getRecalcPhaseFunc;
            this.CurrentCellStack = currentCellStack;
        }

        /// <summary>
        /// Gets the report cache in-context.
        /// </summary>
        protected ReportAgent ReportAgent { get; }

        /// <summary>
        /// Gets the protocol factory to use when executing an <see cref="IOperationOutputCell{TValue}"/>'s <see cref="IOperationOutputCell{TValue}.Operation"/>.
        /// </summary>
        protected IProtocolFactory ProtocolFactory { get; }

        /// <summary>
        /// Gets the timestamp (in UTC) to use when recording a <see cref="CellOpExecutionEventBase"/> with an <see cref="IOperationOutputCell{TValue}"/>.
        /// </summary>
        protected DateTime TimestampUtc { get; }

        /// <summary>
        /// Gets a func that gets the <see cref="RecalcPhase"/>.
        /// </summary>
        protected Func<RecalcPhase> GetRecalcPhaseFunc { get; }

        /// <summary>
        /// Gets a stack of cells that are the "current" cell.
        /// </summary>
        protected Stack<ICell> CurrentCellStack { get; }

        /// <summary>
        /// Throws an exception if the cell popped off stack is unexpected.
        /// </summary>
        /// <param name="expectedCell">The expected cell.</param>
        /// <param name="actualCell">The actual cell.</param>
        protected static void ThrowIfUnexpectedCellPoppedOffCurrentCellStack(
            ICell expectedCell,
            ICell actualCell)
        {
            if (expectedCell == null)
            {
                throw new ArgumentNullException(nameof(expectedCell));
            }

            if (actualCell == null)
            {
                throw new ArgumentNullException(nameof(actualCell));
            }

            if (!ReferenceEquals(expectedCell, actualCell))
            {
                throw new InvalidOperationException("The cell popped off the current cell stack is not the expected cell.");
            }
        }

        /// <summary>
        /// Executes an operation that gets a cell locator and then uses the locator to locate the cell.
        /// </summary>
        /// <param name="cellLocatorOp">An operation that returns a cell locator.</param>
        /// <returns>
        /// The cell locator and the located cell as an <see cref="IGetCellValue"/>.
        /// </returns>
        protected async Task<LocatedCellHavingValue> GetCellAndExecuteOperationIfNecessaryAsync(
            IOperation cellLocatorOp)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE SYNC METHOD BELOW; NO GOOD WAY TO D.R.Y. IT OUT
            if (cellLocatorOp == null)
            {
                throw new ArgumentNullException(nameof(cellLocatorOp));
            }

            var cellLocator = await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync<ICellLocator>(cellLocatorOp);

            var cell = this.GetCell(cellLocator);

            if (!(cell is IGetCellValue cellWithValue))
            {
                throw new CellNotFoundException(Invariant($"Addressing a cell whose type is not an {typeof(IGetCellValue).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            // This is necessary because we can't simply use new ExecuteOperationCellIfNecessaryOp<TValue>()
            // TValue is the TValue of THIS protocol factory.
            // HasCellValueOp does not have TValue in it's generic arguments; ALL instances of
            // DataStructureCellProtocols can execute that operation and the chain-of-responsibility
            // protocol factory will simply use the first instance of DataStructureCellProtocols that is registered.
            // So TValue of this factory might be int whereas the cell's TValue is a decimal.
            var executeOperationCellIfNecessaryOp = GetExecuteOperationCellIfNecessaryOpOrNull(cell);

            if (executeOperationCellIfNecessaryOp != null)
            {
                await this.ProtocolFactory.GetProtocolAndExecuteViaReflectionAsync(executeOperationCellIfNecessaryOp);
            }

            var result = new LocatedCellHavingValue
            {
                Cell = cellWithValue,
                CellLocator = cellLocator,
            };

            return result;
        }

        /// <summary>
        /// Executes an operation that gets a cell locator and then uses the locator to locate the cell.
        /// </summary>
        /// <param name="cellLocatorOp">An operation that returns a cell locator.</param>
        /// <returns>
        /// The cell locator and the located cell as an <see cref="IGetCellValue"/>.
        /// </returns>
        protected LocatedCellHavingValue GetCellHavingValueAndExecuteOperationIfNecessary(
            IOperation cellLocatorOp)
        {
            // NOTE: THIS CODE IS A NEAR DUPLICATE OF THE ASYNC METHOD ABOVE; NO GOOD WAY TO D.R.Y. IT OUT
            if (cellLocatorOp == null)
            {
                throw new ArgumentNullException(nameof(cellLocatorOp));
            }

            var cellLocator = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<ICellLocator>(cellLocatorOp);

            var cell = this.GetCell(cellLocator);

            if (!(cell is IGetCellValue cellWithValue))
            {
                throw new CellNotFoundException(Invariant($"Addressing a cell whose type is not an {typeof(IGetCellValue).ToStringReadable()}: {cell.GetType().ToStringReadable()}."), cellLocator);
            }

            // This is necessary because we can't simply use new ExecuteOperationCellIfNecessaryOp<TValue>()
            // TValue is the TValue of THIS protocol factory.
            // HasCellValueOp does not have TValue in it's generic arguments; ALL instances of
            // DataStructureCellProtocols can execute that operation and the chain-of-responsibility
            // protocol factory will simply use the first instance of DataStructureCellProtocols that is registered.
            // So TValue of this factory might be int whereas the cell's TValue is a decimal.
            var executeOperationCellIfNecessaryOp = GetExecuteOperationCellIfNecessaryOpOrNull(cell);

            if (executeOperationCellIfNecessaryOp != null)
            {
                this.ProtocolFactory.GetProtocolAndExecuteViaReflection(executeOperationCellIfNecessaryOp);
            }

            var result = new LocatedCellHavingValue
            {
                Cell = cellWithValue,
                CellLocator = cellLocator,
            };

            return result;
        }

        /// <summary>
        /// Gets a cell using the specified locator.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        /// <returns>
        /// The located cell.
        /// </returns>
        protected ICell GetCell(
            ICellLocator cellLocator)
        {
            if (cellLocator == null)
            {
                throw new ArgumentNullException(nameof(cellLocator));
            }

            ICell result;

            if (cellLocator is StandardCellLocator standardCellLocator)
            {
                result = this.ReportAgent.GetCell(standardCellLocator);
            }
            else if (cellLocator is InReportCellLocator reportCellLocator)
            {
                result = this.ReportAgent.GetCell(reportCellLocator);
            }
            else if (cellLocator is InSectionCellLocator sectionCellLocator)
            {
                var currentCell = this.CurrentCellStack.Peek();

                result = this.ReportAgent.GetCell(sectionCellLocator, currentCell);
            }
            else if (cellLocator is SelfCellLocator)
            {
                result = this.CurrentCellStack.Peek();
            }
            else
            {
                throw new NotSupportedException(Invariant($"This type of {nameof(ICellLocator)} is not supported: {cellLocator.GetType().ToStringReadable()}."));
            }

            return result;
        }

        private static IOperation GetExecuteOperationCellIfNecessaryOpOrNull(
            ICell cell)
        {
            IOperation result = null;

            if (cell is IOperationOutputCell)
            {
                var valueType = cell.GetValueTypeOrNull();

                if (valueType == null)
                {
                    throw new InvalidOperationException(Invariant($"This kind of cell is supposed to have a value type: {cell.GetType().ToStringReadable()}."));
                }

                if (!CachedTypeToExecuteOperationCellIfNecessaryOpConstructorInfoMap.TryGetValue(valueType, out var constructorInfo))
                {
                    constructorInfo = typeof(ExecuteOperationCellIfNecessaryOp<>).MakeGenericType(cell.GetValueTypeOrNull()).GetConstructors().Single();

                    CachedTypeToExecuteOperationCellIfNecessaryOpConstructorInfoMap.TryAdd(valueType, constructorInfo);
                }

                // ReSharper disable once CoVariantArrayConversion
                result = (IOperation)constructorInfo.Invoke(new[] { cell });
            }

            return result;
        }

        /// <summary>
        /// A cell locator and the located cell as an <see cref="IGetCellValue"/>.
        /// </summary>
        protected class LocatedCellHavingValue
        {
            /// <summary>
            /// Gets or sets the cell locator.
            /// </summary>
            public ICellLocator CellLocator { get; set; }

            /// <summary>
            /// Gets or sets the located cell.
            /// </summary>
            public IGetCellValue Cell { get; set; }
        }
    }
}
