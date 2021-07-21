// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISlottedCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    /// <summary>
    /// A cell that contains one or more slots, effectively making the tree table 3-dimensional.
    /// </summary>
    public interface ISlottedCell : ICell
    {
        /// <summary>
        /// Gets a map of the slot's id to the cell contained in the slot.
        /// </summary>
        IReadOnlyDictionary<string, INotSlottedCell> SlotIdToCellMap { get; }

        /// <summary>
        /// Gets the name of the slot to use when initially rendering the tree table.
        /// </summary>
        string DefaultSlotId { get; }
    }
}
