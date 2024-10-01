﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.189.0)
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
    public partial class PercentCellValueFormat<TValue> : IModel<PercentCellValueFormat<TValue>>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="PercentCellValueFormat{TValue}"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(PercentCellValueFormat<TValue> left, PercentCellValueFormat<TValue> right)
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
        /// Determines whether two objects of type <see cref="PercentCellValueFormat{TValue}"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(PercentCellValueFormat<TValue> left, PercentCellValueFormat<TValue> right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(PercentCellValueFormat<TValue> other)
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
                      && this.RoundingStrategy.IsEqualTo(other.RoundingStrategy)
                      && this.DecimalSeparator.IsEqualTo(other.DecimalSeparator)
                      && this.DigitGroupKind.IsEqualTo(other.DigitGroupKind)
                      && this.DigitGroupSeparator.IsEqualTo(other.DigitGroupSeparator)
                      && this.NegativeNumberDisplayKind.IsEqualTo(other.NegativeNumberDisplayKind)
                      && this.PercentDisplayKind.IsEqualTo(other.PercentDisplayKind);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as PercentCellValueFormat<TValue>);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.MissingValueText)
            .Hash(this.NumberOfDecimalPlaces)
            .Hash(this.RoundingStrategy)
            .Hash(this.DecimalSeparator)
            .Hash(this.DigitGroupKind)
            .Hash(this.DigitGroupSeparator)
            .Hash(this.NegativeNumberDisplayKind)
            .Hash(this.PercentDisplayKind)
            .Value;

        /// <inheritdoc />
        public new PercentCellValueFormat<TValue> DeepClone() => (PercentCellValueFormat<TValue>)this.DeepCloneInternal();

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
        public override StandardCellValueFormatBase<TValue> DeepCloneWithMissingValueText(string missingValueText)
        {
            var result = new PercentCellValueFormat<TValue>(
                                 this.PercentDisplayKind?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.RoundingStrategy?.DeepClone(),
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
        public override NumberCellFormatBase<TValue> DeepCloneWithNumberOfDecimalPlaces(int? numberOfDecimalPlaces)
        {
            var result = new PercentCellValueFormat<TValue>(
                                 this.PercentDisplayKind?.DeepClone(),
                                 numberOfDecimalPlaces,
                                 this.RoundingStrategy?.DeepClone(),
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
        public override NumberCellFormatBase<TValue> DeepCloneWithRoundingStrategy(MidpointRounding? roundingStrategy)
        {
            var result = new PercentCellValueFormat<TValue>(
                                 this.PercentDisplayKind?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 roundingStrategy,
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
        public override NumberCellFormatBase<TValue> DeepCloneWithDecimalSeparator(char? decimalSeparator)
        {
            var result = new PercentCellValueFormat<TValue>(
                                 this.PercentDisplayKind?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.RoundingStrategy?.DeepClone(),
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
        public override NumberCellFormatBase<TValue> DeepCloneWithDigitGroupKind(NumberFormatDigitGroupKind? digitGroupKind)
        {
            var result = new PercentCellValueFormat<TValue>(
                                 this.PercentDisplayKind?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.RoundingStrategy?.DeepClone(),
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
        public override NumberCellFormatBase<TValue> DeepCloneWithDigitGroupSeparator(char? digitGroupSeparator)
        {
            var result = new PercentCellValueFormat<TValue>(
                                 this.PercentDisplayKind?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.RoundingStrategy?.DeepClone(),
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
        public override NumberCellFormatBase<TValue> DeepCloneWithNegativeNumberDisplayKind(NumberFormatNegativeDisplayKind? negativeNumberDisplayKind)
        {
            var result = new PercentCellValueFormat<TValue>(
                                 this.PercentDisplayKind?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.RoundingStrategy?.DeepClone(),
                                 this.DecimalSeparator?.DeepClone(),
                                 this.DigitGroupKind?.DeepClone(),
                                 this.DigitGroupSeparator?.DeepClone(),
                                 negativeNumberDisplayKind,
                                 this.MissingValueText?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="PercentDisplayKind" />.
        /// </summary>
        /// <param name="percentDisplayKind">The new <see cref="PercentDisplayKind" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="PercentCellValueFormat{TValue}" /> using the specified <paramref name="percentDisplayKind" /> for <see cref="PercentDisplayKind" /> and a deep clone of every other property.</returns>
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
        public PercentCellValueFormat<TValue> DeepCloneWithPercentDisplayKind(NumberFormatPercentDisplayKind? percentDisplayKind)
        {
            var result = new PercentCellValueFormat<TValue>(
                                 percentDisplayKind,
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.RoundingStrategy?.DeepClone(),
                                 this.DecimalSeparator?.DeepClone(),
                                 this.DigitGroupKind?.DeepClone(),
                                 this.DigitGroupSeparator?.DeepClone(),
                                 this.NegativeNumberDisplayKind?.DeepClone(),
                                 this.MissingValueText?.DeepClone());

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override CellValueFormatBase<TValue> DeepCloneInternal()
        {
            var result = new PercentCellValueFormat<TValue>(
                                 this.PercentDisplayKind?.DeepClone(),
                                 this.NumberOfDecimalPlaces?.DeepClone(),
                                 this.RoundingStrategy?.DeepClone(),
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
            var result = Invariant($"OBeautifulCode.DataStructure.{this.GetType().ToStringReadable()}: MissingValueText = {this.MissingValueText?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, NumberOfDecimalPlaces = {this.NumberOfDecimalPlaces?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, RoundingStrategy = {this.RoundingStrategy?.ToString() ?? "<null>"}, DecimalSeparator = {this.DecimalSeparator?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, DigitGroupKind = {this.DigitGroupKind?.ToString() ?? "<null>"}, DigitGroupSeparator = {this.DigitGroupSeparator?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, NegativeNumberDisplayKind = {this.NegativeNumberDisplayKind?.ToString() ?? "<null>"}, PercentDisplayKind = {this.PercentDisplayKind?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}