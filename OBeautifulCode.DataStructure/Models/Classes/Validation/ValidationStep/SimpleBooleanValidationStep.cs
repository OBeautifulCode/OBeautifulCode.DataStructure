﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleBooleanValidationStep.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.Type;

    /// <summary>
    /// A step in a series of steps that determine the validity of a subject by emitting a bool,
    /// with the option to specify a message about the subject's validity to be used when this
    /// validation step stops the validation chain.
    /// </summary>
    /// <remarks>
    /// true and false are given meaning by assignment to specific <see cref="ValidationStepAction"/>s.
    /// </remarks>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class SimpleBooleanValidationStep : BooleanValidationStepBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleBooleanValidationStep"/> class.
        /// </summary>
        /// <param name="operation">The operation to execute whose result determines what to do as specified by <paramref name="trueAction"/> and <paramref name="falseAction"/>.</param>
        /// <param name="stopMessage">OPTIONAL message that should be emitted when this validation step stops the validation (prevents the next step in the chain from executing).  DEFAULT is to omit this message.</param>
        /// <param name="trueAction">OPTIONAL action to perform when executing <paramref name="operation"/> returns true.  DEFAULT is to move on and execute the next step in the chain.</param>
        /// <param name="falseAction">OPTIONAL action to perform when executing <paramref name="operation"/> returns false.  DEFAULT is to stop the validation (do not execute the next step in the chain) and determine that the subject is invalid.</param>
        /// <param name="details">OPTIONAL details about this validation step.  DEFAULT is to omit any details.</param>
        public SimpleBooleanValidationStep(
            IReturningOperation<bool> operation,
            string stopMessage = null,
            ValidationStepAction trueAction = ValidationStepAction.NextStep,
            ValidationStepAction falseAction = ValidationStepAction.StopAsInvalid,
            string details = null)
            : base(trueAction, falseAction, details)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            this.Operation = operation;
            this.StopMessage = stopMessage;
        }

        /// <summary>
        /// Gets The operation to execute whose result determines what to do as specified by
        /// <see cref="BooleanValidationStepBase.TrueAction"/> and <see cref="BooleanValidationStepBase.FalseAction"/>.
        /// </summary>
        public IReturningOperation<bool> Operation { get; private set; }

        /// <summary>
        /// Gets the message that should be emitted when this validation step stops
        /// the validation (prevents the next step in the chain from executing).
        /// </summary>
        public string StopMessage { get; private set; }
    }
}