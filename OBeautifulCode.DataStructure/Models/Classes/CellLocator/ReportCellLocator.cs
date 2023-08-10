// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportCellLocator.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Locates an <see cref="ICell"/> within the <see cref="Report"/> in-context.
    /// </summary>
    /// <remarks>
    /// Assumes that the specified cell identifier is unique within the specified <see cref="Section"/>.
    /// </remarks>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ReportCellLocator : CellLocatorBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportCellLocator"/> class.
        /// </summary>
        /// <param name="sectionId">The id of the section that contains the cell.</param>
        /// <param name="cellId">The id of the cell.</param>
        /// <param name="slotId">OPTIONAL id of the slot to use -OR- null if not addressing an <see cref="ISlottedCell"/>.  DEFAULT is to address an <see cref="INotSlottedCell"/>.</param>
        /// <param name="slotSelectionStrategy">OPTIONAL strategy to use to select a slot if addressing an <see cref="ISlottedCell"/>.  DEFAULT is to throw if addressing an <see cref="ISlottedCell"/> -AND- <paramref name="slotId"/> is not specified.</param>
        public ReportCellLocator(
            string sectionId,
            string cellId,
            string slotId = null,
            SlotSelectionStrategy slotSelectionStrategy = SlotSelectionStrategy.ThrowIfSlotIdNotSpecified)
            : base(cellId, slotId, slotSelectionStrategy)
        {
            if (sectionId == null)
            {
                throw new ArgumentNullException(nameof(sectionId));
            }

            if (string.IsNullOrWhiteSpace(sectionId))
            {
                throw new ArgumentException(Invariant($"{nameof(sectionId)} is white space."));
            }

            this.SectionId = sectionId;
        }

        /// <summary>
        /// Gets the id of the section that contains the cell.
        /// </summary>
        public string SectionId { get; private set; }
    }
}
