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
        /// <param name="expandedSummaryRows">OPTIONAL rows that summarizes the children (e.g. a Total row) when this row is expanded.  DEFAULT is to forgo summary rows when this row is expanded.</param>
        /// <param name="collapsedSummaryRows">OPTIONAL rows that summarizes the children (e.g. a Total row) when this row is collapsed.  DEFAULT is to forgo summary rows when this row is collapsed.</param>
        public Row(
            IReadOnlyList<ICell> cells,
            string id = null,
            RowFormat format = null,
            IReadOnlyList<RowBase> childRows = null,
            IReadOnlyList<FlatRow> expandedSummaryRows = null,
            IReadOnlyList<FlatRow> collapsedSummaryRows = null)
            : base(cells, id, format)
        {
            if ((childRows != null) && childRows.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(childRows)} contains at least one null element."));
            }

            if (expandedSummaryRows != null)
            {
                if (expandedSummaryRows.Any(_ => _ == null))
                {
                    throw new ArgumentException(Invariant($"{nameof(expandedSummaryRows)} contains a null element."));
                }

                if (expandedSummaryRows.Any() && ((childRows == null) || (!childRows.Any())))
                {
                    throw new ArgumentException(Invariant($"{nameof(expandedSummaryRows)} is specified when there are no rows in {nameof(childRows)}."));
                }
            }

            if (collapsedSummaryRows != null)
            {
                if (collapsedSummaryRows.Any(_ => _ == null))
                {
                    throw new ArgumentException(Invariant($"{nameof(collapsedSummaryRows)} contains a null element."));
                }

                if (collapsedSummaryRows.Any() && ((childRows == null) || (!childRows.Any())))
                {
                    throw new ArgumentException(Invariant($"{nameof(collapsedSummaryRows)} is specified when there are no rows in {nameof(childRows)}."));
                }
            }

            this.ChildRows = childRows;
            this.ExpandedSummaryRows = expandedSummaryRows;
            this.CollapsedSummaryRows = collapsedSummaryRows;
        }

        /// <summary>
        /// Gets the child rows.
        /// </summary>
        public IReadOnlyList<RowBase> ChildRows { get; private set; }

        /// <summary>
        /// Gets rows that summarizes the children (e.g. a Total row) when this row is expanded.
        /// </summary>
        public IReadOnlyList<FlatRow> ExpandedSummaryRows { get; private set; }

        /// <summary>
        /// Gets rows that summarizes the children (e.g. a Total row) when this row is collapsed.
        /// </summary>
        public IReadOnlyList<FlatRow> CollapsedSummaryRows { get; private set; }

        /// <summary>
        /// Builds an empty row.
        /// </summary>
        /// <param name="requiredNumberOfColumnsSpanned">The required number of columns spanned in the resulting row.</param>
        /// <returns>
        /// The empty row.
        /// </returns>
        public static Row BuildEmpty(
            int requiredNumberOfColumnsSpanned)
        {
            var result = new NullCell(columnsSpanned: requiredNumberOfColumnsSpanned).ToRow();

            return result;
        }
    }
}