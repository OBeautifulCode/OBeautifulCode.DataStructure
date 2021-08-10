// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateUsingConditionsOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Validates a subject using a list of conditions that determine it's validity.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ValidateUsingConditionsOp : ReturningOperationBase<ValidationResult>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUsingConditionsOp"/> class.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        public ValidateUsingConditionsOp(
            IReadOnlyList<ValidationCondition> conditions)
        {
            if (conditions == null)
            {
                throw new ArgumentNullException(nameof(conditions));
            }

            if (!conditions.Any())
            {
                throw new ArgumentException(Invariant($"{nameof(conditions)} is empty."));
            }

            if (conditions.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(conditions)} contains a null element."));
            }

            this.Conditions = conditions;
        }

        /// <summary>
        /// Gets the conditions.
        /// </summary>
        public IReadOnlyList<ValidationCondition> Conditions { get; private set; }
    }
}