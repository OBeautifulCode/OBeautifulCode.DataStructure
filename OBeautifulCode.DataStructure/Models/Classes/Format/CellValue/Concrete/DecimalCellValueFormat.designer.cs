﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.165.0)
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
    public partial class DecimalCellValueFormat : IModel<DecimalCellValueFormat>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="DecimalCellValueFormat"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(DecimalCellValueFormat left, DecimalCellValueFormat right)
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
        /// Determines whether two objects of type <see cref="DecimalCellValueFormat"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(DecimalCellValueFormat left, DecimalCellValueFormat right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(DecimalCellValueFormat other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.MissingValueText.IsEqualTo(other.MissingValueText, StringComparer.Ordinal)
                      && this.NumberOfDecimalPlaces.IsEqualTo(other.NumberOfDecimalPlaces)
                      && this.DecimalSeparator.IsEqualTo(other.DecimalSeparator)
                      && this.DigitGroupKind.IsEqualTo(other.DigitGroupKind)
                      && this.DigitGroupSeparator.IsEqualTo(other.DigitGroupSeparator)
                      && this.NegativeNumberDisplayKind.IsEqualTo(other.NegativeNumberDisplayKind)
                      && this.Prefix.IsEqualTo(other.Prefix, StringComparer.Ordinal)
                      && this.Suffix.IsEqualTo(other.Suffix, StringComparer.Ordinal);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as DecimalCellValueFormat);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.MissingValueText)
            .Hash(this.NumberOfDecimalPlaces)
            .Hash(this.DecimalSeparator)
            .Hash(this.DigitGroupKind)
            .Hash(this.DigitGroupSeparator)
            .Hash(this.NegativeNumberDisplayKind)
            .Hash(this.Prefix)
            .Hash(this.Suffix)
            .Value;

        /// <inheritdoc />
        public new DecimalCellValueFormat DeepClone() => (DecimalCellValueFormat)this.DeepCloneInternal();

        /// <inheritdoc />
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
        public override StandardCellValueFormatBase<decimal> DeepCloneWithMissingValueText(string missingValueText)
        {
            var result = new DecimalCellValueFormat(
                                 this.Prefix?.DeepClone(),
                                 this.Suffix?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.DecimalSeparator?.DeepClone(),
                                 this.DigitGroupKind?.DeepClone(),
                                 this.DigitGroupSeparator?.DeepClone(),
                                 this.NegativeNumberDisplayKind?.DeepClone(),
                                 missingValueText);

            return result;
        }

        /// <inheritdoc />
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
        public override NumberCellFormatBase<decimal> DeepCloneWithNumberOfDecimalPlaces(int? numberOfDecimalPlaces)
        {
            var result = new DecimalCellValueFormat(
                                 this.Prefix?.DeepClone(),
                                 this.Suffix?.DeepClone(),
                                 numberOfDecimalPlaces,
                                 this.DecimalSeparator?.DeepClone(),
                                 this.DigitGroupKind?.DeepClone(),
                                 this.DigitGroupSeparator?.DeepClone(),
                                 this.NegativeNumberDisplayKind?.DeepClone(),
                                 this.MissingValueText?.DeepClone());

            return result;
        }

        /// <inheritdoc />
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
        public override NumberCellFormatBase<decimal> DeepCloneWithDecimalSeparator(char? decimalSeparator)
        {
            var result = new DecimalCellValueFormat(
                                 this.Prefix?.DeepClone(),
                                 this.Suffix?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 decimalSeparator,
                                 this.DigitGroupKind?.DeepClone(),
                                 this.DigitGroupSeparator?.DeepClone(),
                                 this.NegativeNumberDisplayKind?.DeepClone(),
                                 this.MissingValueText?.DeepClone());

            return result;
        }

        /// <inheritdoc />
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
        public override NumberCellFormatBase<decimal> DeepCloneWithDigitGroupKind(NumberFormatDigitGroupKind? digitGroupKind)
        {
            var result = new DecimalCellValueFormat(
                                 this.Prefix?.DeepClone(),
                                 this.Suffix?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.DecimalSeparator?.DeepClone(),
                                 digitGroupKind,
                                 this.DigitGroupSeparator?.DeepClone(),
                                 this.NegativeNumberDisplayKind?.DeepClone(),
                                 this.MissingValueText?.DeepClone());

            return result;
        }

        /// <inheritdoc />
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
        public override NumberCellFormatBase<decimal> DeepCloneWithDigitGroupSeparator(char? digitGroupSeparator)
        {
            var result = new DecimalCellValueFormat(
                                 this.Prefix?.DeepClone(),
                                 this.Suffix?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.DecimalSeparator?.DeepClone(),
                                 this.DigitGroupKind?.DeepClone(),
                                 digitGroupSeparator,
                                 this.NegativeNumberDisplayKind?.DeepClone(),
                                 this.MissingValueText?.DeepClone());

            return result;
        }

        /// <inheritdoc />
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
        public override NumberCellFormatBase<decimal> DeepCloneWithNegativeNumberDisplayKind(NumberFormatNegativeDisplayKind? negativeNumberDisplayKind)
        {
            var result = new DecimalCellValueFormat(
                                 this.Prefix?.DeepClone(),
                                 this.Suffix?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.DecimalSeparator?.DeepClone(),
                                 this.DigitGroupKind?.DeepClone(),
                                 this.DigitGroupSeparator?.DeepClone(),
                                 negativeNumberDisplayKind,
                                 this.MissingValueText?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Prefix" />.
        /// </summary>
        /// <param name="prefix">The new <see cref="Prefix" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="DecimalCellValueFormat" /> using the specified <paramref name="prefix" /> for <see cref="Prefix" /> and a deep clone of every other property.</returns>
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
        public DecimalCellValueFormat DeepCloneWithPrefix(string prefix)
        {
            var result = new DecimalCellValueFormat(
                                 prefix,
                                 this.Suffix?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.DecimalSeparator?.DeepClone(),
                                 this.DigitGroupKind?.DeepClone(),
                                 this.DigitGroupSeparator?.DeepClone(),
                                 this.NegativeNumberDisplayKind?.DeepClone(),
                                 this.MissingValueText?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Suffix" />.
        /// </summary>
        /// <param name="suffix">The new <see cref="Suffix" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="DecimalCellValueFormat" /> using the specified <paramref name="suffix" /> for <see cref="Suffix" /> and a deep clone of every other property.</returns>
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
        public DecimalCellValueFormat DeepCloneWithSuffix(string suffix)
        {
            var result = new DecimalCellValueFormat(
                                 this.Prefix?.DeepClone(),
                                 suffix,
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.DecimalSeparator?.DeepClone(),
                                 this.DigitGroupKind?.DeepClone(),
                                 this.DigitGroupSeparator?.DeepClone(),
                                 this.NegativeNumberDisplayKind?.DeepClone(),
                                 this.MissingValueText?.DeepClone());

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override CellValueFormatBase<decimal> DeepCloneInternal()
        {
            var result = new DecimalCellValueFormat(
                                 this.Prefix?.DeepClone(),
                                 this.Suffix?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.DecimalSeparator?.DeepClone(),
                                 this.DigitGroupKind?.DeepClone(),
                                 this.DigitGroupSeparator?.DeepClone(),
                                 this.NegativeNumberDisplayKind?.DeepClone(),
                                 this.MissingValueText?.DeepClone());

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"OBeautifulCode.DataStructure.DecimalCellValueFormat: MissingValueText = {this.MissingValueText?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, NumberOfDecimalPlaces = {this.NumberOfDecimalPlaces?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, DecimalSeparator = {this.DecimalSeparator?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, DigitGroupKind = {this.DigitGroupKind?.ToString() ?? "<null>"}, DigitGroupSeparator = {this.DigitGroupSeparator?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, NegativeNumberDisplayKind = {this.NegativeNumberDisplayKind?.ToString() ?? "<null>"}, Prefix = {this.Prefix?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, Suffix = {this.Suffix?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}.");

            return result;
        }
    }
}