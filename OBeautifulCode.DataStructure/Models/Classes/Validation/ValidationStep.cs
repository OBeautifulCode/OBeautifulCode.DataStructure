// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationStep.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A step in a series of steps that determine the validity of a subject.
    /// </summary>
    public partial class ValidationStep : IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationStep"/> class.
        /// </summary>
        /// <param name="operation">The operation to execute whose result determines what to do as specified by <paramref name="trueAction"/> and <paramref name="falseAction"/>.</param>
        /// <param name="stopMessageOp">OPTIONAL operation to execute to get the message that should be emitted when this validation step stops the validation (prevents the next step in the chain from executing).  DEFAULT is to omit this message.</param>
        /// <param name="trueAction">OPTIONAL action to perform when executing <paramref name="operation"/> returns true.  DEFAULT is to move on and execute the next step in the chain.</param>
        /// <param name="falseAction">OPTIONAL action to perform when executing <paramref name="operation"/> returns false.  DEFAULT is to stop the validation (do not execute the next step in the chain) and determine that the subject is invalid.</param>
        /// <param name="details">OPTIONAL details about this validation step.  DEFAULT is to omit any details.</param>
        public ValidationStep(
            IReturningOperation<bool> operation,
            IReturningOperation<string> stopMessageOp = null,
            ValidationStepAction trueAction = ValidationStepAction.NextStep,
            ValidationStepAction falseAction = ValidationStepAction.StopAsInvalid,
            string details = null)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (trueAction == ValidationStepAction.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(trueAction)} is {nameof(ValidationStepAction.Unknown)}."));
            }

            if (falseAction == ValidationStepAction.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(falseAction)} is {nameof(ValidationStepAction.Unknown)}."));
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
        /// Gets the operation to execute to get the message that should be emitted when this validation step stops the validation (prevents the next step in the chain from executing).
        /// </summary>
        public IReturningOperation<string> StopMessageOp { get; private set; }

        /// <summary>
        /// Gets the action to perform when executing <see cref="Operation"/> returns true.
        /// </summary>
        public ValidationStepAction TrueAction { get; private set; }

        /// <summary>
        /// Gets the action to perform when executing <see cref="Operation"/> returns false.
        /// </summary>
        public ValidationStepAction FalseAction { get; private set; }

        /// <summary>
        /// Gets details about this validation step.
        /// </summary>
        public string Details { get; private set; }
    }
}