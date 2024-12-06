// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationBoolWithMessage.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// The result of performing some validation, expressed as a boolean,
    /// with the option to specify a message about the validation (e.g. why the subject was invalid).
    /// </summary>
    /// <remarks>
    /// There are no constraints on whether true is valid or invalid and vice-versa for false.
    /// </remarks>
    public partial class ValidationBoolWithMessage : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBoolWithMessage"/> class.
        /// </summary>
        /// <param name="outcome">The boolean result from performing validation on the subject.  There are no constraints on whether true is valid or invalid and vice-versa for false.</param>
        /// <param name="message">OPTIONAL message about the validation performed (e.g. why the subject is invalid).</param>
        public ValidationBoolWithMessage(
            bool outcome,
            string message = null)
        {
            this.Outcome = outcome;
            this.Message = message;
        }

        /// <summary>
        /// Gets the boolean result from performing validation on the subject.
        /// </summary>
        /// <remarks>
        /// There are no constraints on whether true is valid or invalid and vice-versa for false.
        /// </remarks>
        #pragma warning disable SA1623
        public bool Outcome { get; private set; }
        #pragma warning restore SA1623

        /// <summary>
        /// Gets a message about the validation performed (e.g. why the subject is invalid).
        /// </summary>
        public string Message { get; private set; }
    }
}