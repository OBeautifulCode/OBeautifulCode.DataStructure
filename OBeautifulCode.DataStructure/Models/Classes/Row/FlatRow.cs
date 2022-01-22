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
    // ReSharper disable once RedundantExtendsListEntry
    public partial class FlatRow : RowBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlatRow"/> class.
        /// </summary>
        /// <param name="cells">Cells in the row.</param>
        /// <param name="id">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        public FlatRow(
            IReadOnlyList<ICell> cells,
            string id = null,
            RowFormat format = null)
            : base(cells, id, format)
        {
        }

        /// <summary>
        /// Builds an empty row.
        /// </summary>
        /// <param name="requiredNumberOfColumnsSpanned">The required number of columns spanned in the resulting row.</param>
        /// <returns>
        /// The empty row.
        /// </returns>
        public static FlatRow BuildEmpty(
            int requiredNumberOfColumnsSpanned)
        {
            var result = new NullCell(columnsSpanned: requiredNumberOfColumnsSpanned).ToFlatRow();

            return result;
        }
    }
}