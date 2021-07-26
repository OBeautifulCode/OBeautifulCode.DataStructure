// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationCondition.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A condition is evaluated in context of a <see cref="Validation"/>
    /// and determines the validity of a subject along single dimension.
    /// </summary>
    public class ValidationCondition : IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// The default operation to use when a condition fails to be met.
        /// </summary>
        public const string DefaultFailureMessageOp = "The subject failed to meet a validation condition.";

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCondition"/> class.
        /// </summary>
        /// <param name="operation">The operation to execute to determine whether the subject meets this condition.</param>
        /// <param name="kind">Determines whether the condition passes (is met) or fails to be met based on the result of executing <paramref name="operation"/>.</param>
        /// <param name="failureMessageOp">OPTIONAL operation to execute to get the message that should be emitted by the containing <see cref="Validation"/> if the subject fails to meet this condition.  DEFAULT is to use <see cref="DefaultFailureMessageOp"/>.</param>
        /// <param name="details">OPTIONAL details about the condition.  DEFAULT is to omit any details.</param>
        public ValidationCondition(
            IReturningOperation<bool> operation,
            ValidationConditionKind kind = ValidationConditionKind.PassWhenFalse,
            IReturningOperation<string> failureMessageOp = null,
            string details = null)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (kind == ValidationConditionKind.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(operation)} is {nameof(ValidationConditionKind.Unknown)}."));
            }

            if (failureMessageOp == null)
            {
                failureMessageOp = Do.Value(DefaultFailureMessageOp);
            }

            this.Operation = operation;
            this.Kind = kind;
            this.FailureMessageOp = failureMessageOp;
            this.Details = details;
        }

        /// <summary>
        /// Gets the operation to execute to determine whether the subject meets this condition.
        /// </summary>
        public IReturningOperation<bool> Operation { get; private set; }

        /// <summary>
        /// Gets a value that determines whether the condition passes (is met) or fails to be met based on the result of executing <see cref="Operation"/>.
        /// </summary>
        public ValidationConditionKind Kind { get; private set; }

        /// <summary>
        /// Gets the operation to execute to get the message that should be emitted by the containing <see cref="Validation"/> if the subject fails to meet this condition.
        /// </summary>
        public IReturningOperation<string> FailureMessageOp { get; private set; }

        /// <summary>
        /// Gets details about the condition.
        /// </summary>
        public string Details { get; private set; }
    }
}