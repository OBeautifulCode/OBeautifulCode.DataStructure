// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SlotSelectionStrategy.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies how to select a slot within a <see cref="ISlottedCell"/>.
    /// </summary>
    public enum SlotSelectionStrategy
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Throw if the slot identified is not specified.
        /// </summary>
        ThrowIfSlotIdNotSpecified,

        /// <summary>
        /// Use the default slot.
        /// </summary>
        DefaultSlot,
    }
}