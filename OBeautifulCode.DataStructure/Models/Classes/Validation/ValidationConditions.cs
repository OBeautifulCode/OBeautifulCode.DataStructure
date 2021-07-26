// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationConditions.cs" company="OBeautifulCode">
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
    /// A list of conditions that determine the validity of a subject (e.g. a cell).
    /// </summary>
    /// <remarks>
    /// The conditions are evaluated in order; so if one of them fails, the others are not evaluated.
    /// </remarks>
    public partial class ValidationConditions : IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationConditions"/> class.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <param name="details">OPTIONAL details about the validation.  DEFAULT is to omit any details.</param>
        public ValidationConditions(
            IReadOnlyList<ValidationCondition> conditions,
            string details = null)
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
            this.Details = details;
        }

        /// <summary>
        /// Gets the conditions.
        /// </summary>
        public IReadOnlyList<ValidationCondition> Conditions { get; private set; }

        /// <summary>
        /// Gets details about the validation.
        /// </summary>
        public string Details { get; private set; }
    }
}
