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
        /// <param name="options">OPTIONAL formatting options to apply to the report.  DEFAULT is to not apply any of the formatting options.</param>
        /// <param name="renderMode">OPTIONAL value that indicates the mode of report rendering.  DEFAULT is no specified rendering mode.</param>
        public ReportFormat(
            bool? displayTimestamp = null,
            DateTimeFormat timestampFormat = null,
            ReportFormatOptions? options = null,
            ReportRenderMode? renderMode = null)
        {
            if ((displayTimestamp == false) && (timestampFormat != null))
            {
                throw new ArgumentException(Invariant($"{nameof(displayTimestamp)} is false, but {nameof(timestampFormat)} is not null."));
            }

            if ((renderMode != null) && (renderMode == ReportRenderMode.Unknown))
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(renderMode)} is {nameof(ReportRenderMode.Unknown)}."));
            }

            this.DisplayTimestamp = displayTimestamp;
            this.TimestampFormat = timestampFormat;
            this.Options = options;
            this.RenderMode = renderMode;
        }

        /// <summary>
        /// Gets a value that specifies whether to display the report's timestamp.
        /// </summary>
        /// <remarks>
        /// We purposefully did NOT put this in <see cref="Options"/>.
        /// Unlike other "options" in this project, this setting is not something you would clearly
        /// do or not do (i.e. display the timestamp or hide the timestamp).  We would need an option
        /// for both.  Also, the option is not isolated, given <see cref="TimestampFormat"/>.
        /// </remarks>
        public bool? DisplayTimestamp { get; private set; }

        /// <summary>
        /// Gets the format to apply to the timestamp when displaying it.
        /// </summary>
        public DateTimeFormat TimestampFormat { get; private set; }

        /// <summary>
        /// Gets the formatting options to apply to the report.
        /// </summary>
        public ReportFormatOptions? Options { get; private set; }

        /// <summary>
        /// Gets a value that indicates the mode of report rendering.
        /// </summary>
        public ReportRenderMode? RenderMode { get; private set; }
    }
}
