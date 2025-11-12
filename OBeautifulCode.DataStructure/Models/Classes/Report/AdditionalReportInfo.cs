// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdditionalReportInfo.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using OBeautifulCode.CodeAnalysis.Recipes;
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
        /// <param name="version">OPTIONAL version of the report.  DEFAULT is no version specified.</param>
        /// <param name="helpUrl">OPTIONAL URL to a resource that provides help with the report.  DEFAULT is no help URL.</param>
        /// <param name="details">OPTIONAL details for the report.  DEFAULT is no details.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "3#", Justification = ObcSuppressBecause.CA1054_UriParametersShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public AdditionalReportInfo(
            string copyright = null,
            string termsOfUse = null,
            Version version = null,
            string helpUrl = null,
            IReadOnlyList<IDetails> details = null)
        {
            if ((details != null) && details.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(details)} contains at least one null element."));
            }

            this.Copyright = copyright;
            this.TermsOfUse = termsOfUse;
            this.Version = version;
            this.HelpUrl = helpUrl;
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
        /// Gets the version of the report.
        /// </summary>
        public Version Version { get; private set; }

        /// <summary>
        /// Gets a URL to a resource that provides help with the report.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = ObcSuppressBecause.CA1056_UriPropertiesShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public string HelpUrl { get; private set; }

        /// <summary>
        /// Gets the details for the report.
        /// </summary>
        public IReadOnlyList<IDetails> Details { get; private set; }
    }
}