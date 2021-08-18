// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureCellProtocols.cs" company="OBeautifulCode">
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

    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Statics for <see cref="DataStructureCellProtocols{TValue}"/>.
    /// </summary>
    internal static class DataStructureCellProtocols
    {
        /// <summary>
        /// Cache of the constructor info to use to build an <see cref="ExecuteOperationCellIfNecessaryOp{TValue}"/>.
        /// </summary>
        public static readonly ConcurrentDictionary<Type, ConstructorInfo> CachedTypeToExecuteOperationCellIfNecessaryOpConstructorInfoMap =
            new ConcurrentDictionary<Type, ConstructorInfo>();

        /// <summary>
        /// Gets a stack of cells that are the "current" cell.
        /// </summary>
        public static readonly Stack<ICell> CurrentCellStack = new Stack<ICell>();

        /// <summary>
        /// Throws an exception if the cell popped off <see cref="CurrentCellStack"/> is unexpected.
        /// </summary>
        /// <param name="expectedCell">The expected cell.</param>
        /// <param name="actualCell">The actual cell.</param>
        public static void ThrowIfUnexpectedCellPoppedOffCurrentCellStack(
            ICell expectedCell,
            ICell actualCell)
        {
            if (!ReferenceEquals(expectedCell, actualCell))
            {
                throw new InvalidOperationException("The cell popped off the current cell stack is not the expected cell.");
            }
        }

        /// <summary>
        /// Gets a <see cref="ExecuteOperationCellIfNecessaryOp{TValue}"/> for the specified cell or null if not applicable.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>
        /// The operation to execute or null if not applicable.
        /// </returns>
        public static IOperation GetExecuteOperationCellIfNecessaryOpOrNull(
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
    }
}
