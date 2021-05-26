// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringPayloadLinkedResource.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A resource that is bundled within a link as a string payload
    /// and interpreted as a specified kind of resource (e.g. JSON).
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class StringPayloadLinkedResource : LinkedResourceBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringPayloadLinkedResource"/> class.
        /// </summary>
        /// <param name="value">The string payload value.</param>
        /// <param name="resourceKind">The kind of resource.</param>
        public StringPayloadLinkedResource(
            string value,
            StringPayloadLinkedResourceKind resourceKind)
        {
            if (resourceKind == StringPayloadLinkedResourceKind.Unknown)
            {
                throw new ArgumentException(Invariant($"{nameof(resourceKind)} is {nameof(StringPayloadLinkedResourceKind.Unknown)}."));
            }

            this.Value = value;
            this.ResourceKind = resourceKind;
        }

        /// <summary>
        /// Gets the string payload value.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Gets the kind of resource.
        /// </summary>
        public StringPayloadLinkedResourceKind ResourceKind { get; private set; }
    }
}
