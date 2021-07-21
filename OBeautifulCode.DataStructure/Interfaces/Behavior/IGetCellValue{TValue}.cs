// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGetCellValue{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Gets the value of a cell.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IGetCellValue<TValue> : IGetCellValue
    {
        /// <summary>
        /// Gets the cell's underlying value.
        /// </summary>
        /// <returns>
        /// The value of the cell.
        /// </returns>
        TValue GetCellValue();
    }
}
