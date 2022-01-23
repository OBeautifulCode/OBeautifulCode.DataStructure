// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportProjector.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Excel
{
    using Aspose.Cells;
    using OBeautifulCode.Excel.AsposeCells;

    /// <summary>
    /// Projects a <see cref="Report"/> into an Excel workbook.
    /// </summary>
    public static class ReportProjector
    {
        /// <summary>
        /// Projects a <see cref="Report"/> into a <see cref="Workbook"/>.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="context">The context to use.</param>
        /// <returns>
        /// The <see cref="Workbook"/>.
        /// </returns>
        public static Workbook ToExcelWorkbook(
            this Report report,
            ReportToWorkbookProjectionContext context)
        {
            var result = General.CreateStandardWorkbook().RemoveDefaultWorksheet();

            if (context.BuildDocumentPropertiesDelegate != null)
            {
                result.SetDocumentProperties(context.BuildDocumentPropertiesDelegate(report.Title));
            }

            return result;
        }
    }
}