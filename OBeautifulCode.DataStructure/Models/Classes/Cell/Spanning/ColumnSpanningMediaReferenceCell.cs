// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnSpanningMediaReferenceCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// A cell that contains media via a reference to that media (i.e. must be fetched from a server) and spans multiple columns.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ColumnSpanningMediaReferenceCell : ColumnSpanningStandardCellBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSpanningMediaReferenceCell"/> class.
        /// </summary>
        /// <param name="mediaReference">The media reference.</param>
        /// <param name="columnsSpanned">The number of columns spanned.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public ColumnSpanningMediaReferenceCell(
            MediaReference mediaReference,
            int columnsSpanned,
            string id = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(columnsSpanned, id, format, hoverOver, link)
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
        public override object GetCellValue() => this.MediaReference;
    }
}