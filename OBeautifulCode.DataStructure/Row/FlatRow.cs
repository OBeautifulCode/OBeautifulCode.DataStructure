// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlatRow.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// A row in a tree table that cannot have children.
    /// </summary>
    public partial class FlatRow : RowBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlatRow"/> class.
        /// </summary>
        /// <param name="id">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <param name="cells">OPTIONAL cells in the row.  DEFAULT is none.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        public FlatRow(
            string id = null,
            IReadOnlyList<ICell> cells = null,
            RowFormat format = null)
            : base(id, cells, format)
        {
        }
    }
}