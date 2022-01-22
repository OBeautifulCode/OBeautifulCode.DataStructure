// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeCellValueFormat.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.Type;

    /// <summary>
    /// A cell value format for a <see cref="DateTime"/> value.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class DateTimeCellValueFormat : StandardCellValueFormatBase<DateTime>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeCellValueFormat"/> class.
        /// </summary>
        /// <param name="format">The format to apply.</param>
        /// <param name="missingValueText">OPTIONAL text to use when the cell is missing a value.  DEFAULT is to leave this text unspecified.</param>
        public DateTimeCellValueFormat(
            DateTimeFormat format,
            string missingValueText = null)
            : base(missingValueText)
        {
            if (format == null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            this.Format = format;
        }

        /// <summary>
        /// Gets the format to apply.
        /// </summary>
        public DateTimeFormat Format { get; private set; }
    }
}
