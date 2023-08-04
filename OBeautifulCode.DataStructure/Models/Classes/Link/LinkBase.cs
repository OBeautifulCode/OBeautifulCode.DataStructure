// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Base implementation of <see cref="ILink"/>.
    /// </summary>
    public abstract partial class LinkBase : ILink, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkBase"/> class.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="formatsToApplyWhenActivated">OPTIONAL formatting to apply, in order, when the link is activated (e.g. clicked).  DEFAULT is to leave the formatting unchanged.</param>
        protected LinkBase(
            ILinkedResource resource,
            IReadOnlyList<RegionFormatBase> formatsToApplyWhenActivated = null)
        {
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

            this.Resource = resource;
            this.FormatsToApplyWhenActivated = formatsToApplyWhenActivated;
        }

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
