// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using Xunit;

    public static class ToExtensionsTest
    {
        [Fact]
        public static void ToReport_Section___Should_return_report___When_called()
        {
            // Arrange
            var section = A.Dummy<Section>();
            var reportId = A.Dummy<string>();
            var reportTitle = A.Dummy<string>();
            var reportTimestampUtc = (DateTime?)A.Dummy<UtcDateTime>();
            var reportDownloadKinds = A.Dummy<IReadOnlyList<DownloadKind>>();
            var reportAdditionalInfo = A.Dummy<AdditionalReportInfo>();
            var reportFormat = A.Dummy<ReportFormat>();

            // Act
            var actual = section.ToReport(reportId, reportTitle, reportTimestampUtc, reportDownloadKinds, reportAdditionalInfo, reportFormat);

            // Assert
            actual.Sections.AsTest().Must().HaveCount(1);
            actual.Sections.First().Must().BeSameReferenceAs(section);
            actual.Title.Must().BeSameReferenceAs(reportTitle);
            actual.TimestampUtc.Must().BeEqualTo(reportTimestampUtc);
            actual.DownloadKinds.Must().BeSameReferenceAs(reportDownloadKinds);
            actual.AdditionalInfo.Must().BeSameReferenceAs(reportAdditionalInfo);
            actual.Format.Must().BeSameReferenceAs(reportFormat);
        }

        [Fact]
        public static void ToReport_Section___Should_assign_GUID_id___When_id_is_null()
        {
            // Arrange
            var section = A.Dummy<Section>();

            // Act
            var actual = section.ToReport(null);

            // Assert
            var ex = Record.Exception(() => Guid.Parse(actual.Id));
            ex.AsTest().Must().BeNull();
        }

        [Fact]
        public static void ToReport_TreeTable___Should_return_report___When_called()
        {
            // Arrange
            var reportId = A.Dummy<string>();
            var reportTitle = A.Dummy<string>();
            var reportTimestampUtc = (DateTime?)A.Dummy<UtcDateTime>();
            var reportDownloadKinds = A.Dummy<IReadOnlyList<DownloadKind>>();
            var reportAdditionalInfo = A.Dummy<AdditionalReportInfo>();
            var reportFormat = A.Dummy<ReportFormat>();
            var sectionId = A.Dummy<string>();
            var sectionName = A.Dummy<string>();
            var sectionTitle = A.Dummy<string>();
            var sectionAdditionalInfo = A.Dummy<AdditionalSectionInfo>();
            var sectionFormat = A.Dummy<SectionFormat>();

            var treeTable = A.Dummy<TreeTable>();

            // Act
            var actual = treeTable.ToReport(reportId, reportTitle, reportTimestampUtc, reportDownloadKinds, reportAdditionalInfo, reportFormat, sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat);

            // Assert
            actual.Sections.AsTest().Must().HaveCount(1);
            actual.Sections.First().TreeTable.AsTest().Must().BeSameReferenceAs(treeTable);
            actual.Sections.First().Id.AsTest().Must().BeSameReferenceAs(sectionId);
            actual.Sections.First().Name.AsTest().Must().BeSameReferenceAs(sectionName);
            actual.Sections.First().Title.AsTest().Must().BeSameReferenceAs(sectionTitle);
            actual.Sections.First().AdditionalInfo.AsTest().Must().BeSameReferenceAs(sectionAdditionalInfo);
            actual.Sections.First().Format.AsTest().Must().BeSameReferenceAs(sectionFormat);
            actual.Title.Must().BeSameReferenceAs(reportTitle);
            actual.TimestampUtc.Must().BeEqualTo(reportTimestampUtc);
            actual.DownloadKinds.Must().BeSameReferenceAs(reportDownloadKinds);
            actual.AdditionalInfo.Must().BeSameReferenceAs(reportAdditionalInfo);
            actual.Format.Must().BeSameReferenceAs(reportFormat);
        }

        [Fact]
        public static void ToReport_TreeTable___Should_assign_GUID_id___When_id_is_null()
        {
            // Arrange
            var section = A.Dummy<TreeTable>();

            // Act
            var actual = section.ToReport(null);

            // Assert
            var ex = Record.Exception(() => Guid.Parse(actual.Id));
            ex.AsTest().Must().BeNull();
        }

        [Fact]
        public static void ToSection_TreeTable___Should_return_section___When_called()
        {
            // Arrange
            var sectionId = A.Dummy<string>();
            var sectionName = A.Dummy<string>();
            var sectionTitle = A.Dummy<string>();
            var sectionAdditionalInfo = A.Dummy<AdditionalSectionInfo>();
            var sectionFormat = A.Dummy<SectionFormat>();

            var treeTable = A.Dummy<TreeTable>();

            // Act
            var actual = treeTable.ToSection(sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat);

            // Assert
            actual.TreeTable.AsTest().Must().BeSameReferenceAs(treeTable);
            actual.Id.AsTest().Must().BeSameReferenceAs(sectionId);
            actual.Name.AsTest().Must().BeSameReferenceAs(sectionName);
            actual.Title.AsTest().Must().BeSameReferenceAs(sectionTitle);
            actual.AdditionalInfo.AsTest().Must().BeSameReferenceAs(sectionAdditionalInfo);
            actual.Format.AsTest().Must().BeSameReferenceAs(sectionFormat);
        }

        [Fact]
        public static void ToReport_IReadOnlyList_RowBase___Should_return_report___When_called()
        {
            // Arrange
            var numberOfColumns = 3;
            var reportId = A.Dummy<string>();
            var reportTitle = A.Dummy<string>();
            var reportTimestampUtc = (DateTime?)A.Dummy<UtcDateTime>();
            var reportDownloadKinds = A.Dummy<IReadOnlyList<DownloadKind>>();
            var reportAdditionalInfo = A.Dummy<AdditionalReportInfo>();
            var reportFormat = A.Dummy<ReportFormat>();
            var sectionId = A.Dummy<string>();
            var sectionName = A.Dummy<string>();
            var sectionTitle = A.Dummy<string>();
            var sectionAdditionalInfo = A.Dummy<AdditionalSectionInfo>();
            var sectionFormat = A.Dummy<SectionFormat>();
            var treeTableFormat = A.Dummy<TableFormat>();
            var tableColumnsColumnsFormat = A.Dummy<ColumnFormat>();
            var columnIdPrefix = "prefix-";
            var tableRowsRowsFormat = A.Dummy<RowFormat>();
            var dataRowsFormat = A.Dummy<DataRowsFormat>();

            HeaderRows headerRows = null;
            FooterRows footerRows = null;
            IReadOnlyList<RowBase> rows = null;
            while (headerRows == null || ((headerRows.Rows.Count + footerRows.Rows.Count + rows.Count) == 0))
            {
                headerRows = A.Dummy<HeaderRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
                footerRows = A.Dummy<FooterRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
                rows = DataStructureDummyFactory.BuildRowBases(numberOfColumns);
            }

            // Act
            var actual = rows.ToReport(reportId, reportTitle, reportTimestampUtc, reportDownloadKinds, reportAdditionalInfo, reportFormat, sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat);

            // Assert
            actual.Sections.AsTest().Must().HaveCount(1);
            actual.Sections.First().TreeTable.Format.AsTest().Must().BeSameReferenceAs(treeTableFormat);
            actual.Sections.First().TreeTable.TableColumns.ColumnsFormat.AsTest().Must().BeSameReferenceAs(tableColumnsColumnsFormat);
            actual.Sections.First().TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.Sections.First().TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new[] { "prefix-1", "prefix-2", "prefix-3" });
            actual.Sections.First().TreeTable.TableRows.HeaderRows.AsTest().Must().BeSameReferenceAs(headerRows);
            actual.Sections.First().TreeTable.TableRows.FooterRows.AsTest().Must().BeSameReferenceAs(footerRows);
            actual.Sections.First().TreeTable.TableRows.RowsFormat.AsTest().Must().BeSameReferenceAs(tableRowsRowsFormat);
            actual.Sections.First().TreeTable.TableRows.DataRows.Format.AsTest().Must().BeSameReferenceAs(dataRowsFormat);
            actual.Sections.First().TreeTable.TableRows.DataRows.Rows.AsTest().Must().BeSameReferenceAs(rows);

            actual.Sections.First().Id.AsTest().Must().BeSameReferenceAs(sectionId);
            actual.Sections.First().Name.AsTest().Must().BeSameReferenceAs(sectionName);
            actual.Sections.First().Title.AsTest().Must().BeSameReferenceAs(sectionTitle);
            actual.Sections.First().AdditionalInfo.AsTest().Must().BeSameReferenceAs(sectionAdditionalInfo);
            actual.Sections.First().Format.AsTest().Must().BeSameReferenceAs(sectionFormat);
            actual.Title.Must().BeSameReferenceAs(reportTitle);
            actual.TimestampUtc.Must().BeEqualTo(reportTimestampUtc);
            actual.DownloadKinds.Must().BeSameReferenceAs(reportDownloadKinds);
            actual.AdditionalInfo.Must().BeSameReferenceAs(reportAdditionalInfo);
            actual.Format.Must().BeSameReferenceAs(reportFormat);
        }

        [Fact]
        public static void ToReport_IReadOnlyList_RowBase___Should_assign_GUID_id___When_id_is_null()
        {
            // Arrange
            var numberOfColumns = 3;
            var rows = DataStructureDummyFactory.BuildRowBases(numberOfColumns);

            // Act
            var actual = rows.ToReport(null);

            // Assert
            var ex = Record.Exception(() => Guid.Parse(actual.Id));
            ex.AsTest().Must().BeNull();
        }

        [Fact]
        public static void ToReport_IReadOnlyList_RowBase___Should_return_columns_with_null_ids___When_columnIdPrefix_is_null()
        {
            // Arrange
            var numberOfColumns = 3;
            var rows = DataStructureDummyFactory.BuildRowBases(numberOfColumns);

            // Act
            var actual = rows.ToReport(columnIdPrefix: null);

            // Assert
            actual.Sections.First().TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(rows.Any() ? numberOfColumns : 1);
            actual.Sections.First().TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(rows.Any() ? new string[] { null, null, null } : new string[] { null });
        }

        [Fact]
        public static void ToReport_IReadOnlyList_RowBase___Should_return_one_column___When_rows_is_empty_and_no_header_nor_footer_rows_specified()
        {
            // Arrange
            var rows = new RowBase[0];

            // Act
            var actual = rows.ToReport(columnIdPrefix: "prefix-");

            // Assert
            actual.Sections.First().TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(1);
            actual.Sections.First().TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new string[] { "prefix-1" });
        }

        [Fact]
        public static void ToSection_IReadOnlyList_RowBase___Should_return_report___When_called()
        {
            // Arrange
            var numberOfColumns = 3;
            var sectionId = A.Dummy<string>();
            var sectionName = A.Dummy<string>();
            var sectionTitle = A.Dummy<string>();
            var sectionAdditionalInfo = A.Dummy<AdditionalSectionInfo>();
            var sectionFormat = A.Dummy<SectionFormat>();
            var treeTableFormat = A.Dummy<TableFormat>();
            var tableColumnsColumnsFormat = A.Dummy<ColumnFormat>();
            var columnIdPrefix = "prefix-";
            var tableRowsRowsFormat = A.Dummy<RowFormat>();
            var dataRowsFormat = A.Dummy<DataRowsFormat>();

            HeaderRows headerRows = null;
            FooterRows footerRows = null;
            IReadOnlyList<RowBase> rows = null;
            while (headerRows == null || ((headerRows.Rows.Count + footerRows.Rows.Count + rows.Count) == 0))
            {
                headerRows = A.Dummy<HeaderRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
                footerRows = A.Dummy<FooterRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
                rows = DataStructureDummyFactory.BuildRowBases(numberOfColumns);
            }

            // Act
            var actual = rows.ToSection(sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat);

            // Assert
            actual.TreeTable.Format.AsTest().Must().BeSameReferenceAs(treeTableFormat);
            actual.TreeTable.TableColumns.ColumnsFormat.AsTest().Must().BeSameReferenceAs(tableColumnsColumnsFormat);
            actual.TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new[] { "prefix-1", "prefix-2", "prefix-3" });
            actual.TreeTable.TableRows.HeaderRows.AsTest().Must().BeSameReferenceAs(headerRows);
            actual.TreeTable.TableRows.FooterRows.AsTest().Must().BeSameReferenceAs(footerRows);
            actual.TreeTable.TableRows.RowsFormat.AsTest().Must().BeSameReferenceAs(tableRowsRowsFormat);
            actual.TreeTable.TableRows.DataRows.Format.AsTest().Must().BeSameReferenceAs(dataRowsFormat);
            actual.TreeTable.TableRows.DataRows.Rows.AsTest().Must().BeSameReferenceAs(rows);
            actual.Id.AsTest().Must().BeSameReferenceAs(sectionId);
            actual.Name.AsTest().Must().BeSameReferenceAs(sectionName);
            actual.Title.AsTest().Must().BeSameReferenceAs(sectionTitle);
            actual.AdditionalInfo.AsTest().Must().BeSameReferenceAs(sectionAdditionalInfo);
            actual.Format.AsTest().Must().BeSameReferenceAs(sectionFormat);
        }

        [Fact]
        public static void ToSection_IReadOnlyList_RowBase___Should_return_columns_with_null_ids___When_columnIdPrefix_is_null()
        {
            // Arrange
            var numberOfColumns = 3;
            var rows = DataStructureDummyFactory.BuildRowBases(numberOfColumns);

            // Act
            var actual = rows.ToSection(columnIdPrefix: null);

            // Assert
            actual.TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(rows.Any() ? numberOfColumns : 1);
            actual.TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(rows.Any() ? new string[] { null, null, null } : new string[] { null });
        }

        [Fact]
        public static void ToSection_IReadOnlyList_RowBase___Should_return_one_column___When_rows_is_empty_and_no_header_nor_footer_rows_specified()
        {
            // Arrange
            var rows = new RowBase[0];

            // Act
            var actual = rows.ToSection(columnIdPrefix: "prefix-");

            // Assert
            actual.TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(1);
            actual.TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new string[] { "prefix-1" });
        }

        [Fact]
        public static void ToTreeTable_IReadOnlyList_RowBase___Should_return_report___When_called()
        {
            // Arrange
            var numberOfColumns = 3;
            var treeTableFormat = A.Dummy<TableFormat>();
            var tableColumnsColumnsFormat = A.Dummy<ColumnFormat>();
            var columnIdPrefix = "prefix-";
            var tableRowsRowsFormat = A.Dummy<RowFormat>();
            var dataRowsFormat = A.Dummy<DataRowsFormat>();

            HeaderRows headerRows = null;
            FooterRows footerRows = null;
            IReadOnlyList<RowBase> rows = null;
            while (headerRows == null || ((headerRows.Rows.Count + footerRows.Rows.Count + rows.Count) == 0))
            {
                headerRows = A.Dummy<HeaderRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
                footerRows = A.Dummy<FooterRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
                rows = DataStructureDummyFactory.BuildRowBases(numberOfColumns);
            }

            // Act
            var actual = rows.ToTreeTable(treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat);

            // Assert
            actual.Format.AsTest().Must().BeSameReferenceAs(treeTableFormat);
            actual.TableColumns.ColumnsFormat.AsTest().Must().BeSameReferenceAs(tableColumnsColumnsFormat);
            actual.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new[] { "prefix-1", "prefix-2", "prefix-3" });
            actual.TableRows.HeaderRows.AsTest().Must().BeSameReferenceAs(headerRows);
            actual.TableRows.FooterRows.AsTest().Must().BeSameReferenceAs(footerRows);
            actual.TableRows.RowsFormat.AsTest().Must().BeSameReferenceAs(tableRowsRowsFormat);
            actual.TableRows.DataRows.Format.AsTest().Must().BeSameReferenceAs(dataRowsFormat);
            actual.TableRows.DataRows.Rows.AsTest().Must().BeSameReferenceAs(rows);
        }

        [Fact]
        public static void ToTreeTable_IReadOnlyList_RowBase___Should_return_columns_with_null_ids___When_columnIdPrefix_is_null()
        {
            // Arrange
            var numberOfColumns = 3;
            var rows = DataStructureDummyFactory.BuildRowBases(numberOfColumns);

            // Act
            var actual = rows.ToTreeTable(columnIdPrefix: null);

            // Assert
            actual.TableColumns.Columns.AsTest().Must().HaveCount(rows.Any() ? numberOfColumns : 1);
            actual.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(rows.Any() ? new string[] { null, null, null } : new string[] { null });
        }

        [Fact]
        public static void ToTreeTable_IReadOnlyList_RowBase___Should_return_one_column___When_rows_is_empty_and_no_header_nor_footer_rows_specified()
        {
            // Arrange
            var rows = new RowBase[0];

            // Act
            var actual = rows.ToTreeTable(columnIdPrefix: "prefix-");

            // Assert
            actual.TableColumns.Columns.AsTest().Must().HaveCount(1);
            actual.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new string[] { "prefix-1" });
        }

        [Fact]
        public static void ToReport_RowBase___Should_return_report___When_called()
        {
            // Arrange
            var numberOfColumns = 3;
            var reportId = A.Dummy<string>();
            var reportTitle = A.Dummy<string>();
            var reportTimestampUtc = (DateTime?)A.Dummy<UtcDateTime>();
            var reportDownloadKinds = A.Dummy<IReadOnlyList<DownloadKind>>();
            var reportAdditionalInfo = A.Dummy<AdditionalReportInfo>();
            var reportFormat = A.Dummy<ReportFormat>();
            var sectionId = A.Dummy<string>();
            var sectionName = A.Dummy<string>();
            var sectionTitle = A.Dummy<string>();
            var sectionAdditionalInfo = A.Dummy<AdditionalSectionInfo>();
            var sectionFormat = A.Dummy<SectionFormat>();
            var treeTableFormat = A.Dummy<TableFormat>();
            var tableColumnsColumnsFormat = A.Dummy<ColumnFormat>();
            var columnIdPrefix = "prefix-";
            var headerRows = A.Dummy<HeaderRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var footerRows = A.Dummy<FooterRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var tableRowsRowsFormat = A.Dummy<RowFormat>();
            var dataRowsFormat = A.Dummy<DataRowsFormat>();
            var row = DataStructureDummyFactory.BuildRowBase(numberOfColumns);

            // Act
            var actual = row.ToReport(reportId, reportTitle, reportTimestampUtc, reportDownloadKinds, reportAdditionalInfo, reportFormat, sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat);

            // Assert
            actual.Sections.AsTest().Must().HaveCount(1);
            actual.Sections.First().TreeTable.Format.AsTest().Must().BeSameReferenceAs(treeTableFormat);
            actual.Sections.First().TreeTable.TableColumns.ColumnsFormat.AsTest().Must().BeSameReferenceAs(tableColumnsColumnsFormat);
            actual.Sections.First().TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.Sections.First().TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new[] { "prefix-1", "prefix-2", "prefix-3" });
            actual.Sections.First().TreeTable.TableRows.HeaderRows.AsTest().Must().BeSameReferenceAs(headerRows);
            actual.Sections.First().TreeTable.TableRows.FooterRows.AsTest().Must().BeSameReferenceAs(footerRows);
            actual.Sections.First().TreeTable.TableRows.RowsFormat.AsTest().Must().BeSameReferenceAs(tableRowsRowsFormat);
            actual.Sections.First().TreeTable.TableRows.DataRows.Format.AsTest().Must().BeSameReferenceAs(dataRowsFormat);
            actual.Sections.First().TreeTable.TableRows.DataRows.Rows.AsTest().Must().HaveCount(1);
            actual.Sections.First().TreeTable.TableRows.DataRows.Rows.First().AsTest().Must().BeSameReferenceAs(row);

            actual.Sections.First().Id.AsTest().Must().BeSameReferenceAs(sectionId);
            actual.Sections.First().Name.AsTest().Must().BeSameReferenceAs(sectionName);
            actual.Sections.First().Title.AsTest().Must().BeSameReferenceAs(sectionTitle);
            actual.Sections.First().AdditionalInfo.AsTest().Must().BeSameReferenceAs(sectionAdditionalInfo);
            actual.Sections.First().Format.AsTest().Must().BeSameReferenceAs(sectionFormat);
            actual.Title.Must().BeSameReferenceAs(reportTitle);
            actual.TimestampUtc.Must().BeEqualTo(reportTimestampUtc);
            actual.DownloadKinds.Must().BeSameReferenceAs(reportDownloadKinds);
            actual.AdditionalInfo.Must().BeSameReferenceAs(reportAdditionalInfo);
            actual.Format.Must().BeSameReferenceAs(reportFormat);
        }

        [Fact]
        public static void ToReport_RowBase___Should_assign_GUID_id___When_id_is_null()
        {
            // Arrange
            var numberOfColumns = 3;
            var row = DataStructureDummyFactory.BuildRowBase(numberOfColumns);

            // Act
            var actual = row.ToReport(null);

            // Assert
            var ex = Record.Exception(() => Guid.Parse(actual.Id));
            ex.AsTest().Must().BeNull();
        }

        [Fact]
        public static void ToReport_RowBase___Should_return_columns_with_null_ids___When_columnIdPrefix_is_null()
        {
            // Arrange
            var numberOfColumns = 3;
            var row = DataStructureDummyFactory.BuildRowBase(numberOfColumns);

            // Act
            var actual = row.ToReport(columnIdPrefix: null);

            // Assert
            actual.Sections.First().TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.Sections.First().TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new string[] { null, null, null });
        }

        [Fact]
        public static void ToSection_RowBase___Should_return_report___When_called()
        {
            // Arrange
            var numberOfColumns = 3;
            var sectionId = A.Dummy<string>();
            var sectionName = A.Dummy<string>();
            var sectionTitle = A.Dummy<string>();
            var sectionAdditionalInfo = A.Dummy<AdditionalSectionInfo>();
            var sectionFormat = A.Dummy<SectionFormat>();
            var treeTableFormat = A.Dummy<TableFormat>();
            var tableColumnsColumnsFormat = A.Dummy<ColumnFormat>();
            var columnIdPrefix = "prefix-";
            var headerRows = A.Dummy<HeaderRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var footerRows = A.Dummy<FooterRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var tableRowsRowsFormat = A.Dummy<RowFormat>();
            var dataRowsFormat = A.Dummy<DataRowsFormat>();
            var row = DataStructureDummyFactory.BuildRowBase(numberOfColumns);

            // Act
            var actual = row.ToSection(sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat);

            // Assert
            actual.TreeTable.Format.AsTest().Must().BeSameReferenceAs(treeTableFormat);
            actual.TreeTable.TableColumns.ColumnsFormat.AsTest().Must().BeSameReferenceAs(tableColumnsColumnsFormat);
            actual.TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new[] { "prefix-1", "prefix-2", "prefix-3" });
            actual.TreeTable.TableRows.HeaderRows.AsTest().Must().BeSameReferenceAs(headerRows);
            actual.TreeTable.TableRows.FooterRows.AsTest().Must().BeSameReferenceAs(footerRows);
            actual.TreeTable.TableRows.RowsFormat.AsTest().Must().BeSameReferenceAs(tableRowsRowsFormat);
            actual.TreeTable.TableRows.DataRows.Format.AsTest().Must().BeSameReferenceAs(dataRowsFormat);
            actual.TreeTable.TableRows.DataRows.Rows.AsTest().Must().HaveCount(1);
            actual.TreeTable.TableRows.DataRows.Rows.First().AsTest().Must().BeSameReferenceAs(row);
            actual.Id.AsTest().Must().BeSameReferenceAs(sectionId);
            actual.Name.AsTest().Must().BeSameReferenceAs(sectionName);
            actual.Title.AsTest().Must().BeSameReferenceAs(sectionTitle);
            actual.AdditionalInfo.AsTest().Must().BeSameReferenceAs(sectionAdditionalInfo);
            actual.Format.AsTest().Must().BeSameReferenceAs(sectionFormat);
        }

        [Fact]
        public static void ToSection_RowBase___Should_return_columns_with_null_ids___When_columnIdPrefix_is_null()
        {
            // Arrange
            var numberOfColumns = 3;
            var row = DataStructureDummyFactory.BuildRowBase(numberOfColumns);

            // Act
            var actual = row.ToSection(columnIdPrefix: null);

            // Assert
            actual.TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new string[] { null, null, null });
        }

        [Fact]
        public static void ToTreeTable_RowBase___Should_return_report___When_called()
        {
            // Arrange
            var numberOfColumns = 3;
            var treeTableFormat = A.Dummy<TableFormat>();
            var tableColumnsColumnsFormat = A.Dummy<ColumnFormat>();
            var columnIdPrefix = "prefix-";
            var headerRows = A.Dummy<HeaderRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var footerRows = A.Dummy<FooterRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var tableRowsRowsFormat = A.Dummy<RowFormat>();
            var dataRowsFormat = A.Dummy<DataRowsFormat>();
            var row = DataStructureDummyFactory.BuildRowBase(numberOfColumns);

            // Act
            var actual = row.ToTreeTable(treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat);

            // Assert
            actual.Format.AsTest().Must().BeSameReferenceAs(treeTableFormat);
            actual.TableColumns.ColumnsFormat.AsTest().Must().BeSameReferenceAs(tableColumnsColumnsFormat);
            actual.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new[] { "prefix-1", "prefix-2", "prefix-3" });
            actual.TableRows.HeaderRows.AsTest().Must().BeSameReferenceAs(headerRows);
            actual.TableRows.FooterRows.AsTest().Must().BeSameReferenceAs(footerRows);
            actual.TableRows.RowsFormat.AsTest().Must().BeSameReferenceAs(tableRowsRowsFormat);
            actual.TableRows.DataRows.Format.AsTest().Must().BeSameReferenceAs(dataRowsFormat);
            actual.TableRows.DataRows.Rows.AsTest().Must().HaveCount(1);
            actual.TableRows.DataRows.Rows.First().AsTest().Must().BeSameReferenceAs(row);
        }

        [Fact]
        public static void ToTreeTable_RowBase___Should_return_columns_with_null_ids___When_columnIdPrefix_is_null()
        {
            // Arrange
            var numberOfColumns = 3;
            var row = DataStructureDummyFactory.BuildRowBase(numberOfColumns);

            // Act
            var actual = row.ToTreeTable(columnIdPrefix: null);

            // Assert
            actual.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new string[] { null, null, null });
        }

        [Fact]
        public static void ToReport_IReadOnlyList_ICell___Should_return_report___When_called()
        {
            // Arrange
            var numberOfColumns = 3;
            var reportId = A.Dummy<string>();
            var reportTitle = A.Dummy<string>();
            var reportTimestampUtc = (DateTime?)A.Dummy<UtcDateTime>();
            var reportDownloadKinds = A.Dummy<IReadOnlyList<DownloadKind>>();
            var reportAdditionalInfo = A.Dummy<AdditionalReportInfo>();
            var reportFormat = A.Dummy<ReportFormat>();
            var sectionId = A.Dummy<string>();
            var sectionName = A.Dummy<string>();
            var sectionTitle = A.Dummy<string>();
            var sectionAdditionalInfo = A.Dummy<AdditionalSectionInfo>();
            var sectionFormat = A.Dummy<SectionFormat>();
            var treeTableFormat = A.Dummy<TableFormat>();
            var tableColumnsColumnsFormat = A.Dummy<ColumnFormat>();
            var columnIdPrefix = "prefix-";
            var headerRows = A.Dummy<HeaderRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var footerRows = A.Dummy<FooterRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var tableRowsRowsFormat = A.Dummy<RowFormat>();
            var dataRowsFormat = A.Dummy<DataRowsFormat>();
            var rowId = A.Dummy<string>();
            var cells = DataStructureDummyFactory.BuildRowCells(numberOfColumns, true);

            // Act
            var actual = cells.ToReport(reportId, reportTitle, reportTimestampUtc, reportDownloadKinds, reportAdditionalInfo, reportFormat, sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat, rowId);

            // Assert
            actual.Sections.AsTest().Must().HaveCount(1);
            actual.Sections.First().TreeTable.Format.AsTest().Must().BeSameReferenceAs(treeTableFormat);
            actual.Sections.First().TreeTable.TableColumns.ColumnsFormat.AsTest().Must().BeSameReferenceAs(tableColumnsColumnsFormat);
            actual.Sections.First().TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.Sections.First().TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new[] { "prefix-1", "prefix-2", "prefix-3" });
            actual.Sections.First().TreeTable.TableRows.HeaderRows.AsTest().Must().BeSameReferenceAs(headerRows);
            actual.Sections.First().TreeTable.TableRows.FooterRows.AsTest().Must().BeSameReferenceAs(footerRows);
            actual.Sections.First().TreeTable.TableRows.RowsFormat.AsTest().Must().BeSameReferenceAs(tableRowsRowsFormat);
            actual.Sections.First().TreeTable.TableRows.DataRows.Format.AsTest().Must().BeSameReferenceAs(dataRowsFormat);
            actual.Sections.First().TreeTable.TableRows.DataRows.Rows.AsTest().Must().HaveCount(1);
            actual.Sections.First().TreeTable.TableRows.DataRows.Rows.First().Cells.Must().BeSameReferenceAs(cells);
            actual.Sections.First().Id.AsTest().Must().BeSameReferenceAs(sectionId);
            actual.Sections.First().Name.AsTest().Must().BeSameReferenceAs(sectionName);
            actual.Sections.First().Title.AsTest().Must().BeSameReferenceAs(sectionTitle);
            actual.Sections.First().AdditionalInfo.AsTest().Must().BeSameReferenceAs(sectionAdditionalInfo);
            actual.Sections.First().Format.AsTest().Must().BeSameReferenceAs(sectionFormat);
            actual.Title.Must().BeSameReferenceAs(reportTitle);
            actual.TimestampUtc.Must().BeEqualTo(reportTimestampUtc);
            actual.DownloadKinds.Must().BeSameReferenceAs(reportDownloadKinds);
            actual.AdditionalInfo.Must().BeSameReferenceAs(reportAdditionalInfo);
            actual.Format.Must().BeSameReferenceAs(reportFormat);
        }

        [Fact]
        public static void ToReport_IReadOnlyList_ICell___Should_assign_GUID_id___When_id_is_null()
        {
            // Arrange
            var numberOfColumns = 3;
            var cells = DataStructureDummyFactory.BuildRowCells(numberOfColumns, true);

            // Act
            var actual = cells.ToReport(null);

            // Assert
            var ex = Record.Exception(() => Guid.Parse(actual.Id));
            ex.AsTest().Must().BeNull();
        }

        [Fact]
        public static void ToReport_IReadOnlyList_ICell___Should_return_columns_with_null_ids___When_columnIdPrefix_is_null()
        {
            // Arrange
            var numberOfColumns = 3;
            var cells = DataStructureDummyFactory.BuildRowCells(numberOfColumns, false);

            // Act
            var actual = cells.ToReport(columnIdPrefix: null);

            // Assert
            actual.Sections.First().TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.Sections.First().TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new string[] { null, null, null });
        }

        [Fact]
        public static void ToSection_IReadOnlyList_ICell___Should_return_report___When_called()
        {
            // Arrange
            var numberOfColumns = 3;
            var sectionId = A.Dummy<string>();
            var sectionName = A.Dummy<string>();
            var sectionTitle = A.Dummy<string>();
            var sectionAdditionalInfo = A.Dummy<AdditionalSectionInfo>();
            var sectionFormat = A.Dummy<SectionFormat>();
            var treeTableFormat = A.Dummy<TableFormat>();
            var tableColumnsColumnsFormat = A.Dummy<ColumnFormat>();
            var columnIdPrefix = "prefix-";
            var headerRows = A.Dummy<HeaderRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var footerRows = A.Dummy<FooterRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var tableRowsRowsFormat = A.Dummy<RowFormat>();
            var dataRowsFormat = A.Dummy<DataRowsFormat>();
            var rowId = A.Dummy<string>();
            var cells = DataStructureDummyFactory.BuildRowCells(numberOfColumns, true);

            // Act
            var actual = cells.ToSection(sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat, rowId);

            // Assert
            actual.TreeTable.Format.AsTest().Must().BeSameReferenceAs(treeTableFormat);
            actual.TreeTable.TableColumns.ColumnsFormat.AsTest().Must().BeSameReferenceAs(tableColumnsColumnsFormat);
            actual.TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new[] { "prefix-1", "prefix-2", "prefix-3" });
            actual.TreeTable.TableRows.HeaderRows.AsTest().Must().BeSameReferenceAs(headerRows);
            actual.TreeTable.TableRows.FooterRows.AsTest().Must().BeSameReferenceAs(footerRows);
            actual.TreeTable.TableRows.RowsFormat.AsTest().Must().BeSameReferenceAs(tableRowsRowsFormat);
            actual.TreeTable.TableRows.DataRows.Format.AsTest().Must().BeSameReferenceAs(dataRowsFormat);
            actual.TreeTable.TableRows.DataRows.Rows.AsTest().Must().HaveCount(1);
            actual.TreeTable.TableRows.DataRows.Rows.First().Cells.AsTest().Must().BeSameReferenceAs(cells);
            actual.Id.AsTest().Must().BeSameReferenceAs(sectionId);
            actual.Name.AsTest().Must().BeSameReferenceAs(sectionName);
            actual.Title.AsTest().Must().BeSameReferenceAs(sectionTitle);
            actual.AdditionalInfo.AsTest().Must().BeSameReferenceAs(sectionAdditionalInfo);
            actual.Format.AsTest().Must().BeSameReferenceAs(sectionFormat);
        }

        [Fact]
        public static void ToSection_IReadOnlyList_ICell___Should_return_columns_with_null_ids___When_columnIdPrefix_is_null()
        {
            // Arrange
            var numberOfColumns = 3;
            var cells = DataStructureDummyFactory.BuildRowCells(numberOfColumns, true);

            // Act
            var actual = cells.ToSection(columnIdPrefix: null);

            // Assert
            actual.TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new string[] { null, null, null });
        }

        [Fact]
        public static void ToTreeTable_IReadOnlyList_ICell___Should_return_report___When_called()
        {
            // Arrange
            var numberOfColumns = 3;
            var treeTableFormat = A.Dummy<TableFormat>();
            var tableColumnsColumnsFormat = A.Dummy<ColumnFormat>();
            var columnIdPrefix = "prefix-";
            var headerRows = A.Dummy<HeaderRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var footerRows = A.Dummy<FooterRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var tableRowsRowsFormat = A.Dummy<RowFormat>();
            var dataRowsFormat = A.Dummy<DataRowsFormat>();
            var rowId = A.Dummy<string>();
            var cells = DataStructureDummyFactory.BuildRowCells(numberOfColumns, true);

            // Act
            var actual = cells.ToTreeTable(treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat, rowId);

            // Assert
            actual.Format.AsTest().Must().BeSameReferenceAs(treeTableFormat);
            actual.TableColumns.ColumnsFormat.AsTest().Must().BeSameReferenceAs(tableColumnsColumnsFormat);
            actual.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new[] { "prefix-1", "prefix-2", "prefix-3" });
            actual.TableRows.HeaderRows.AsTest().Must().BeSameReferenceAs(headerRows);
            actual.TableRows.FooterRows.AsTest().Must().BeSameReferenceAs(footerRows);
            actual.TableRows.RowsFormat.AsTest().Must().BeSameReferenceAs(tableRowsRowsFormat);
            actual.TableRows.DataRows.Format.AsTest().Must().BeSameReferenceAs(dataRowsFormat);
            actual.TableRows.DataRows.Rows.AsTest().Must().HaveCount(1);
            actual.TableRows.DataRows.Rows.First().Cells.AsTest().Must().BeSameReferenceAs(cells);
        }

        [Fact]
        public static void ToTreeTable_IReadOnlyList_ICell___Should_return_columns_with_null_ids___When_columnIdPrefix_is_null()
        {
            // Arrange
            var numberOfColumns = 3;
            var cells = DataStructureDummyFactory.BuildRowCells(numberOfColumns, true);

            // Act
            var actual = cells.ToTreeTable(columnIdPrefix: null);

            // Assert
            actual.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new string[] { null, null, null });
        }

        [Fact]
        public static void ToReport_ICell___Should_return_report___When_called()
        {
            // Arrange
            var numberOfColumns = 1;
            var reportId = A.Dummy<string>();
            var reportTitle = A.Dummy<string>();
            var reportTimestampUtc = (DateTime?)A.Dummy<UtcDateTime>();
            var reportDownloadKinds = A.Dummy<IReadOnlyList<DownloadKind>>();
            var reportAdditionalInfo = A.Dummy<AdditionalReportInfo>();
            var reportFormat = A.Dummy<ReportFormat>();
            var sectionId = A.Dummy<string>();
            var sectionName = A.Dummy<string>();
            var sectionTitle = A.Dummy<string>();
            var sectionAdditionalInfo = A.Dummy<AdditionalSectionInfo>();
            var sectionFormat = A.Dummy<SectionFormat>();
            var treeTableFormat = A.Dummy<TableFormat>();
            var tableColumnsColumnsFormat = A.Dummy<ColumnFormat>();
            var columnIdPrefix = "prefix-";
            var headerRows = A.Dummy<HeaderRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var footerRows = A.Dummy<FooterRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var tableRowsRowsFormat = A.Dummy<RowFormat>();
            var dataRowsFormat = A.Dummy<DataRowsFormat>();
            var rowId = A.Dummy<string>();
            var cell = DataStructureDummyFactory.BuildRowCells(numberOfColumns, false).First();

            // Act
            var actual = cell.ToReport(reportId, reportTitle, reportTimestampUtc, reportDownloadKinds, reportAdditionalInfo, reportFormat, sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat, rowId);

            // Assert
            actual.Sections.AsTest().Must().HaveCount(1);
            actual.Sections.First().TreeTable.Format.AsTest().Must().BeSameReferenceAs(treeTableFormat);
            actual.Sections.First().TreeTable.TableColumns.ColumnsFormat.AsTest().Must().BeSameReferenceAs(tableColumnsColumnsFormat);
            actual.Sections.First().TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.Sections.First().TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new[] { "prefix-1" });
            actual.Sections.First().TreeTable.TableRows.HeaderRows.AsTest().Must().BeSameReferenceAs(headerRows);
            actual.Sections.First().TreeTable.TableRows.FooterRows.AsTest().Must().BeSameReferenceAs(footerRows);
            actual.Sections.First().TreeTable.TableRows.RowsFormat.AsTest().Must().BeSameReferenceAs(tableRowsRowsFormat);
            actual.Sections.First().TreeTable.TableRows.DataRows.Format.AsTest().Must().BeSameReferenceAs(dataRowsFormat);
            actual.Sections.First().TreeTable.TableRows.DataRows.Rows.AsTest().Must().HaveCount(1);
            actual.Sections.First().TreeTable.TableRows.DataRows.Rows.First().Cells.Must().HaveCount(1);
            actual.Sections.First().TreeTable.TableRows.DataRows.Rows.First().Cells.First().Must().BeSameReferenceAs(cell);
            actual.Sections.First().Id.AsTest().Must().BeSameReferenceAs(sectionId);
            actual.Sections.First().Name.AsTest().Must().BeSameReferenceAs(sectionName);
            actual.Sections.First().Title.AsTest().Must().BeSameReferenceAs(sectionTitle);
            actual.Sections.First().AdditionalInfo.AsTest().Must().BeSameReferenceAs(sectionAdditionalInfo);
            actual.Sections.First().Format.AsTest().Must().BeSameReferenceAs(sectionFormat);
            actual.Title.Must().BeSameReferenceAs(reportTitle);
            actual.TimestampUtc.Must().BeEqualTo(reportTimestampUtc);
            actual.DownloadKinds.Must().BeSameReferenceAs(reportDownloadKinds);
            actual.AdditionalInfo.Must().BeSameReferenceAs(reportAdditionalInfo);
            actual.Format.Must().BeSameReferenceAs(reportFormat);
        }

        [Fact]
        public static void ToReport_ICell___Should_assign_GUID_id___When_id_is_null()
        {
            // Arrange
            var numberOfColumns = 1;
            var cell = DataStructureDummyFactory.BuildRowCells(numberOfColumns, false).First();

            // Act
            var actual = cell.ToReport(null);

            // Assert
            var ex = Record.Exception(() => Guid.Parse(actual.Id));
            ex.AsTest().Must().BeNull();
        }

        [Fact]
        public static void ToReport_ICell___Should_return_columns_with_null_ids___When_columnIdPrefix_is_null()
        {
            // Arrange
            var numberOfColumns = 1;
            var cell = DataStructureDummyFactory.BuildRowCells(numberOfColumns, false).First();

            // Act
            var actual = cell.ToReport(columnIdPrefix: null);

            // Assert
            actual.Sections.First().TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.Sections.First().TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new string[] { null });
        }

        [Fact]
        public static void ToReport_ICell___Should_not_throw___When_cell_spans_multiple_columns()
        {
            // Arrange
            var cell = A.Dummy<CellBase>().Whose(_ => _.GetType() != typeof(SlottedCell)).DeepCloneWithColumnsSpanned(3);

            // Act
            var actual = Record.Exception(() => cell.ToReport());

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void ToSection_ICell___Should_return_report___When_called()
        {
            // Arrange
            var numberOfColumns = 1;
            var sectionId = A.Dummy<string>();
            var sectionName = A.Dummy<string>();
            var sectionTitle = A.Dummy<string>();
            var sectionAdditionalInfo = A.Dummy<AdditionalSectionInfo>();
            var sectionFormat = A.Dummy<SectionFormat>();
            var treeTableFormat = A.Dummy<TableFormat>();
            var tableColumnsColumnsFormat = A.Dummy<ColumnFormat>();
            var columnIdPrefix = "prefix-";
            var headerRows = A.Dummy<HeaderRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var footerRows = A.Dummy<FooterRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var tableRowsRowsFormat = A.Dummy<RowFormat>();
            var dataRowsFormat = A.Dummy<DataRowsFormat>();
            var rowId = A.Dummy<string>();
            var cell = DataStructureDummyFactory.BuildRowCells(numberOfColumns, false).First();

            // Act
            var actual = cell.ToSection(sectionId, sectionName, sectionTitle, sectionAdditionalInfo, sectionFormat, treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat, rowId);

            // Assert
            actual.TreeTable.Format.AsTest().Must().BeSameReferenceAs(treeTableFormat);
            actual.TreeTable.TableColumns.ColumnsFormat.AsTest().Must().BeSameReferenceAs(tableColumnsColumnsFormat);
            actual.TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new[] { "prefix-1" });
            actual.TreeTable.TableRows.HeaderRows.AsTest().Must().BeSameReferenceAs(headerRows);
            actual.TreeTable.TableRows.FooterRows.AsTest().Must().BeSameReferenceAs(footerRows);
            actual.TreeTable.TableRows.RowsFormat.AsTest().Must().BeSameReferenceAs(tableRowsRowsFormat);
            actual.TreeTable.TableRows.DataRows.Format.AsTest().Must().BeSameReferenceAs(dataRowsFormat);
            actual.TreeTable.TableRows.DataRows.Rows.AsTest().Must().HaveCount(1);
            actual.TreeTable.TableRows.DataRows.Rows.First().Cells.AsTest().Must().HaveCount(1);
            actual.TreeTable.TableRows.DataRows.Rows.First().Cells.First().AsTest().Must().BeSameReferenceAs(cell);
            actual.Id.AsTest().Must().BeSameReferenceAs(sectionId);
            actual.Name.AsTest().Must().BeSameReferenceAs(sectionName);
            actual.Title.AsTest().Must().BeSameReferenceAs(sectionTitle);
            actual.AdditionalInfo.AsTest().Must().BeSameReferenceAs(sectionAdditionalInfo);
            actual.Format.AsTest().Must().BeSameReferenceAs(sectionFormat);
        }

        [Fact]
        public static void ToSection_ICell___Should_return_columns_with_null_ids___When_columnIdPrefix_is_null()
        {
            // Arrange
            var numberOfColumns = 1;
            var cell = DataStructureDummyFactory.BuildRowCells(numberOfColumns, false).First();

            // Act
            var actual = cell.ToSection(columnIdPrefix: null);

            // Assert
            actual.TreeTable.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TreeTable.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new string[] { null });
        }

        [Fact]
        public static void ToSection_ICell___Should_not_throw___When_cell_spans_multiple_columns()
        {
            // Arrange
            var cell = A.Dummy<CellBase>().Whose(_ => _.GetType() != typeof(SlottedCell)).DeepCloneWithColumnsSpanned(3);

            // Act
            var actual = Record.Exception(() => cell.ToSection());

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void ToTreeTable_ICell___Should_return_report___When_called()
        {
            // Arrange
            var numberOfColumns = 1;
            var treeTableFormat = A.Dummy<TableFormat>();
            var tableColumnsColumnsFormat = A.Dummy<ColumnFormat>();
            var columnIdPrefix = "prefix-";
            var headerRows = A.Dummy<HeaderRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var footerRows = A.Dummy<FooterRows>().DeepCloneWithRows(DataStructureDummyFactory.BuildFlatRows(numberOfColumns));
            var tableRowsRowsFormat = A.Dummy<RowFormat>();
            var dataRowsFormat = A.Dummy<DataRowsFormat>();
            var rowId = A.Dummy<string>();
            var cell = DataStructureDummyFactory.BuildRowCells(numberOfColumns, false).First();

            // Act
            var actual = cell.ToTreeTable(treeTableFormat, tableColumnsColumnsFormat, columnIdPrefix, headerRows, footerRows, tableRowsRowsFormat, dataRowsFormat, rowId);

            // Assert
            actual.Format.AsTest().Must().BeSameReferenceAs(treeTableFormat);
            actual.TableColumns.ColumnsFormat.AsTest().Must().BeSameReferenceAs(tableColumnsColumnsFormat);
            actual.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new[] { "prefix-1" });
            actual.TableRows.HeaderRows.AsTest().Must().BeSameReferenceAs(headerRows);
            actual.TableRows.FooterRows.AsTest().Must().BeSameReferenceAs(footerRows);
            actual.TableRows.RowsFormat.AsTest().Must().BeSameReferenceAs(tableRowsRowsFormat);
            actual.TableRows.DataRows.Format.AsTest().Must().BeSameReferenceAs(dataRowsFormat);
            actual.TableRows.DataRows.Rows.AsTest().Must().HaveCount(1);
            actual.TableRows.DataRows.Rows.First().Cells.AsTest().Must().HaveCount(1);
            actual.TableRows.DataRows.Rows.First().Cells.First().AsTest().Must().BeSameReferenceAs(cell);
        }

        [Fact]
        public static void ToTreeTable_ICell___Should_return_columns_with_null_ids___When_columnIdPrefix_is_null()
        {
            // Arrange
            var numberOfColumns = 1;
            var cell = DataStructureDummyFactory.BuildRowCells(numberOfColumns, false).First();

            // Act
            var actual = cell.ToTreeTable(columnIdPrefix: null);

            // Assert
            actual.TableColumns.Columns.AsTest().Must().HaveCount(numberOfColumns);
            actual.TableColumns.Columns.Select(_ => _.Id).ToArray().AsTest().Must().BeEqualTo(new string[] { null });
        }

        [Fact]
        public static void ToTreeTable_ICell___Should_not_throw___When_cell_spans_multiple_columns()
        {
            // Arrange
            var cell = A.Dummy<CellBase>().Whose(_ => _.GetType() != typeof(SlottedCell)).DeepCloneWithColumnsSpanned(3);

            // Act
            var actual = Record.Exception(() => cell.ToTreeTable());

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void ToFlatRow___Should_return_single_cell_FlatRow___When_called()
        {
            // Arrange
            var id = A.Dummy<string>();
            var cell = A.Dummy<ICell>();
            var format = A.Dummy<RowFormat>();

            var expected = new FlatRow(new[] { cell }, id, format);

            // Act
            var actual = cell.ToFlatRow(id, format);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ToRow___Should_return_single_cell_Row___When_called()
        {
            // Arrange
            var id = A.Dummy<string>();
            var cell = A.Dummy<ICell>();
            var format = A.Dummy<RowFormat>();
            var childRows = DataStructureDummyFactory.BuildRowBases(cell.ColumnsSpanned ?? 1);
            var expandedSummaryRows = childRows.Any() ? DataStructureDummyFactory.BuildFlatRows(cell.ColumnsSpanned ?? 1) : null;
            var collapsedSummaryRows = childRows.Any() ? DataStructureDummyFactory.BuildFlatRows(cell.ColumnsSpanned ?? 1) : null;

            var expected = new Row(new[] { cell }, id, format, childRows, expandedSummaryRows, collapsedSummaryRows);

            // Act
            var actual = cell.ToRow(id, format, childRows, expandedSummaryRows, collapsedSummaryRows);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }
    }
}
