// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.DateTime.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Extensions methods on <see cref="Report"/>.
    /// </summary>
    public static class ReportExtensions
    {
        /// <summary>
        /// Gets a formatted timestamp to display for a report if a timestamp is specified
        /// AND report is configured to display the timestamp.  Otherwise, returns null.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="fallbackCultureKind">OPTIONAL fallback culture kind to use when formatting the timestamp if the report does not specify one.  DEFAULT is none specified.</param>
        /// <param name="fallbackTimeZone">OPTIONAL fallback time zone to use when formatting the timestamp if the report does not specify one.  DEFAULT is none specified.</param>
        /// <returns>
        /// The formatted timestamp or null if a timestamp is not specified or is specified but the report is not configured to display it.
        /// </returns>
        public static string GetFormattedTimestampToDisplayOrNull(
            this Report report,
            CultureKind? fallbackCultureKind = null,
            StandardTimeZone? fallbackTimeZone = null)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            string result = null;

            if ((report.TimestampUtc != null) && (report.Format?.DisplayTimestamp ?? false))
            {
                result = ((DateTime)report.TimestampUtc).Format(
                    report.Format?.TimestampFormat,
                    fallbackCultureKind,
                    fallbackTimeZone);
            }

            return result;
        }

        private static string Format(
            this DateTime valueUtc,
            DateTimeFormat dateTimeFormat,
            CultureKind? fallbackCultureKind,
            StandardTimeZone? fallbackTimeZone)
        {
            var cultureKind = dateTimeFormat?.CultureKind ?? fallbackCultureKind ?? CultureKind.Invariant;

            var dateTimeFormatKind = dateTimeFormat?.FormatKind;

            var localizeTimeZone = dateTimeFormat?.LocalizeTimeZone ?? (fallbackTimeZone != null);

            var localTimeZone = dateTimeFormat?.LocalTimeZone ?? fallbackTimeZone ?? StandardTimeZone.Unknown;

            TimeZoneInfo localTimeZoneInfo = null;

            if (localizeTimeZone)
            {
                if (localTimeZone == StandardTimeZone.Unknown)
                {
                    throw new InvalidOperationException("Cannot localize time zone of timestamp unless the local time zone is specified.");
                }

                localTimeZoneInfo = localTimeZone.ToTimeZoneInfo();
            }

            string result;

            if (dateTimeFormatKind == null)
            {
                result = valueUtc.ToStringPretty(localTimeZoneInfo);
            }
            else
            {
                DateTime valueToUse = valueUtc;

                if (localizeTimeZone)
                {
                    valueToUse = TimeZoneInfo.ConvertTimeFromUtc(valueUtc, localTimeZoneInfo);
                }

                result = valueToUse.ToString((DateTimeFormatKind)dateTimeFormatKind, cultureKind);
            }

            return result;
        }
    }
}
