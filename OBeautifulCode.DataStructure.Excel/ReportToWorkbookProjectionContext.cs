// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportToWorkbookProjectionContext.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Excel
{
    using Aspose.Cells;

    /// <summary>
    /// Context object for a <see cref="Report"/> to <see cref="Workbook"/> projection.
    /// </summary>
    public class ReportToWorkbookProjectionContext
    {
        /// <summary>
        /// Gets or sets the delegate to use to build the Excel document properties.
        /// </summary>
        public BuildDocumentPropertiesDelegate BuildDocumentPropertiesDelegate { get; set; }
    }
}