// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckAvailabilityOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Checks the availability of a subject.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CheckAvailabilityOp : ReturningOperationBase<AvailabilityCheckResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckAvailabilityOp"/> class.
        /// </summary>
        /// <param name="availabilityCheckChain">A series of steps that determine the availability of the subject.</param>
        public CheckAvailabilityOp(
            AvailabilityCheckChain availabilityCheckChain)
        {
            if (availabilityCheckChain == null)
            {
                throw new ArgumentNullException(nameof(availabilityCheckChain));
            }

            this.AvailabilityCheckChain = availabilityCheckChain;
        }

        /// <summary>
        /// Gets a series of steps that determine the availability of the subject.
        /// </summary>
        public AvailabilityCheckChain AvailabilityCheckChain { get; private set; }
    }
}