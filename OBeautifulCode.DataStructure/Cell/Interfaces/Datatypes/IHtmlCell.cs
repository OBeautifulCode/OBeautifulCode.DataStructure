// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHtmlCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// A cell that contains an HTML value.
    /// </summary>
    public interface IHtmlCell
    {
        /// <summary>
        /// Gets the cell's HTML value.
        /// </summary>
        string Html { get; }
    }
}
