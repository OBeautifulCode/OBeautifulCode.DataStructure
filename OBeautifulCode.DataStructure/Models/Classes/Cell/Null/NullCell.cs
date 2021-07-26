// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Implementation of <see cref="INullCell"/> with a standard set of features.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class NullCell : NullCellBase, IHaveStandardCellFeatures, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullCell"/> class.
        /// </summary>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public NullCell(
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(id, columnsSpanned, details)
        {
            this.Format = format;
            this.HoverOver = hoverOver;
            this.Link = link;
        }

        /// <inheritdoc />
        public CellFormat Format { get; private set; }

        /// <inheritdoc />
        public IHoverOver HoverOver { get; private set; }

        /// <inheritdoc />
        public ILink Link { get; private set; }

        /// <inheritdoc />
        public override bool IsConstCell() => false;

        /// <inheritdoc />
        public override bool IsInputCell() => false;

        /// <inheritdoc />
        public override bool IsOperationCell() => false;

        /// <inheritdoc />
        public override Type GetValueTypeOrNull() => null;
    }
}