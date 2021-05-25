// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringPayloadLinkedResourceKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies how to interpret a string payload that is bundled within a link.
    /// </summary>
    public enum StringPayloadLinkedResourceKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The resource is JSON.
        /// </summary>
        Json,

        /// <summary>
        /// The resource is HTML.
        /// </summary>
        Html,

        /// <summary>
        /// The resource is a string.
        /// </summary>
        String,
    }
}