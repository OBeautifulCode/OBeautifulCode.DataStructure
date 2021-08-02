﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Report.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// An organized collection of <see cref="TreeTable"/> objects.
    /// </summary>
    public partial class Report : IModelViaCodeGen
    {
        private IReadOnlyDictionary<string, Section> sectionIdToSectionMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="Report"/> class.
        /// </summary>
        /// <param name="id">The report's unique identifier.</param>
        /// <param name="sections">The sections of the report.</param>
        /// <param name="title">OPTIONAL title of the report.  DEFAULT is a report with no title.</param>
        /// <param name="format">OPTIONAL format to apply to the report.  DEFAULT is to leave the format unchanged.</param>
        public Report(
            string id,
            IReadOnlyCollection<Section> sections,
            string title = null,
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

            this.sectionIdToSectionMap = sections.ToDictionary(_ => _.Id, _ => _);

            this.Id = id;
            this.Sections = sections;
            this.Title = title;
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
        /// Gets the format to apply to the report.
        /// </summary>
        public ReportFormat Format { get; private set; }

        /// <summary>
        /// Gets a map of section id to the corresponding section.
        /// </summary>
        /// <returns>
        /// A map of section id to the corresponding section.
        /// </returns>
        public IReadOnlyDictionary<string, Section> GetSectionIdToSectionMap() => this.sectionIdToSectionMap;
    }
}