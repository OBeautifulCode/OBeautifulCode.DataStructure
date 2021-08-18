// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// A cell in a tree table.
    /// </summary>
    public interface ICell : IHaveStringId
    {
        /// <summary>
        /// Gets the number of columns spanned by a cell or null if none (cell occupies a single column).
        /// </summary>
        int? ColumnsSpanned { get; }

        /// <summary>
        /// Gets the type of value that the cell stores or null if not applicable.
        /// </summary>
        /// <returns>
        /// The type of value that the cell stores or null if not applicable.
        /// </returns>
        Type GetValueTypeOrNull();
    }
}
