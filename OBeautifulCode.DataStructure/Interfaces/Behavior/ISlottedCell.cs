// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISlottedCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    /// <summary>
    /// A cell having slots.
    /// </summary>
    public interface ISlottedCell : ICell
    {
        /// <summary>
        /// Gets a map of the slot's id to the cell contained in the slot.
        /// </summary>
        IReadOnlyDictionary<string, IHaveValueCell> SlotIdToCellMap { get; }

        /// <summary>
        /// Gets the name of the slot to use when initially rendering the tree table.
        /// </summary>
        string DefaultSlotId { get; }
    }
}