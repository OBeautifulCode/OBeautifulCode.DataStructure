// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdditionalSectionInfo.cs" company="OBeautifulCode">
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
    /// Additional information related to a <see cref="Section"/>.
    /// </summary>
    public partial class AdditionalSectionInfo : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalSectionInfo"/> class.
        /// </summary>
        /// <param name="details">OPTIONAL details for the section.  DEFAULT is no details.</param>
        public AdditionalSectionInfo(
            IReadOnlyList<IDetails> details = null)
        {
            if ((details != null) && details.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(details)} contains at least one null element."));
            }

            this.Details = details;
        }

        /// <summary>
        /// Gets the details for the section.
        /// </summary>
        public IReadOnlyList<IDetails> Details { get; private set; }
    }
}