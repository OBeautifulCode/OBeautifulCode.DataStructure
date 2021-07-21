// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullCellBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Base implementation of <see cref="INullCell"/>.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class NullCellBase : CellBase, INullCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullCellBase"/> class.
        /// </summary>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        protected NullCellBase(
            string id = null,
            int? columnsSpanned = null)
            : base(id, columnsSpanned)
        {
        }
    }
}