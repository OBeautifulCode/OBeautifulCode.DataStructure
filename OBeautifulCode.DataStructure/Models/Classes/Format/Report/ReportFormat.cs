// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// The format to apply to a <see cref="Report"/>.
    /// </summary>
    public partial class ReportFormat : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportFormat"/> class.
        /// </summary>
        /// <param name="displayTimestamp">OPTIONAL value that specifies whether to display the report's timestamp.  DEFAULT is to let the consumer decide (e.g. always display the timestamp when it's not null).</param>
        /// <param name="timestampFormat">OPTIONAL format to apply to the timestamp when displaying it.  DEFAULT is to let the consumer decide (e.g. when displaying the timestamp, use a pre-specified/standard format).</param>
        public ReportFormat(
            bool? displayTimestamp = null,
            DateTimeFormat timestampFormat = null)
        {
            if ((displayTimestamp == false) && (timestampFormat != null))
            {
                throw new ArgumentException(Invariant($"{nameof(displayTimestamp)} is false, but {nameof(timestampFormat)} is not null."));
            }

            this.DisplayTimestamp = displayTimestamp;
            this.TimestampFormat = timestampFormat;
        }

        /// <summary>
        /// Gets a value that specifies whether to display the report's timestamp.
        /// </summary>
        public bool? DisplayTimestamp { get; private set; }

        /// <summary>
        /// Gets the format to apply to the timestamp when displaying it.
        /// </summary>
        public DateTimeFormat TimestampFormat { get; private set; }
    }
}
