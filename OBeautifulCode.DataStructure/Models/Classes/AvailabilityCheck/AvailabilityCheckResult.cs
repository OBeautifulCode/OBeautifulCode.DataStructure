// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AvailabilityCheckResult.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// The result of checking the availability of a subject.
    /// </summary>
    public partial class AvailabilityCheckResult : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityCheckResult"/> class.
        /// </summary>
        /// <param name="availability">The availability of the subject.</param>
        /// <param name="messageOp">OPTIONAL operation to execute to get the message to emit about the availability.  DEFAULT is no message.</param>
        public AvailabilityCheckResult(
            Availability availability,
            IReturningOperation<string> messageOp = null)
        {
            if (availability == Availability.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(availability)} is {nameof(DataStructure.Availability)}.{nameof(Availability.Unknown)}."));
            }

            this.Availability = availability;
            this.MessageOp = messageOp;
        }

        /// <summary>
        /// Gets the availability of the subject.
        /// </summary>
        public Availability Availability { get; private set; }

        /// <summary>
        /// Gets the operation to execute to get the message to emit about the availability.
        /// </summary>
        public IReturningOperation<string> MessageOp { get; private set; }

        /// <summary>
        /// Performs an implicit conversion from a <see cref="Availability"/> to a <see cref="AvailabilityCheckResult"/>.
        /// </summary>
        /// <param name="from">The <see cref="Availability"/> to convert from.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator AvailabilityCheckResult(
            Availability from)
        {
            var result = new AvailabilityCheckResult(from);

            return result;
        }
    }
}