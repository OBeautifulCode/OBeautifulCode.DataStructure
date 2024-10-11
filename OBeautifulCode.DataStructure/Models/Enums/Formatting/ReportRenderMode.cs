// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportRenderMode.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies a mode of report rendering; an overall visual approach to take when rendering the report.
    /// </summary>
    public enum ReportRenderMode
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The report contains mixed content, such as icons, links, paragraphs of text, other reports.
        /// </summary>
        Content,

        /// <summary>
        /// The report primary contains links to navigate to other resources such as other reports.
        /// </summary>
        Navigation,

        /// <summary>
        /// The report is a survey, with input cells, validation, availability checks, etc.
        /// </summary>
        Survey,

        /// <summary>
        /// The report is data-heavy, with an emphasis on the tabular information.
        /// </summary>
        TabularData,
    }
}