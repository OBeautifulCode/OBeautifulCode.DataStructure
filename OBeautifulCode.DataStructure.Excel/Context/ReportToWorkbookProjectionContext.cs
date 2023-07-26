// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportToWorkbookProjectionContext.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Excel
{
    using System.Collections.Generic;
    using Aspose.Cells;
    using OBeautifulCode.Type;

    /// <summary>
    /// Context object for a <see cref="Report"/> to <see cref="Workbook"/> projection.
    /// </summary>
    public class ReportToWorkbookProjectionContext
    {
        /// <summary>
        /// Gets or sets the <see cref="AdditionalReportInfo"/> to use, overriding the one specified in the report.
        /// </summary>
        public AdditionalReportInfo AdditionalReportInfoOverride { get; set; }

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

        /// <summary>
        /// Gets or sets a map of <see cref="Section.Id"/> to worksheet name,
        /// overriding the default behavior of using <see cref="Section.Name"/> as the worksheet name.
        /// </summary>
        public IReadOnlyDictionary<string, string> SectionIdToWorksheetNameOverrideMap { get; set; }
    }
}