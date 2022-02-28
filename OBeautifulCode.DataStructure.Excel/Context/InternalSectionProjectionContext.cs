// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InternalSectionProjectionContext.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Excel
{
    /// <summary>
    /// An internal context object for the projection of a <see cref="Section"/>.
    /// </summary>
    public class InternalSectionProjectionContext
    {
        /// <summary>
        /// Gets or sets a value indicating whether auto-filters are used.
        /// </summary>
        public bool UsesAutoFilter { get; set; }
    }
}