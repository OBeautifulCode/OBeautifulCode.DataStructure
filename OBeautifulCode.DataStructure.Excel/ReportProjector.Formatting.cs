// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportProjector.Formatting.cs" company="OBeautifulCode">
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
    using OBeautifulCode.Enum.Recipes;
    using OBeautifulCode.Excel;
    using OBeautifulCode.Excel.AsposeCells;
    using OBeautifulCode.Reflection.Recipes;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    public static partial class ReportProjector
    {
        private static readonly ConcurrentDictionary<Type, IReadOnlyCollection<PropertyInfo>> CachedTypeToNotImplementedPropertiesMap =
            new ConcurrentDictionary<Type, IReadOnlyCollection<PropertyInfo>>();

        private static readonly ConcurrentDictionary<Type, IReadOnlyDictionary<string, PropertyInfo>> CachedTypeToPropertyNameToPropertyMap =
            new ConcurrentDictionary<Type, IReadOnlyDictionary<string, PropertyInfo>>();

        private static void ApplyReportFormat(
            this Range range,
            ReportFormat reportFormat)
        {
            if (reportFormat == null)
            {
                return;
            }

            var implementedProperties = new[]
            {
                nameof(ReportFormat.DisplayTimestamp),
                nameof(ReportFormat.TimestampFormat),
                nameof(ReportFormat.Options),
            };

            reportFormat.ThrowOnNotImplementedProperty(implementedProperties);

            // Timestamp is added to bottom chrome of worksheet and formatting is managed there.
            range.ApplyReportFormatOptions(reportFormat.Options);
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "range", Justification = "Future-proofing.")]
        private static void ApplyReportFormatOptions(
            this Range range,
            ReportFormatOptions? options)
        {
            if (options == null)
            {
                return;
            }

            var implementedOptions = new ReportFormatOptions[]
            {
            };

            var reportFormatOptions = (ReportFormatOptions)options;

            reportFormatOptions.ThrowOnNotImplementedEnumFlag(implementedOptions);
        }

        private static void ApplySectionFormat(
            this Range range,
            SectionFormat sectionFormat)
        {
            if (sectionFormat == null)
            {
                return;
            }

            var implementedProperties = new[]
            {
                nameof(SectionFormat.Options),
            };

            sectionFormat.ThrowOnNotImplementedProperty(implementedProperties);

            range.ApplySectionFormatOptions(sectionFormat.Options);
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "range", Justification = "Future-proofing.")]
        private static void ApplySectionFormatOptions(
            this Range range,
            SectionFormatOptions? options)
        {
            if (options == null)
            {
                return;
            }

            var implementedOptions = new SectionFormatOptions[]
            {
            };

            var sectionFormatOptions = (SectionFormatOptions)options;

            sectionFormatOptions.ThrowOnNotImplementedEnumFlag(implementedOptions);
        }

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
                wholeColumnRange.Worksheet.AutoFitColumn(
                    lastHeaderCellToLastNonSummaryDataCellRange.FirstColumn,
                    lastHeaderCellToLastNonSummaryDataCellRange.FirstRow,
                    lastHeaderCellToLastNonSummaryDataCellRange.FirstRow + lastHeaderCellToLastNonSummaryDataCellRange.RowCount - 1);
            }
        }

        private static void ApplyColumnFormatOptions(
            this Range wholeColumnRange,
            Range lastHeaderCellToLastNonSummaryDataCellRange,
            ColumnFormatOptions? options)
        {
            if (options == null)
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

            var columnFormatOptions = (ColumnFormatOptions)options;

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

            if (options.RequiresAutoFilter())
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

        private static void ApplyTableRowsFormat(
            this Range range,
            RowFormat rowFormat,
            InternalProjectionContext context)
        {
            if (rowFormat == null)
            {
                return;
            }

            if ((rowFormat.Options != null) && ((RowFormatOptions)rowFormat.Options).HasFlag(RowFormatOptions.AlignChildRowsWithParent))
            {
                context.DisableIndentationByTreeLevel = true;
            }

            range.ApplyRowFormat(rowFormat);
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
            DataRowsFormat format,
            InternalProjectionContext context)
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

            if ((format.RowsFormat?.Options != null) && ((RowFormatOptions)format.RowsFormat.Options).HasFlag(RowFormatOptions.AlignChildRowsWithParent))
            {
                context.DisableIndentationByTreeLevel = true;
            }

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
            RowFormatOptions? options)
        {
            if (options == null)
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

            var rowFormatOptions = (RowFormatOptions)options;

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
                // The indentation is applied to the first cell in the row only, so it's not handled in row formatting.
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
                nameof(CellFormat.HorizontalAlignment),
            };

            format.ThrowOnNotImplementedProperty(implementedProperties);

            if (format.BackgroundColor != null)
            {
                range.SetBackgroundColor(format.BackgroundColor);
            }

            if (format.HorizontalAlignment != null)
            {
                HorizontalAlignment horizontalAlignment;

                switch ((DataStructure.HorizontalAlignment)format.HorizontalAlignment)
                {
                    case DataStructure.HorizontalAlignment.Center:
                        horizontalAlignment = HorizontalAlignment.Center;
                        break;
                    case DataStructure.HorizontalAlignment.Left:
                        horizontalAlignment = HorizontalAlignment.Left;
                        break;
                    case DataStructure.HorizontalAlignment.Right:
                        horizontalAlignment = HorizontalAlignment.Right;
                        break;
                    default:
                        throw new NotImplementedException(Invariant($"This {nameof(DataStructure.HorizontalAlignment)} is not yet implemented: {format.HorizontalAlignment}."));
                }

                range.SetHorizontalAlignment(horizontalAlignment);
            }

            range.ApplyFontFormat(format.FontFormat);
        }

        private static void ApplyCellValueFormat(
            this Range range,
            ICellValueFormat valueFormat)
        {
            if (valueFormat == null)
            {
                return;
            }

            var valueFormatType = valueFormat.GetType();

            if (valueFormatType.GetGenericTypeDefinitionOrSpecifiedType() == typeof(CurrencyCellValueFormat<>))
            {
                var numberOfDecimalPlacesName = nameof(CurrencyCellValueFormat<object>.NumberOfDecimalPlaces);
                var roundingStrategyName = nameof(CurrencyCellValueFormat<object>.RoundingStrategy);
                var currencyCodeName = nameof(CurrencyCellValueFormat<object>.CurrencyCode);

                var implementedProperties = new[]
                {
                    numberOfDecimalPlacesName,
                    roundingStrategyName,
                    currencyCodeName,
                };

                valueFormat.ThrowOnNotImplementedProperty(valueFormatType, implementedProperties);

                var propertyNameToPropertyMap = CachedTypeToPropertyNameToPropertyMap[valueFormatType];

                var roundingStrategy = (MidpointRounding?)propertyNameToPropertyMap[roundingStrategyName].GetValue(valueFormat);

                if ((roundingStrategy != null) && (roundingStrategy != MidpointRounding.AwayFromZero))
                {
                    throw new NotImplementedException(Invariant($"This {roundingStrategyName} is not yet implemented: {roundingStrategy}."));
                }

                var currencyCode = (CurrencyCode?)propertyNameToPropertyMap[currencyCodeName].GetValue(valueFormat);

                if ((currencyCode != null) && (currencyCode != CurrencyCode.Usd))
                {
                    throw new NotImplementedException(Invariant($"This {currencyCodeName} is not yet implemented: {currencyCode}."));
                }

                var numberOfDecimalPlaces = (int?)propertyNameToPropertyMap[numberOfDecimalPlacesName].GetValue(valueFormat);

                if ((numberOfDecimalPlaces == null) || (numberOfDecimalPlaces == 0))
                {
                    range.SetCustomFormat(Invariant($"$#,##0;-$#,##0"));
                }
                else
                {
                    range.SetCustomFormat(Invariant($"$#,##0.{new string('0', (int)numberOfDecimalPlaces)};-$#,##0.{new string('0', (int)numberOfDecimalPlaces)}"));
                }

                valueFormat.ThrowOnNotImplementedProperty(valueFormatType, implementedProperties);
            }
            else if (valueFormatType.GetGenericTypeDefinitionOrSpecifiedType() == typeof(NumberCellValueFormat<>))
            {
                var numberOfDecimalPlacesName = nameof(NumberCellValueFormat<object>.NumberOfDecimalPlaces);
                var roundingStrategyName = nameof(NumberCellValueFormat<object>.RoundingStrategy);

                var implementedProperties = new[]
                {
                    numberOfDecimalPlacesName,
                    roundingStrategyName,
                };

                valueFormat.ThrowOnNotImplementedProperty(valueFormatType, implementedProperties);

                var propertyNameToPropertyMap = CachedTypeToPropertyNameToPropertyMap[valueFormatType];

                var roundingStrategy = (MidpointRounding?)propertyNameToPropertyMap[roundingStrategyName].GetValue(valueFormat);

                if ((roundingStrategy != null) && (roundingStrategy != MidpointRounding.AwayFromZero))
                {
                    throw new NotImplementedException(Invariant($"This {roundingStrategyName} is not yet implemented: {roundingStrategy}."));
                }

                var numberOfDecimalPlaces = (int?)propertyNameToPropertyMap[numberOfDecimalPlacesName].GetValue(valueFormat);

                if (numberOfDecimalPlaces == null)
                {
                    range.SetFormat(OBeautifulCode.Excel.Format.Decimal4);
                }
                else if (numberOfDecimalPlaces == 0)
                {
                    range.SetFormat(OBeautifulCode.Excel.Format.Decimal3);
                }
                else if (numberOfDecimalPlaces == 2)
                {
                    range.SetFormat(OBeautifulCode.Excel.Format.Decimal4);
                }
                else
                {
                    range.SetCustomFormat(Invariant($"#,##0.{new string('0', (int)numberOfDecimalPlaces)}"));
                }
            }
            else if (valueFormatType.GetGenericTypeDefinitionOrSpecifiedType() == typeof(PercentCellValueFormat<>))
            {
                var numberOfDecimalPlacesName = nameof(PercentCellValueFormat<object>.NumberOfDecimalPlaces);
                var roundingStrategyName = nameof(PercentCellValueFormat<object>.RoundingStrategy);

                var implementedProperties = new[]
                {
                    numberOfDecimalPlacesName,
                    roundingStrategyName,
                };

                valueFormat.ThrowOnNotImplementedProperty(valueFormatType, implementedProperties);

                var propertyNameToPropertyMap = CachedTypeToPropertyNameToPropertyMap[valueFormatType];

                var roundingStrategy = (MidpointRounding?)propertyNameToPropertyMap[roundingStrategyName].GetValue(valueFormat);

                if ((roundingStrategy != null) && (roundingStrategy != MidpointRounding.AwayFromZero))
                {
                    throw new NotImplementedException(Invariant($"This {roundingStrategyName} is not yet implemented: {roundingStrategy}."));
                }

                var numberOfDecimalPlaces = (int?)propertyNameToPropertyMap[numberOfDecimalPlacesName].GetValue(valueFormat);

                if (numberOfDecimalPlaces == null)
                {
                    range.SetFormat(OBeautifulCode.Excel.Format.Percentage2);
                }
                else if (numberOfDecimalPlaces == 0)
                {
                    range.SetFormat(OBeautifulCode.Excel.Format.Percentage1);
                }
                else if (numberOfDecimalPlaces == 2)
                {
                    range.SetFormat(OBeautifulCode.Excel.Format.Percentage2);
                }
                else
                {
                    range.SetCustomFormat(Invariant($"0.{new string('0', (int)numberOfDecimalPlaces)}%"));
                }
            }
            else
            {
                throw new NotImplementedException(Invariant($"This {nameof(ICellValueFormat)} is not yet implemented: {valueFormat.GetType().ToStringReadable()}."));
            }
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
            FontFormatOptions? options)
        {
            if (options == null)
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

            var fontFormatOptions = (FontFormatOptions)options;

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

        private static void ApplyTreeLevelFormat(
            this Range range,
            InternalProjectionContext context)
        {
            if (context.CurrentTreeLevel.Any())
            {
                if (!context.DisableIndentationByTreeLevel)
                {
                    var treeLevels = context.CurrentTreeLevel.ToList();
                    var alignments = context.AlignChildRowsWithParent.Skip(1).ToList();
                    alignments.Add(true);

                    var indentLevel = 0;

                    for (var x = 0; x < treeLevels.Count; x++)
                    {
                        indentLevel = treeLevels[x];

                        if (!alignments[x])
                        {
                            break;
                        }
                    }

                    range.SetIndentLevel(indentLevel);
                }
            }
        }

        private static string Format(
            this DateTime value,
            DateTimeFormat dateTimeFormat,
            InternalProjectionContext context)
        {
            var cultureKind = dateTimeFormat?.CultureKind ?? context.ExternalContext.CultureKind ?? CultureKind.Invariant;

            var dateTimeFormatKind = dateTimeFormat?.FormatKind ?? DateTimeFormatKind.FullDateTimePatternShortTime;

            var localizeTimeZone = dateTimeFormat?.LocalizeTimeZone ?? false;

            var localTimeZone = dateTimeFormat?.LocalTimeZone ?? context.ExternalContext.LocalTimeZone ?? StandardTimeZone.Unknown;

            if (localizeTimeZone && (localTimeZone == StandardTimeZone.Unknown))
            {
                throw new InvalidOperationException("Cannot localize time zone of timestamp unless the local time zone is specified.");
            }

            if (localizeTimeZone)
            {
                value = TimeZoneInfo.ConvertTimeFromUtc(value, localTimeZone.ToTimeZoneInfo());
            }

            var result = value.ToString(dateTimeFormatKind, cultureKind);

            return result;
        }

        private static void ThrowOnNotImplementedProperty<TObject>(
            this TObject item,
            IReadOnlyCollection<string> implementedPropertyNames)
        {
            item.ThrowOnNotImplementedProperty(typeof(TObject), implementedPropertyNames);
        }

        private static void ThrowOnNotImplementedProperty(
            this object item,
            Type itemType,
            IReadOnlyCollection<string> implementedPropertyNames)
        {
            if (!CachedTypeToNotImplementedPropertiesMap.TryGetValue(itemType, out var notImplementedProperties))
            {
                var properties = itemType.GetPropertiesFiltered(MemberRelationships.DeclaredOrInherited, MemberOwners.Instance, MemberAccessModifiers.PublicGet);

                var implementedPropertyNameToPropertyInfoMap = properties
                    .Where(_ => implementedPropertyNames.Contains(_.Name))
                    .ToDictionary(_ => _.Name, _ => _);

                notImplementedProperties = properties
                    .Where(_ => !implementedPropertyNames.Contains(_.Name))
                    .ToList();

                CachedTypeToPropertyNameToPropertyMap.TryAdd(itemType, implementedPropertyNameToPropertyInfoMap);

                CachedTypeToNotImplementedPropertiesMap.TryAdd(itemType, notImplementedProperties);
            }

            foreach (var notImplementedProperty in notImplementedProperties)
            {
                if (notImplementedProperty.GetValue(item) != null)
                {
                    throw new NotImplementedException(Invariant($"{itemType.ToStringReadable()}.{notImplementedProperty.Name}"));
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

        private static bool RequiresAutoFilter(
            this ColumnFormatOptions? options)
        {
            bool result;

            if (options == null)
            {
                result = false;
            }
            else
            {
                var columnFormatOptions = (ColumnFormatOptions)options;

                result = columnFormatOptions.HasFlag(ColumnFormatOptions.Sortable) ||
                             columnFormatOptions.HasFlag(ColumnFormatOptions.Filterable);
            }

            return result;
        }
    }
}