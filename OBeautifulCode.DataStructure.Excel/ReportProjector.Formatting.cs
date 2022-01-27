// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportProjector.Formatting.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Aspose.Cells;
    using OBeautifulCode.Enum.Recipes;
    using OBeautifulCode.Excel;
    using OBeautifulCode.Excel.AsposeCells;
    using OBeautifulCode.Reflection.Recipes;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    public static partial class ReportProjector
    {
        private static void ApplyTableFormat(
            this Range range,
            TableFormat tableFormat)
        {
            if (tableFormat == null)
            {
                return;
            }

            var implementedProperties = new[]
            {
                nameof(TableFormat.CellsFormat),
            };

            tableFormat.ThrowOnNotImplementedProperty(implementedProperties);

            range.ApplyCellFormat(tableFormat.CellsFormat);
        }

        private static void ApplyColumnFormat(
            this Range wholeColumnRange,
            Range dataCellsRange,
            Range lastHeaderCellToLastNonSummaryDataCellRange,
            ColumnFormat columnFormat)
        {
            if (columnFormat == null)
            {
                return;
            }

            var implementedProperties = new[]
            {
                nameof(ColumnFormat.CellsFormat),
                nameof(ColumnFormat.WidthInPixels),
                nameof(ColumnFormat.AutofitColumnWidth),
                nameof(ColumnFormat.Options),
            };

            columnFormat.ThrowOnNotImplementedProperty(implementedProperties);

            dataCellsRange.ApplyCellFormat(columnFormat.CellsFormat);

            wholeColumnRange.SetPerColumnWidthInPixels(columnFormat.WidthInPixels);

            // Apply options before auto-fitting, in case options change width of column (e.g. filtering)
            wholeColumnRange.ApplyColumnFormatOptions(lastHeaderCellToLastNonSummaryDataCellRange, columnFormat.Options);

            if (columnFormat.AutofitColumnWidth == true)
            {
                wholeColumnRange.Worksheet.AutoFitColumn(wholeColumnRange.FirstColumn);
            }
        }

        private static void ApplyColumnFormatOptions(
            this Range wholeColumnRange,
            Range lastHeaderCellToLastNonSummaryDataCellRange,
            ColumnFormatOptions? format)
        {
            if (format == null)
            {
                return;
            }

            var implementedOptions = new[]
            {
                ColumnFormatOptions.None,
                ColumnFormatOptions.Hide,
                ColumnFormatOptions.Freeze,
                ColumnFormatOptions.Sortable,
                ColumnFormatOptions.Filterable,
            };

            var columnFormatOptions = (ColumnFormatOptions)format;

            columnFormatOptions.ThrowOnNotImplementedEnumFlag(implementedOptions);

            if (columnFormatOptions.HasFlag(ColumnFormatOptions.Hide))
            {
                wholeColumnRange.Worksheet.Cells.HideColumn(wholeColumnRange.FirstColumn);
            }

            if (columnFormatOptions.HasFlag(ColumnFormatOptions.Freeze))
            {
                wholeColumnRange.Worksheet.GetFreezedPanes(out _, out _, out int frozenCellRowIndex, out _);

                var paneKinds = PaneKinds.Column;

                if (frozenCellRowIndex != 0)
                {
                    paneKinds |= PaneKinds.Row;
                }

                var freezePanesCell = wholeColumnRange.Worksheet.GetCell(frozenCellRowIndex + 1, wholeColumnRange.FirstColumn + 1 + 1);

                freezePanesCell.SetFreezePanes(paneKinds);
            }

            if (columnFormatOptions.HasFlag(ColumnFormatOptions.Sortable) || columnFormatOptions.HasFlag(ColumnFormatOptions.Filterable))
            {
                if (lastHeaderCellToLastNonSummaryDataCellRange == null)
                {
                    throw new InvalidOperationException("No header row; cannot make sortable nor filterable.");
                }

                var worksheet = wholeColumnRange.Worksheet;

                var filterRangeString = worksheet.AutoFilter.Range;

                Range filterRange;

                if (filterRangeString == null)
                {
                    // no existing filter
                    filterRange = lastHeaderCellToLastNonSummaryDataCellRange;
                }
                else
                {
                    // existing filter; expand the range
                    filterRange = worksheet.Cells.CreateRange(filterRangeString);

                    if ((filterRange.FirstRow != lastHeaderCellToLastNonSummaryDataCellRange.FirstRow) || (filterRange.RowCount != lastHeaderCellToLastNonSummaryDataCellRange.RowCount))
                    {
                        throw new InvalidOperationException("something went wrong");
                    }

                    filterRange = worksheet.GetRange(filterRange.FirstRow + 1, filterRange.FirstRow + filterRange.RowCount, filterRange.FirstColumn + 1, lastHeaderCellToLastNonSummaryDataCellRange.FirstColumn + 1);
                }

                filterRange.SetAutoFilter();
            }
        }

        private static void ApplyHeaderRowsFormat(
            this Range range,
            HeaderRowsFormat format)
        {
            if (format == null)
            {
                return;
            }

            var implementedProperties = new[]
            {
                nameof(HeaderRowsFormat.RowsFormat),
            };

            format.ThrowOnNotImplementedProperty(implementedProperties);

            range.ApplyRowFormat(format.RowsFormat);
        }

        private static void ApplyFooterRowsFormat(
            this Range range,
            FooterRowsFormat format)
        {
            if (format == null)
            {
                return;
            }

            var implementedProperties = new[]
            {
                nameof(FooterRowsFormat.RowsFormat),
            };

            format.ThrowOnNotImplementedProperty(implementedProperties);

            range.ApplyRowFormat(format.RowsFormat);
        }

        private static void ApplyDataRowsFormat(
            this Range range,
            DataRowsFormat format)
        {
            if (format == null)
            {
                return;
            }

            var implementedProperties = new[]
            {
                nameof(DataRowsFormat.RowsFormat),
            };

            format.ThrowOnNotImplementedProperty(implementedProperties);

            range.ApplyRowFormat(format.RowsFormat);
        }

        private static void ApplyRowFormat(
            this Range range,
            RowFormat format)
        {
            if (format == null)
            {
                return;
            }

            var implementedProperties = new[]
            {
                nameof(RowFormat.CellsFormat),
                nameof(RowFormat.HeightInPixels),
                nameof(RowFormat.Options),
            };

            format.ThrowOnNotImplementedProperty(implementedProperties);

            range.ApplyCellFormat(format.CellsFormat);

            range.SetPerRowHeightInPixels(format.HeightInPixels);

            range.ApplyRowFormatOptions(format.Options);
        }

        private static void ApplyRowFormatOptions(
            this Range range,
            RowFormatOptions? format)
        {
            if (format == null)
            {
                return;
            }

            var implementedOptions = new[]
            {
                RowFormatOptions.None,
                RowFormatOptions.Hide,
                RowFormatOptions.Freeze,
                RowFormatOptions.AlignChildRowsWithParent,
                RowFormatOptions.DisableCollapsing,
            };

            var rowFormatOptions = (RowFormatOptions)format;

            rowFormatOptions.ThrowOnNotImplementedEnumFlag(implementedOptions);

            if (rowFormatOptions.HasFlag(RowFormatOptions.Hide))
            {
                range.Worksheet.Cells.HideRow(range.FirstRow);
            }

            if (rowFormatOptions.HasFlag(RowFormatOptions.Freeze))
            {
                range.Worksheet.GetFreezedPanes(out _, out _, out int _, out var frozenCellColumnIndex);

                var paneKinds = PaneKinds.Row;

                if (frozenCellColumnIndex != 0)
                {
                    paneKinds |= PaneKinds.Column;
                }

                var freezePanesCell = range.Worksheet.GetCell(range.FirstRow + 1 + 1, frozenCellColumnIndex + 1);

                freezePanesCell.SetFreezePanes(paneKinds);
            }

            if (rowFormatOptions.HasFlag(RowFormatOptions.AlignChildRowsWithParent))
            {
                // todo: fix
            }
        }

        private static void ApplyCellFormat(
            this Range range,
            CellFormat format)
        {
            if (format == null)
            {
                return;
            }

            var implementedProperties = new[]
            {
                nameof(CellFormat.BackgroundColor),
                nameof(CellFormat.FontFormat),
            };

            format.ThrowOnNotImplementedProperty(implementedProperties);

            if (format.BackgroundColor != null)
            {
                range.SetBackgroundColor(format.BackgroundColor);
            }

            range.ApplyFontFormat(format.FontFormat);
        }

        private static void ApplyFontFormat(
            this Range range,
            FontFormat format)
        {
            if (format == null)
            {
                return;
            }

            var implementedProperties = new[]
            {
                nameof(FontFormat.FontColor),
                nameof(FontFormat.FontNamesInFallbackOrder),
                nameof(FontFormat.FontSizeInPoints),
                nameof(FontFormat.Options),
            };

            format.ThrowOnNotImplementedProperty(implementedProperties);

            range.SetFontColor(format.FontColor);

            range.SetFontName(format.FontNamesInFallbackOrder?.FirstOrDefault());

            range.SetFontSize(format.FontSizeInPoints == null ? (int?)null : decimal.ToInt32(Math.Round((decimal)format.FontSizeInPoints)));

            range.ApplyFontFormatOptions(format.Options);
        }

        private static void ApplyFontFormatOptions(
            this Range range,
            FontFormatOptions? format)
        {
            if (format == null)
            {
                return;
            }

            var implementedOptions = new[]
            {
                FontFormatOptions.None,
                FontFormatOptions.Bold,
                FontFormatOptions.Italics,
                FontFormatOptions.Underline,
            };

            var fontFormatOptions = (FontFormatOptions)format;

            fontFormatOptions.ThrowOnNotImplementedEnumFlag(implementedOptions);

            if (fontFormatOptions.HasFlag(FontFormatOptions.Bold))
            {
                range.SetFontIsBold(true);
            }

            if (fontFormatOptions.HasFlag(FontFormatOptions.Italics))
            {
                range.SetFontIsItalic(true);
            }

            if (fontFormatOptions.HasFlag(FontFormatOptions.Underline))
            {
                range.SetFontUnderline(UnderlineKind.Single);
            }
        }

        private static void ThrowOnNotImplementedProperty<TObject>(
            this TObject item,
            IReadOnlyCollection<string> implementedPropertyNames)
        {
            if (!CachedTypeToNotImplementedPropertiesMap.TryGetValue(typeof(TObject), out var notImplementedProperties))
            {
                notImplementedProperties = typeof(TObject)
                    .GetPropertiesFiltered(MemberRelationships.DeclaredOrInherited, MemberOwners.Instance, MemberAccessModifiers.PublicGet)
                    .Where(_ => !implementedPropertyNames.Contains(_.Name))
                    .ToList();

                CachedTypeToNotImplementedPropertiesMap.TryAdd(typeof(TObject), notImplementedProperties);
            }

            foreach (var notImplementedProperty in notImplementedProperties)
            {
                if (notImplementedProperty.GetValue(item) != null)
                {
                    throw new NotImplementedException(Invariant($"{typeof(TObject).ToStringReadable()}.{notImplementedProperty.Name}"));
                }
            }
        }

        private static void ThrowOnNotImplementedEnumFlag<TEnum>(
            this TEnum value,
            IReadOnlyCollection<TEnum> implementedFlags)
            where TEnum : struct, Enum
        {
            var notImplementedFlags = EnumExtensions.GetIndividualFlags<TEnum>().Except(implementedFlags).ToList();

            foreach (var notImplementedFlag in notImplementedFlags)
            {
                if (value.HasFlag(notImplementedFlag))
                {
                    throw new NotImplementedException(Invariant($"{typeof(TEnum).ToStringReadable()}.{notImplementedFlag}"));
                }
            }
        }
    }
}