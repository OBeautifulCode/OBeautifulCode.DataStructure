// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILinkedCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// A cell that is linked to some resource.
    /// </summary>
    public interface ILinkedCell : ICell
    {
        /// <summary>
        /// Gets the link to some resource.
        /// </summary>
        ILink Link { get; }
    }
}
