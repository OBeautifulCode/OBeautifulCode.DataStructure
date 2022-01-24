// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Delegates.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Excel
{
    using System.Diagnostics.CodeAnalysis;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Excel;

    /// <summary>
    /// Builds the Excel document properties to use.
    /// </summary>
    /// <param name="title">The title of report.</param>
    /// <returns>
    /// The Excel document properties to use.
    /// </returns>
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = ObcSuppressBecause.CA1711_IdentifiersShouldNotHaveIncorrectSuffix_PreferToUseDelegateForNonEventHandlingNonCallbackDelegates)]
    public delegate DocumentProperties BuildDocumentPropertiesDelegate(string title);
}
