// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BytesPayloadLinkedResource.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

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
