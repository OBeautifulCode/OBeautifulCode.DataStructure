// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveLink.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies a cell's link to some resource.
    /// </summary>
    public interface IHaveLink
    {
        /// <summary>
        /// Gets the link to some resource.
        /// </summary>
        ILink Link { get; }
    }
}
