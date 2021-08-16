// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotSlottedCellBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Base implementation of <see cref="INotSlottedCell"/>.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class NotSlottedCellBase : CellBase, INotSlottedCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotSlottedCellBase"/> class.
        /// </summary>
        /// <param name="id">The cell's unique identifier.</param>
        /// <param name="columnsSpanned">The number of columns spanned or null if none (cell occupies a single column).</param>
        /// <param name="details">Details about the cell.</param>
        /// <param name="validation">The validation to perform.</param>
        /// <param name="validationEvents">The events that record the validation of this cell.</param>
        /// <param name="defaultAvailability">The default availability of the cell (before <paramref name="availabilityCheck"/> is run).</param>
        /// <param name="availabilityCheck">The availability check to perform.</param>
        /// <param name="availabilityCheckEvents">The events that record the availability checks on this cell.</param>
        protected NotSlottedCellBase(
            string id,
            int? columnsSpanned,
            string details,
            Validation validation,
            IReadOnlyList<CellValidationEventBase> validationEvents,
            Availability defaultAvailability,
            AvailabilityCheck availabilityCheck,
            IReadOnlyList<CellAvailabilityCheckEventBase> availabilityCheckEvents)
            : base(id, columnsSpanned, details)
        {
            if ((validationEvents != null) && validationEvents.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(validationEvents)} contains a null element."));
            }

            if ((validation == null) && (validationEvents != null) && validationEvents.Any())
            {
                throw new ArgumentException(Invariant($"There is no {nameof(validation)} specified, however one or more {nameof(validationEvents)} exists."));
            }

            if ((defaultAvailability != Availability.Enabled) && (defaultAvailability != Availability.Disabled))
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(defaultAvailability)} is neither {nameof(Availability)}.{nameof(Availability.Enabled)} nor {nameof(Availability)}.{nameof(Availability.Disabled)}."));
            }

            if ((availabilityCheckEvents != null) && availabilityCheckEvents.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(availabilityCheckEvents)} contains a null element."));
            }

            if ((availabilityCheck == null) && (availabilityCheckEvents != null) && availabilityCheckEvents.Any())
            {
                throw new ArgumentException(Invariant($"There is no {nameof(availabilityCheck)} specified, however one or more {nameof(availabilityCheckEvents)} exists."));
            }

            this.Validation = validation;
            this.ValidationEvents = validationEvents;
            this.DefaultAvailability = defaultAvailability;
            this.AvailabilityCheck = availabilityCheck;
            this.AvailabilityCheckEvents = availabilityCheckEvents;
        }

        /// <inheritdoc />
        public Validation Validation { get; private set; }

        /// <inheritdoc />
        public IReadOnlyList<CellValidationEventBase> ValidationEvents { get; private set; }

        /// <inheritdoc />
        public Availability DefaultAvailability { get; private set; }

        /// <inheritdoc />
        public IReadOnlyList<CellAvailabilityCheckEventBase> AvailabilityCheckEvents { get; private set; }

        /// <inheritdoc />
        public AvailabilityCheck AvailabilityCheck { get; private set; }

        /// <inheritdoc />
        public void Record(
            CellValidationEventBase validationEvent)
        {
            if (validationEvent == null)
            {
                throw new ArgumentNullException(nameof(validationEvent));
            }

            if (this.Validation == null)
            {
                throw new InvalidOperationException(Invariant($"Cannot record {nameof(validationEvent)} when there is no {nameof(this.Validation)}."));
            }

            this.ValidationEvents = new CellValidationEventBase[0]
                .Concat(this.ValidationEvents ?? new CellValidationEventBase[0])
                .Concat(new[] { validationEvent })
                .ToList();
        }

        /// <inheritdoc />
        public void Record(
            CellAvailabilityCheckEventBase availabilityCheckEvent)
        {
            if (availabilityCheckEvent == null)
            {
                throw new ArgumentNullException(nameof(availabilityCheckEvent));
            }

            if (this.AvailabilityCheck == null)
            {
                throw new InvalidOperationException(Invariant($"Cannot record {nameof(availabilityCheckEvent)} when there is no {nameof(this.AvailabilityCheck)}."));
            }

            this.AvailabilityCheckEvents = new CellAvailabilityCheckEventBase[0]
                .Concat(this.AvailabilityCheckEvents ?? new CellAvailabilityCheckEventBase[0])
                .Concat(new[] { availabilityCheckEvent })
                .ToList();
        }

        /// <inheritdoc />
        public void ClearValidation(
            DateTime timestampUtc,
            string details)
        {
            if (this.Validation != null)
            {
                var cellValidationClearedEvent = new CellValidationClearedEvent(timestampUtc, details);

                this.Record(cellValidationClearedEvent);
            }
        }

        /// <inheritdoc />
        public void ClearAvailabilityCheck(
            DateTime timestampUtc,
            string details)
        {
            if (this.AvailabilityCheck != null)
            {
                var cellAvailabilityCheckClearedEvent = new CellAvailabilityCheckClearedEvent(timestampUtc, details);

                this.Record(cellAvailabilityCheckClearedEvent);
            }
        }
    }
}