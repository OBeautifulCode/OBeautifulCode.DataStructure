// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AvailabilityCheckChain.cs" company="OBeautifulCode">
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
    /// A series of steps that determine the availability of a subject.
    /// </summary>
    public partial class AvailabilityCheckChain : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityCheckChain"/> class.
        /// </summary>
        /// <param name="steps">The individual availability check steps.</param>
        /// <param name="endMessageOp">OPTIONAL operation to execute to get the message that should be emitted when all <paramref name="steps"/> have been evaluated and none have stopped the availability check (we've reached the end of the chain).  DEFAULT is to omit this message.</param>
        /// <param name="endAvailability">OPTIONAL value that specifies the availability of the subject when all <paramref name="steps"/> have been evaluated and none have stopped the availability check (we've reached the end of the chain).  DEFAULT is to determine that the subject is enabled.</param>
        public AvailabilityCheckChain(
            IReadOnlyList<AvailabilityCheckStep> steps,
            IReturningOperation<string> endMessageOp = null,
            Availability endAvailability = Availability.Enabled)
        {
            if (steps == null)
            {
                throw new ArgumentNullException(nameof(steps));
            }

            if (!steps.Any())
            {
                throw new ArgumentException(Invariant($"{nameof(steps)} is an empty enumerable."));
            }

            if (steps.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(steps)} contains at least one null element."));
            }

            if (endAvailability == Availability.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(endAvailability)} is {nameof(Availability.Unknown)}."));
            }

            this.Steps = steps;
            this.EndMessageOp = endMessageOp;
            this.EndAvailability = endAvailability;
        }

        /// <summary>
        /// Gets the individual availability check steps.
        /// </summary>
        public IReadOnlyList<AvailabilityCheckStep> Steps { get; private set; }

        /// <summary>
        /// Gets the operation to execute to get the message that should be emitted when all <see cref="Steps"/> have been evaluated and none have stopped the availability check (we've reached the end of the chain).
        /// </summary>
        public IReturningOperation<string> EndMessageOp { get; private set; }

        /// <summary>
        /// Gets a value that specifies the availability of the subject when all <see cref="Steps"/> have been evaluated and none have stopped the availability check (we've reached the end of the chain).
        /// </summary>
        public Availability EndAvailability { get; private set; }
    }
}