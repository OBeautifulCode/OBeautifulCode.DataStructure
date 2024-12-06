// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationChain.cs" company="OBeautifulCode">
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
    /// A series of steps that determine the validity of a subject.
    /// </summary>
    public partial class ValidationChain : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationChain"/> class.
        /// </summary>
        /// <param name="steps">The individual validation steps.</param>
        /// <param name="endMessageOp">OPTIONAL operation to execute to get the message that should be emitted when all <paramref name="steps"/> have been evaluated and none have stopped the validation (we've reached the end of the chain).  DEFAULT is to omit this message.</param>
        /// <param name="endValidity">OPTIONAL value that specifies the validity of the subject when all <paramref name="steps"/> have been evaluated and none have stopped the validation (we've reached the end of the chain).  DEFAULT is to determine that the subject is valid.</param>
        public ValidationChain(
            IReadOnlyList<ValidationStepBase> steps,
            IReturningOperation<string> endMessageOp = null,
            Validity endValidity = Validity.Valid)
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

            if (endValidity == Validity.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(endValidity)} is {nameof(Validity.Unknown)}."));
            }

            this.Steps = steps;
            this.EndMessageOp = endMessageOp;
            this.EndValidity = endValidity;
        }

        /// <summary>
        /// Gets the individual validation steps.
        /// </summary>
        public IReadOnlyList<ValidationStepBase> Steps { get; private set; }

        /// <summary>
        /// Gets the operation to execute to get the message that should be emitted when all <see cref="Steps"/> have been evaluated and none have stopped the validation (we've reached the end of the chain).
        /// </summary>
        public IReturningOperation<string> EndMessageOp { get; private set; }

        /// <summary>
        /// Gets a value that specifies the validity of the subject when all <see cref="Steps"/> have been evaluated and none have stopped the validation (we've reached the end of the chain).
        /// </summary>
        public Validity EndValidity { get; private set; }
    }
}