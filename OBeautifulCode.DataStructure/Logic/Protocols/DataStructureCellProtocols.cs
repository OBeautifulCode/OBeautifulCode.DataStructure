// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureCellProtocols.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Concurrent;
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
    }
}
