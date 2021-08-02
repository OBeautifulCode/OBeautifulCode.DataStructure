// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FooterRows.cs" company="OBeautifulCode">
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
    /// The footer rows in a tree table.
    /// </summary>
    public partial class FooterRows : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FooterRows"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="format">OPTIONAL format to apply to all footer rows.  DEFAULT is to leave the format unchanged.</param>
        public FooterRows(
            IReadOnlyList<FlatRow> rows,
            FooterRowsFormat format = null)
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
        /// Gets the rows.
        /// </summary>
        public IReadOnlyList<FlatRow> Rows { get; private set; }

        /// <summary>
        /// Gets the format to apply to all footer rows.
        /// </summary>
        public FooterRowsFormat Format { get; private set; }
    }
}