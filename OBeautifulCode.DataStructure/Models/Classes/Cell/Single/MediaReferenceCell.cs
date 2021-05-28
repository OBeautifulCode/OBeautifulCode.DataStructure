// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaReferenceCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// A cell that contains media via a reference to that media (i.e. must be fetched from a server).
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MediaReferenceCell : StandardCellBase, IHaveValueCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaReferenceCell"/> class.
        /// </summary>
        /// <param name="mediaReference">The media reference.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public MediaReferenceCell(
            MediaReference mediaReference,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(format, hoverOver, link)
        {
            if (mediaReference == null)
            {
                throw new ArgumentNullException(nameof(mediaReference));
            }

            this.MediaReference = mediaReference;
        }

        /// <summary>
        /// Gets the media reference.
        /// </summary>
        public MediaReference MediaReference { get; private set; }

        /// <inheritdoc />
        public object GetCellValue() => this.MediaReference;
    }
}