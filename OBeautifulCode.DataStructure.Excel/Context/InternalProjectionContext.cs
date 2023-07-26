// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InternalProjectionContext.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Excel
{
    using System.Collections.Generic;

    /// <summary>
    /// An internal context object for use when projecting.
    /// </summary>
    public class InternalProjectionContext
    {
        /// <summary>
        /// Gets or sets the external context.
        /// </summary>
        public ReportToWorkbookProjectionContext ExternalContext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether auto-filters are used.
        /// </summary>
        public bool UsesAutoFilter { get; set; }

        /// <summary>
        /// Gets or sets the stack of tree levels with the root rows being level 0.
        /// </summary>
        public Stack<int> TreeLevelStack { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to disable the indention that
        /// occurs by default and determined by the level in the tree.
        /// </summary>
        public bool DisableIndentationByTreeLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to disable the ability
        /// to collapse child rows.
        /// </summary>
        public bool DisableCollapsingOfChildRows { get; set; }

        /// <summary>
        /// Gets or sets a stack of values indicating whether to align child rows with their parent.
        /// </summary>
        public Stack<bool> AlignChildRowsWithParentStack { get; set; }
    }
}