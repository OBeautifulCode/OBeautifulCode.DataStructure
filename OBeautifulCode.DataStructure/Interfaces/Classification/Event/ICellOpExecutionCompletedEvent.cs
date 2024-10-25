// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICellOpExecutionCompletedEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Diagnostics.CodeAnalysis;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// The execution of a cell's operation completed.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = ObcSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface ICellOpExecutionCompletedEvent : IHaveDetails, IHaveTimestampUtc, IModel
    {
        /// <summary>
        /// Gets the result of executing the operation as an object.
        /// </summary>
        /// <returns>
        /// The result of executing the operation.
        /// </returns>
        object GetExecutionResultObjectValue();
    }
}