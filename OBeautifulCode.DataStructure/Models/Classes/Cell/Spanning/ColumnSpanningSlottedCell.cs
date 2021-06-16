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
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        public ColumnSpanningSlottedCell(
            IReadOnlyDictionary<string, IHaveValueCell> slotIdToCellMap,
            string defaultSlotName,
            int columnsSpanned,
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

            if (defaultSlotName == null)
            {
                throw new ArgumentNullException(nameof(defaultSlotName));
            }

            if (string.IsNullOrWhiteSpace(defaultSlotName))
            {
                throw new ArgumentException(Invariant($"{nameof(defaultSlotName)} is white space."));
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