// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellInputAppliedEventBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Input has been applied to an <see cref="IInputCell"/>.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class CellInputAppliedEventBase : CellInputEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellInputAppliedEventBase"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">OPTIONAL details about the input that was applied to the cell.  DEFAULT is to omit any details.</param>
        protected CellInputAppliedEventBase(
            DateTime timestampUtc,
            string details = null)
            : base(timestampUtc, details)
        {
        }

        /// <summary>
        /// Gets the inputted value as an object.
        /// </summary>
        /// <returns>
        /// The inputted value.
        /// </returns>
        public abstract object GetObjectValue();

        /// <summary>
        /// Gets the declared type of the input value.
        /// </summary>
        /// <returns>
        /// The declared type of the input value.
        /// </returns>
        public abstract Type GetValueType();
    }
}