// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellOpExecutionDeemedNotApplicableEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The execution of <see cref="IOperationOutputCell{TResult}"/>'s <see cref="IOperationOutputCell{TResult}.Operation"/>,
    /// deemed itself not applicable.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellOpExecutionDeemedNotApplicableEvent : CellOpExecutionEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellOpExecutionDeemedNotApplicableEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the aborted execution.</param>
        public CellOpExecutionDeemedNotApplicableEvent(
            DateTime timestampUtc,
            string details)
            : base(timestampUtc, details)
        {
        }
    }
}