// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SourcedMedia.cs" company="OBeautifulCode">
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
    /// Media with a source url (i.e. fetched from a server).
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class SourcedMedia : MediaBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourcedMedia"/> class.
        /// </summary>
        /// <param name="url">The source url of the media.</param>
        /// <param name="mediaKind">The kind of media.</param>
        /// <param name="name">OPTIONAL name of the media.  DEFAULT is to use name-less media.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = ObcSuppressBecause.CA1054_UriParametersShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public SourcedMedia(
            string url,
            MediaKind mediaKind,
            string name = null)
            : base(mediaKind, name)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(Invariant($"{nameof(url)} is white space."));
            }

            this.Url = url;
        }

        /// <summary>
        /// Gets the source url of the media.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = ObcSuppressBecause.CA1056_UriPropertiesShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public string Url { get; private set; }
    }
}