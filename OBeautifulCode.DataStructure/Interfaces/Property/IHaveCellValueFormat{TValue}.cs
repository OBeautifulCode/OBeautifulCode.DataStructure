// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveCellValueFormat{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies formatting for a cell.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    public interface IHaveCellValueFormat<TValue>
    {
        /// <summary>
        /// Gets the format to apply to the value of a cell.
        /// </summary>
        ICellValueFormat<TValue> ValueFormat { get; }
    }
}
