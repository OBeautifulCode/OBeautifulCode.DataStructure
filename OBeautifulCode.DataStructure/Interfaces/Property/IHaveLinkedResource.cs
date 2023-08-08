// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveLinkedResource.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies a resource that is linked.
    /// </summary>
    public interface IHaveLinkedResource
    {
        /// <summary>
        /// Gets the resource.
        /// </summary>
        ILinkedResource Resource { get; }
    }
}
