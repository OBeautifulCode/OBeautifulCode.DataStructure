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
    public partial class SlottedCell : CellBase, ISlottedCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlottedCell"/> class.
        /// </summary>
        /// <param name="slotIdToCellMap">A map of the slot's id to the cell contained in the slot.</param>
        /// <param name="defaultSlotId">The id of the slot to use when initially rendering the tree table.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        public SlottedCell(
            IReadOnlyDictionary<string, IHaveValueCell> slotIdToCellMap,
            string defaultSlotId,
            string id = null)
            : base(id)
        {
            if (slotIdToCellMap == null)
            {
                throw new ArgumentNullException(nameof(slotIdToCellMap));
            }

            if (!slotIdToCellMap.Any())
            {
                throw new ArgumentException(Invariant($"{nameof(slotIdToCellMap)} is an empty dictionary."));
            }

            if (slotIdToCellMap.Any(_ => _.Value == null))
            {
                throw new ArgumentException(Invariant($"{nameof(slotIdToCellMap)} contains at least one key-value pair with a null value."));
            }

            if (slotIdToCellMap.Any(_ => string.IsNullOrWhiteSpace(_.Key)))
            {
                throw new ArgumentException(Invariant($"{nameof(slotIdToCellMap)} contains at least one key-value pair with a white space key."));
            }

            if (defaultSlotId == null)
            {
                throw new ArgumentNullException(nameof(defaultSlotId));
            }

            if (string.IsNullOrWhiteSpace(defaultSlotId))
            {
                throw new ArgumentException(Invariant($"{nameof(defaultSlotId)} is white space."));
            }

            if (!slotIdToCellMap.ContainsKey(defaultSlotId))
            {
                throw new ArgumentException(Invariant($"{nameof(slotIdToCellMap)} does not contain the specified {nameof(defaultSlotId)}."));
            }

            this.SlotIdToCellMap = slotIdToCellMap;
            this.DefaultSlotId = defaultSlotId;
        }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, IHaveValueCell> SlotIdToCellMap { get; private set; }

        /// <inheritdoc />
        public string DefaultSlotId { get; private set; }
    }
}