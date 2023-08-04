// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TargetedLinkBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using static System.FormattableString;

    /// <summary>
    /// Base class for a link to a resource with instructions on where/how the resource is displayed/experienced.
    /// </summary>
    public abstract partial class TargetedLinkBase : LinkBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TargetedLinkBase"/> class.
        /// </summary>
        /// <param name="target">A value that specifies where/how a linked resource is displayed/experienced.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="formatsToApplyWhenActivated">OPTIONAL formatting to apply, in order, when the link is activated (e.g. clicked).  DEFAULT is to leave the formatting unchanged.</param>
        protected TargetedLinkBase(
            LinkTarget target,
            ILinkedResource resource,
            IReadOnlyList<RegionFormatBase> formatsToApplyWhenActivated = null)
            : base(resource, formatsToApplyWhenActivated)
        {
            if (target == LinkTarget.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(target)} is {nameof(LinkTarget.Unknown)}."));
            }

            this.Target = target;
        }

        /// <summary>
        /// Gets a value that specifies where/how a linked resource is displayed/experienced.
        /// </summary>
        public LinkTarget Target { get; private set; }
    }
}