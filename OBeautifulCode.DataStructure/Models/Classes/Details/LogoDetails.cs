// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogoDetails.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Details about a logo.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class LogoDetails : DetailsBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogoDetails"/> class.
        /// </summary>
        /// <param name="media">The media of the logo.</param>
        public LogoDetails(
            MediaBase media)
        {
            if (media == null)
            {
                throw new ArgumentNullException(nameof(media));
            }

            if (media.MediaKind != MediaKind.Image)
            {
                throw new ArgumentException(Invariant($"{nameof(media)} must be {nameof(MediaKind.Image)}.  Specified value is: {media.MediaKind}"));
            }

            this.Media = media;
        }

        /// <summary>
        /// Gets the media of the logo.
        /// </summary>
        public MediaBase Media { get; private set; }
    }
}
