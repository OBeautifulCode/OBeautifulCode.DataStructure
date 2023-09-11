// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Base class implementation of <see cref="IMedia"/>.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class MediaBase : IMedia, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaBase"/> class.
        /// </summary>
        /// <param name="mediaKind">The kind of media.</param>
        /// <param name="name">OPTIONAL name of the media.  DEFAULT is to use name-less media.</param>
        protected MediaBase(
            MediaKind mediaKind,
            string name = null)
        {
            if (mediaKind == MediaKind.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(mediaKind)} is {nameof(MediaKind.Unknown)}."));
            }

            this.MediaKind = mediaKind;
            this.Name = name;
        }

        /// <summary>
        /// Gets the kind of media.
        /// </summary>
        public MediaKind MediaKind { get; private set; }

        /// <summary>
        /// Gets the name of the media.
        /// </summary>
        public string Name { get; private set; }
    }
}