// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableRows.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// The data rows (the rows below the header row) in a tree table.
    /// </summary>
    public partial class TableRows : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableRows"/> class.
        /// </summary>
        /// <param name="headerRows">OPTIONAL header rows.  DEFAULT is no headers rows.</param>
        /// <param name="dataRows">OPTIONAL data rows.  DEFAULT is no data rows.</param>
        /// <param name="rowsFormat">OPTIONAL format to apply to all rows, individually.  DEFAULT is to leave the format unchanged.</param>
        public TableRows(
            HeaderRows headerRows = null,
            DataRows dataRows = null,
            RowFormat rowsFormat = null)
        {
            this.HeaderRows = headerRows;
            this.DataRows = dataRows;
            this.RowsFormat = rowsFormat;
        }

        /// <summary>
        /// Gets the header rows.
        /// </summary>
        public HeaderRows HeaderRows { get; private set; }

        /// <summary>
        /// Gets the data rows.
        /// </summary>
        public DataRows DataRows { get; private set; }

        /// <summary>
        /// Gets the format to apply to all rows, individually.
        /// </summary>
        public RowFormat RowsFormat { get; private set; }
    }
}