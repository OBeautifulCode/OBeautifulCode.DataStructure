// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AvailabilityCheckResult.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The result of checking the availability of a subject.
    /// </summary>
    public partial class AvailabilityCheckResult : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityCheckResult"/> class.
        /// </summary>
        /// <param name="availabilityOp">Operation to execute to get the availability of the subject.</param>
        /// <param name="messageOp">OPTIONAL operation to execute to get the message to emit about the availability.  DEFAULT is no message.</param>
        public AvailabilityCheckResult(
            IReturningOperation<Availability> availabilityOp,
            IReturningOperation<string> messageOp = null)
        {
            // ReSharper disable once JoinNullCheckWithUsage
            if (availabilityOp == null)
            {
                throw new ArgumentNullException(nameof(availabilityOp));
            }

            this.AvailabilityOp = availabilityOp;
            this.MessageOp = messageOp;
        }

        /// <summary>
        /// Gets the operation to execute to get the availability of the subject.
        /// </summary>
        public IReturningOperation<Availability> AvailabilityOp { get; private set; }

        /// <summary>
        /// Gets the operation to execute to get the message to emit about the availability.
        /// </summary>
        public IReturningOperation<string> MessageOp { get; private set; }
    }
}