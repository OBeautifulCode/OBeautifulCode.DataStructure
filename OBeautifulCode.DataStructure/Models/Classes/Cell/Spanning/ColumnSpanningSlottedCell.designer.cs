﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.154.0)
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
    public partial class ColumnSpanningSlottedCell : IModel<ColumnSpanningSlottedCell>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="ColumnSpanningSlottedCell"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(ColumnSpanningSlottedCell left, ColumnSpanningSlottedCell right)
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
        /// Determines whether two objects of type <see cref="ColumnSpanningSlottedCell"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(ColumnSpanningSlottedCell left, ColumnSpanningSlottedCell right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(ColumnSpanningSlottedCell other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.SlotIdToCellMap.IsEqualTo(other.SlotIdToCellMap)
                      && this.DefaultSlotName.IsEqualTo(other.DefaultSlotName, StringComparer.Ordinal)
                      && this.ColumnsSpanned.IsEqualTo(other.ColumnsSpanned);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as ColumnSpanningSlottedCell);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.SlotIdToCellMap)
            .Hash(this.DefaultSlotName)
            .Hash(this.ColumnsSpanned)
            .Value;

        /// <inheritdoc />
        public new ColumnSpanningSlottedCell DeepClone() => (ColumnSpanningSlottedCell)this.DeepCloneInternal();

        /// <summary>
        /// Deep clones this object with a new <see cref="SlotIdToCellMap" />.
        /// </summary>
        /// <param name="slotIdToCellMap">The new <see cref="SlotIdToCellMap" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="ColumnSpanningSlottedCell" /> using the specified <paramref name="slotIdToCellMap" /> for <see cref="SlotIdToCellMap" /> and a deep clone of every other property.</returns>
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
        public ColumnSpanningSlottedCell DeepCloneWithSlotIdToCellMap(IReadOnlyDictionary<string, IHaveValueCell> slotIdToCellMap)
        {
            var result = new ColumnSpanningSlottedCell(
                                 slotIdToCellMap,
                                 this.DefaultSlotName?.DeepClone(),
                                 this.ColumnsSpanned.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="DefaultSlotName" />.
        /// </summary>
        /// <param name="defaultSlotName">The new <see cref="DefaultSlotName" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="ColumnSpanningSlottedCell" /> using the specified <paramref name="defaultSlotName" /> for <see cref="DefaultSlotName" /> and a deep clone of every other property.</returns>
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
        public ColumnSpanningSlottedCell DeepCloneWithDefaultSlotName(string defaultSlotName)
        {
            var result = new ColumnSpanningSlottedCell(
                                 this.SlotIdToCellMap?.DeepClone(),
                                 defaultSlotName,
                                 this.ColumnsSpanned.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="ColumnsSpanned" />.
        /// </summary>
        /// <param name="columnsSpanned">The new <see cref="ColumnsSpanned" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="ColumnSpanningSlottedCell" /> using the specified <paramref name="columnsSpanned" /> for <see cref="ColumnsSpanned" /> and a deep clone of every other property.</returns>
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
        public ColumnSpanningSlottedCell DeepCloneWithColumnsSpanned(int columnsSpanned)
        {
            var result = new ColumnSpanningSlottedCell(
                                 this.SlotIdToCellMap?.DeepClone(),
                                 this.DefaultSlotName?.DeepClone(),
                                 columnsSpanned);

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override CellBase DeepCloneInternal()
        {
            var result = new ColumnSpanningSlottedCell(
                                 this.SlotIdToCellMap?.DeepClone(),
                                 this.DefaultSlotName?.DeepClone(),
                                 this.ColumnsSpanned.DeepClone());

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"OBeautifulCode.DataStructure.ColumnSpanningSlottedCell: SlotIdToCellMap = {this.SlotIdToCellMap?.ToString() ?? "<null>"}, DefaultSlotName = {this.DefaultSlotName?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, ColumnsSpanned = {this.ColumnsSpanned.ToString(CultureInfo.InvariantCulture) ?? "<null>"}.");

            return result;
        }
    }
}