// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportToWorkbookProjectionContext.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Excel
{
    using Aspose.Cells;
    using OBeautifulCode.Type;

    /// <summary>
    /// Context object for a <see cref="Report"/> to <see cref="Workbook"/> projection.
    /// </summary>
    public class ReportToWorkbookProjectionContext
    {
        /// <summary>
        /// Gets or sets the delegate to use to build the Excel document properties.
        /// </summary>
        public BuildDocumentPropertiesDelegate BuildDocumentPropertiesDelegate { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="CultureKind"/> to use when not specified.
        /// </summary>
        public CultureKind? CultureKind { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="StandardTimeZone"/> to use when not specified.
        /// </summary>
        public StandardTimeZone? LocalTimeZone { get; set; }
    }
}