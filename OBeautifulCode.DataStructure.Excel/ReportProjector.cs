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
    using OBeautifulCode.Excel;
    using OBeautifulCode.Excel.AsposeCells;
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
            foreach (var section in report.Sections)
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

            // Add upper chrome (e.g. section title)
            var chromeCursor = cursors.ChromeCursor;

            chromeCursor.Reset();

            var hasTitle = (!string.IsNullOrWhiteSpace(report.Title)) || (!string.IsNullOrWhiteSpace(section.Title));

            if (passKind == PassKind.Data)
            {
                if (hasTitle)
                {
                    var title = string.IsNullOrWhiteSpace(report.Title) ? string.Empty : report.Title;

                    if (!string.IsNullOrWhiteSpace(section.Title))
                    {
                        title = title + ": " + section.Title;
                    }

                    chromeCursor.Cell.Value = title;
                }
            }

            if (passKind == PassKind.Formatting)
            {
                if (hasTitle)
                {
                    // Section model doesn't have any formatting for title.
                    chromeCursor.Cell.GetRange().ApplyCellFormat(new CellFormat(fontFormat: new FontFormat(fontSizeInPoints: 18)));
                }
            }

            // Add tree table
            cursors.TreeTableCursor = cursors.TreeTableCursor ?? new CellCursor(chromeCursor.Worksheet, chromeCursor.RowNumber, chromeCursor.StartColumnNumber);

            var treeTableCursor = cursors.TreeTableCursor;

            treeTableCursor.Reset();

            treeTableCursor.AddTreeTable(section.TreeTable, hasTitle, context, passKind);

            // Add bottom chrome (e.g. copyright and terms of use)
            chromeCursor.MoveDown(treeTableCursor.MaxRowNumber - chromeCursor.RowNumber + 1);

            if (passKind == PassKind.Data)
            {
                // By default, display the timestamp if available.
                if ((report.TimestampUtc != null) && (report.Format?.DisplayTimestamp ?? true))
                {
                    chromeCursor.MoveDown();

                    chromeCursor.Cell.Value = ((DateTime)report.TimestampUtc).Format(report.Format?.TimestampFormat, context);
                }

                var additionalInfo = report.AdditionalInfo;

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

            // Overall formatting
            if (passKind == PassKind.Formatting)
            {
                chromeCursor.CanvassedRange.ApplySectionFormat(section.Format);

                chromeCursor.CanvassedRange.ApplyReportFormat(report.Format);
            }
        }

        private static void AddTreeTable(
            this CellCursor cursor,
            TreeTable treeTable,
            bool moveDownForFirstRow,
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

                    cursor.MoveRight();
                }

                cursor.ResetColumn();
            }

            // Add rows
            cursor.AddTableRows(treeTable.TableRows, moveDownForFirstRow, context, passKind);
        }

        private static void AddTableRows(
            this CellCursor cursor,
            TableRows tableRows,
            bool moveDownForFirstRow,
            InternalProjectionContext context,
            PassKind passKind)
        {
            if (tableRows == null)
            {
                return;
            }

            if (passKind == PassKind.Formatting)
            {
                var tableRange = cursor.GetMarkedRange(TableTopLeftBottomRightCornersCellMarker);

                tableRange.ApplyTableRowsFormat(tableRows.RowsFormat, context);
            }

            context.CurrentTreeLevel = new Stack<int>();
            context.AlignChildRowsWithParent = new Stack<bool>();

            cursor.AddMarker(TableTopLeftBottomRightCornersCellMarker);

            if (cursor.AddHeaderRows(tableRows.HeaderRows, moveDownForFirstRow, context, passKind))
            {
                moveDownForFirstRow = true;
            }

            if (cursor.AddDataRows(tableRows.DataRows, moveDownForFirstRow, context, passKind))
            {
                moveDownForFirstRow = true;
            }

            cursor.AddFooterRows(tableRows.FooterRows, moveDownForFirstRow, context, passKind);

            cursor.AddMarker(TableTopLeftBottomRightCornersCellMarker);
        }

        private static bool AddHeaderRows(
            this CellCursor cursor,
            HeaderRows headerRows,
            bool moveDownForFirstRow,
            InternalProjectionContext context,
            PassKind passKind)
        {
            if (headerRows == null)
            {
                return false;
            }

            if (headerRows.Rows.Any())
            {
                for (var x = 0; x < headerRows.Rows.Count; x++)
                {
                    cursor.ResetColumn();

                    if (moveDownForFirstRow || (x != 0))
                    {
                        cursor.MoveDown();
                    }
                    else
                    {
                        if (cursor.StartRowNumber != cursor.RowNumber)
                        {
                            cursor.MoveDown();
                        }
                    }

                    if ((x == 0) && (passKind == PassKind.Data))
                    {
                        cursor.AddMarker(TopLeftHeaderCellMarker);
                    }

                    if (passKind == PassKind.Formatting)
                    {
                        cursor.CanvassedRowRange.ApplyHeaderRowsFormat(headerRows.Format);
                    }

                    cursor.AddRowCells(headerRows.Rows[x], context, passKind);
                }

                if (passKind == PassKind.Data)
                {
                    cursor.AddMarker(BottomRightHeaderCellMarker);
                }
            }

            return true;
        }

        private static void AddFooterRows(
            this CellCursor cursor,
            FooterRows footerRows,
            bool moveDownForFirstRow,
            InternalProjectionContext context,
            PassKind passKind)
        {
            if (footerRows == null)
            {
                return;
            }

            if (footerRows.Rows.Any())
            {
                for (var x = 0; x < footerRows.Rows.Count; x++)
                {
                    cursor.ResetColumn();

                    if (moveDownForFirstRow || (x != 0))
                    {
                        cursor.MoveDown();
                    }
                    else
                    {
                        if (cursor.StartRowNumber != cursor.RowNumber)
                        {
                            cursor.MoveDown();
                        }
                    }

                    if (passKind == PassKind.Formatting)
                    {
                        cursor.CanvassedRowRange.ApplyFooterRowsFormat(footerRows.Format);
                    }

                    cursor.AddRowCells(footerRows.Rows[x], context, passKind);
                }
            }
        }

        private static bool AddDataRows(
            this CellCursor cursor,
            DataRows dataRows,
            bool moveDownForFirstRow,
            InternalProjectionContext context,
            PassKind passKind)
        {
            if (dataRows == null)
            {
                return false;
            }

            if (dataRows.Rows.Any())
            {
                for (var x = 0; x < dataRows.Rows.Count; x++)
                {
                    cursor.ResetColumn();

                    if (moveDownForFirstRow || (x != 0))
                    {
                        cursor.MoveDown();
                    }
                    else
                    {
                        if (cursor.StartRowNumber != cursor.RowNumber)
                        {
                            cursor.MoveDown();
                        }
                    }

                    if (x == 0)
                    {
                        cursor.AddMarker(TopLeftDataCellMarker);
                        cursor.AddMarker(TopLeftAndBottomRightDataCellMarker);
                    }

                    cursor.AddRowBase(dataRows.Rows[x], context, passKind);
                }

                cursor.AddMarker(TopLeftAndBottomRightDataCellMarker);

                if (passKind == PassKind.Formatting)
                {
                    cursor.GetMarkedRange(TopLeftAndBottomRightDataCellMarker).ApplyDataRowsFormat(dataRows.Format, context);
                }
            }

            return true;
        }

        private static void AddRowBase(
            this CellCursor cursor,
            RowBase rowBase,
            InternalProjectionContext context,
            PassKind passKind)
        {
            AddTreeLevel(rowBase, context);

            cursor.AddRowCells(rowBase, context, passKind);

            if (cursor.HasMarker(BottomRightNonSummaryDataCellMarker))
            {
                cursor.RemoveMarker(BottomRightNonSummaryDataCellMarker);
            }

            cursor.AddMarker(BottomRightNonSummaryDataCellMarker);

            if (rowBase is FlatRow)
            {
                // no-op
            }
            else if (rowBase is DataStructure.Row row)
            {
                if ((row.ChildRows != null) && row.ChildRows.Any())
                {
                    foreach (var childRow in row.ChildRows)
                    {
                        cursor.ResetColumn();

                        cursor.MoveDown();

                        cursor.AddRowBase(childRow, context, passKind);
                    }
                }

                if (row.CollapsedSummaryRows != null && row.CollapsedSummaryRows.Any())
                {
                    throw new NotSupportedException("collapsed summary rows are not supported");
                }

                if (row.ExpandedSummaryRows != null)
                {
                    // Auto-filters will include summary rows (e.g. a total row will get sorted along with data rows)
                    // unless there's a blank row separating the last data row and first summary row.
                    if (row.ExpandedSummaryRows.Any() && context.UsesAutoFilter)
                    {
                        cursor.MoveDown();
                    }

                    foreach (var expandedSummaryRow in row.ExpandedSummaryRows)
                    {
                        cursor.ResetColumn();

                        cursor.MoveDown();

                        cursor.AddRowCells(expandedSummaryRow, context, passKind);
                    }
                }
            }
            else
            {
                throw new NotSupportedException(Invariant($"This type of {nameof(RowBase)} is not supported: {rowBase.GetType().ToStringReadable()}"));
            }

            RemoveTreeLevel(context);
        }

        private static void AddRowCells(
            this CellCursor cursor,
            RowBase rowBase,
            InternalProjectionContext context,
            PassKind passKind)
        {
            if (passKind == PassKind.Formatting)
            {
                cursor.CanvassedRowRange.ApplyRowFormat(rowBase.Format);
            }

            for (var x = 0; x < rowBase.Cells.Count; x++)
            {
                if (x > 0)
                {
                    cursor.MoveRight(rowBase.Cells[x - 1].ColumnsSpanned ?? 1);
                }

                cursor.AddCell(rowBase.Cells[x], context, passKind);
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = ObcSuppressBecause.CA1502_AvoidExcessiveComplexity_DisagreeWithAssessment)]
        private static void AddCell(
            this CellCursor cursor,
            ICell cell,
            InternalProjectionContext context,
            PassKind passKind)
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

            if ((passKind == PassKind.Formatting) && (cell is IHaveCellValueFormat haveCellValueFormat))
            {
                var valueFormat = haveCellValueFormat.GetCellValueFormat();

                cursor.CellRange.ApplyCellValueFormat(valueFormat);
            }

            var notSupportedException = new NotSupportedException(Invariant($"This type of cell is not supported: {cell.GetType().ToStringReadable()}."));

            if (cell is INotSlottedCell)
            {
                if (cell is INullCell)
                {
                }
                else if (cell is ConstCell<string> stringConstCell)
                {
                    if (passKind == PassKind.Data)
                    {
                        cursor.Cell.Value = stringConstCell.Value;
                    }
                }
                else if (cell is ConstCell<bool> boolConstCell)
                {
                    if (passKind == PassKind.Data)
                    {
                        cursor.Cell.Value = boolConstCell.Value;
                    }
                }
                else if (cell is ConstCell<int> intConstCell)
                {
                    if (passKind == PassKind.Data)
                    {
                        cursor.Cell.Value = intConstCell.Value;
                    }
                }
                else if (cell is ConstCell<long> longConstCell)
                {
                    if (passKind == PassKind.Data)
                    {
                        cursor.Cell.Value = longConstCell.Value;
                    }
                }
                else if (cell is ConstCell<DateTime> dateTimeConstCell)
                {
                    if (passKind == PassKind.Data)
                    {
                        cursor.Cell.Value = dateTimeConstCell.Value;
                    }
                }
                else if (cell is ConstCell<byte> byteConstCell)
                {
                    if (passKind == PassKind.Data)
                    {
                        cursor.Cell.Value = byteConstCell.Value;
                    }
                }
                else if (cell is ConstCell<float> floatConstCell)
                {
                    if (passKind == PassKind.Data)
                    {
                        cursor.Cell.Value = floatConstCell.Value;
                    }
                }
                else if (cell is ConstCell<double> doubleConstCell)
                {
                    if (passKind == PassKind.Data)
                    {
                        cursor.Cell.Value = doubleConstCell.Value;
                    }
                }
                else if (cell is ConstCell<decimal> decimalConstCell)
                {
                    if (passKind == PassKind.Data)
                    {
                        cursor.Cell.Value = decimalConstCell.Value;
                    }
                }
                else if (cell is ConstCell<decimal?> nullableDecimalConstCell)
                {
                    if ((passKind == PassKind.Data) && (nullableDecimalConstCell.Value != null))
                    {
                        cursor.Cell.Value = nullableDecimalConstCell.Value;
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

            if ((passKind == PassKind.Formatting) && (cursor.ColumnNumber == cursor.StartColumnNumber))
            {
                cursor.CellRange.ApplyTreeLevelFormat(context);
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

            var result = worksheetName.Length > 31
                ? worksheetName.Substring(0, 31)
                : worksheetName;

            return result;
        }

        private static void AddTreeLevel(
            RowBase row,
            InternalProjectionContext context)
        {
            if (context.CurrentTreeLevel.Any())
            {
                context.CurrentTreeLevel.Push(context.CurrentTreeLevel.Peek() + 1);
            }
            else
            {
                context.CurrentTreeLevel.Push(0);
            }

            var alignChildrenWithParent = (row.Format?.Options != null) && ((RowFormatOptions)row.Format?.Options).HasFlag(RowFormatOptions.AlignChildRowsWithParent);

            context.AlignChildRowsWithParent.Push(alignChildrenWithParent);
        }

        private static void RemoveTreeLevel(
            InternalProjectionContext context)
        {
            context.AlignChildRowsWithParent.Pop();
            context.CurrentTreeLevel.Pop();
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