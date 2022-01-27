﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportProjector.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Excel
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Aspose.Cells;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Excel;
    using OBeautifulCode.Excel.AsposeCells;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Projects a <see cref="Report"/> into an Excel workbook.
    /// </summary>
    public static partial class ReportProjector
    {
        private const string TopLeftHeaderCellMarker = "top-left-header-cell";

        private const string BottomRightHeaderCellMarker = "bottom-right-header-cell-marker";

        private const string TopLeftDataCellMarker = "top-left-data-cell-marker";

        private const string BottomRightDataCellMarker = "bottom-right-data-cell-marker";

        private const string BottomRightNonSummaryDataCellMarker = "bottom-right-non-summary-data-cell-marker";

        private static readonly ConcurrentDictionary<Type, IReadOnlyCollection<PropertyInfo>> CachedTypeToNotImplementedPropertiesMap =
            new ConcurrentDictionary<Type, IReadOnlyCollection<PropertyInfo>>();

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
                var worksheet = result.Worksheets.Add(section.Name);

                var cursors = new Cursors
                {
                    ChromeCursor = new CellCursor(worksheet),
                };

                // On first pass, add data.
                result.AddSection(section, context, PassKind.Data, cursors);

                // On second pass, apply formatting.
                result.AddSection(section, context, PassKind.Formatting, cursors);
            }

            return result;
        }

        private static void AddSection(
            this Workbook workbook,
            Section section,
            ReportToWorkbookProjectionContext context,
            PassKind passKind,
            Cursors cursors)
        {
            // Add upper chrome (e.g. section title)
            var chromeCursor = cursors.ChromeCursor;

            chromeCursor.Reset();

            if (section.Title != null)
            {
                if (passKind == PassKind.Data)
                {
                    chromeCursor.Cell.Value = section.Title;
                }

                if (passKind == PassKind.Formatting)
                {
                    // Section model doesn't have any formatting for title.
                    chromeCursor.Cell.GetRange().ApplyCellFormat(new CellFormat(fontFormat: new FontFormat(fontSizeInPoints: 18)));
                }
            }

            // Add tree table
            if (cursors.TreeTableCursor == null)
            {
                cursors.TreeTableCursor = new CellCursor(chromeCursor.Worksheet, chromeCursor.RowNumber, chromeCursor.StartColumnNumber);
            }
            else
            {
                cursors.TreeTableCursor.Reset();
            }

            cursors.TreeTableCursor.AddTreeTable(section.TreeTable, context, passKind);

            // Add bottom chrome (e.g. copyright and terms of use)
            // TODO: ADD copyright and terms of use
            // TODO: ADD report timestamp
        }

        private static void AddTreeTable(
            this CellCursor cursor,
            TreeTable treeTable,
            ReportToWorkbookProjectionContext context,
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
            cursor.AddTableRows(treeTable.TableRows, context, passKind);
        }

        private static void AddTableRows(
            this CellCursor cursor,
            TableRows tableRows,
            ReportToWorkbookProjectionContext context,
            PassKind passKind)
        {
            if (tableRows == null)
            {
                return;
            }

            cursor.AddHeaderRows(tableRows.HeaderRows, context, passKind);

            cursor.AddDataRows(tableRows.DataRows, context, passKind);

            cursor.AddFooterRows(tableRows.FooterRows, context, passKind);
        }

        private static void AddHeaderRows(
            this CellCursor cursor,
            HeaderRows headerRows,
            ReportToWorkbookProjectionContext context,
            PassKind passKind)
        {
            if (headerRows == null)
            {
                return;
            }

            if (headerRows.Rows.Any())
            {
                for (var x = 0; x < headerRows.Rows.Count; x++)
                {
                    cursor.ResetColumn();

                    cursor.MoveDown();

                    if ((x == 0) && (passKind == PassKind.Data))
                    {
                        cursor.AddMarker(TopLeftHeaderCellMarker);
                    }

                    if (passKind == PassKind.Formatting)
                    {
                        cursor.CanvassedRowRange.ApplyHeaderRowsFormat(headerRows.Format);
                    }

                    cursor.AddFlatRow(headerRows.Rows[x], context, passKind);
                }

                if (passKind == PassKind.Data)
                {
                    cursor.AddMarker(BottomRightHeaderCellMarker);
                }
            }
        }

        private static void AddFooterRows(
            this CellCursor cursor,
            FooterRows footerRows,
            ReportToWorkbookProjectionContext context,
            PassKind passKind)
        {
            if (footerRows == null)
            {
                return;
            }

            if (footerRows.Rows.Any())
            {
                foreach (var flatRow in footerRows.Rows)
                {
                    cursor.ResetColumn();

                    cursor.MoveDown();

                    if (passKind == PassKind.Formatting)
                    {
                        cursor.CanvassedRowRange.ApplyFooterRowsFormat(footerRows.Format);
                    }

                    cursor.AddFlatRow(flatRow, context, passKind);
                }
            }
        }

        private static void AddDataRows(
            this CellCursor cursor,
            DataRows dataRows,
            ReportToWorkbookProjectionContext context,
            PassKind passKind)
        {
            if (dataRows == null)
            {
                return;
            }

            if (dataRows.Rows.Any())
            {
                for (var x = 0; x < dataRows.Rows.Count; x++)
                {
                    cursor.ResetColumn();

                    cursor.MoveDown();

                    if (x == 0)
                    {
                        cursor.AddMarker(TopLeftDataCellMarker);
                    }

                    if (passKind == PassKind.Formatting)
                    {
                        cursor.CanvassedRowRange.ApplyDataRowsFormat(dataRows.Format);
                    }

                    cursor.AddRow(dataRows.Rows[x], context, passKind, dataRows.Format);
                }

                cursor.AddMarker(BottomRightDataCellMarker);
            }
        }

        private static void AddFlatRow(
            this CellCursor cursor,
            FlatRow flatRow,
            ReportToWorkbookProjectionContext context,
            PassKind passKind)
        {
            cursor.AddRowBase(flatRow, context, passKind);
        }

        private static void AddRow(
            this CellCursor cursor,
            DataStructure.Row row,
            ReportToWorkbookProjectionContext context,
            PassKind passKind,
            DataRowsFormat dataRowsFormat)
        {
            cursor.AddRowBase(row, context, passKind);

            if (cursor.HasMarker(BottomRightNonSummaryDataCellMarker))
            {
                cursor.RemoveMarker(BottomRightNonSummaryDataCellMarker);
            }

            cursor.AddMarker(BottomRightNonSummaryDataCellMarker);

            if ((row.ChildRows != null) && row.ChildRows.Any())
            {
                foreach (var childRow in row.ChildRows)
                {
                    cursor.ResetColumn();

                    cursor.MoveDown();

                    if (passKind == PassKind.Formatting)
                    {
                        cursor.CanvassedRowRange.ApplyDataRowsFormat(dataRowsFormat);
                    }

                    cursor.AddRow(childRow, context, passKind, dataRowsFormat);
                }
            }

            if (row.CollapsedSummaryRows != null && row.CollapsedSummaryRows.Any())
            {
                throw new NotSupportedException("collapsed summary rows are not supported");
            }

            if (row.ExpandedSummaryRows != null)
            {
                foreach (var expandedSummaryRow in row.ExpandedSummaryRows)
                {
                    cursor.ResetColumn();

                    cursor.MoveDown();

                    if (passKind == PassKind.Formatting)
                    {
                        cursor.CanvassedRowRange.ApplyDataRowsFormat(dataRowsFormat);
                    }

                    cursor.AddFlatRow(expandedSummaryRow, context, passKind);
                }
            }
        }

        private static void AddRowBase(
            this CellCursor cursor,
            RowBase rowBase,
            ReportToWorkbookProjectionContext context,
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

                if (passKind == PassKind.Formatting)
                {
                    cursor.CanvassedRowRange.ApplyRowFormat(rowBase.Format);
                }

                cursor.AddCell(rowBase.Cells[x], context, passKind);
            }
        }

        private static void AddCell(
            this CellCursor cursor,
            ICell cell,
            ReportToWorkbookProjectionContext context,
            PassKind passKind)
        {
            if (passKind == PassKind.Data)
            {
                if ((cell.ColumnsSpanned ?? 1) > 1)
                {
                    var mergeRange = cursor.Worksheet.GetRange(cursor.RowNumber, cursor.RowNumber, cursor.ColumnNumber, cursor.ColumnNumber + (int)cell.ColumnsSpanned - 1);

                    mergeRange.SetMergeCells(true);
                }
            }

            if ((passKind == PassKind.Formatting) && (cell is IHaveCellFormat haveCellFormat))
            {
                cursor.CellRange.ApplyCellFormat(haveCellFormat.Format);
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
                else if (cell is ConstCell<int> intConstCell)
                {
                    if (passKind == PassKind.Data)
                    {
                        cursor.Cell.Value = intConstCell.Value;
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