// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationResult.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// The result of validating a subject.
    /// </summary>
    public partial class ValidationResult : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        /// <param name="validity">The validity of the subject.</param>
        /// <param name="messageOp">OPTIONAL operation to execute to get the message to emit about the validity.  DEFAULT is no message.</param>
        public ValidationResult(
            Validity validity,
            IReturningOperation<string> messageOp = null)
        {
            if (validity == Validity.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(validity)} is {nameof(DataStructure.Validity)}.{nameof(Validity.Unknown)}."));
            }

            this.Validity = validity;
            this.MessageOp = messageOp;
        }

        /// <summary>
        /// Gets the validity of the subject.
        /// </summary>
        public Validity Validity { get; private set; }

        /// <summary>
        /// Gets the operation to execute to get the message to emit about the validity.
        /// </summary>
        public IReturningOperation<string> MessageOp { get; private set; }
    }
}