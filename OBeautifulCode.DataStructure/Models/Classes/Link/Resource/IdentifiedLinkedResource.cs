// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentifiedLinkedResource.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace OBeautifulCode.DataStructure
{
    using System;
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
        /// <param name="kind">OPTIONAL kind of resource.  Can be used to group resources or qualify the identifier.  DEFAULT is no specified type.</param>
        public IdentifiedLinkedResource(
            string id,
            string kind = null)
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
            this.Kind = kind;
        }

        /// <inheritdoc />
        public string Id { get; private set; }

        /// <summary>
        /// Gets the kind of resource.  Can be used to group resources or qualify the identifier.
        /// </summary>
        public string Kind { get; private set; }
    }
}
