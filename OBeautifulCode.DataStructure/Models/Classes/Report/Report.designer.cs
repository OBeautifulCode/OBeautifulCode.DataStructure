﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.195.0)
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using global::System;
    using global::System.CodeDom.Compiler;
    using global::System.Collections.Concurrent;
    using global::System.Collections.Generic;
    using global::System.Collections.ObjectModel;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Globalization;
    using global::System.Linq;

    using global::OBeautifulCode.Cloning.Recipes;
    using global::OBeautifulCode.Equality.Recipes;
    using global::OBeautifulCode.Type;
    using global::OBeautifulCode.Type.Recipes;

    using static global::System.FormattableString;

    [Serializable]
    public partial class Report : IModel<Report>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="Report"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(Report left, Report right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = left.Equals(right);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="Report"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(Report left, Report right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(Report other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.Id.IsEqualTo(other.Id, StringComparer.Ordinal)
                      && this.Sections.IsEqualTo(other.Sections)
                      && this.Title.IsEqualTo(other.Title, StringComparer.Ordinal)
                      && this.TimestampUtc.IsEqualTo(other.TimestampUtc)
                      && this.DownloadKinds.IsEqualTo(other.DownloadKinds)
                      && this.AdditionalInfo.IsEqualTo(other.AdditionalInfo)
                      && this.Format.IsEqualTo(other.Format);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as Report);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.Id)
            .Hash(this.Sections)
            .Hash(this.Title)
            .Hash(this.TimestampUtc)
            .Hash(this.DownloadKinds)
            .Hash(this.AdditionalInfo)
            .Hash(this.Format)
            .Value;

        /// <inheritdoc />
        public object Clone() => this.DeepClone();

        /// <inheritdoc />
        public Report DeepClone()
        {
            var result = new Report(
                                 this.Id?.DeepClone(),
                                 this.Sections?.DeepClone(),
                                 this.Title?.DeepClone(),
                                 this.TimestampUtc?.DeepClone(),
                                 this.DownloadKinds?.DeepClone(),
                                 this.AdditionalInfo?.DeepClone(),
                                 this.Format?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Id" />.
        /// </summary>
        /// <param name="id">The new <see cref="Id" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="Report" /> using the specified <paramref name="id" /> for <see cref="Id" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public Report DeepCloneWithId(string id)
        {
            var result = new Report(
                                 id,
                                 this.Sections?.DeepClone(),
                                 this.Title?.DeepClone(),
                                 this.TimestampUtc?.DeepClone(),
                                 this.DownloadKinds?.DeepClone(),
                                 this.AdditionalInfo?.DeepClone(),
                                 this.Format?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Sections" />.
        /// </summary>
        /// <param name="sections">The new <see cref="Sections" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="Report" /> using the specified <paramref name="sections" /> for <see cref="Sections" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public Report DeepCloneWithSections(IReadOnlyCollection<Section> sections)
        {
            var result = new Report(
                                 this.Id?.DeepClone(),
                                 sections,
                                 this.Title?.DeepClone(),
                                 this.TimestampUtc?.DeepClone(),
                                 this.DownloadKinds?.DeepClone(),
                                 this.AdditionalInfo?.DeepClone(),
                                 this.Format?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Title" />.
        /// </summary>
        /// <param name="title">The new <see cref="Title" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="Report" /> using the specified <paramref name="title" /> for <see cref="Title" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public Report DeepCloneWithTitle(string title)
        {
            var result = new Report(
                                 this.Id?.DeepClone(),
                                 this.Sections?.DeepClone(),
                                 title,
                                 this.TimestampUtc?.DeepClone(),
                                 this.DownloadKinds?.DeepClone(),
                                 this.AdditionalInfo?.DeepClone(),
                                 this.Format?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="TimestampUtc" />.
        /// </summary>
        /// <param name="timestampUtc">The new <see cref="TimestampUtc" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="Report" /> using the specified <paramref name="timestampUtc" /> for <see cref="TimestampUtc" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public Report DeepCloneWithTimestampUtc(DateTime? timestampUtc)
        {
            var result = new Report(
                                 this.Id?.DeepClone(),
                                 this.Sections?.DeepClone(),
                                 this.Title?.DeepClone(),
                                 timestampUtc,
                                 this.DownloadKinds?.DeepClone(),
                                 this.AdditionalInfo?.DeepClone(),
                                 this.Format?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="DownloadKinds" />.
        /// </summary>
        /// <param name="downloadKinds">The new <see cref="DownloadKinds" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="Report" /> using the specified <paramref name="downloadKinds" /> for <see cref="DownloadKinds" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public Report DeepCloneWithDownloadKinds(IReadOnlyList<DownloadKind> downloadKinds)
        {
            var result = new Report(
                                 this.Id?.DeepClone(),
                                 this.Sections?.DeepClone(),
                                 this.Title?.DeepClone(),
                                 this.TimestampUtc?.DeepClone(),
                                 downloadKinds,
                                 this.AdditionalInfo?.DeepClone(),
                                 this.Format?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="AdditionalInfo" />.
        /// </summary>
        /// <param name="additionalInfo">The new <see cref="AdditionalInfo" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="Report" /> using the specified <paramref name="additionalInfo" /> for <see cref="AdditionalInfo" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public Report DeepCloneWithAdditionalInfo(AdditionalReportInfo additionalInfo)
        {
            var result = new Report(
                                 this.Id?.DeepClone(),
                                 this.Sections?.DeepClone(),
                                 this.Title?.DeepClone(),
                                 this.TimestampUtc?.DeepClone(),
                                 this.DownloadKinds?.DeepClone(),
                                 additionalInfo,
                                 this.Format?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Format" />.
        /// </summary>
        /// <param name="format">The new <see cref="Format" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="Report" /> using the specified <paramref name="format" /> for <see cref="Format" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public Report DeepCloneWithFormat(ReportFormat format)
        {
            var result = new Report(
                                 this.Id?.DeepClone(),
                                 this.Sections?.DeepClone(),
                                 this.Title?.DeepClone(),
                                 this.TimestampUtc?.DeepClone(),
                                 this.DownloadKinds?.DeepClone(),
                                 this.AdditionalInfo?.DeepClone(),
                                 format);

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"OBeautifulCode.DataStructure.Report: Id = {this.Id?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, Sections = {this.Sections?.ToString() ?? "<null>"}, Title = {this.Title?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, TimestampUtc = {this.TimestampUtc?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, DownloadKinds = {this.DownloadKinds?.ToString() ?? "<null>"}, AdditionalInfo = {this.AdditionalInfo?.ToString() ?? "<null>"}, Format = {this.Format?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}