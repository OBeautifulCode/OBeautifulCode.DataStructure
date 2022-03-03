// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdditionalReportInfo.cs" company="OBeautifulCode">
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
    /// Additional information related to a <see cref="Report"/>.
    /// </summary>
    public partial class AdditionalReportInfo : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalReportInfo"/> class.
        /// </summary>
        /// <param name="copyright">OPTIONAL copyright of the report.  DEFAULT is no copyright.</param>
        /// <param name="termsOfUse">OPTIONAL terms-of-use for the report.  DEFAULT is no terms-of-use.</param>
        /// <param name="details">OPTIONAL details for the report.  DEFAULT is no details.</param>
        public AdditionalReportInfo(
            string copyright = null,
            string termsOfUse = null,
            IReadOnlyList<IDetails> details = null)
        {
            if ((details != null) && details.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(details)} contains at least one null element."));
            }

            this.Copyright = copyright;
            this.TermsOfUse = termsOfUse;
            this.Details = details;
        }

        /// <summary>
        /// Gets the copyright of the report.
        /// </summary>
        public string Copyright { get; private set; }

        /// <summary>
        /// Gets the terms-of-use for the report.
        /// </summary>
        public string TermsOfUse { get; private set; }

        /// <summary>
        /// Gets the details for the report.
        /// </summary>
        public IReadOnlyList<IDetails> Details { get; private set; }
    }
}