// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Linq;
    using FakeItEasy;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Type;
    using Xunit;

    public static class ReportExtensionsTest
    {
        [Fact]
        public static void GetFormattedTimestampToDisplayOrNull___Should_throw_ArgumentNullException___When_parameter_report_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ReportExtensions.GetFormattedTimestampToDisplayOrNull(null));

            // Act
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("report");
        }

        [Fact]
        public static void GetFormattedTimestampToDisplayOrNull___Should_return_null___When_report_timestamp_is_null()
        {
            // Arrange
            var report1 = A.Dummy<ConstCell<Version>>().ToReport(timestampUtc: null);
            var report2 = A.Dummy<ConstCell<Version>>().ToReport(timestampUtc: null, format: new ReportFormat(displayTimestamp: true));

            // Act
            var actual1 = report1.GetFormattedTimestampToDisplayOrNull();
            var actual2 = report2.GetFormattedTimestampToDisplayOrNull();

            // Act
            actual1.AsTest().Must().BeNull();
            actual2.AsTest().Must().BeNull();
        }

        [Fact]
        public static void GetFormattedTimestampToDisplayOrNull___Should_return_null___When_report_not_configured_to_display_timestamp()
        {
            // Arrange
            var report1 = A.Dummy<ConstCell<Version>>().ToReport(timestampUtc: A.Dummy<UtcDateTime>(), format: new ReportFormat(displayTimestamp: null));
            var report2 = A.Dummy<ConstCell<Version>>().ToReport(timestampUtc: A.Dummy<UtcDateTime>(), format: new ReportFormat(displayTimestamp: false));

            // Act
            var actual1 = report1.GetFormattedTimestampToDisplayOrNull();
            var actual2 = report2.GetFormattedTimestampToDisplayOrNull();

            // Act
            actual1.AsTest().Must().BeNull();
            actual2.AsTest().Must().BeNull();
        }

        [Fact]
        public static void GetFormattedTimestampToDisplayOrNull___Should_return_formatted_timestamp___When_called()
        {
            // Arrange
            var timestampFormatsAndExpected = new[]
            {
                new
                {
                    Format = (DateTimeFormat)null,
                    Expected = "Monday, August 4, 2025 at 2:07 PM (gmt)",
                    FallbackCultureKind = (CultureKind?)null,
                    FallbackTimezone = (StandardTimeZone?)null,
                },
                new
                {
                    Format = new DateTimeFormat(),
                    Expected = "Monday, August 4, 2025 at 2:07 PM (gmt)",
                    FallbackCultureKind = (CultureKind?)null,
                    FallbackTimezone = (StandardTimeZone?)null,
                },
                new
                {
                    Format = new DateTimeFormat(
                        localizeTimeZone: true,
                        localTimeZone: StandardTimeZone.Eastern),
                    Expected = "Monday, August 4, 2025 at 10:07 AM (eastern time)",
                    FallbackCultureKind = (CultureKind?)null,
                    FallbackTimezone = (StandardTimeZone?)null,
                },
                new
                {
                    Format = new DateTimeFormat(
                        cultureKind: CultureKind.EnglishUnitedKingdom),
                    Expected = "Monday, August 4, 2025 at 2:07 PM (gmt)",
                    FallbackCultureKind = (CultureKind?)null,
                    FallbackTimezone = (StandardTimeZone?)null,
                },
                new
                {
                    Format = new DateTimeFormat(
                        DateTimeFormatKind.GeneralDateTimePatternShortTime),
                    Expected = "08/04/2025 14:07",
                    FallbackCultureKind = (CultureKind?)null,
                    FallbackTimezone = (StandardTimeZone?)null,
                },
                new
                {
                    Format = new DateTimeFormat(
                        DateTimeFormatKind.GeneralDateTimePatternShortTime,
                        CultureKind.EnglishUnitedKingdom),
                    Expected = "04/08/2025 14:07",
                    FallbackCultureKind = (CultureKind?)null,
                    FallbackTimezone = (StandardTimeZone?)null,
                },
                new
                {
                    Format = new DateTimeFormat(
                        DateTimeFormatKind.GeneralDateTimePatternShortTime,
                        CultureKind.EnglishUnitedKingdom,
                        localizeTimeZone: true,
                        localTimeZone: StandardTimeZone.Eastern),
                    Expected = "04/08/2025 10:07",
                    FallbackCultureKind = (CultureKind?)null,
                    FallbackTimezone = (StandardTimeZone?)null,
                },
                new
                {
                    Format = new DateTimeFormat(
                        DateTimeFormatKind.GeneralDateTimePatternShortTime,
                        CultureKind.EnglishUnitedKingdom,
                        localizeTimeZone: true,
                        localTimeZone: StandardTimeZone.Eastern),
                    Expected = "04/08/2025 10:07",
                    FallbackCultureKind = (CultureKind?)CultureKind.EnglishUnitedStates,
                    FallbackTimezone = (StandardTimeZone?)StandardTimeZone.Gmt,
                },
                new
                {
                    Format = (DateTimeFormat)null,
                    Expected = "Monday, August 4, 2025 at 2:07 PM (gmt)",
                    FallbackCultureKind = (CultureKind?)CultureKind.EnglishUnitedKingdom,
                    FallbackTimezone = (StandardTimeZone?)null,
                },
                new
                {
                    Format = (DateTimeFormat)null,
                    Expected = "Monday, August 4, 2025 at 10:07 AM (eastern time)",
                    FallbackCultureKind = (CultureKind?)null,
                    FallbackTimezone = (StandardTimeZone?)StandardTimeZone.Eastern,
                },
                new
                {
                    Format = new DateTimeFormat(
                        DateTimeFormatKind.GeneralDateTimePatternShortTime),
                    Expected = "04/08/2025 14:07",
                    FallbackCultureKind = (CultureKind?)CultureKind.EnglishUnitedKingdom,
                    FallbackTimezone = (StandardTimeZone?)null,
                },
                new
                {
                    Format = new DateTimeFormat(
                        DateTimeFormatKind.GeneralDateTimePatternShortTime),
                    Expected = "08/04/2025 10:07",
                    FallbackCultureKind = (CultureKind?)null,
                    FallbackTimezone = (StandardTimeZone?)StandardTimeZone.Eastern,
                },
                new
                {
                    Format = new DateTimeFormat(
                        DateTimeFormatKind.GeneralDateTimePatternShortTime),
                    Expected = "04/08/2025 10:07",
                    FallbackCultureKind = (CultureKind?)CultureKind.EnglishUnitedKingdom,
                    FallbackTimezone = (StandardTimeZone?)StandardTimeZone.Eastern,
                },
            };

            var timestamp = new DateTime(2025, 8, 4, 14, 7, 22, DateTimeKind.Utc);

            var reports = timestampFormatsAndExpected.Select(_ => new
            {
                Report = A.Dummy<ConstCell<Version>>().ToReport(timestampUtc: timestamp, format: new ReportFormat(displayTimestamp: true, timestampFormat: _.Format)),
                FallbackCultureKind = _.FallbackCultureKind,
                FallbackTimezone = _.FallbackTimezone,
            }).ToList();

            var expected = timestampFormatsAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual = reports.Select(_ => _.Report.GetFormattedTimestampToDisplayOrNull(_.FallbackCultureKind, _.FallbackTimezone)).ToList();

            // Act
            actual.AsTest().Must().BeEqualTo(expected);
        }
    }
}
