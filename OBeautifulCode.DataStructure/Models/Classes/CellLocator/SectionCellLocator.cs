// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SectionCellLocator.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Locates an <see cref="ICell"/> within the section in-context.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class SectionCellLocator : CellLocatorBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionCellLocator"/> class.
        /// </summary>
        /// <param name="cellId">The id of the cell.</param>
        /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
        /// <param name="slotSelectionStrategy">OPTIONAL strategy to use to select a slot if addressing an <see cref="ISlottedCell"/>.  DEFAULT is to throw if addressing an <see cref="ISlottedCell"/> -AND- <paramref name="slotId"/> is not specified.</param>
        public SectionCellLocator(
            string cellId,
            string slotId = null,
            SlotSelectionStrategy slotSelectionStrategy = SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
        {
            if (cellId == null)
            {
                throw new ArgumentNullException(nameof(cellId));
            }

            if (string.IsNullOrWhiteSpace(cellId))
            {
                throw new ArgumentException(Invariant($"{nameof(cellId)} is white space."));
            }

            if (slotSelectionStrategy == SlotSelectionStrategy.Unknown)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.CellId = cellId;
            this.SlotId = slotId;
            this.SlotSelectionStrategy = slotSelectionStrategy;
        }

        /// <summary>
        /// Gets the id of the cell.
        /// </summary>
        public string CellId { get; private set; }

        /// <summary>
        /// Gets the id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.
        /// </summary>
        public string SlotId { get; private set; }

        /// <summary>
        /// Gets the strategy to use to select a slot if addressing an <see cref="ISlottedCell"/>.
        /// </summary>
        public SlotSelectionStrategy SlotSelectionStrategy { get; private set; }
    }
}
