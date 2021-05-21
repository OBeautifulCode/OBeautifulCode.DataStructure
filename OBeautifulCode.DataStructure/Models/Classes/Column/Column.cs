// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Column.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// A column in a tree table.
    /// </summary>
    public partial class Column : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="id">OPTIONAL id of the column.  DEFAULT is non-identified column.</param>
        /// <param name="format">OPTIONAL format to apply to the whole column.  DEFAULT is to leave the format unchanged.</param>
        public Column(
            string id = null,
            ColumnFormat format = null)
        {
            this.Id = id;
            this.Format = format;
        }

        /// <summary>
        /// Gets the id of the column.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the format to apply to the whole column.
        /// </summary>
        public ColumnFormat Format { get; private set; }
    }
}