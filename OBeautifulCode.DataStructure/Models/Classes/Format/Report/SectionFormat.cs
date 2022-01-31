// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SectionFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// The format to apply to a <see cref="Section"/>.
    /// </summary>
    public partial class SectionFormat : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionFormat"/> class.
        /// </summary>
        /// <param name="options">OPTIONAL formatting options to apply to the report.  DEFAULT is to not apply any of the formatting options.</param>
        public SectionFormat(
            SectionFormatOptions? options = null)
        {
            this.Options = options;
        }

        /// <summary>
        /// Gets the formatting options to apply to the section.
        /// </summary>
        public SectionFormatOptions? Options { get; private set; }
    }
}
