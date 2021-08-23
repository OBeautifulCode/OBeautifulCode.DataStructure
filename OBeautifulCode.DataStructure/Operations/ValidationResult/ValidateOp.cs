// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Validates a subject.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ValidateOp : ReturningOperationBase<ValidationResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateOp"/> class.
        /// </summary>
        /// <param name="validationChain">A series of steps that determine the validity of the subject.</param>
        public ValidateOp(
            ValidationChain validationChain)
        {
            if (validationChain == null)
            {
                throw new ArgumentNullException(nameof(validationChain));
            }

            this.ValidationChain = validationChain;
        }

        /// <summary>
        /// Gets a series of steps that determine the validity of the subject.
        /// </summary>
        public ValidationChain ValidationChain { get; private set; }
    }
}