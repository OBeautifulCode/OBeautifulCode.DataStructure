// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// Implementation of <see cref="INullCell"/> with a standard set of features.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class NullCell : NullCellBase, IHaveStandardCellFeatures, IHaveLink, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullCell"/> class.
        /// </summary>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="validation">OPTIONAL validation to perform.  DEFAULT is no validation.</param>
        /// <param name="validationEvents">OPTIONAL events that record the validation of this cell.  DEFAULT is a cell that has not yet been validated.</param>
        /// <param name="defaultAvailability">OPTIONAL default availability of the cell (before <paramref name="availabilityCheck"/> is run).  DEFAULT is an enabled cell.</param>
        /// <param name="availabilityCheck">OPTIONAL availability check to perform.  DEFAULT is a cell with no availability check.</param>
        /// <param name="availabilityCheckEvents">OPTIONAL events that record the availability checks on this cell.  DEFAULT is a cell that has not yet had an availability check.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public NullCell(
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            Validation validation = null,
            IReadOnlyList<CellValidationEventBase> validationEvents = null,
            Availability defaultAvailability = Availability.Enabled,
            AvailabilityCheck availabilityCheck = null,
            IReadOnlyList<CellAvailabilityCheckEventBase> availabilityCheckEvents = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(id, columnsSpanned, details, validation, validationEvents, defaultAvailability, availabilityCheck, availabilityCheckEvents)
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
        public override Type GetValueTypeOrNull() => null;
    }
}