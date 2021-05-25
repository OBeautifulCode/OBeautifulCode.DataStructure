// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleLink.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// A reference to a resource with instructions on where/how it is displayed/experienced.
    /// </summary>
    public class SimpleLink : LinkBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleLink"/> class.
        /// </summary>
        /// <param name="target">A value that specifies where/how a linked resource is displayed/experienced.</param>
        /// <param name="resource">The resource.</param>
        public SimpleLink(
            LinkTarget target,
            ILinkedResource resource)
        {
            if (target == LinkTarget.Unknown)
            {
                throw new ArgumentException(Invariant($"{nameof(target)} is {nameof(LinkTarget.Unknown)}."));
            }

            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            this.Target = target;
            this.Resource = resource;
        }

        /// <summary>
        /// Gets a value that specifies where/how a linked resource is displayed/experienced.
        /// </summary>
        public LinkTarget Target { get; private set; }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        public ILinkedResource Resource { get; private set; }
    }
}