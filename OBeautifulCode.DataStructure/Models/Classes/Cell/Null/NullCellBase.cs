// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullCellBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    /// <summary>
    /// Base implementation of <see cref="INullCell"/>.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class NullCellBase : NotSlottedCellBase, INullCell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullCellBase"/> class.
        /// </summary>
        /// <param name="id">Unique identifier of the cell.</param>
        /// <param name="columnsSpanned">Number of columns spanned or null if none (cell occupies a single column).</param>
        /// <param name="details">Details about the cell.</param>
        /// <param name="validation">The validation to perform.</param>
        /// <param name="validationEvents">The events that record the validation of this cell.</param>
        protected NullCellBase(
            string id,
            int? columnsSpanned,
            string details,
            Validation validation,
            IReadOnlyList<CellValidationEventBase> validationEvents)
            : base(id, columnsSpanned, details, validation, validationEvents)
        {
        }
    }
}