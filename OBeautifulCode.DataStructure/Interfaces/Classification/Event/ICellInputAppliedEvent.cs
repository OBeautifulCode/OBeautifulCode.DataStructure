// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICellInputAppliedEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Input has been applied to a cell.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = ObcSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface ICellInputAppliedEvent : IHaveDetails, IHaveTimestampUtc, IModel
    {
        /// <summary>
        /// Gets the inputted value as an object.
        /// </summary>
        /// <returns>
        /// The inputted value.
        /// </returns>
        object GetObjectValue();
    }
}