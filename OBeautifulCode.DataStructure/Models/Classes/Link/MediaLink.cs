// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaLink.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Media (e.g. an icon) that is linked to a resource with instructions on where/how the resource is displayed/experienced.
    /// </summary>
    public partial class MediaLink : TargetedLinkBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaLink"/> class.
        /// </summary>
        /// <param name="media">The media that represents the link.</param>
        /// <param name="target">A value that specifies where/how a linked resource is displayed/experienced.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="formatsToApplyWhenActivated">OPTIONAL formatting to apply, in order, when the link is activated (e.g. clicked).  DEFAULT is to leave the formatting unchanged.</param>
        public MediaLink(
            MediaBase media,
            LinkTarget target,
            ILinkedResource resource,
            IReadOnlyList<RegionFormatBase> formatsToApplyWhenActivated = null)
            : base(target, resource, formatsToApplyWhenActivated)
        {
            if (media == null)
            {
                throw new ArgumentNullException(nameof(media));
            }

            this.Media = media;
        }

        /// <summary>
        /// Gets the media that represents the link.
        /// </summary>
        public MediaBase Media { get; private set; }
    }
}