// --------------------------------------------------------------------------------------------------------------------
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
    using OBeautifulCode.Enum.Recipes;
    using OBeautifulCode.Excel;
    using OBeautifulCode.Excel.AsposeCells;
    using OBeautifulCode.Reflection.Recipes;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Projects a <see cref="Report"/> into an Excel workbook.
    /// </summary>
    public static class ReportProjector
    {
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

            var result = General.CreateStandardWorkbook().RemoveDefaultWorksheet();

            var documentProperties = context.BuildDocumentPropertiesDelegate == null
                ? new DocumentProperties()
                : context.BuildDocumentPropertiesDelegate(report.Title);

            // If report has a timestamp, should we set BuiltInDocumentPropertyKind.CreationDate and LastSaveTime to that timestamp?
            // Should those properties reflect when the workbook was created or when the report was created?
            result.SetDocumentProperties(documentProperties);

            var internalContext = new InternalReportToWorkbookProjectionContext
            {
                Report = report,
                ExternalContext = context,
            };

            foreach (var section in report.Sections)
            {
                result.AddSection(section, internalContext);
            }

            // TODO: ADD copyright and terms of use
            // TODO: ADD report timestamp
            return result;
        }

        private static void AddSection(
            this Workbook workbook,
            Section section,
            InternalReportToWorkbookProjectionContext context)
        {
            var worksheet = workbook.Worksheets.Add(section.Name);

            var cursor = new CellCursor(worksheet);

            if (section.Title != null)
            {
                cursor.Cell.Value = section.Title;

                // Section model doesn't have any formatting for title.
                cursor.Cell.ApplyFormat(new CellFormat(fontFormat: new FontFormat(fontSizeInPoints: 18)));

                cursor.MoveDown();
            }
        }

        private static void ApplyFormat(
            this Cell cell,
            CellFormat format)
        {
            if (format == null)
            {
                return;
            }

            var implementedProperties = new[]
            {
                nameof(CellFormat.FontFormat),
            };

            format.ThrowOnNotImplementedProperty(implementedProperties);

            cell.ApplyFormat(format.FontFormat);
        }

        private static void ApplyFormat(
            this Cell cell,
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

            var cellRange = cell.GetRange();

            cellRange.SetFontColor(format.FontColor);

            cellRange.SetFontName(format.FontNamesInFallbackOrder?.FirstOrDefault());

            cellRange.SetFontSize(format.FontSizeInPoints == null ? (int?)null : decimal.ToInt32(Math.Round((decimal)format.FontSizeInPoints)));

            cell.ApplyFormat(format.Options);
        }

        private static void ApplyFormat(
            this Cell cell,
            FontFormatOptions? format)
        {
            if (format == null)
            {
                return;
            }

            var implementedFontFormatOptions = new[]
            {
                FontFormatOptions.None,
                FontFormatOptions.Bold,
                FontFormatOptions.Italics,
                FontFormatOptions.Underline,
            };

            var fontFormatOptions = (FontFormatOptions)format;

            fontFormatOptions.ThrowOnNotImplementedEnumFlag(implementedFontFormatOptions);

            var cellRange = cell.GetRange();

            if (fontFormatOptions.HasFlag(FontFormatOptions.Bold))
            {
                cellRange.SetFontIsBold(true);
            }

            if (fontFormatOptions.HasFlag(FontFormatOptions.Italics))
            {
                cellRange.SetFontIsItalic(true);
            }

            if (fontFormatOptions.HasFlag(FontFormatOptions.Underline))
            {
                cellRange.SetFontUnderline(UnderlineKind.Single);
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

        private class InternalReportToWorkbookProjectionContext
        {
            public Report Report { get; set; }

            public ReportToWorkbookProjectionContext ExternalContext { get; set; }
        }
    }
}