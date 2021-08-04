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
        protected NotSlottedCellBase(
            string id,
            int? columnsSpanned,
            string details,
            Validation validation,
            IReadOnlyList<CellValidationEventBase> validationEvents)
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

            this.Validation = validation;
            this.ValidationEvents = validationEvents;
        }

        /// <inheritdoc />
        public Validation Validation { get; private set; }

        /// <inheritdoc />
        public IReadOnlyList<CellValidationEventBase> ValidationEvents { get; private set; }

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
    }
}