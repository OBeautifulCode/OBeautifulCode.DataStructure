// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentifiedLinkedResource.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// A resource that is referenced by an identifier.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class IdentifiedLinkedResource : LinkedResourceBase, IHaveStringId, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifiedLinkedResource"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="additionalInfoIdToValueMap">
        /// OPTIONAL map of additional information that supplements the specified <paramref name="id"/>.
        /// DEFAULT is no additional information.
        /// </param>
        public IdentifiedLinkedResource(
            string id,
            IReadOnlyDictionary<string, string> additionalInfoIdToValueMap = null)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(Invariant($"{nameof(id)} is white space."));
            }

            this.Id = id;
            this.AdditionalInfoIdToValueMap = additionalInfoIdToValueMap;
        }

        /// <inheritdoc />
        public string Id { get; private set; }

        /// <summary>
        /// Gets a map of additional information that supplements the specified <see cref="Id"/>.
        /// </summary>
        public IReadOnlyDictionary<string, string> AdditionalInfoIdToValueMap { get; private set; }
    }
}
