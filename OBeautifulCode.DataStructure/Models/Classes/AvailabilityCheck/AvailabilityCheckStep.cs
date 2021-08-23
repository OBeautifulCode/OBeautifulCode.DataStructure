// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AvailabilityCheckStep.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A step in a series of steps that determine the availability of a subject.
    /// </summary>
    public partial class AvailabilityCheckStep : IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityCheckStep"/> class.
        /// </summary>
        /// <param name="operation">The operation to execute whose result determines what to do as specified by <paramref name="trueAction"/> and <paramref name="falseAction"/>.</param>
        /// <param name="stopMessageOp">OPTIONAL operation to execute to get the message that should be emitted when this availability check step stops the availability check (prevents the next step in the chain from executing).  DEFAULT is to omit this message.</param>
        /// <param name="trueAction">OPTIONAL action to perform when executing <paramref name="operation"/> returns true.  DEFAULT is to move on and execute the next step in the chain.</param>
        /// <param name="falseAction">OPTIONAL action to perform when executing <paramref name="operation"/> returns false.  DEFAULT is to stop the availability check (do not execute the next step in the chain) and determine that the subject is disabled.</param>
        /// <param name="details">OPTIONAL details about this availability check step.  DEFAULT is to omit any details.</param>
        public AvailabilityCheckStep(
            IReturningOperation<bool> operation,
            IReturningOperation<string> stopMessageOp = null,
            AvailabilityCheckStepAction trueAction = AvailabilityCheckStepAction.NextStep,
            AvailabilityCheckStepAction falseAction = AvailabilityCheckStepAction.StopAsDisabled,
            string details = null)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (trueAction == AvailabilityCheckStepAction.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(trueAction)} is {nameof(AvailabilityCheckStepAction.Unknown)}."));
            }

            if (falseAction == AvailabilityCheckStepAction.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(falseAction)} is {nameof(AvailabilityCheckStepAction.Unknown)}."));
            }

            this.Operation = operation;
            this.StopMessageOp = stopMessageOp;
            this.TrueAction = trueAction;
            this.FalseAction = falseAction;
            this.Details = details;
        }

        /// <summary>
        /// Gets The operation to execute whose result determines what to do as specified by <see cref="TrueAction"/> and <see cref="FalseAction"/>.
        /// </summary>
        public IReturningOperation<bool> Operation { get; private set; }

        /// <summary>
        /// Gets the operation to execute to get the message that should be emitted when this availability check step stops the availability check (prevents the next step in the chain from executing).
        /// </summary>
        public IReturningOperation<string> StopMessageOp { get; private set; }

        /// <summary>
        /// Gets the action to perform when executing <see cref="Operation"/> returns true.
        /// </summary>
        public AvailabilityCheckStepAction TrueAction { get; private set; }

        /// <summary>
        /// Gets the action to perform when executing <see cref="Operation"/> returns false.
        /// </summary>
        public AvailabilityCheckStepAction FalseAction { get; private set; }

        /// <summary>
        /// Gets details about this availability check step.
        /// </summary>
        public string Details { get; private set; }
    }
}