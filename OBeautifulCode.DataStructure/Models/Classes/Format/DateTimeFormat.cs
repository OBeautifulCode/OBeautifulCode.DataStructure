// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Specifies how to format a <see cref="DateTime"/>.
    /// </summary>
    public partial class DateTimeFormat : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeFormat"/> class.
        /// </summary>
        /// <param name="formatKind">OPTIONAL kind of formatting to apply.  DEFAULT is to let the consumer decide (e.g. use formatting kind from user's profile or always use a fixed formatting).</param>
        /// <param name="cultureKind">OPTIONAL kind of culture to apply when formatting.  DEFAULT is let the consumer decide (e.g. use culture from user's profile or always use invariant culture).</param>
        /// <param name="localize">OPTIONAL value that determines whether to localize a <see cref="DateTimeKind.Utc"/> <see cref="DateTime"/> before generating it's string representation.  DEFAULT is to let the consumer decide (e.g. never localize unless explicitly set to true).</param>
        /// <param name="localTimeZone">OPTIONAL time zone to use when localizing a <see cref="DateTimeKind.Utc"/> <see cref="DateTime"/> before generating it's string representation.  DEFAULT is to let the consumer decide (e.g. when <paramref name="localize"/> is true, use the user's local timezone).</param>
        public DateTimeFormat(
            DateTimeFormatKind? formatKind = null,
            CultureKind? cultureKind = null,
            bool? localize = null,
            StandardTimeZone? localTimeZone = null)
        {
            if ((formatKind != null) && (formatKind == DateTimeFormatKind.Unknown))
            {
                throw new ArgumentOutOfRangeException(nameof(formatKind), Invariant($"{nameof(formatKind)} is {nameof(DateTimeFormatKind.Unknown)}."));
            }

            if ((cultureKind != null) && (cultureKind == OBeautifulCode.Type.CultureKind.Unknown))
            {
                throw new ArgumentOutOfRangeException(nameof(cultureKind), Invariant($"{nameof(cultureKind)} is {nameof(OBeautifulCode.Type.CultureKind.Unknown)}."));
            }

            if ((localize == false) && (localTimeZone != null))
            {
                throw new ArgumentException(Invariant($"{nameof(localize)} is false, but {nameof(localTimeZone)} is not null."));
            }

            this.FormatKind = formatKind;
            this.CultureKind = cultureKind;
            this.Localize = localize;
            this.LocalTimeZone = localTimeZone;
        }

        /// <summary>
        /// Gets the kind of formatting to apply.
        /// </summary>
        public DateTimeFormatKind? FormatKind { get; private set; }

        /// <summary>
        /// Gets the kind of culture to apply when formatting.
        /// </summary>
        public CultureKind? CultureKind { get; private set; }

        /// <summary>
        /// Gets a value value that determines whether to localize a <see cref="DateTimeKind.Utc"/> <see cref="DateTime"/> before generating it's string representation.
        /// </summary>
        public bool? Localize { get; private set; }

        /// <summary>
        /// Gets the time zone to use when localizing a <see cref="DateTimeKind.Utc"/> <see cref="DateTime"/> before generating it's string representation.
        /// </summary>
        public StandardTimeZone? LocalTimeZone { get; private set; }
    }
}
