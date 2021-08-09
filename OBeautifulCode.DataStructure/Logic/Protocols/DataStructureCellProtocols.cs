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
    using System.Reflection;

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
    }
}
