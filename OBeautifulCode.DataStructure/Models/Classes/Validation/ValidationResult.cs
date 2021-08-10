// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationResult.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The result of validating a subject.
    /// </summary>
    public partial class ValidationResult : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        /// <param name="validityOp">Operation to execute to get the validity of the subject.</param>
        /// <param name="messageOp">OPTIONAL operation to execute to get the message to emit about the validity.  DEFAULT is no message.</param>
        public ValidationResult(
            IReturningOperation<Validity> validityOp,
            IReturningOperation<string> messageOp = null)
        {
            if (validityOp == null)
            {
                throw new ArgumentNullException(nameof(validityOp));
            }

            this.ValidityOp = validityOp;
            this.MessageOp = messageOp;
        }

        /// <summary>
        /// Gets the operation to execute to get the validity of the subject.
        /// </summary>
        public IReturningOperation<Validity> ValidityOp { get; private set; }

        /// <summary>
        /// Gets the operation to execute to get the message to emit about the validity.
        /// </summary>
        public IReturningOperation<string> MessageOp { get; private set; }
    }
}