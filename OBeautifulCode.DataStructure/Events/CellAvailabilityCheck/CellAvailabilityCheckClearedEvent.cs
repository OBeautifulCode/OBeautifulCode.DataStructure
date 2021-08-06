﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellAvailabilityCheckClearedEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;

    using OBeautifulCode.Type;

    /// <summary>
    /// The availability check on a cell was cleared-out.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class CellAvailabilityCheckClearedEvent : CellAvailabilityCheckEventBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellAvailabilityCheckClearedEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="details">Details about the cleared-out availability check.</param>
        public CellAvailabilityCheckClearedEvent(
            DateTime timestampUtc,
            string details)
            : base(timestampUtc, details)
        {
        }
    }
}