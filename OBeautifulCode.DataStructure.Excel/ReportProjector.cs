// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportProjector.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Aspose.Cells;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Excel;
    using OBeautifulCode.Excel.AsposeCells;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Projects a <see cref="Report"/> into an Excel workbook.
    /// </summary>
    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = ObcSuppressBecause.CA1506_AvoidExcessiveClassCoupling_DisagreeWithAssessment)]
    public static partial class ReportProjector
    {
        private const string TopLeftHeaderCellMarker = "top-left-header-cell";

        private const string BottomRightHeaderCellMarker = "bottom-right-header-cell-marker";

        private const string TopLeftDataCellMarker = "top-left-data-cell-marker";

        private const string TopLeftAndBottomRightDataCellMarker = "top-left-and-bottom-right-data-cell-marker";

        private const string BottomRightNonSummaryDataCellMarker = "bottom-right-non-summary-data-cell-marker";

        private const string TableTopLeftBottomRightCornersCellMarker = "table-top-left-bottom-right-corners-cell-marker";

        /// <summary>
        /// Projects a <see cref="Report"/> into a <see cref="Workbook"/>.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="context">OPTIONAL context to use.  DEFAULT is no context.</param>
        /// <returns>
        /// The <see cref="Workbook"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = ObcSuppressBecause.CA2000_DisposeObjectsBeforeLosingScope_DisposableObjectIsMethodReturnObject)]
        public static Workbook ToExcelWorkbook(
            this Report report,
            ReportToWorkbookProjectionContext context = null)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            context = context ?? new ReportToWorkbookProjectionContext();

            // check context
            var sectionsToAppend = context.SectionsToAppend ?? new Section[0];
            if (sectionsToAppend.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(context)}.{nameof(context.SectionsToAppend)} contains a null element"));
            }

            // Setup the workbook
            var result = General.CreateStandardWorkbook().RemoveDefaultWorksheet();

            // Add document properties
            var documentProperties = context.BuildDocumentPropertiesDelegate == null
                ? new DocumentProperties()
                : context.BuildDocumentPropertiesDelegate(report.Title);

            // If report has a timestamp, should we set BuiltInDocumentPropertyKind.CreationDate and LastSaveTime to that timestamp?
            // Should those properties reflect when the workbook was created or when the report was created?
            result.SetDocumentProperties(documentProperties);

            // Add sections
            var sections = report.Sections.Concat(sectionsToAppend).ToList();

            foreach (var section in sections)
            {
                var worksheetName = section.GetWorksheetName(context);

                var worksheet = result.Worksheets.Add(worksheetName);

                var cursors = new Cursors
                {
                    ChromeCursor = new CellCursor(worksheet),
                };

                // On first pass, add data.
                cursors.AddSection(section, context, PassKind.Data, report);

                // On second pass, apply formatting.
                cursors.AddSection(section, context, PassKind.Formatting, report);
            }

            return result;
        }

        private static void AddSection(
            this Cursors cursors,
            Section section,
            ReportToWorkbookProjectionContext reportToWorkbookProjectionContext,
            PassKind passKind,
            Report report)
        {
            var context = new InternalProjectionContext
            {
                ExternalContext = reportToWorkbookProjectionContext,
                UsesAutoFilter = section.RequiresAutoFilter(),
            };

            var chromeCursor = cursors.ChromeCursor.Reset();

            if (passKind == PassKind.Formatting)
            {
                chromeCursor.CanvassedRange.ApplyReportFormat(report.Format);

                chromeCursor.CanvassedRange.ApplySectionFormat(section.Format, section.Id, context);
            }

            chromeCursor.AddSectionHeader(report, section, passKind, context);

            cursors.TreeTableCursor = cursors.TreeTableCursor ?? new CellCursor(chromeCursor.Worksheet, chromeCursor.RowNumber, chromeCursor.StartColumnNumber);
            var treeTableCursor = cursors.TreeTableCursor.Reset();

            treeTableCursor.AddTreeTable(section.TreeTable, context, passKind);

            chromeCursor.MoveDown(treeTableCursor.MaxRowNumber - chromeCursor.RowNumber + 1);

            chromeCursor.AddSectionFooter(report, passKind, context);
        }

        private static void AddSectionHeader(
            this CellCursor cursor,
            Report report,
            Section section,
            PassKind passKind,
            InternalProjectionContext context)
        {
            cursor.AddSectionHeaderAdditionalInfoDetails(report, section, passKind, context);

            cursor.AddSectionHeaderTitle(report, section, passKind);
        }

        private static void AddSectionHeaderAdditionalInfoDetails(
            this CellCursor cursor,
            Report report,
            Section section,
            PassKind passKind,
            InternalProjectionContext context)
        {
            var reportAdditionalInfo = context.ExternalContext.AdditionalReportInfoOverride ?? report.AdditionalInfo;

            var details = (reportAdditionalInfo?.Details ?? new List<IDetails>())
                .Concat(section.AdditionalInfo?.Details ?? new List<IDetails>())
                .Where(_ => _ != null)
                .ToList();

            var logoDetails = details.OfType<LogoDetails>().ToList();

            if (logoDetails.Any())
            {
                var medias = logoDetails.Select(_ => _.Media).ToList();

                if (medias.Any(_ => !(_ is SourcedMedia)))
                {
                    throw new NotSupportedException(Invariant($"This type of {nameof(MediaBase)} is not supported: {medias.First(_ => !(_ is SourcedMedia)).GetType().ToStringReadable()}"));
                }

                var urls = medias.Cast<SourcedMedia>().Select(_ => _.Url).ToList();

                var insertImagesResult = cursor.Cell.InsertImages(urls, cellSizeChanges: ImagesCellSizeChanges.None);

                if (passKind == PassKind.Data)
                {
                    var pictureIndicesInReverseOrder = insertImagesResult.PictureIndexes.OrderByDescending(_ => _).ToList();

                    foreach (var pictureIndex in pictureIndicesInReverseOrder)
                    {
                        cursor.Worksheet.Pictures.RemoveAt(pictureIndex);
                    }
                }

                cursor.MoveDown(insertImagesResult.ContainedWithinRange.RowCount);
            }

            details = details.Where(_ => !logoDetails.Contains(_)).ToList();

            foreach (var detail in details)
            {
                throw new NotSupportedException(Invariant($"This type of {nameof(IDetails)} is not supported: {detail.GetType().ToStringReadable()}"));
            }
        }

        private static void AddSectionHeaderTitle(
            this CellCursor cursor,
            Report report,
            Section section,
            PassKind passKind)
        {
            // note: Unlike the tree table, for the section header, whenever we add a row we will move down a row.
            // This is so that the tree table always starts on a row that can be written to, which is what it expects.
            if ((!string.IsNullOrWhiteSpace(report.Title)) || (!string.IsNullOrWhiteSpace(section.Title)))
            {
                if (passKind == PassKind.Data)
                {
                    var title = string.IsNullOrWhiteSpace(report.Title) ? string.Empty : report.Title;

                    if (!string.IsNullOrWhiteSpace(section.Title))
                    {
                        title = title + ": " + section.Title;
                    }

                    cursor.Cell.Value = title;
                }

                if (passKind == PassKind.Formatting)
                {
                    // Section model doesn't have any formatting for title.
                    cursor
                        .Cell
                        .GetRange()
                        .ApplyCellFormat(new CellFormat(fontFormat: new FontFormat(fontSizeInPoints: 18)));
                }

                cursor.MoveDown();
            }
        }

        private static void AddSectionFooter(
            this CellCursor chromeCursor,
            Report report,
            PassKind passKind,
            InternalProjectionContext context)
        {
            if (passKind == PassKind.Data)
            {
                // By default, display the timestamp if available.
                if ((report.TimestampUtc != null) && (report.Format?.DisplayTimestamp ?? true))
                {
                    chromeCursor.MoveDown();

                    chromeCursor.Cell.Value = ((DateTime)report.TimestampUtc).Format(report.Format?.TimestampFormat, context);
                }

                var additionalInfo = context.ExternalContext.AdditionalReportInfoOverride ?? report.AdditionalInfo;

                if (additionalInfo != null)
                {
                    if (additionalInfo.Copyright != null)
                    {
                        chromeCursor.MoveDown();

                        chromeCursor.Cell.Value = additionalInfo.Copyright;
                    }

                    if (additionalInfo.TermsOfUse != null)
                    {
                        chromeCursor.MoveDown();

                        chromeCursor.Cell.Value = additionalInfo.TermsOfUse;
                    }
                }
            }
        }

        private static void AddTreeTable(
            this CellCursor cursor,
            TreeTable treeTable,
            InternalProjectionContext context,
            PassKind passKind)
        {
            if (passKind == PassKind.Formatting)
            {
                // Format whole table
                cursor.CanvassedRange.ApplyTableFormat(treeTable.Format);

                // Format columns
                var tableColumns = treeTable.TableColumns;

                var topLeftDataCellReference = cursor.HasMarker(TopLeftDataCellMarker) ? cursor.GetMarkedCellReference(TopLeftDataCellMarker) : null;

                var bottomRightNonSummaryDataCellReference = cursor.HasMarker(BottomRightNonSummaryDataCellMarker) ? cursor.GetMarkedCellReference(BottomRightNonSummaryDataCellMarker) : null;

                var bottomRightHeaderCellReference = cursor.HasMarker(BottomRightHeaderCellMarker) ? cursor.GetMarkedCellReference(BottomRightHeaderCellMarker) : null;

                foreach (var column in tableColumns.Columns)
                {
                    var wholeColumnRange = cursor.Worksheet.GetRange(cursor.RowNumber, cursor.MaxRowNumber, cursor.ColumnNumber, cursor.ColumnNumber);

                    // ReSharper disable once PossibleNullReferenceException
                    var dataCellsRange = topLeftDataCellReference == null
                        ? null
                        : cursor.Worksheet.GetRange(topLeftDataCellReference.RowNumber, bottomRightNonSummaryDataCellReference.RowNumber, cursor.ColumnNumber, cursor.ColumnNumber);

                    var lastHeaderCellToLastNonSummaryDataCellRange = bottomRightHeaderCellReference == null
                        ? null
                        : dataCellsRange == null
                            ? null
                            : cursor.Worksheet.GetRange(bottomRightHeaderCellReference.RowNumber, bottomRightNonSummaryDataCellReference.RowNumber, cursor.ColumnNumber, cursor.ColumnNumber);

                    // Individual column format will override the format applied to all table columns,
                    // that's why we first apply the table columns format and then the individual column's format.
                    wholeColumnRange.ApplyColumnFormat(dataCellsRange, lastHeaderCellToLastNonSummaryDataCellRange, tableColumns.ColumnsFormat);

                    wholeColumnRange.ApplyColumnFormat(dataCellsRange, lastHeaderCellToLastNonSummaryDataCellRange, column.Format);

                    if (dataCellsRange != null)
                    {
                        dataCellsRange.ApplyCellValueFormat(column.ValueFormat, context);
                    }

                    cursor.MoveRight();
                }

                cursor.ResetColumn();
            }

            // Add rows
            var columnIndexToColumnCellValueFormatMap = treeTable.TableColumns.Columns
                .Select((col, index) => new { Index = index, ValueFormat = col.ValueFormat })
                .ToDictionary(_ => _.Index, _ => _.ValueFormat);

            cursor.AddTableRows(treeTable.TableRows, context, passKind, columnIndexToColumnCellValueFormatMap);
        }

        private static void AddTableRows(
            this CellCursor cursor,
            TableRows tableRows,
            InternalProjectionContext context,
            PassKind passKind,
            IReadOnlyDictionary<int, ICellValueFormat> columnIndexToColumnCellValueFormatMap)
        {
            if (tableRows == null)
            {
                return;
            }

            tableRows.RowsFormat.ProcessIntoContext(context);

            if (passKind == PassKind.Formatting)
            {
                var tableRange = cursor.GetMarkedRange(TableTopLeftBottomRightCornersCellMarker);

                tableRange.ApplyTableRowsFormat(tableRows.RowsFormat, context);

                context.TreeLevelStack = new Stack<int>();

                context.AlignChildRowsWithParentStack = new Stack<bool>();
            }

            cursor.AddMarker(TableTopLeftBottomRightCornersCellMarker);

            var moveDownForFirstRow = cursor.AddHeaderRows(tableRows.HeaderRows, false, context, passKind);

            moveDownForFirstRow = cursor.AddDataRows(tableRows.DataRows, moveDownForFirstRow, context, passKind, columnIndexToColumnCellValueFormatMap) || moveDownForFirstRow;

            // ReSharper disable once RedundantAssignment
            moveDownForFirstRow = cursor.AddFooterRows(tableRows.FooterRows, moveDownForFirstRow, context, passKind) || moveDownForFirstRow;

            cursor.AddMarker(TableTopLeftBottomRightCornersCellMarker);
        }

        private static bool AddHeaderRows(
            this CellCursor cursor,
            HeaderRows headerRows,
            bool moveDownForFirstRow,
            InternalProjectionContext context,
            PassKind passKind)
        {
            var result = false;

            if ((headerRows != null) && headerRows.Rows.Any())
            {
                for (var x = 0; x < headerRows.Rows.Count; x++)
                {
                    cursor.ResetColumn();

                    if (moveDownForFirstRow || (x != 0))
                    {
                        cursor.MoveDown();
                    }

                    if ((x == 0) && (passKind == PassKind.Data))
                    {
                        cursor.AddMarker(TopLeftHeaderCellMarker);
                    }

                    if (passKind == PassKind.Formatting)
                    {
                        cursor.CanvassedRowRange.ApplyHeaderRowsFormat(headerRows.Format);
                    }

                    cursor.AddRowCells(headerRows.Rows[x], context, passKind, null);
                }

                if (passKind == PassKind.Data)
                {
                    cursor.AddMarker(BottomRightHeaderCellMarker);
                }

                result = true;
            }

            return result;
        }

        private static bool AddFooterRows(
            this CellCursor cursor,
            FooterRows footerRows,
            bool moveDownForFirstRow,
            InternalProjectionContext context,
            PassKind passKind)
        {
            var result = false;

            if ((footerRows != null) && footerRows.Rows.Any())
            {
                for (var x = 0; x < footerRows.Rows.Count; x++)
                {
                    cursor.ResetColumn();

                    if (moveDownForFirstRow || (x != 0))
                    {
                        cursor.MoveDown();
                    }

                    if (passKind == PassKind.Formatting)
                    {
                        cursor.CanvassedRowRange.ApplyFooterRowsFormat(footerRows.Format);
                    }

                    cursor.AddRowCells(footerRows.Rows[x], context, passKind, null);
                }

                result = true;
            }

            return result;
        }

        private static bool AddDataRows(
            this CellCursor cursor,
            DataRows dataRows,
            bool moveDownForFirstRow,
            InternalProjectionContext context,
            PassKind passKind,
            IReadOnlyDictionary<int, ICellValueFormat> columnIndexToColumnCellValueFormatMap)
        {
            dataRows?.Format.ProcessIntoContext(context);

            var result = false;

            if ((dataRows != null) && dataRows.Rows.Any())
            {
                for (var x = 0; x < dataRows.Rows.Count; x++)
                {
                    cursor.ResetColumn();

                    if (moveDownForFirstRow || (x != 0))
                    {
                        cursor.MoveDown();
                    }

                    if (x == 0)
                    {
                        cursor.AddMarker(TopLeftDataCellMarker);
                        cursor.AddMarker(TopLeftAndBottomRightDataCellMarker);
                    }

                    cursor.AddRowBase(dataRows.Rows[x], context, passKind, columnIndexToColumnCellValueFormatMap);
                }

                cursor.AddMarker(TopLeftAndBottomRightDataCellMarker);

                if (passKind == PassKind.Formatting)
                {
                    cursor.GetMarkedRange(TopLeftAndBottomRightDataCellMarker).ApplyDataRowsFormat(dataRows.Format, context);
                }

                result = true;
            }

            return result;
        }

        private static void AddRowBase(
            this CellCursor cursor,
            RowBase rowBase,
            InternalProjectionContext context,
            PassKind passKind,
            IReadOnlyDictionary<int, ICellValueFormat> columnIndexToColumnCellValueFormatMap)
        {
            if (passKind == PassKind.Formatting)
            {
                AddTreeLevel(rowBase, context);
            }

            cursor.AddRowCells(rowBase, context, passKind, columnIndexToColumnCellValueFormatMap);

            cursor.RemoveMarker(BottomRightNonSummaryDataCellMarker);

            cursor.AddMarker(BottomRightNonSummaryDataCellMarker);

            if (rowBase is FlatRow)
            {
                // no-op
            }
            else if (rowBase is DataStructure.Row row)
            {
                var parentMarkerName = "grouping-marker-parent-" + Guid.NewGuid();
                cursor.AddMarker(parentMarkerName);

                var groupingMarkerName = "grouping-marker-" + Guid.NewGuid();

                if ((row.ChildRows != null) && row.ChildRows.Any())
                {
                    for (var x = 0; x < row.ChildRows.Count; x++)
                    {
                        cursor.ResetColumn().MoveDown();

                        if (x == 0)
                        {
                            cursor.AddMarker(groupingMarkerName);
                        }

                        cursor.AddRowBase(row.ChildRows[x], context, passKind, columnIndexToColumnCellValueFormatMap);
                    }

                    cursor.AddMarker(groupingMarkerName);
                }

                if (((row.CollapsedSummaryRows != null) && row.CollapsedSummaryRows.Any()) && (!row.CollapsedSummaryRows.IsEqualTo(row.ExpandedSummaryRows)))
                {
                    throw new NotSupportedException("Collapsed summary rows are not supported when collapsed and expanded rows are not equal.");
                }

                if ((row.ExpandedSummaryRows != null) && row.ExpandedSummaryRows.Any())
                {
                    // Auto-filters will include summary rows (e.g. a total row will get sorted along with data rows)
                    // unless there's a blank row separating the last data row and first summary row.
                    if (context.UsesAutoFilter)
                    {
                        cursor.MoveDown();
                    }

                    foreach (var expandedSummaryRow in row.ExpandedSummaryRows)
                    {
                        cursor.ResetColumn().MoveDown();

                        cursor.AddRowCells(expandedSummaryRow, context, passKind, columnIndexToColumnCellValueFormatMap);
                    }

                    // Note that you can't have summary rows without child rows so we're guaranteed to have the grouping marker at this point.
                    cursor.MergeMarkers(parentMarkerName, groupingMarkerName);
                }
                else
                {
                    if (cursor.HasMarker(groupingMarkerName))
                    {
                        if (!DisableCollapsing(rowBase.Format, context))
                        {
                            // For rows with children but without a summary row, the additional blank row
                            // makes it so that Excel's expand/collapse toggle control doesn't appear on
                            // the next row.  It will appear on the blank row and thus feel like it is on
                            // a child row and thus within the scope of the row itself, not outside of it's scope.
                            // Otherwise, it's causes a pretty big usability problem.
                            cursor.ResetColumn().MoveDown();
                        }
                    }
                }

                if ((passKind == PassKind.Formatting) && cursor.HasMarker(groupingMarkerName))
                {
                    cursor.GetMarkedRange(groupingMarkerName).ApplyGroupingFormat(rowBase.Format, context);

                    cursor.RemoveMarker(groupingMarkerName);
                }

                cursor.RemoveMarker(parentMarkerName);
            }
            else
            {
                throw new NotSupportedException(Invariant($"This type of {nameof(RowBase)} is not supported: {rowBase.GetType().ToStringReadable()}"));
            }

            if (passKind == PassKind.Formatting)
            {
                RemoveTreeLevel(context);
            }
        }

        private static void AddRowCells(
            this CellCursor cursor,
            RowBase rowBase,
            InternalProjectionContext context,
            PassKind passKind,
            IReadOnlyDictionary<int, ICellValueFormat> columnIndexToColumnCellValueFormatMap)
        {
            if (passKind == PassKind.Formatting)
            {
                cursor.CanvassedRowRange.ApplyRowFormat(rowBase.Format);
            }

            int columnIndexStart = 0;

            for (var x = 0; x < rowBase.Cells.Count; x++)
            {
                if (x > 0)
                {
                    cursor.MoveRight(rowBase.Cells[x - 1].ColumnsSpanned ?? 1);
                }

                // Get the ICellValueFormat applied to the column.  While many of the formats won't actually be
                // applied at the cell level, but rather at the column level in code that deals with that,
                // some of these formats require manipulating the value itself and thus why we are passing it along.
                ICellValueFormat columnCellValueFormat = null;
                if (columnIndexToColumnCellValueFormatMap != null)
                {
                    var columnIndexEnd = columnIndexStart + (rowBase.Cells[x].ColumnsSpanned ?? 1) - 1;

                    for (var columnIndex = columnIndexStart; columnIndex <= columnIndexEnd; columnIndex++)
                    {
                        // Just take the first non-null column cell value format if a cell spans multiple columns.
                        if (columnIndexToColumnCellValueFormatMap[columnIndex] != null)
                        {
                            columnCellValueFormat = columnIndexToColumnCellValueFormatMap[columnIndex];
                            break;
                        }
                    }

                    columnIndexStart = columnIndexEnd + 1;
                }

                cursor.AddCell(rowBase.Cells[x], context, passKind, columnCellValueFormat);
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = ObcSuppressBecause.CA1502_AvoidExcessiveComplexity_DisagreeWithAssessment)]
        private static void AddCell(
            this CellCursor cursor,
            ICell cell,
            InternalProjectionContext context,
            PassKind passKind,
            ICellValueFormat columnCellValueFormat)
        {
            if (passKind == PassKind.Data)
            {
                if ((cell.ColumnsSpanned ?? 1) > 1)
                {
                    // ReSharper disable once PossibleInvalidOperationException
                    var mergeRange = cursor.Worksheet.GetRange(cursor.RowNumber, cursor.RowNumber, cursor.ColumnNumber, cursor.ColumnNumber + (int)cell.ColumnsSpanned - 1);

                    mergeRange.SetMergeCells(true);
                }
            }

            if ((passKind == PassKind.Formatting) && (cell is IHaveCellFormat haveCellFormat))
            {
                cursor.CellRange.ApplyCellFormat(haveCellFormat.Format);
            }

            ICellValueFormat cellValueFormat = null;

            if ((passKind == PassKind.Formatting) && (cell is IHaveCellValueFormat haveCellValueFormat))
            {
                cellValueFormat = haveCellValueFormat.GetCellValueFormat();

                cursor.CellRange.ApplyCellValueFormat(cellValueFormat, context);
            }

            var notSupportedException = new NotSupportedException(Invariant($"This type of cell is not supported: {cell.GetType().ToStringReadable()}."));

            if (cell is INotSlottedCell)
            {
                if (cell is INullCell)
                {
                }
                else if (cell is ConstCell<SourcedMedia> sourcedMedia)
                {
                    if ((passKind == PassKind.Data) && (sourcedMedia.Value != null))
                    {
                        var media = sourcedMedia.Value;

                        if (media.MediaKind == MediaKind.Image)
                        {
                            cursor.Cell.InsertImages(new[] { media.Url }, cellSizeChanges: ImagesCellSizeChanges.ExpandRowAndColumnToFitImages);
                        }
                        else
                        {
                            throw new NotSupportedException(Invariant($"This {nameof(MediaKind)} is not supported: {media.MediaKind}."));
                        }
                    }
                }
                else if (cell is IConstOutputCell constOutputCell)
                {
                    if (passKind == PassKind.Data)
                    {
                        var cellValue = ModifyValuePerCellValueFormat(
                            constOutputCell.GetCellObjectValue(),
                            cellValueFormat ?? columnCellValueFormat,
                            context);

                        if (cellValue != null)
                        {
                            cursor.Cell.Value = cellValue;
                        }
                    }
                }
                else
                {
                    throw notSupportedException;
                }
            }
            else if (cell is ISlottedCell)
            {
                throw notSupportedException;
            }
            else
            {
                throw notSupportedException;
            }

            if (passKind == PassKind.Data)
            {
                cursor.AddHoverOver(cell);
            }

            if ((passKind == PassKind.Formatting) && (cursor.ColumnNumber == cursor.StartColumnNumber))
            {
                cursor.CellRange.ApplyTreeLevelFormat(context);
            }
        }

        private static object ModifyValuePerCellValueFormat(
            object cellObjectValue,
            ICellValueFormat columnCellValueFormat,
            InternalProjectionContext context)
        {
            var result = cellObjectValue;

            if (columnCellValueFormat == null)
            {
                // no-op
            }
            else if (columnCellValueFormat is DateTimeCellValueFormat dateTimeCellValueFormat)
            {
                // Is DateTime or non-null nullable DateTime?
                if ((cellObjectValue != null) && (cellObjectValue is DateTime dateTimeValue))
                {
                    var localizeTimeZone = dateTimeCellValueFormat.Format.LocalizeTimeZone ?? (context.ExternalContext.LocalTimeZone != null);

                    var localTimeZone = dateTimeCellValueFormat.Format.LocalTimeZone ?? context.ExternalContext.LocalTimeZone ?? StandardTimeZone.Unknown;

                    if (localizeTimeZone)
                    {
                        if (dateTimeValue.Kind != DateTimeKind.Utc)
                        {
                            throw new NotImplementedException(Invariant($"Cannot localize time zone of date/time unless it's a UTC date/time."));
                        }

                        if (localTimeZone == StandardTimeZone.Unknown)
                        {
                            throw new InvalidOperationException("Cannot localize time zone of date/time unless the local time zone is specified.");
                        }

                        var localTimeZoneInfo = localTimeZone.ToTimeZoneInfo();

                        result = TimeZoneInfo.ConvertTimeFromUtc(dateTimeValue, localTimeZoneInfo);
                    }
                }
            }

            return result;
        }

        private static void AddHoverOver(
            this CellCursor cursor,
            ICell cell)
        {
            if ((cell is IHaveHoverOver hasHoveOver) && (hasHoveOver.HoverOver != null))
            {
                OBeautifulCode.Excel.Comment comment;

                if (hasHoveOver.HoverOver is StringHoverOver stringHoverOver)
                {
                    comment = new OBeautifulCode.Excel.Comment
                    {
                        Body = stringHoverOver.Value,
                    };
                }
                else if (hasHoveOver.HoverOver is HtmlHoverOver htmlHoverOver)
                {
                    comment = new OBeautifulCode.Excel.Comment
                    {
                        HtmlBody = htmlHoverOver.Html,
                    };
                }
                else
                {
                    throw new NotSupportedException(Invariant($"This type of {nameof(IHaveHoverOver)} is not supported: {hasHoveOver.HoverOver.GetType().ToStringReadable()}."));
                }

                comment.AutoSize = true;

                cursor.Cell.SetComment(comment);
            }
        }

        private static bool RequiresAutoFilter(
            this Section section)
        {
            var options = section.TreeTable.TableColumns.ColumnsFormat?.Options;

            var result = options.RequiresAutoFilter() ||
                         section.TreeTable.TableColumns.Columns.Any(_ => (_.Format?.Options).RequiresAutoFilter());

            return result;
        }

        private static string GetWorksheetName(
            this Section section,
            ReportToWorkbookProjectionContext context)
        {
            var worksheetName = section.Name;

            if ((context.SectionIdToWorksheetNameOverrideMap != null) && context.SectionIdToWorksheetNameOverrideMap.ContainsKey(section.Id))
            {
                worksheetName = context.SectionIdToWorksheetNameOverrideMap[section.Id];
            }

            if (string.IsNullOrWhiteSpace(worksheetName))
            {
                worksheetName = section.Id;
            }

            // Remove invalid characters - replace with compiled regex in the future.
            worksheetName = worksheetName
                .Replace("[", string.Empty)
                .Replace("]", string.Empty)
                .Replace("*", string.Empty)
                .Replace("/", string.Empty)
                .Replace("\\", string.Empty)
                .Replace("?", string.Empty)
                .Replace(":", string.Empty);

            var result = worksheetName.Length > 31
                ? worksheetName.Substring(0, 31)
                : worksheetName;

            return result;
        }

        private static void AddTreeLevel(
            RowBase row,
            InternalProjectionContext context)
        {
            if (context.TreeLevelStack.Any())
            {
                context.TreeLevelStack.Push(context.TreeLevelStack.Peek() + 1);
            }
            else
            {
                context.TreeLevelStack.Push(0);
            }

            var alignChildrenWithParent = (row.Format?.Options != null) && ((RowFormatOptions)row.Format?.Options).HasFlag(RowFormatOptions.AlignChildRowsWithParent);

            context.AlignChildRowsWithParentStack.Push(alignChildrenWithParent);
        }

        private static void RemoveTreeLevel(
            InternalProjectionContext context)
        {
            context.AlignChildRowsWithParentStack.Pop();
            context.TreeLevelStack.Pop();
        }

        private static void ProcessIntoContext(
            this RowFormat rowFormat,
            InternalProjectionContext context)
        {
            if (rowFormat == null)
            {
                return;
            }

            if (rowFormat.Options != null)
            {
                var rowFormatOptions = (RowFormatOptions)rowFormat.Options;

                if (rowFormatOptions.HasFlag(RowFormatOptions.DisableCollapsing))
                {
                    context.DisableCollapsingOfChildRows = true;
                }
            }
        }

        private static void ProcessIntoContext(
            this DataRowsFormat format,
            InternalProjectionContext context)
        {
            if (format == null)
            {
                return;
            }

            if (format.RowsFormat?.Options != null)
            {
                var rowFormatOptions = (RowFormatOptions)format.RowsFormat.Options;

                if (rowFormatOptions.HasFlag(RowFormatOptions.DisableCollapsing))
                {
                    context.DisableCollapsingOfChildRows = true;
                }
            }
        }

        private static bool DisableCollapsing(
            this RowFormat format,
            InternalProjectionContext context)
        {
            var result = context.DisableCollapsingOfChildRows ||
                         ((format?.Options != null) && ((RowFormatOptions)format.Options).HasFlag(RowFormatOptions.DisableCollapsing));

            return result;
        }

#pragma warning disable SA1201 // Elements should appear in the correct order
        private enum PassKind
        {
            Formatting,

            Data,
        }
        #pragma warning restore SA1201 // Elements should appear in the correct order

        private class Cursors
        {
            public CellCursor ChromeCursor { get; set; }

            public CellCursor TreeTableCursor { get; set; }
        }
    }
}