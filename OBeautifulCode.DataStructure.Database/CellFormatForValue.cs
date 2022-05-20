// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellFormatForValue.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Database
{
    using System;

    /// <summary>
    /// Specifies the cell format to apply to the cell that contains a specified value.
    /// </summary>
    public class CellFormatForValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellFormatForValue"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cellFormat">The cell format.</param>
        public CellFormatForValue(
            object value,
            CellFormat cellFormat)
        {
            if (cellFormat == null)
            {
                throw new ArgumentNullException(nameof(cellFormat));
            }

            this.Value = value;
            this.CellFormat = cellFormat;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Gets the format to apply to the cell containing the value.
        /// </summary>
        public CellFormat CellFormat { get; }
    }
}
