﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleLink.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    /// <summary>
    /// A link to a resource with instructions on where/how the resource is displayed/experienced.
    /// </summary>
    public partial class SimpleLink : TargetedLinkBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleLink"/> class.
        /// </summary>
        /// <param name="target">A value that specifies where/how a linked resource is displayed/experienced.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="formatsToApplyWhenActivated">OPTIONAL formatting to apply, in order, when the link is activated (e.g. clicked).  DEFAULT is to leave the formatting unchanged.</param>
        public SimpleLink(
            LinkTarget target,
            ILinkedResource resource,
            IReadOnlyList<RegionFormatBase> formatsToApplyWhenActivated = null)
            : base(target, resource, formatsToApplyWhenActivated)
        {
        }
    }
}