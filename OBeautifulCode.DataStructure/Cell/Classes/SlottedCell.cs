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
        /// <param name="slotNameToCellMap">A map of the slot's name to the cell contained in the slot.</param>
        /// <param name="defaultSlotName">The name of the slot to use when initially rendering the tree table.</param>
        protected SlottedCell(
            IReadOnlyDictionary<string, ICell> slotNameToCellMap,
            string defaultSlotName)
        {
            if (slotNameToCellMap == null)
            {
                throw new ArgumentNullException(nameof(slotNameToCellMap));
            }

            if (!slotNameToCellMap.Any())
            {
                throw new ArgumentException(Invariant($"{nameof(slotNameToCellMap)} is empty."));
            }

            if (!slotNameToCellMap.Values.Any(_ => _ is SlottedCell))
            {
                throw new ArgumentException(Invariant($"{nameof(slotNameToCellMap)} contains a slot which itself is a slotted cell, which is not allowed."));
            }

            if (!slotNameToCellMap.ContainsKey(defaultSlotName))
            {
                throw new ArgumentException(Invariant($"{nameof(slotNameToCellMap)} does not contain the specified {nameof(defaultSlotName)}."));
            }

            this.SlotNameToCellMap = slotNameToCellMap;
            this.DefaultSlotName = defaultSlotName;
        }

        /// <summary>
        /// Gets a map of the slot's name to the cell contained in the slot.
        /// </summary>
        public IReadOnlyDictionary<string, ICell> SlotNameToCellMap { get; private set; }

        /// <summary>
        /// Gets the name of the slot to use when initially rendering the tree table.
        /// </summary>
        public string DefaultSlotName { get; private set; }
    }
}