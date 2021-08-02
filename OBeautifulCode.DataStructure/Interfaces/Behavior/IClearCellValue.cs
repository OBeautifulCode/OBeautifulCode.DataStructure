// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClearCellValue.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    /// <summary>
    /// Clears the value of a cell.
    /// </summary>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IClearCellValue
    {
        /// <summary>
        /// Clears the value of a cell.
        /// </summary>
        /// <param name="timestampUtc">The timestamp (in UTC) to use when recording a <see cref="CellOpExecutionClearedEvent"/>.</param>
        /// <param name="details">OPTIONAL details related to clearing the cell.  DEFAULT is to omit any details.</param>
        void ClearCellValue(
            DateTime timestampUtc,
            string details = null);
    }
}
