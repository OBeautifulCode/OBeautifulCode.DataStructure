// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// Implementation of <see cref="IOperationOutputCell{TValue}"/> with a standard set of features.
    /// </summary>
    /// <typeparam name="TValue">The type of output value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class OperationCell<TValue> : OperationOutputCellBase<TValue>, IHaveStandardCellFeatures<TValue>, IHaveLink, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationCell{TValue}"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="id">OPTIONAL unique identifier of the cell.  DEFAULT is a cell with no unique identifier.</param>
        /// <param name="columnsSpanned">OPTIONAL number of columns spanned or null if none (cell occupies a single column).  DEFAULT is none.</param>
        /// <param name="details">OPTIONAL details about the cell.  DEFAULT is to omit any details.</param>
        /// <param name="validation">OPTIONAL validation to perform.  DEFAULT is no validation.</param>
        /// <param name="validationEvents">OPTIONAL events that record the validation of this cell.  DEFAULT is a cell that has not yet been validated.</param>
        /// <param name="initialAvailability">OPTIONAL initial availability of the cell (before <paramref name="availabilityCheck"/> is run).  DEFAULT is an enabled cell.</param>
        /// <param name="availabilityCheck">OPTIONAL availability check to perform.  DEFAULT is a cell with no availability check.</param>
        /// <param name="availabilityCheckEvents">OPTIONAL events that record the availability checks on this cell.  DEFAULT is a cell that has not yet had an availability check.</param>
        /// <param name="operationExecutionEvents">OPTIONAL events that record the execution of <paramref name="operation"/>.  DEFAULT is a cell who's operation has not yet been executed.</param>
        /// <param name="valueFormat">OPTIONAL format to apply to the cell value.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="format">OPTIONAL format to apply to the cell.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="hoverOver">OPTIONAL hover-over for the cell.  DEFAULT is no hover-over.</param>
        /// <param name="link">OPTIONAL link to some resource.  DEFAULT is no link.</param>
        public OperationCell(
            IReturningOperation<TValue> operation,
            string id = null,
            int? columnsSpanned = null,
            string details = null,
            Validation validation = null,
            IReadOnlyList<CellValidationEventBase> validationEvents = null,
            Availability initialAvailability = Availability.Enabled,
            AvailabilityCheck availabilityCheck = null,
            IReadOnlyList<CellAvailabilityCheckEventBase> availabilityCheckEvents = null,
            IReadOnlyList<CellOpExecutionEventBase> operationExecutionEvents = null,
            ICellValueFormat<TValue> valueFormat = null,
            CellFormat format = null,
            IHoverOver hoverOver = null,
            ILink link = null)
            : base(id, columnsSpanned, details, validation, validationEvents, initialAvailability, availabilityCheck, availabilityCheckEvents, operation, operationExecutionEvents)
        {
            this.ValueFormat = valueFormat;
            this.Format = format;
            this.HoverOver = hoverOver;
            this.Link = link;
        }

        /// <inheritdoc />
        public ICellValueFormat<TValue> ValueFormat { get; private set; }

        /// <inheritdoc />
        public CellFormat Format { get; private set; }

        /// <inheritdoc />
        public IHoverOver HoverOver { get; private set; }

        /// <inheritdoc />
        public ILink Link { get; private set; }
    }
}