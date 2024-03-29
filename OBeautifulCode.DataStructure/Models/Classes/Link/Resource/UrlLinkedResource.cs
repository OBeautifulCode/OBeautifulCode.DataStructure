﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrlLinkedResource.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// A resource that is fetched from a server via a URL.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class UrlLinkedResource : LinkedResourceBase, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlLinkedResource"/> class.
        /// </summary>
        /// <param name="url">The url of the resource.</param>
        /// <param name="resourceKind">The kind of resource.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = ObcSuppressBecause.CA1054_UriParametersShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public UrlLinkedResource(
            string url,
            UrlLinkedResourceKind resourceKind)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(Invariant($"{nameof(url)} is white space."));
            }

            if (resourceKind == UrlLinkedResourceKind.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(resourceKind)} is {nameof(UrlLinkedResourceKind.Unknown)}."));
            }

            this.Url = url;
            this.ResourceKind = resourceKind;
        }

        /// <summary>
        /// Gets the url of the resource.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = ObcSuppressBecause.CA1056_UriPropertiesShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public string Url { get; private set; }

        /// <summary>
        /// Gets the kind of resource.
        /// </summary>
        public UrlLinkedResourceKind ResourceKind { get; private set; }
    }
}
