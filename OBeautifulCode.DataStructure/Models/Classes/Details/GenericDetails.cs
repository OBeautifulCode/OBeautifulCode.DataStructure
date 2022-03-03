// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericDetails.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Some generic (not structured) details.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class GenericDetails : DetailsBase, IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDetails"/> class.
        /// </summary>
        /// <param name="details">The details.</param>
        public GenericDetails(
            string details)
        {
            if (details == null)
            {
                throw new ArgumentNullException(nameof(details));
            }

            if (string.IsNullOrWhiteSpace(details))
            {
                throw new ArgumentException(Invariant($"{nameof(details)} is white space."));
            }

            this.Details = details;
        }

        /// <inheritdoc />
        public string Details { get; private set; }
    }
}
