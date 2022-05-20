// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataReaderToTreeTableConversionContext.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Database
{
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// Context object for converting an <see cref="IDataReader"/> to a <see cref="TreeTable"/>.
    /// </summary>
    public class DataReaderToTreeTableConversionContext
    {
        /// <summary>
        /// Gets or sets a value indicating whether to convert values to
        /// the preferred string representation of those values using the invariant culture
        /// (versus just maintaining the value's data type).
        /// </summary>
        public bool ConvertValuesToPreferredInvariantString { get; set; }

        /// <summary>
        /// Gets or sets a map of column name to <see cref="CellFormatForValue"/>,
        /// (e.g. for column "Status", apply a background color of red when value == "fail").
        /// </summary>
        public IReadOnlyDictionary<string, IReadOnlyCollection<CellFormatForValue>> ColumnNameToCellFormatForValueMap { get; set; }

        /// <summary>
        /// Gets or sets the format to apply to the whole table.
        /// See <see cref="TreeTable.Format"/>.
        /// </summary>
        public TableFormat TableFormat { get; set; }

        /// <summary>
        /// Gets or sets format to apply to all columns in the table, individually.
        /// See <see cref="TableColumns.ColumnsFormat"/>.
        /// </summary>
        public ColumnFormat ColumnsFormat { get; set; }

        /// <summary>
        /// Gets or sets format to apply to all rows in the table, individually.
        /// See <see cref="TableRows.RowsFormat"/>.
        /// </summary>
        public RowFormat RowsFormat { get; set; }

        /// <summary>
        /// Gets or sets format to apply to the header row.
        /// See <see cref="RowBase.Format"/>.
        /// </summary>
        public RowFormat HeaderRowFormat { get; set; }

        /// <summary>
        /// Gets or sets format to apply to all data rows.
        /// See <see cref="DataRows.Format"/>.
        /// </summary>
        public DataRowsFormat DataRowsFormat { get; set; }
    }
}