// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Report.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// An organized collection of <see cref="TreeTable"/> objects.
    /// </summary>
    public partial class Report : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Report"/> class.
        /// </summary>
        /// <param name="id">The report's unique identifier.</param>
        /// <param name="sections">The sections of the report.</param>
        /// <param name="title">OPTIONAL title of the report.  DEFAULT is a report with no title.</param>
        /// <param name="timestampUtc">OPTIONAL timestamp of the report, in UTC.  DEFAULT is a report that is not timestamped.</param>
        /// <param name="additionalInfo">OPTIONAL additional information related to the report.  DEFAULT no additional information.</param>
        /// <param name="format">OPTIONAL format to apply to the report.  DEFAULT is to leave the format unchanged.</param>
        public Report(
            string id,
            IReadOnlyCollection<Section> sections,
            string title = null,
            DateTime? timestampUtc = null,
            AdditionalReportInfo additionalInfo = null,
            ReportFormat format = null)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(Invariant($"{nameof(id)} is white space."));
            }

            if (sections == null)
            {
                throw new ArgumentNullException(nameof(sections));
            }

            if (!sections.Any())
            {
                throw new ArgumentException(Invariant($"{nameof(sections)} is an empty enumerable."));
            }

            if (sections.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(sections)} contains at least one null element."));
            }

            if (sections.Select(_ => _.Id).Distinct().Count() != sections.Count)
            {
                throw new ArgumentException(Invariant($"{nameof(sections)} contains two or more elements with the same {nameof(Section.Id)}."));
            }

            var allCells = sections.SelectMany(_ => _.TreeTable.GetAllCells()).ToList();

            var distinctCells = allCells.Distinct(new ReferenceEqualityComparer<ICell>()).ToList();

            if (allCells.Count != distinctCells.Count)
            {
                throw new ArgumentException(Invariant($"One or more {nameof(ICell)} objects are used multiple times in the report."));
            }

            if ((timestampUtc != null) && (((DateTime)timestampUtc).Kind != DateTimeKind.Utc))
            {
                throw new ArgumentException(Invariant($"{nameof(timestampUtc)} is not in UTC."));
            }

            this.Id = id;
            this.Sections = sections;
            this.Title = title;
            this.TimestampUtc = timestampUtc;
            this.AdditionalInfo = additionalInfo;
            this.Format = format;
        }

        /// <summary>
        /// Gets the report's unique identifier.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the sections of the report.
        /// </summary>
        public IReadOnlyCollection<Section> Sections { get; private set; }

        /// <summary>
        /// Gets the title of the report.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the timestamp of the report, in UTC.
        /// </summary>
        public DateTime? TimestampUtc { get; private set; }

        /// <summary>
        /// Gets additional information related to the report.
        /// </summary>
        public AdditionalReportInfo AdditionalInfo { get; private set; }

        /// <summary>
        /// Gets the format to apply to the report.
        /// </summary>
        public ReportFormat Format { get; private set; }
    }
}