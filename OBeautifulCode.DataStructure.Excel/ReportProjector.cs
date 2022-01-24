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
    using OBeautifulCode.Enum.Recipes;
    using OBeautifulCode.Excel;
    using OBeautifulCode.Excel.AsposeCells;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Projects a <see cref="Report"/> into an Excel workbook.
    /// </summary>
    public static class ReportProjector
    {
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

            return result;
        }

        private static void AddSection(
            this Workbook workbook,
            Section section,
            InternalReportToWorkbookProjectionContext context)
        {
            // TODO: add worksheet name.
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.ExternalContext == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Report == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var worksheet = workbook.Worksheets.Add("TODO: CHANGE THIS");

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

            var notImplemented = new Dictionary<string, object>
            {
                { Invariant($"{nameof(CellFormat)}.{nameof(CellFormat.OuterBorders)}"), format.OuterBorders },
                { Invariant($"{nameof(CellFormat)}.{nameof(CellFormat.BackgroundColor)}"), format.BackgroundColor },
                { Invariant($"{nameof(CellFormat)}.{nameof(CellFormat.VerticalAlignment)}"), format.VerticalAlignment },
                { Invariant($"{nameof(CellFormat)}.{nameof(CellFormat.HorizontalAlignment)}"), format.HorizontalAlignment },
                { Invariant($"{nameof(CellFormat)}.{nameof(CellFormat.FontRotationAngle)}"), format.FontRotationAngle },
                { Invariant($"{nameof(CellFormat)}.{nameof(CellFormat.FillPattern)}"), format.FillPattern },
                { Invariant($"{nameof(CellFormat)}.{nameof(CellFormat.Options)}"), format.Options },
            };

            notImplemented.ThrowOnNotImplementedProperty();

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

            var cellRange = cell.GetRange();

            cellRange.SetFontColor(format.FontColor);

            cellRange.SetFontName(format.FontNamesInFallbackOrder?.FirstOrDefault());

            cellRange.SetFontSize(format.FontSizeInPoints == null ? (int?)null : decimal.ToInt32(Math.Round((decimal)format.FontSizeInPoints)));

            if (format.Options != null)
            {
                var notImplementedFontFormatOptions = EnumExtensions.GetIndividualFlags<FontFormatOptions>().Except(new[] { FontFormatOptions.None, FontFormatOptions.Bold, FontFormatOptions.Italics, FontFormatOptions.Underline }).ToList();

                var fontFormatOptions = (FontFormatOptions)format.Options;

                fontFormatOptions.ThrowOnNotImplementedEnumFlag(notImplementedFontFormatOptions);

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
        }

        private static void ThrowOnNotImplementedProperty(
            this IReadOnlyDictionary<string, object> notImplementedPropertyNameToValueMap)
        {
            foreach (var kvp in notImplementedPropertyNameToValueMap)
            {
                if (kvp.Value != null)
                {
                    throw new NotImplementedException(kvp.Key);
                }
            }
        }

        private static void ThrowOnNotImplementedEnumFlag<TEnum>(
            this TEnum value,
            IReadOnlyCollection<TEnum> notImplementedFlags)
            where TEnum : Enum
        {
            foreach (var flag in notImplementedFlags)
            {
                if (value.HasFlag(flag))
                {
                    throw new NotImplementedException(Invariant($"{typeof(TEnum).ToStringReadable()}.{flag}"));
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