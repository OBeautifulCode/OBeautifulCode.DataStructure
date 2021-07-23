// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputAppliedToCellEvent{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// Input has been applied to an <see cref="IInputCell{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of inputted value.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class InputAppliedToCellEvent<TValue> : EventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputAppliedToCellEvent{TValue}"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="value">The inputted value.</param>
        public InputAppliedToCellEvent(
            DateTime timestampUtc,
            TValue value)
            : base(timestampUtc)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the inputted value.
        /// </summary>
        public TValue Value { get; private set; }
    }
}