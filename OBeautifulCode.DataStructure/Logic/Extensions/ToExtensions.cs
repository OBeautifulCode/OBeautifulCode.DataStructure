// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OBeautifulCode.String.Recipes;

    /// <summary>
    /// Extension methods to build higher level objects from lower-level objects.
    /// </summary>
    public static class ToExtensions
    {
        /// <summary>
        /// Converts a <see cref="Section"/> to a <see cref="Report"/>.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="id">OPTIONAL report unique identifier.  DEFAULT is to use a randomly assigned GUID.</param>
        /// <param name="title">OPTIONAL title of the report.  DEFAULT is a report with no title.</param>
        /// <param name="timestampUtc">OPTIONAL timestamp of the report, in UTC.  DEFAULT is a report that is not timestamped.</param>
        /// <param name="downloadKinds">OPTIONAL kinds of download supported.  DEFAULT is no download option.</param>
        /// <param name="additionalInfo">OPTIONAL additional information related to the report.  DEFAULT no additional information.</param>
        /// <param name="format">OPTIONAL format to apply to the report.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// A <see cref="Report"/> containing the specified <paramref name="section"/>.
        /// </returns>
        public static Report ToReport(
            this Section section,
            string id = null,
            string title = null,
            DateTime? timestampUtc = null,
            IReadOnlyList<DownloadKind> downloadKinds = null,
            AdditionalReportInfo additionalInfo = null,
            ReportFormat format = null)
        {
            var result = new Report(
                id ?? Guid.NewGuid().ToStringInvariantPreferred(),
                new[] { section },
                title,
                timestampUtc,
                downloadKinds,
                additionalInfo,
                format);

            return result;
        }

        /// <summary>
        /// Converts a <see cref="TreeTable"/> to a single-<see cref="Section"/> <see cref="Report"/>.
        /// </summary>
        /// <param name="treeTable">The tree table.</param>
        /// <param name="id">OPTIONAL report unique identifier.  DEFAULT is to use a randomly assigned GUID.</param>
        /// <param name="title">OPTIONAL title of the report.  DEFAULT is a report with no title.</param>
        /// <param name="timestampUtc">OPTIONAL timestamp of the report, in UTC.  DEFAULT is a report that is not timestamped.</param>
        /// <param name="downloadKinds">OPTIONAL kinds of download supported.  DEFAULT is no download option.</param>
        /// <param name="additionalInfo">OPTIONAL additional information related to the report.  DEFAULT no additional information.</param>
        /// <param name="format">OPTIONAL format to apply to the report.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="sectionId">OPTIONAL section unique identifier.  DEFAULT is to use <see cref="DefaultIds.DefaultSectionId"/>.</param>
        /// <param name="sectionName">OPTIONAL name of the section.  DEFAULT is a section with no name.</param>
        /// <param name="sectionTitle">OPTIONAL title of the section.  DEFAULT is a section with no title.</param>
        /// <param name="sectionAdditionalInfo">OPTIONAL additional information related to the section.  DEFAULT no additional information.</param>
        /// <param name="sectionFormat">OPTIONAL format to apply to the section.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// A single-<see cref="Section"/> <see cref="Report"/> containing the specified <paramref name="treeTable"/>.
        /// </returns>
        public static Report ToReport(
            this TreeTable treeTable,
            string id = null,
            string title = null,
            DateTime? timestampUtc = null,
            IReadOnlyList<DownloadKind> downloadKinds = null,
            AdditionalReportInfo additionalInfo = null,
            ReportFormat format = null,
            string sectionId = DefaultIds.DefaultSectionId,
            string sectionName = null,
            string sectionTitle = null,
            AdditionalSectionInfo sectionAdditionalInfo = null,
            SectionFormat sectionFormat = null)
        {
            var result = treeTable
                .ToSection(sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat)
                .ToReport(id, title, timestampUtc, downloadKinds, additionalInfo, format);

            return result;
        }

        /// <summary>
        /// Converts a <see cref="TreeTable"/> to a <see cref="Section"/>.
        /// </summary>
        /// <param name="treeTable">The tree table.</param>
        /// <param name="id">OPTIONAL section unique identifier.  DEFAULT is to use <see cref="DefaultIds.DefaultSectionId"/>.</param>
        /// <param name="name">OPTIONAL name of the section.  DEFAULT is a section with no name.</param>
        /// <param name="title">OPTIONAL title of the section.  DEFAULT is a section with no title.</param>
        /// <param name="additionalInfo">OPTIONAL additional information related to the section.  DEFAULT no additional information.</param>
        /// <param name="format">OPTIONAL format to apply to the section.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// A <see cref="Section"/> containing the specified <paramref name="treeTable"/>.
        /// </returns>
        public static Section ToSection(
            this TreeTable treeTable,
            string id = DefaultIds.DefaultSectionId,
            string name = null,
            string title = null,
            AdditionalSectionInfo additionalInfo = null,
            SectionFormat format = null)
        {
            var result = new Section(
                id,
                treeTable,
                name,
                title,
                additionalInfo,
                format);

            return result;
        }

        /// <summary>
        /// Converts an ordered collection of <see cref="RowBase"/> to a <see cref="Report"/>, treating them as data rows.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="id">OPTIONAL report unique identifier.  DEFAULT is to use a randomly assigned GUID.</param>
        /// <param name="title">OPTIONAL title of the report.  DEFAULT is a report with no title.</param>
        /// <param name="timestampUtc">OPTIONAL timestamp of the report, in UTC.  DEFAULT is a report that is not timestamped.</param>
        /// <param name="downloadKinds">OPTIONAL kinds of download supported.  DEFAULT is no download option.</param>
        /// <param name="additionalInfo">OPTIONAL additional information related to the report.  DEFAULT no additional information.</param>
        /// <param name="format">OPTIONAL format to apply to the report.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="sectionId">OPTIONAL section unique identifier.  DEFAULT is to use <see cref="DefaultIds.DefaultSectionId"/>.</param>
        /// <param name="sectionName">OPTIONAL name of the section.  DEFAULT is a section with no name.</param>
        /// <param name="sectionTitle">OPTIONAL title of the section.  DEFAULT is a section with no title.</param>
        /// <param name="sectionAdditionalInfo">OPTIONAL additional information related to the section.  DEFAULT no additional information.</param>
        /// <param name="sectionFormat">OPTIONAL format to apply to the section.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="treeTableFormat">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="tableColumnsColumnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        /// <param name="columnIdPrefix">OPTIONAL prefix to use for column identifiers.  If specified, the column number (starting at 1) will be appended to this prefix and used as the column identifier.  DEFAULT is to not assign any identifiers to columns.</param>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="footerRows">OPTIONAL footer rows.  DEFAULT is no footer rows.</param>
        /// <param name="tableRowsRowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="dataRowsFormat">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// A <see cref="Report"/> containing the specified data rows.
        /// </returns>
        public static Report ToReport(
            this IReadOnlyList<RowBase> rows,
            string id = null,
            string title = null,
            DateTime? timestampUtc = null,
            IReadOnlyList<DownloadKind> downloadKinds = null,
            AdditionalReportInfo additionalInfo = null,
            ReportFormat format = null,
            string sectionId = DefaultIds.DefaultSectionId,
            string sectionName = null,
            string sectionTitle = null,
            AdditionalSectionInfo sectionAdditionalInfo = null,
            SectionFormat sectionFormat = null,
            TableFormat treeTableFormat = null,
            ColumnFormat tableColumnsColumnsFormat = null,
            string columnIdPrefix = null,
            HeaderRows headerRows = null,
            FooterRows footerRows = null,
            RowFormat tableRowsRowsFormat = null,
            DataRowsFormat dataRowsFormat = null)
        {
            var result = rows
                .ToTreeTable(treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat)
                .ToReport(id, title, timestampUtc, downloadKinds, additionalInfo, format, sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat);

            return result;
        }

        /// <summary>
        /// Converts an ordered collection of <see cref="RowBase"/> into a <see cref="Section"/>, treating them as data rows.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="id">OPTIONAL section unique identifier.  DEFAULT is to use <see cref="DefaultIds.DefaultSectionId"/>.</param>
        /// <param name="name">OPTIONAL name of the section.  DEFAULT is a section with no name.</param>
        /// <param name="title">OPTIONAL title of the section.  DEFAULT is a section with no title.</param>
        /// <param name="additionalInfo">OPTIONAL additional information related to the section.  DEFAULT no additional information.</param>
        /// <param name="format">OPTIONAL format to apply to the section.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="treeTableFormat">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="tableColumnsColumnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        /// <param name="columnIdPrefix">OPTIONAL prefix to use for column identifiers.  If specified, the column number (starting at 1) will be appended to this prefix and used as the column identifier.  DEFAULT is to not assign any identifiers to columns.</param>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="footerRows">OPTIONAL footer rows.  DEFAULT is no footer rows.</param>
        /// <param name="tableRowsRowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="dataRowsFormat">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// A <see cref="Section"/> containing the specified data rows.
        /// </returns>
        public static Section ToSection(
            this IReadOnlyList<RowBase> rows,
            string id = DefaultIds.DefaultSectionId,
            string name = null,
            string title = null,
            AdditionalSectionInfo additionalInfo = null,
            SectionFormat format = null,
            TableFormat treeTableFormat = null,
            ColumnFormat tableColumnsColumnsFormat = null,
            string columnIdPrefix = null,
            HeaderRows headerRows = null,
            FooterRows footerRows = null,
            RowFormat tableRowsRowsFormat = null,
            DataRowsFormat dataRowsFormat = null)
        {
            var result = rows
                .ToTreeTable(treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat)
                .ToSection(id, name, title, additionalInfo, format);

            return result;
        }

        /// <summary>
        /// Converts an ordered collection of <see cref="RowBase"/> into a <see cref="TreeTable"/>, treating them as data rows.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="format">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="tableColumnsColumnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        /// <param name="columnIdPrefix">OPTIONAL prefix to use for column identifiers.  If specified, the column number (starting at 1) will be appended to this prefix and used as the column identifier.  DEFAULT is to not assign any identifiers to columns.</param>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="footerRows">OPTIONAL footer rows.  DEFAULT is no footer rows.</param>
        /// <param name="tableRowsRowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="dataRowsFormat">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// A <see cref="TreeTable"/> containing the specified data rows.
        /// </returns>
        public static TreeTable ToTreeTable(
            this IReadOnlyList<RowBase> rows,
            TableFormat format = null,
            ColumnFormat tableColumnsColumnsFormat = null,
            string columnIdPrefix = null,
            HeaderRows headerRows = null,
            FooterRows footerRows = null,
            RowFormat tableRowsRowsFormat = null,
            DataRowsFormat dataRowsFormat = null)
        {
            var dataRows = new DataRows(rows, dataRowsFormat);

            var tableRows = new TableRows(headerRows, dataRows, footerRows, tableRowsRowsFormat);

            int numberOfColumns;

            if (rows.Any())
            {
                numberOfColumns = rows.First().GetNumberOfColumnsSpanned();
            }
            else if ((headerRows != null) && headerRows.Rows.Any())
            {
                numberOfColumns = headerRows.Rows.First().GetNumberOfColumnsSpanned();
            }
            else if ((footerRows != null) && footerRows.Rows.Any())
            {
                numberOfColumns = footerRows.Rows.First().GetNumberOfColumnsSpanned();
            }
            else
            {
                numberOfColumns = 1;
            }

            var columns = Enumerable.Range(1, numberOfColumns)
                    .Select(_ => new Column(columnIdPrefix != null ? (columnIdPrefix + _.ToStringInvariantPreferred()) : null))
                    .ToArray();

            var tableColumns = new TableColumns(columns, tableColumnsColumnsFormat);

            var result = new TreeTable(tableColumns, tableRows, format);

            return result;
        }

        /// <summary>
        /// Converts a <see cref="RowBase"/> to a <see cref="Report"/>, treating it as a data row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="id">OPTIONAL report unique identifier.  DEFAULT is to use a randomly assigned GUID.</param>
        /// <param name="title">OPTIONAL title of the report.  DEFAULT is a report with no title.</param>
        /// <param name="timestampUtc">OPTIONAL timestamp of the report, in UTC.  DEFAULT is a report that is not timestamped.</param>
        /// <param name="downloadKinds">OPTIONAL kinds of download supported.  DEFAULT is no download option.</param>
        /// <param name="additionalInfo">OPTIONAL additional information related to the report.  DEFAULT no additional information.</param>
        /// <param name="format">OPTIONAL format to apply to the report.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="sectionId">OPTIONAL section unique identifier.  DEFAULT is to use <see cref="DefaultIds.DefaultSectionId"/>.</param>
        /// <param name="sectionName">OPTIONAL name of the section.  DEFAULT is a section with no name.</param>
        /// <param name="sectionTitle">OPTIONAL title of the section.  DEFAULT is a section with no title.</param>
        /// <param name="sectionAdditionalInfo">OPTIONAL additional information related to the section.  DEFAULT no additional information.</param>
        /// <param name="sectionFormat">OPTIONAL format to apply to the section.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="treeTableFormat">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="tableColumnsColumnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        /// <param name="columnIdPrefix">OPTIONAL prefix to use for column identifiers.  If specified, the column number (starting at 1) will be appended to this prefix and used as the column identifier.  DEFAULT is to not assign any identifiers to columns.</param>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="footerRows">OPTIONAL footer rows.  DEFAULT is no footer rows.</param>
        /// <param name="tableRowsRowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="dataRowsFormat">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// A <see cref="Report"/> containing the specified data row.
        /// </returns>
        public static Report ToReport(
            this RowBase row,
            string id = null,
            string title = null,
            DateTime? timestampUtc = null,
            IReadOnlyList<DownloadKind> downloadKinds = null,
            AdditionalReportInfo additionalInfo = null,
            ReportFormat format = null,
            string sectionId = DefaultIds.DefaultSectionId,
            string sectionName = null,
            string sectionTitle = null,
            AdditionalSectionInfo sectionAdditionalInfo = null,
            SectionFormat sectionFormat = null,
            TableFormat treeTableFormat = null,
            ColumnFormat tableColumnsColumnsFormat = null,
            string columnIdPrefix = null,
            HeaderRows headerRows = null,
            FooterRows footerRows = null,
            RowFormat tableRowsRowsFormat = null,
            DataRowsFormat dataRowsFormat = null)
        {
            var result = new[] { row }
                .ToReport(id, title, timestampUtc, downloadKinds, additionalInfo, format, sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat);

            return result;
        }

        /// <summary>
        /// Converts a <see cref="RowBase"/> into a <see cref="Section"/>, treating it as a data row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="id">OPTIONAL section unique identifier.  DEFAULT is to use <see cref="DefaultIds.DefaultSectionId"/>.</param>
        /// <param name="name">OPTIONAL name of the section.  DEFAULT is a section with no name.</param>
        /// <param name="title">OPTIONAL title of the section.  DEFAULT is a section with no title.</param>
        /// <param name="additionalInfo">OPTIONAL additional information related to the section.  DEFAULT no additional information.</param>
        /// <param name="format">OPTIONAL format to apply to the section.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="treeTableFormat">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="tableColumnsColumnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        /// <param name="columnIdPrefix">OPTIONAL prefix to use for column identifiers.  If specified, the column number (starting at 1) will be appended to this prefix and used as the column identifier.  DEFAULT is to not assign any identifiers to columns.</param>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="footerRows">OPTIONAL footer rows.  DEFAULT is no footer rows.</param>
        /// <param name="tableRowsRowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="dataRowsFormat">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// A <see cref="Section"/> containing the specified data row.
        /// </returns>
        public static Section ToSection(
            this RowBase row,
            string id = DefaultIds.DefaultSectionId,
            string name = null,
            string title = null,
            AdditionalSectionInfo additionalInfo = null,
            SectionFormat format = null,
            TableFormat treeTableFormat = null,
            ColumnFormat tableColumnsColumnsFormat = null,
            string columnIdPrefix = null,
            HeaderRows headerRows = null,
            FooterRows footerRows = null,
            RowFormat tableRowsRowsFormat = null,
            DataRowsFormat dataRowsFormat = null)
        {
            var result = new[] { row }
                .ToSection(id, name, title, additionalInfo, format, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat);

            return result;
        }

        /// <summary>
        /// Convert a <see cref="RowBase"/> into a <see cref="TreeTable"/>, treating it as a data row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="format">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="tableColumnsColumnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        /// <param name="columnIdPrefix">OPTIONAL prefix to use for column identifiers.  If specified, the column number (starting at 1) will be appended to this prefix and used as the column identifier.  DEFAULT is to not assign any identifiers to columns.</param>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="footerRows">OPTIONAL footer rows.  DEFAULT is no footer rows.</param>
        /// <param name="tableRowsRowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="dataRowsFormat">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// A <see cref="TreeTable"/> containing the specified data row.
        /// </returns>
        public static TreeTable ToTreeTable(
            this RowBase row,
            TableFormat format = null,
            ColumnFormat tableColumnsColumnsFormat = null,
            string columnIdPrefix = null,
            HeaderRows headerRows = null,
            FooterRows footerRows = null,
            RowFormat tableRowsRowsFormat = null,
            DataRowsFormat dataRowsFormat = null)
        {
            var result = new[] { row }
                .ToTreeTable(format, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat);

            return result;
        }

        /// <summary>
        /// Converts an ordered collection of <see cref="ICell"/>s into a <see cref="Report"/>, treating them as cells in a single data row.
        /// </summary>
        /// <param name="cells">The cells.</param>
        /// <param name="id">OPTIONAL report unique identifier.  DEFAULT is to use a randomly assigned GUID.</param>
        /// <param name="title">OPTIONAL title of the report.  DEFAULT is a report with no title.</param>
        /// <param name="timestampUtc">OPTIONAL timestamp of the report, in UTC.  DEFAULT is a report that is not timestamped.</param>
        /// <param name="downloadKinds">OPTIONAL kinds of download supported.  DEFAULT is no download option.</param>
        /// <param name="additionalInfo">OPTIONAL additional information related to the report.  DEFAULT no additional information.</param>
        /// <param name="format">OPTIONAL format to apply to the report.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="sectionId">OPTIONAL section unique identifier.  DEFAULT is to use <see cref="DefaultIds.DefaultSectionId"/>.</param>
        /// <param name="sectionName">OPTIONAL name of the section.  DEFAULT is a section with no name.</param>
        /// <param name="sectionTitle">OPTIONAL title of the section.  DEFAULT is a section with no title.</param>
        /// <param name="sectionAdditionalInfo">OPTIONAL additional information related to the section.  DEFAULT no additional information.</param>
        /// <param name="sectionFormat">OPTIONAL format to apply to the section.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="treeTableFormat">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="tableColumnsColumnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        /// <param name="columnIdPrefix">OPTIONAL prefix to use for column identifiers.  If specified, the column number (starting at 1) will be appended to this prefix and used as the column identifier.  DEFAULT is to not assign any identifiers to columns.</param>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="footerRows">OPTIONAL footer rows.  DEFAULT is no footer rows.</param>
        /// <param name="tableRowsRowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="dataRowsFormat">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="rowId">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <returns>
        /// A <see cref="Report"/> containing the specified cells in a single data row.
        /// </returns>
        public static Report ToReport(
            this IReadOnlyList<ICell> cells,
            string id = null,
            string title = null,
            DateTime? timestampUtc = null,
            IReadOnlyList<DownloadKind> downloadKinds = null,
            AdditionalReportInfo additionalInfo = null,
            ReportFormat format = null,
            string sectionId = DefaultIds.DefaultSectionId,
            string sectionName = null,
            string sectionTitle = null,
            AdditionalSectionInfo sectionAdditionalInfo = null,
            SectionFormat sectionFormat = null,
            TableFormat treeTableFormat = null,
            ColumnFormat tableColumnsColumnsFormat = null,
            string columnIdPrefix = null,
            HeaderRows headerRows = null,
            FooterRows footerRows = null,
            RowFormat tableRowsRowsFormat = null,
            DataRowsFormat dataRowsFormat = null,
            string rowId = null)
        {
            var result = cells
                .ToFlatRow(rowId)
                .ToReport(id, title, timestampUtc, downloadKinds, additionalInfo, format, sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat);

            return result;
        }

        /// <summary>
        /// Converts an ordered collection of <see cref="ICell"/>s into a <see cref="Section"/>, treating them as cells in a single data row.
        /// </summary>
        /// <param name="cells">The cells.</param>
        /// <param name="id">OPTIONAL section unique identifier.  DEFAULT is to use <see cref="DefaultIds.DefaultSectionId"/>.</param>
        /// <param name="name">OPTIONAL name of the section.  DEFAULT is a section with no name.</param>
        /// <param name="title">OPTIONAL title of the section.  DEFAULT is a section with no title.</param>
        /// <param name="additionalInfo">OPTIONAL additional information related to the section.  DEFAULT no additional information.</param>
        /// <param name="format">OPTIONAL format to apply to the section.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="treeTableFormat">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="tableColumnsColumnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        /// <param name="columnIdPrefix">OPTIONAL prefix to use for column identifiers.  If specified, the column number (starting at 1) will be appended to this prefix and used as the column identifier.  DEFAULT is to not assign any identifiers to columns.</param>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="footerRows">OPTIONAL footer rows.  DEFAULT is no footer rows.</param>
        /// <param name="tableRowsRowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="dataRowsFormat">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="rowId">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <returns>
        /// A <see cref="Section"/> containing the specified cells in a single data row.
        /// </returns>
        public static Section ToSection(
            this IReadOnlyList<ICell> cells,
            string id = DefaultIds.DefaultSectionId,
            string name = null,
            string title = null,
            AdditionalSectionInfo additionalInfo = null,
            SectionFormat format = null,
            TableFormat treeTableFormat = null,
            ColumnFormat tableColumnsColumnsFormat = null,
            string columnIdPrefix = null,
            HeaderRows headerRows = null,
            FooterRows footerRows = null,
            RowFormat tableRowsRowsFormat = null,
            DataRowsFormat dataRowsFormat = null,
            string rowId = null)
        {
            var result = cells
                .ToFlatRow(rowId)
                .ToSection(id, name, title, additionalInfo, format, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat);

            return result;
        }

        /// <summary>
        /// Converts an ordered collection of <see cref="ICell"/>s into a <see cref="TreeTable"/>, treating them as cells in a single data row.
        /// </summary>
        /// <param name="cells">The cells.</param>
        /// <param name="format">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="tableColumnsColumnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        /// <param name="columnIdPrefix">OPTIONAL prefix to use for column identifiers.  If specified, the column number (starting at 1) will be appended to this prefix and used as the column identifier.  DEFAULT is to not assign any identifiers to columns.</param>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="footerRows">OPTIONAL footer rows.  DEFAULT is no footer rows.</param>
        /// <param name="tableRowsRowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="dataRowsFormat">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="rowId">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <returns>
        /// A <see cref="TreeTable"/> containing the specified cells in a single data row.
        /// </returns>
        public static TreeTable ToTreeTable(
            this IReadOnlyList<ICell> cells,
            TableFormat format = null,
            ColumnFormat tableColumnsColumnsFormat = null,
            string columnIdPrefix = null,
            HeaderRows headerRows = null,
            FooterRows footerRows = null,
            RowFormat tableRowsRowsFormat = null,
            DataRowsFormat dataRowsFormat = null,
            string rowId = null)
        {
            var result = cells
                .ToFlatRow(rowId)
                .ToTreeTable(format, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat);

            return result;
        }

        /// <summary>
        /// Converts an <see cref="ICell"/> into a <see cref="Report"/>, treating it as a cell in a single data row.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="id">OPTIONAL report unique identifier.  DEFAULT is to use a randomly assigned GUID.</param>
        /// <param name="title">OPTIONAL title of the report.  DEFAULT is a report with no title.</param>
        /// <param name="timestampUtc">OPTIONAL timestamp of the report, in UTC.  DEFAULT is a report that is not timestamped.</param>
        /// <param name="downloadKinds">OPTIONAL kinds of download supported.  DEFAULT is no download option.</param>
        /// <param name="additionalInfo">OPTIONAL additional information related to the report.  DEFAULT no additional information.</param>
        /// <param name="format">OPTIONAL format to apply to the report.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="sectionId">OPTIONAL section unique identifier.  DEFAULT is to use <see cref="DefaultIds.DefaultSectionId"/>.</param>
        /// <param name="sectionName">OPTIONAL name of the section.  DEFAULT is a section with no name.</param>
        /// <param name="sectionTitle">OPTIONAL title of the section.  DEFAULT is a section with no title.</param>
        /// <param name="sectionAdditionalInfo">OPTIONAL additional information related to the section.  DEFAULT no additional information.</param>
        /// <param name="sectionFormat">OPTIONAL format to apply to the section.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="treeTableFormat">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="tableColumnsColumnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        /// <param name="columnIdPrefix">OPTIONAL prefix to use for column identifiers.  If specified, the column number (starting at 1) will be appended to this prefix and used as the column identifier.  DEFAULT is to not assign any identifiers to columns.</param>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="footerRows">OPTIONAL footer rows.  DEFAULT is no footer rows.</param>
        /// <param name="tableRowsRowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="dataRowsFormat">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="rowId">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <returns>
        /// A <see cref="Report"/> containing the specified cell in a single data row.
        /// </returns>
        public static Report ToReport(
            this ICell cell,
            string id = null,
            string title = null,
            DateTime? timestampUtc = null,
            IReadOnlyList<DownloadKind> downloadKinds = null,
            AdditionalReportInfo additionalInfo = null,
            ReportFormat format = null,
            string sectionId = DefaultIds.DefaultSectionId,
            string sectionName = null,
            string sectionTitle = null,
            AdditionalSectionInfo sectionAdditionalInfo = null,
            SectionFormat sectionFormat = null,
            TableFormat treeTableFormat = null,
            ColumnFormat tableColumnsColumnsFormat = null,
            string columnIdPrefix = null,
            HeaderRows headerRows = null,
            FooterRows footerRows = null,
            RowFormat tableRowsRowsFormat = null,
            DataRowsFormat dataRowsFormat = null,
            string rowId = null)
        {
            var result = new[] { cell }
                .ToReport(id, title, timestampUtc, downloadKinds, additionalInfo, format, sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat, rowId);

            return result;
        }

        /// <summary>
        /// Converts an <see cref="ICell"/> into a <see cref="Section"/>, treating it as as a cell in a single data row.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="id">OPTIONAL section unique identifier.  DEFAULT is to use <see cref="DefaultIds.DefaultSectionId"/>.</param>
        /// <param name="name">OPTIONAL name of the section.  DEFAULT is a section with no name.</param>
        /// <param name="title">OPTIONAL title of the section.  DEFAULT is a section with no title.</param>
        /// <param name="additionalInfo">OPTIONAL additional information related to the section.  DEFAULT no additional information.</param>
        /// <param name="format">OPTIONAL format to apply to the section.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="treeTableFormat">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="tableColumnsColumnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        /// <param name="columnIdPrefix">OPTIONAL prefix to use for column identifiers.  If specified, the column number (starting at 1) will be appended to this prefix and used as the column identifier.  DEFAULT is to not assign any identifiers to columns.</param>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="footerRows">OPTIONAL footer rows.  DEFAULT is no footer rows.</param>
        /// <param name="tableRowsRowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="dataRowsFormat">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="rowId">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <returns>
        /// A <see cref="Section"/> containing the specified cell in a single data row.
        /// </returns>
        public static Section ToSection(
            this ICell cell,
            string id = DefaultIds.DefaultSectionId,
            string name = null,
            string title = null,
            AdditionalSectionInfo additionalInfo = null,
            SectionFormat format = null,
            TableFormat treeTableFormat = null,
            ColumnFormat tableColumnsColumnsFormat = null,
            string columnIdPrefix = null,
            HeaderRows headerRows = null,
            FooterRows footerRows = null,
            RowFormat tableRowsRowsFormat = null,
            DataRowsFormat dataRowsFormat = null,
            string rowId = null)
        {
            var result = new[] { cell }
                .ToSection(id, name, title, additionalInfo, format, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat, rowId);

            return result;
        }

        /// <summary>
        /// Converts an <see cref="ICell"/> into a <see cref="TreeTable"/>, treating it as a cell in a single data row.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="format">OPTIONAL format to apply to the whole table.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="tableColumnsColumnsFormat">
        /// OPTIONAL format to apply to all columns in the table, individually.
        /// Some format-items, by their nature, apply to the whole column (e.g. <see cref="ColumnFormat.WidthInPixels"/>, <see cref="ColumnFormat.AutofitColumnWidth"/>).
        /// All others will be applied to just the <see cref="DataRows"/> within the column (e.g. <see cref="ColumnFormat.CellsFormat"/>
        /// will NOT be applied to <see cref="HeaderRows"/> nor <see cref="FooterRows"/>).
        /// DEFAULT is to leave the format unchanged.
        /// </param>
        /// <param name="columnIdPrefix">OPTIONAL prefix to use for column identifiers.  If specified, the column number (starting at 1) will be appended to this prefix and used as the column identifier.  DEFAULT is to not assign any identifiers to columns.</param>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="footerRows">OPTIONAL footer rows.  DEFAULT is no footer rows.</param>
        /// <param name="tableRowsRowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="dataRowsFormat">OPTIONAL format to apply to all data rows.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="rowId">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <returns>
        /// A <see cref="TreeTable"/> containing the specified cell in a single data row.
        /// </returns>
        public static TreeTable ToTreeTable(
            this ICell cell,
            TableFormat format = null,
            ColumnFormat tableColumnsColumnsFormat = null,
            string columnIdPrefix = null,
            HeaderRows headerRows = null,
            FooterRows footerRows = null,
            RowFormat tableRowsRowsFormat = null,
            DataRowsFormat dataRowsFormat = null,
            string rowId = null)
        {
            var result = new[] { cell }
                .ToTreeTable(format, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat, rowId);

            return result;
        }

        /// <summary>
        /// Converts an ordered collection of <see cref="ICell"/> to a <see cref="FlatRow"/>.
        /// </summary>
        /// <param name="cells">The cell.</param>
        /// <param name="id">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// The <see cref="FlatRow"/>.
        /// </returns>
        public static FlatRow ToFlatRow(
            this IReadOnlyList<ICell> cells,
            string id = null,
            RowFormat format = null)
        {
            var result = new FlatRow(cells, id, format);

            return result;
        }

        /// <summary>
        /// Converts an ordered collection of <see cref="ICell"/> to a <see cref="Row"/>.
        /// </summary>
        /// <param name="cells">The cells.</param>
        /// <param name="id">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="childRows">OPTIONAL child rows.  DEFAULT is none.</param>
        /// <param name="expandedSummaryRows">OPTIONAL rows that summarizes the children (e.g. a Total row) when this row is expanded.  DEFAULT is to forgo summary rows when this row is expanded.</param>
        /// <param name="collapsedSummaryRows">OPTIONAL rows that summarizes the children (e.g. a Total row) when this row is collapsed.  DEFAULT is to forgo summary rows when this row is collapsed.</param>
        /// <returns>
        /// The <see cref="Row"/>.
        /// </returns>
        public static Row ToRow(
            this IReadOnlyList<ICell> cells,
            string id = null,
            RowFormat format = null,
            IReadOnlyList<RowBase> childRows = null,
            IReadOnlyList<FlatRow> expandedSummaryRows = null,
            IReadOnlyList<FlatRow> collapsedSummaryRows = null)
        {
            var result = new Row(cells, id, format, childRows, expandedSummaryRows, collapsedSummaryRows);

            return result;
        }

        /// <summary>
        /// Converts an <see cref="ICell"/> to a single-cell <see cref="FlatRow"/>.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="id">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        /// <returns>
        /// The single-cell <see cref="FlatRow"/>.
        /// </returns>
        public static FlatRow ToFlatRow(
            this ICell cell,
            string id = null,
            RowFormat format = null)
        {
            var result = new[] { cell }
                .ToFlatRow(id, format);

            return result;
        }

        /// <summary>
        /// Converts an <see cref="ICell"/> to a single-cell <see cref="Row"/>.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="id">OPTIONAL row identifier.  DEFAULT is a row without an identifier.</param>
        /// <param name="format">OPTIONAL format to apply to the whole row.  DEFAULT is to leave the format unchanged.</param>
        /// <param name="childRows">OPTIONAL child rows.  DEFAULT is none.</param>
        /// <param name="expandedSummaryRows">OPTIONAL rows that summarizes the children (e.g. a Total row) when this row is expanded.  DEFAULT is to forgo summary rows when this row is expanded.</param>
        /// <param name="collapsedSummaryRows">OPTIONAL rows that summarizes the children (e.g. a Total row) when this row is collapsed.  DEFAULT is to forgo summary rows when this row is collapsed.</param>
        /// <returns>
        /// The single-cell <see cref="Row"/>.
        /// </returns>
        public static Row ToRow(
            this ICell cell,
            string id = null,
            RowFormat format = null,
            IReadOnlyList<RowBase> childRows = null,
            IReadOnlyList<FlatRow> expandedSummaryRows = null,
            IReadOnlyList<FlatRow> collapsedSummaryRows = null)
        {
            var result = new[] { cell }
                .ToRow(id, format, childRows, expandedSummaryRows, collapsedSummaryRows);

            return result;
        }
    }
}
