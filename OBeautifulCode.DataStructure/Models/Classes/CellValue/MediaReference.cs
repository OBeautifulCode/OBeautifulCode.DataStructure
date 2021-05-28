// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaReference.cs" company="OBeautifulCode">
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
    /// A a reference to that media (i.e. must be fetched from a server).
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MediaReference : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaReference"/> class.
        /// </summary>
        /// <param name="url">The url of the media reference.</param>
        /// <param name="mediaReferenceKind">The kind of media reference.</param>
        /// <param name="name">OPTIONAL name of the media.  DEFAULT is to use name-less media.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = ObcSuppressBecause.CA1054_UriParametersShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public MediaReference(
            string url,
            MediaReferenceKind mediaReferenceKind,
            string name = null)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(Invariant($"{nameof(url)} is white space."));
            }

            if (mediaReferenceKind == MediaReferenceKind.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(mediaReferenceKind)} is {nameof(MediaReferenceKind.Unknown)}."));
            }

            this.Url = url;
            this.MediaReferenceKind = mediaReferenceKind;
            this.Name = name;
        }

        /// <summary>
        /// Gets the url of the media reference.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = ObcSuppressBecause.CA1056_UriPropertiesShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public string Url { get; private set; }

        /// <summary>
        /// Gets the kind of media reference.
        /// </summary>
        public MediaReferenceKind MediaReferenceKind { get; private set; }

        /// <summary>
        /// Gets the name of the media.
        /// </summary>
        public string Name { get; private set; }
    }
}