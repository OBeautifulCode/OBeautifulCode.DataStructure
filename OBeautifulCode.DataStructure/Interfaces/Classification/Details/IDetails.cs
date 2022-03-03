// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDetails.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System.Diagnostics.CodeAnalysis;
    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Additional information attached to a container such as a <see cref="Report"/> or a <see cref="Section"/>.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = ObcSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface IDetails
    {
    }
}