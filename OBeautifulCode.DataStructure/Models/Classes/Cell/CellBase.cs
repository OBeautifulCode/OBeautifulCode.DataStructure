// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Base implementation of <see cref="ICell"/>.
    /// </summary>
    public abstract partial class CellBase : ICell, IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellBase"/> class.
        /// </summary>
        /// <param name="id">The cell's unique identifier.</param>
        /// <param name="columnsSpanned">The number of columns spanned or null if none (cell occupies a single column).</param>
        /// <param name="details">Details about the cell.</param>
        protected CellBase(
            string id,
            int? columnsSpanned,
            string details)
        {
            if ((columnsSpanned != null) && (columnsSpanned < 1))
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(columnsSpanned)} is {columnsSpanned}; must be null or >= 1."));
            }

            this.Id = id;
            this.ColumnsSpanned = columnsSpanned;
            this.Details = details;
        }

        /// <inheritdoc />
        public string Id { get; private set; }

        /// <inheritdoc />
        public int? ColumnsSpanned { get; private set; }

        /// <inheritdoc />
        public string Details { get; private set; }

        /// <inheritdoc />
        public abstract bool IsConstCell();

        /// <inheritdoc />
        public abstract bool IsInputCell();

        /// <inheritdoc />
        public abstract bool IsOperationCell();

        /// <inheritdoc />
        public abstract Type GetValueTypeOrNull();
    }
}