// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleLink.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static System.FormattableString;

    /// <summary>
    /// A reference to a resource with instructions on where/how it is displayed/experienced.
    /// </summary>
    public partial class SimpleLink : LinkBase
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
        {
            if (target == LinkTarget.Unknown)
            {
                throw new ArgumentException(Invariant($"{nameof(target)} is {nameof(LinkTarget.Unknown)}."));
            }

            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            if (formatsToApplyWhenActivated != null)
            {
                if (!formatsToApplyWhenActivated.Any())
                {
                    throw new ArgumentException(Invariant($"{nameof(formatsToApplyWhenActivated)} is an empty enumerable."));
                }

                if (formatsToApplyWhenActivated.Any(_ => _ == null))
                {
                    throw new ArgumentException(Invariant($"{nameof(formatsToApplyWhenActivated)} contains at least one null element."));
                }
            }

            this.Target = target;
            this.Resource = resource;
            this.FormatsToApplyWhenActivated = formatsToApplyWhenActivated;
        }

        /// <summary>
        /// Gets a value that specifies where/how a linked resource is displayed/experienced.
        /// </summary>
        public LinkTarget Target { get; private set; }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        public ILinkedResource Resource { get; private set; }

        /// <summary>
        /// Gets the formatting to apply, in order, when the link is activated (e.g. clicked).
        /// </summary>
        public IReadOnlyList<RegionFormatBase> FormatsToApplyWhenActivated { get; private set; }
    }
}