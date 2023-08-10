// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InSectionCellLocator.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Locates an <see cref="ICell"/> within the <see cref="Section"/> in-context.
    /// </summary>
    /// <remarks>
    /// Assumes that the specified cell identifier is unique within the <see cref="Section"/> in-context.
    /// </remarks>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class InSectionCellLocator : CellLocatorBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InSectionCellLocator"/> class.
        /// </summary>
        /// <param name="cellId">The id of the cell.</param>
        /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
        /// <param name="slotSelectionStrategy">OPTIONAL strategy to use to select a slot if addressing an <see cref="ISlottedCell"/>.  DEFAULT is to throw if addressing an <see cref="ISlottedCell"/> -AND- <paramref name="slotId"/> is not specified.</param>
        public InSectionCellLocator(
            string cellId,
            string slotId = null,
            SlotSelectionStrategy slotSelectionStrategy = SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
            : base(cellId, slotId, slotSelectionStrategy)
        {
        }
    }
}
