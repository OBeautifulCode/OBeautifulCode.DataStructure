// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellInputClearedEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Input has been cleared from an <see cref="IInputCell{TValue}"/>.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellInputClearedEvent : CellInputEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellInputClearedEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">OPTIONAL details about input that was cleared from the cell.  DEFAULT is to omit any details.</param>
        public CellInputClearedEvent(
            DateTime timestampUtc,
            string details)
            : base(timestampUtc, details)
        {
        }
    }
}