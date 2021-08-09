// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaReferenceKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies the kind of media reference.
    /// </summary>
    public enum MediaReferenceKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Image media.
        /// </summary>
        Image,

        /// <summary>
        /// Audio media.
        /// </summary>
        Audio,

        /// <summary>
        /// Video media.
        /// </summary>
        Video,
    }
}