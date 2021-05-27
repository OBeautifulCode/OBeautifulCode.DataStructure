// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BytesPayloadLinkedResource.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Linq;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A resource that is bundled within a link as payload of bytes
    /// and interpreted as a specified kind of resource (e.g. an image).
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class BytesPayloadLinkedResource : LinkedResourceBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BytesPayloadLinkedResource"/> class.
        /// </summary>
        /// <param name="bytes">The bytes payload.</param>
        /// <param name="resourceKind">The kind of resource.</param>
        public BytesPayloadLinkedResource(
            byte[] bytes,
            BytesPayloadLinkedResourceKind resourceKind)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (!bytes.Any())
            {
                throw new ArgumentException(Invariant($"{nameof(bytes)} is an empty enumerable."));
            }

            if (resourceKind == BytesPayloadLinkedResourceKind.Unknown)
            {
                throw new ArgumentException(Invariant($"{nameof(resourceKind)} is {nameof(BytesPayloadLinkedResourceKind.Unknown)}."));
            }

            this.Bytes = bytes;
            this.ResourceKind = resourceKind;
        }

        /// <summary>
        /// Gets the bytes payload.
        /// </summary>
        public byte[] Bytes { get; private set; }

        /// <summary>
        /// Gets the kind of resource.
        /// </summary>
        public BytesPayloadLinkedResourceKind ResourceKind { get; private set; }
    }
}
