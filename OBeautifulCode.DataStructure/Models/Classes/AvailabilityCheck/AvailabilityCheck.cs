// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AvailabilityCheck.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Determine the availability of a subject (e.g. a cell).
    /// </summary>
    public partial class AvailabilityCheck : IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityCheck"/> class.
        /// </summary>
        /// <param name="operation">The operation to execute to check the availability of the subject.</param>
        /// <param name="details">OPTIONAL details about this availability check.  DEFAULT is to omit any details.</param>
        public AvailabilityCheck(
            IReturningOperation<AvailabilityCheckResult> operation,
            string details = null)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            this.Operation = operation;
            this.Details = details;
        }

        /// <summary>
        /// Gets the operation to execute to check the availability of the subject.
        /// </summary>
        public IReturningOperation<AvailabilityCheckResult> Operation { get; private set; }

        /// <summary>
        /// Gets details about this availability check.
        /// </summary>
        public string Details { get; private set; }
    }
}