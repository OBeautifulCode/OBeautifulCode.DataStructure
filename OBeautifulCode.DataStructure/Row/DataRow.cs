// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataRow.cs" company="OBeautifulCode">
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
    /// A row in a tree table.
    /// </summary>
    public partial class DataRow : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataRow"/> class.
        /// </summary>
        /// <param name="id">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <param name="cells">OPTIONAL cells in the row.  DEFAULT is none.</param>
        /// <param name="childRows">OPTIONAL child rows.  DEFAULT is none.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        public DataRow(
            string id = null,
            IReadOnlyList<ICell> cells = null,
            IReadOnlyList<DataRow> childRows = null,
            RowFormat format = null)
        {
            if ((cells != null) && cells.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(cells)} contains a null element."));
            }

            if ((childRows != null) && childRows.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(childRows)} contains a null element."));
            }

            this.Id = id;
            this.Cells = cells;
            this.ChildRows = childRows;
            this.Format = format;
        }

        /// <summary>
        /// Gets the row identifier.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the cells in the row.
        /// </summary>
        public IReadOnlyList<ICell> Cells { get; private set; }

        /// <summary>
        /// Gets the child rows.
        /// </summary>
        public IReadOnlyList<DataRow> ChildRows { get; private set; }

        /// <summary>
        /// Gets the format to apply to the whole row.
        /// </summary>
        public RowFormat Format { get; private set; }
    }
}