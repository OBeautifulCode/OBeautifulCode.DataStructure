// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataRows.cs" company="OBeautifulCode">
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
    /// The data rows (the rows below the header row) in a tree table.
    /// </summary>
    public partial class DataRows : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataRows"/> class.
        /// </summary>
        /// <param name="rows">The rows (at the root level of the row-tree).</param>
        /// <param name="format">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        public DataRows(
            IReadOnlyList<RowBase> rows,
            DataRowsFormat format = null)
        {
            if (rows == null)
            {
                throw new ArgumentNullException(nameof(rows));
            }

            if (rows.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(rows)} contains at least one null element."));
            }

            this.Rows = rows;
            this.Format = format;
        }

        /// <summary>
        /// Gets the rows of the table (at the root level of the row-tree).
        /// </summary>
        public IReadOnlyList<RowBase> Rows { get; private set; }

        /// <summary>
        /// Gets the format to apply to all data rows.
        /// </summary>
        public DataRowsFormat Format { get; private set; }
    }
}