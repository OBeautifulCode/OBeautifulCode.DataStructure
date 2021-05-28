// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BorderWeight.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Specifies the weight of a border.
    /// </summary>
    public enum BorderWeight
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// An extra-light border.
        /// </summary>
        ExtraLight,

        /// <summary>
        /// A light border.
        /// </summary>
        Light,

        /// <summary>
        /// A normal border.
        /// </summary>
        Normal,

        /// <summary>
        /// A bold border.
        /// </summary>
        Bold,

        /// <summary>
        /// An extra-bold border.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ExtraBold", Justification = ObcSuppressBecause.CA1702_CompoundWordsShouldBeCasedCorrectly_AnalyzerIsIncorrectlyDetectingCompoundWords)]
        ExtraBold,
    }
}