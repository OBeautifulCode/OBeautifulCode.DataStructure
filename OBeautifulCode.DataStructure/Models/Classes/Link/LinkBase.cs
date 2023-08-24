// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Base implementation of <see cref="ILink"/>.
    /// </summary>
    public abstract partial class LinkBase : ILink, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkBase"/> class.
        /// </summary>
        /// <param name="resource">The resource.</param>
        protected LinkBase(
            ILinkedResource resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            this.Resource = resource;
        }

        /// <inheritdoc />
        public ILinkedResource Resource { get; private set; }
    }
}
