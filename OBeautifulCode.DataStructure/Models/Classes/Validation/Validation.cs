// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Validation.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Determine the validity of a subject (e.g. a cell).
    /// </summary>
    public partial class Validation : IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Validation"/> class.
        /// </summary>
        /// <param name="operation">The operation to execute to get the validity of the subject.</param>
        /// <param name="messageFormatKind">OPTIONAL kind of format to apply to the message that is emitted about the validity of the subject.  DEFAULT is to omit this specification.</param>
        /// <param name="details">OPTIONAL details about this validation.  DEFAULT is to omit any details.</param>
        public Validation(
            IReturningOperation<ValidationResult> operation,
            MessageFormatKind? messageFormatKind = null,
            string details = null)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if ((messageFormatKind != null) && (messageFormatKind == DataStructure.MessageFormatKind.Unknown))
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(messageFormatKind)} is {nameof(DataStructure.MessageFormatKind)}.{nameof(DataStructure.MessageFormatKind.Unknown)}"));
            }

            this.Operation = operation;
            this.MessageFormatKind = messageFormatKind;
            this.Details = details;
        }

        /// <summary>
        /// Gets the operation to execute to get the validity of the subject.
        /// </summary>
        public IReturningOperation<ValidationResult> Operation { get; private set; }

        /// <summary>
        /// Gets the kind of format to apply to the message that is emitted about the validity of the subject.
        /// </summary>
        public MessageFormatKind? MessageFormatKind { get; private set; }

        /// <summary>
        /// Gets details about this validation.
        /// </summary>
        public string Details { get; private set; }
    }
}