// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkedMedia.cs" company="OBeautifulCode">
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
    /// A link to media (i.e. must be fetched from a server).
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class LinkedMedia : MediaBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedMedia"/> class.
        /// </summary>
        /// <param name="url">The url of the media.</param>
        /// <param name="mediaKind">The kind of media.</param>
        /// <param name="name">OPTIONAL name of the media.  DEFAULT is to use name-less media.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = ObcSuppressBecause.CA1054_UriParametersShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public LinkedMedia(
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
        /// Gets the url of the media.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = ObcSuppressBecause.CA1056_UriPropertiesShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public string Url { get; private set; }
    }
}