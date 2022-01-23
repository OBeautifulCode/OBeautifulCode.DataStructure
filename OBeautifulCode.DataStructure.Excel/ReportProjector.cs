// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportProjector.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Excel
{
    using System;
    using Aspose.Cells;

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
            throw new NotImplementedException();
        }
    }
}