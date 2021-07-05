// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Section.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A section of a <see cref="Report"/>.
    /// </summary>
    public partial class Section : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Section"/> class.
        /// </summary>
        /// <param name="id">The section's unique identifier.</param>
        /// <param name="treeTable">The tree table.</param>
        /// <param name="title">OPTIONAL title of the section.  DEFAULT is a section with no title.</param>
        /// <param name="format">OPTIONAL format to apply to the section.  DEFAULT is to leave the format unchanged.</param>
        public Section(
            string id,
            TreeTable treeTable,
            string title = null,
            SectionFormat format = null)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(Invariant($"{nameof(id)} is white space."));
            }

            if (treeTable == null)
            {
                throw new ArgumentNullException(nameof(treeTable));
            }

            this.Id = id;
            this.TreeTable = treeTable;
            this.Title = title;
            this.Format = format;
        }

        /// <summary>
        /// Gets the section's unique identifier.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the tree table.
        /// </summary>
        public TreeTable TreeTable { get; private set; }

        /// <summary>
        /// Gets the title of the section.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the format to apply to the section.
        /// </summary>
        public SectionFormat Format { get; private set; }
    }
}