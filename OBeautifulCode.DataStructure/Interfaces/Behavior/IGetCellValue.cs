// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGetCellValue.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Gets the value of a cell.
    /// </summary>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IGetCellValue
    {
        /// <summary>
        /// Gets the cell's underlying value as an object.
        /// </summary>
        /// <returns>
        /// The value of the cell.
        /// </returns>
        object GetCellObjectValue();

        /// <summary>
        /// Determines if the cell has an underlying value.
        /// </summary>
        /// <returns>
        /// true if the cell has an underlying value, otherwise false.
        /// </returns>
        bool HasCellValue();
    }
}
