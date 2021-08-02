// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveCellValidationConditions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    /// <summary>
    /// Specifies validation conditions for a cell.
    /// </summary>
    public interface IHaveCellValidationConditions
    {
        /// <summary>
        /// Gets a list of conditions that determine the validity of the cell's value.
        /// </summary>
        ValidationConditions ValidationConditions { get; }

        /// <summary>
        /// Gets the events that record the validation of this cell's value.
        /// </summary>
        IReadOnlyList<CellValidationEventBase> CellValidationEvents { get; }
    }
}
