// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnSpanningSlottedCell.cs" company="OBeautifulCode">
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
    public partial class ColumnSpanningSlottedCell : CellBase, IColumnSpanningCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSpanningSlottedCell"/> class.
        /// </summary>
        /// <param name="slotIdToCellMap">A map of the slot's id to the cell contained in the slot.</param>
        /// <param name="defaultSlotName">The name of the slot to use when initially rendering the tree table.</param>
        /// <param name="columnsSpanned">The number of columns spanned.</param>
        public ColumnSpanningSlottedCell(
            IReadOnlyDictionary<string, IHaveValueCell> slotIdToCellMap,
            string defaultSlotName,
            int columnsSpanned)
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

            if (columnsSpanned < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(columnsSpanned), Invariant($"{nameof(columnsSpanned)} is {columnsSpanned}; must be >= 2."));
            }

            this.SlotIdToCellMap = slotIdToCellMap;
            this.DefaultSlotName = defaultSlotName;
            this.ColumnsSpanned = columnsSpanned;
        }

        /// <summary>
        /// Gets a map of the slot's id to the cell contained in the slot.
        /// </summary>
        public IReadOnlyDictionary<string, IHaveValueCell> SlotIdToCellMap { get; private set; }

        /// <summary>
        /// Gets the name of the slot to use when initially rendering the tree table.
        /// </summary>
        public string DefaultSlotName { get; private set; }

        /// <inheritdoc />
        public int ColumnsSpanned { get; private set; }
    }
}