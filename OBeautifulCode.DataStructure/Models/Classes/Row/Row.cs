// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Row.cs" company="OBeautifulCode">
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
    /// A row in a tree table; optionally having child rows.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class Row : RowBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class.
        /// </summary>
        /// <param name="cells">Cells in the row.</param>
        /// <param name="id">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="childRows">OPTIONAL child rows.  DEFAULT is none.</param>
        /// <param name="expandedSummaryRow">OPTIONAL row that summarizes the children (e.g. a Total row) when this row is expanded.  DEFAULT is to forgo a summary row when this row is expanded.</param>
        /// <param name="collapsedSummaryRow">OPTIONAL row that summarizes the children (e.g. a Total row) when this row is collapsed.  DEFAULT is to forgo a summary row when this row is collapsed.</param>
        public Row(
            IReadOnlyList<ICell> cells,
            string id = null,
            RowFormat format = null,
            IReadOnlyList<Row> childRows = null,
            FlatRow expandedSummaryRow = null,
            FlatRow collapsedSummaryRow = null)
            : base(cells, id, format)
        {
            if ((childRows != null) && childRows.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(childRows)} contains at least one null element."));
            }

            if ((expandedSummaryRow != null) && ((childRows == null) || (!childRows.Any())))
            {
                throw new ArgumentException(Invariant($"{nameof(expandedSummaryRow)} is specified when there are no rows in {nameof(childRows)}."));
            }

            if ((collapsedSummaryRow != null) && ((childRows == null) || (!childRows.Any())))
            {
                throw new ArgumentException(Invariant($"{nameof(collapsedSummaryRow)} is specified when there are no rows in {nameof(childRows)}."));
            }

            this.ChildRows = childRows;
            this.ExpandedSummaryRow = expandedSummaryRow;
            this.CollapsedSummaryRow = collapsedSummaryRow;
        }

        /// <summary>
        /// Gets the child rows.
        /// </summary>
        public IReadOnlyList<Row> ChildRows { get; private set; }

        /// <summary>
        /// Gets a row that summarizes the children (e.g. a Total row) when this row is expanded.
        /// </summary>
        public FlatRow ExpandedSummaryRow { get; private set; }

        /// <summary>
        /// Gets a row that summarizes the children (e.g. a Total row) when this row is collapsed.
        /// </summary>
        public FlatRow CollapsedSummaryRow { get; private set; }
    }
}