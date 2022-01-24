// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdditionalReportInfo.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

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
        public AdditionalReportInfo(
            string copyright = null,
            string termsOfUse = null)
        {
            this.Copyright = copyright;
            this.TermsOfUse = termsOfUse;
        }

        /// <summary>
        /// Gets the copyright of the report.
        /// </summary>
        public string Copyright { get; private set; }

        /// <summary>
        /// Gets the terms-of-use for the report.
        /// </summary>
        public string TermsOfUse { get; private set; }
    }
}