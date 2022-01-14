// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrlLinkedResourceKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies the kind of resource that is fetched from a server via a URL.
    /// </summary>
    public enum UrlLinkedResourceKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// A <see cref="Report"/>.
        /// </summary>
        Report,

        /// <summary>
        /// JSON document.
        /// </summary>
        Json,

        /// <summary>
        /// An image.
        /// </summary>
        Image,

        /// <summary>
        /// Audio.
        /// </summary>
        Audio,

        /// <summary>
        /// Video.
        /// </summary>
        Video,

        /// <summary>
        /// HTML.
        /// </summary>
        Html,
    }
}