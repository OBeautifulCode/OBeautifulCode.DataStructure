// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BytesPayloadLinkedResourceKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies how to interpret a payload of bytes that is bundled within a link.
    /// </summary>
    public enum BytesPayloadLinkedResourceKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// An image.
        /// </summary>
        Image,

        /// <summary>
        /// ZIP file.
        /// </summary>
        Zip,
    }
}