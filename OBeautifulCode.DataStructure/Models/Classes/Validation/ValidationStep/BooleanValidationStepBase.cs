// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanValidationStepBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// A step in a series of steps that determine the validity of a subject based on evaluating an emitted boolean.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class BooleanValidationStepBase : ValidationStepBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanValidationStepBase"/> class.
        /// </summary>
        /// <param name="trueAction">OPTIONAL action to perform when executing the validation operation returns true.  DEFAULT is to move on and execute the next step in the chain.</param>
        /// <param name="falseAction">OPTIONAL action to perform when executing the validation operation returns false.  DEFAULT is to stop the validation (do not execute the next step in the chain) and determine that the subject is invalid.</param>
        /// <param name="details">OPTIONAL details about this validation step.  DEFAULT is to omit any details.</param>
        protected BooleanValidationStepBase(
            ValidationStepAction trueAction = ValidationStepAction.NextStep,
            ValidationStepAction falseAction = ValidationStepAction.StopAsInvalid,
            string details = null)
            : base(details)
        {
            if (trueAction == ValidationStepAction.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(trueAction)} is {nameof(ValidationStepAction.Unknown)}."));
            }

            if (falseAction == ValidationStepAction.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(falseAction)} is {nameof(ValidationStepAction.Unknown)}."));
            }

            this.TrueAction = trueAction;
            this.FalseAction = falseAction;
        }

        /// <summary>
        /// Gets the action to perform when executing the validation operation returns true.
        /// </summary>
        public ValidationStepAction TrueAction { get; private set; }

        /// <summary>
        /// Gets the action to perform when executing the validation operation returns false.
        /// </summary>
        public ValidationStepAction FalseAction { get; private set; }
    }
}