﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.160.0)
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
    public partial class CategoricalCellValueFormat<TValue> : IModel<CategoricalCellValueFormat<TValue>>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="CategoricalCellValueFormat{TValue}"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(CategoricalCellValueFormat<TValue> left, CategoricalCellValueFormat<TValue> right)
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
        /// Determines whether two objects of type <see cref="CategoricalCellValueFormat{TValue}"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(CategoricalCellValueFormat<TValue> left, CategoricalCellValueFormat<TValue> right) => !(left == right);

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        public bool Equals(CategoricalCellValueFormat<TValue> other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var genericArgumentsUsedAsKeyInDictionary = new[] { typeof(TValue) };

            if (genericArgumentsUsedAsKeyInDictionary.Any(_ => (_ == typeof(DateTime)) || (_ == typeof(DateTime?))))
            {
                throw new NotSupportedException("This model contains one or more properties that are OR have within their generic argument tree or array element type a System Dictionary that is keyed on DateTime or DateTime?; IsEqualTo may do the wrong thing when comparing the keys of two such dictionaries (because it uses dictionary's embedded equality comparer, which is most likely the default comparer, which determines two DateTime objects to be equal if they have the same Ticks, regardless of whether they have the same Kind).");
            }

            var result = this.MissingValueText.IsEqualTo(other.MissingValueText, StringComparer.Ordinal)
                      && this.ValueToTextMap.IsEqualTo(other.ValueToTextMap);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as CategoricalCellValueFormat<TValue>);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.MissingValueText)
            .Hash(this.ValueToTextMap)
            .Value;

        /// <inheritdoc />
        public new CategoricalCellValueFormat<TValue> DeepClone() => (CategoricalCellValueFormat<TValue>)this.DeepCloneInternal();

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
            var result = new CategoricalCellValueFormat<TValue>(
                                 this.ValueToTextMap?.DeepClone(),
                                 missingValueText);

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="ValueToTextMap" />.
        /// </summary>
        /// <param name="valueToTextMap">The new <see cref="ValueToTextMap" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="CategoricalCellValueFormat{TValue}" /> using the specified <paramref name="valueToTextMap" /> for <see cref="ValueToTextMap" /> and a deep clone of every other property.</returns>
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
        public CategoricalCellValueFormat<TValue> DeepCloneWithValueToTextMap(IReadOnlyDictionary<TValue, string> valueToTextMap)
        {
            var result = new CategoricalCellValueFormat<TValue>(
                                 valueToTextMap,
                                 this.MissingValueText?.DeepClone());

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override CellValueFormatBase<TValue> DeepCloneInternal()
        {
            var result = new CategoricalCellValueFormat<TValue>(
                                 this.ValueToTextMap?.DeepClone(),
                                 this.MissingValueText?.DeepClone());

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"OBeautifulCode.DataStructure.{this.GetType().ToStringReadable()}: MissingValueText = {this.MissingValueText?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, ValueToTextMap = {this.ValueToTextMap?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}