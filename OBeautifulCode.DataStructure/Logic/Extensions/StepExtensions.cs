// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StepExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Extension methods on <see cref="ValidationStepBase"/> and <see cref="AvailabilityCheckStep"/>.
    /// </summary>
    public static class StepExtensions
    {
        /// <summary>
        /// Builds a <see cref="ValidateOp"/>.
        /// </summary>
        /// <param name="steps">The individual validation steps/checks.</param>
        /// <param name="endMessageOp">OPTIONAL operation to execute to get the message that should be emitted when all <paramref name="steps"/> have been evaluated and none have stopped the validation (we've reached the end of the chain).  DEFAULT is to omit this message.</param>
        /// <param name="endValidity">OPTIONAL value that specifies the validity of the subject when all <paramref name="steps"/> have been evaluated and none have stopped the validation (we've reached the end of the chain).  DEFAULT is to determine that the subject is valid.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static ValidateOp Validate(
            this IReadOnlyList<ValidationStepBase> steps,
            IReturningOperation<string> endMessageOp = null,
            Validity endValidity = Validity.Valid)
        {
            var validationChain = new ValidationChain(steps, endMessageOp, endValidity);

            var result = new ValidateOp(validationChain);

            return result;
        }

        /// <summary>
        /// Builds a <see cref="CheckAvailabilityOp"/>.
        /// </summary>
        /// <param name="steps">The individual availability check steps.</param>
        /// <param name="endMessageOp">OPTIONAL operation to execute to get the message that should be emitted when all <paramref name="steps"/> have been evaluated and none have stopped the availability check (we've reached the end of the chain).  DEFAULT is to omit this message.</param>
        /// <param name="endAvailability">OPTIONAL value that specifies the availability of the subject when all <paramref name="steps"/> have been evaluated and none have stopped the availability check (we've reached the end of the chain).  DEFAULT is to determine that the subject is enabled.</param>
        /// <returns>
        /// The operation.
        /// </returns>
        public static CheckAvailabilityOp CheckAvailability(
            this IReadOnlyList<AvailabilityCheckStep> steps,
            IReturningOperation<string> endMessageOp = null,
            Availability endAvailability = Availability.Enabled)
        {
            var availabilityCheckChain = new AvailabilityCheckChain(steps, endMessageOp, endAvailability);

            var result = new CheckAvailabilityOp(availabilityCheckChain);

            return result;
        }
    }
}
