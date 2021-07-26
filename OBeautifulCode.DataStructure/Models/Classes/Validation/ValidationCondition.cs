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
    /// A condition that determines the validity of a subject along a single dimension/concern.
    /// </summary>
    public partial class ValidationCondition : IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCondition"/> class.
        /// </summary>
        /// <param name="operation">The operation to execute to determine whether the subject meets this condition.</param>
        /// <param name="failureMessageOp">The operation to execute to get the message that should be emitted if the subject fails to meet this condition.</param>
        /// <param name="kind">OPTIONAL value that determines whether the condition passes (is met) or fails to be met based on the result of executing <paramref name="operation"/>.</param>
        /// <param name="details">OPTIONAL details about the condition.  DEFAULT is to omit any details.</param>
        public ValidationCondition(
            IReturningOperation<bool> operation,
            IReturningOperation<string> failureMessageOp,
            ValidationConditionKind kind = ValidationConditionKind.PassWhenTrue,
            string details = null)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (failureMessageOp == null)
            {
                throw new ArgumentNullException(nameof(failureMessageOp));
            }

            if (kind == ValidationConditionKind.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(operation)} is {nameof(ValidationConditionKind.Unknown)}."));
            }

            this.Operation = operation;
            this.FailureMessageOp = failureMessageOp;
            this.Kind = kind;
            this.Details = details;
        }

        /// <summary>
        /// Gets the operation to execute to determine whether the subject meets this condition.
        /// </summary>
        public IReturningOperation<bool> Operation { get; private set; }

        /// <summary>
        /// Gets the operation to execute to get the message that should be emitted if the subject fails to meet this condition.
        /// </summary>
        public IReturningOperation<string> FailureMessageOp { get; private set; }

        /// <summary>
        /// Gets a value that determines whether the condition passes (is met) or fails to be met based on the result of executing <see cref="Operation"/>.
        /// </summary>
        public ValidationConditionKind Kind { get; private set; }

        /// <summary>
        /// Gets details about the condition.
        /// </summary>
        public string Details { get; private set; }
    }
}