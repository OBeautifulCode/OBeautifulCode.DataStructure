// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationStepBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// A step in a series of steps that determine the validity of a subject.
    /// </summary>
    public abstract partial class ValidationStepBase : IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationStepBase"/> class.
        /// </summary>
        /// <param name="details">OPTIONAL details about this validation step.  DEFAULT is to omit any details.</param>
        protected ValidationStepBase(
            string details = null)
        {
            this.Details = details;
        }

        /// <summary>
        /// Gets details about this validation step.
        /// </summary>
        public string Details { get; private set; }
    }
}