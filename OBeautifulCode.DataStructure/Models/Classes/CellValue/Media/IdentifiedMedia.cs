// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentifiedMedia.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Media that is referenced by an identifier.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class IdentifiedMedia : MediaBase, IHaveStringId, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifiedMedia"/> class.
        /// </summary>
        /// <param name="id">The identifier of the media.</param>
        /// <param name="mediaKind">The kind of media.</param>
        /// <param name="name">OPTIONAL name of the media.  DEFAULT is to use name-less media.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = ObcSuppressBecause.CA1054_UriParametersShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public IdentifiedMedia(
            string id,
            MediaKind mediaKind,
            string name = null)
            : base(mediaKind, name)
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
        }

        /// <inheritdoc />
        public string Id { get; private set; }
    }
}