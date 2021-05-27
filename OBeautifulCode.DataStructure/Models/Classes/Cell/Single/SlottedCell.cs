// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SlottedCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A cell that contains one or more slots, effectively making the tree table 3-dimensional.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class SlottedCell : CellBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlottedCell"/> class.
        /// </summary>
        /// <param name="slotIdToCellMap">A map of the slot's id to the cell contained in the slot.</param>
        /// <param name="defaultSlotName">The name of the slot to use when initially rendering the tree table.</param>
        public SlottedCell(
            IReadOnlyDictionary<string, IHaveValueCell> slotIdToCellMap,
            string defaultSlotName)
        {
            if (slotIdToCellMap == null)
            {
                throw new ArgumentNullException(nameof(slotIdToCellMap));
            }

            if (!slotIdToCellMap.Any())
            {
                throw new ArgumentException(Invariant($"{nameof(slotIdToCellMap)} is empty."));
            }

            if (!slotIdToCellMap.ContainsKey(defaultSlotName))
            {
                throw new ArgumentException(Invariant($"{nameof(slotIdToCellMap)} does not contain the specified {nameof(defaultSlotName)}."));
            }

            this.SlotIdToCellMap = slotIdToCellMap;
            this.DefaultSlotName = defaultSlotName;
        }

        /// <summary>
        /// Gets a map of the slot's id to the cell contained in the slot.
        /// </summary>
        public IReadOnlyDictionary<string, IHaveValueCell> SlotIdToCellMap { get; private set; }

        /// <summary>
        /// Gets the name of the slot to use when initially rendering the tree table.
        /// </summary>
        public string DefaultSlotName { get; private set; }
    }
}