// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardLink.cs" company="OBeautifulCode">
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
    /// A link to a resource with instructions on where/how the resource is displayed/experienced.
    /// </summary>
    public partial class StandardLink : LinkBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardLink"/> class.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="target">OPTIONAL value that specifies where/how a linked resource is displayed/experienced.  DEFAULT is to leave the target unspecified.</param>
        /// <param name="formatsToApplyWhenActivated">OPTIONAL formatting to apply, in order, when the link is activated (e.g. clicked).  DEFAULT is to leave the formatting unchanged.</param>
        public StandardLink(
            ILinkedResource resource,
            LinkTarget? target = null,
            IReadOnlyList<RegionFormatBase> formatsToApplyWhenActivated = null)
            : base(resource)
        {
            if (target == LinkTarget.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(target)} is {nameof(LinkTarget.Unknown)}."));
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
            this.FormatsToApplyWhenActivated = formatsToApplyWhenActivated;
        }

        /// <summary>
        /// Gets a value that specifies where/how a linked resource is displayed/experienced.
        /// </summary>
        public LinkTarget? Target { get; private set; }

        /// <summary>
        /// Gets the formatting to apply, in order, when the link is activated (e.g. clicked).
        /// </summary>
        public IReadOnlyList<RegionFormatBase> FormatsToApplyWhenActivated { get; private set; }
    }
}