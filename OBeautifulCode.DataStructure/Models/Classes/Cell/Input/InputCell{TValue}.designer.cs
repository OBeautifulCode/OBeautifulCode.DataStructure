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
    public partial class InputCell<TValue> : IModel<InputCell<TValue>>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="InputCell{TValue}"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(InputCell<TValue> left, InputCell<TValue> right)
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
        /// Determines whether two objects of type <see cref="InputCell{TValue}"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(InputCell<TValue> left, InputCell<TValue> right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(InputCell<TValue> other)
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
                      && this.ColumnsSpanned.IsEqualTo(other.ColumnsSpanned)
                      && this.Details.IsEqualTo(other.Details, StringComparer.Ordinal)
                      && this.InputAppliedToCellEvent.IsEqualTo(other.InputAppliedToCellEvent)
                      && this.ValidationConditions.IsEqualTo(other.ValidationConditions)
                      && this.CellValidationEvent.IsEqualTo(other.CellValidationEvent)
                      && this.ValueFormat.IsEqualTo(other.ValueFormat)
                      && this.Format.IsEqualTo(other.Format)
                      && this.HoverOver.IsEqualTo(other.HoverOver)
                      && this.Link.IsEqualTo(other.Link);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as InputCell<TValue>);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.Id)
            .Hash(this.ColumnsSpanned)
            .Hash(this.Details)
            .Hash(this.InputAppliedToCellEvent)
            .Hash(this.ValidationConditions)
            .Hash(this.CellValidationEvent)
            .Hash(this.ValueFormat)
            .Hash(this.Format)
            .Hash(this.HoverOver)
            .Hash(this.Link)
            .Value;

        /// <inheritdoc />
        public new InputCell<TValue> DeepClone() => (InputCell<TValue>)this.DeepCloneInternal();

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
        public override CellBase DeepCloneWithId(string id)
        {
            var result = new InputCell<TValue>(
                                 this.InputAppliedToCellEvent?.DeepClone(),
                                 this.ValidationConditions?.DeepClone(),
                                 this.CellValidationEvent?.DeepClone(),
                                 id,
                                 this.ColumnsSpanned?.DeepClone(),
                                 this.Details?.DeepClone(),
                                 this.ValueFormat?.DeepClone(),
                                 this.Format?.DeepClone(),
                                 this.HoverOver?.DeepClone(),
                                 this.Link?.DeepClone());

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
        public override CellBase DeepCloneWithColumnsSpanned(int? columnsSpanned)
        {
            var result = new InputCell<TValue>(
                                 this.InputAppliedToCellEvent?.DeepClone(),
                                 this.ValidationConditions?.DeepClone(),
                                 this.CellValidationEvent?.DeepClone(),
                                 this.Id?.DeepClone(),
                                 columnsSpanned,
                                 this.Details?.DeepClone(),
                                 this.ValueFormat?.DeepClone(),
                                 this.Format?.DeepClone(),
                                 this.HoverOver?.DeepClone(),
                                 this.Link?.DeepClone());

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
        public override CellBase DeepCloneWithDetails(string details)
        {
            var result = new InputCell<TValue>(
                                 this.InputAppliedToCellEvent?.DeepClone(),
                                 this.ValidationConditions?.DeepClone(),
                                 this.CellValidationEvent?.DeepClone(),
                                 this.Id?.DeepClone(),
                                 this.ColumnsSpanned?.DeepClone(),
                                 details,
                                 this.ValueFormat?.DeepClone(),
                                 this.Format?.DeepClone(),
                                 this.HoverOver?.DeepClone(),
                                 this.Link?.DeepClone());

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
        public override InputCellBase<TValue> DeepCloneWithInputAppliedToCellEvent(InputAppliedToCellEvent<TValue> inputAppliedToCellEvent)
        {
            var result = new InputCell<TValue>(
                                 inputAppliedToCellEvent,
                                 this.ValidationConditions?.DeepClone(),
                                 this.CellValidationEvent?.DeepClone(),
                                 this.Id?.DeepClone(),
                                 this.ColumnsSpanned?.DeepClone(),
                                 this.Details?.DeepClone(),
                                 this.ValueFormat?.DeepClone(),
                                 this.Format?.DeepClone(),
                                 this.HoverOver?.DeepClone(),
                                 this.Link?.DeepClone());

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
        public override InputCellBase<TValue> DeepCloneWithValidationConditions(ValidationConditions validationConditions)
        {
            var result = new InputCell<TValue>(
                                 this.InputAppliedToCellEvent?.DeepClone(),
                                 validationConditions,
                                 this.CellValidationEvent?.DeepClone(),
                                 this.Id?.DeepClone(),
                                 this.ColumnsSpanned?.DeepClone(),
                                 this.Details?.DeepClone(),
                                 this.ValueFormat?.DeepClone(),
                                 this.Format?.DeepClone(),
                                 this.HoverOver?.DeepClone(),
                                 this.Link?.DeepClone());

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
        public override InputCellBase<TValue> DeepCloneWithCellValidationEvent(CellValidationEventBase cellValidationEvent)
        {
            var result = new InputCell<TValue>(
                                 this.InputAppliedToCellEvent?.DeepClone(),
                                 this.ValidationConditions?.DeepClone(),
                                 cellValidationEvent,
                                 this.Id?.DeepClone(),
                                 this.ColumnsSpanned?.DeepClone(),
                                 this.Details?.DeepClone(),
                                 this.ValueFormat?.DeepClone(),
                                 this.Format?.DeepClone(),
                                 this.HoverOver?.DeepClone(),
                                 this.Link?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="ValueFormat" />.
        /// </summary>
        /// <param name="valueFormat">The new <see cref="ValueFormat" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="InputCell{TValue}" /> using the specified <paramref name="valueFormat" /> for <see cref="ValueFormat" /> and a deep clone of every other property.</returns>
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
        public InputCell<TValue> DeepCloneWithValueFormat(ICellValueFormat<TValue> valueFormat)
        {
            var result = new InputCell<TValue>(
                                 this.InputAppliedToCellEvent?.DeepClone(),
                                 this.ValidationConditions?.DeepClone(),
                                 this.CellValidationEvent?.DeepClone(),
                                 this.Id?.DeepClone(),
                                 this.ColumnsSpanned?.DeepClone(),
                                 this.Details?.DeepClone(),
                                 valueFormat,
                                 this.Format?.DeepClone(),
                                 this.HoverOver?.DeepClone(),
                                 this.Link?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Format" />.
        /// </summary>
        /// <param name="format">The new <see cref="Format" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="InputCell{TValue}" /> using the specified <paramref name="format" /> for <see cref="Format" /> and a deep clone of every other property.</returns>
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
        public InputCell<TValue> DeepCloneWithFormat(CellFormat format)
        {
            var result = new InputCell<TValue>(
                                 this.InputAppliedToCellEvent?.DeepClone(),
                                 this.ValidationConditions?.DeepClone(),
                                 this.CellValidationEvent?.DeepClone(),
                                 this.Id?.DeepClone(),
                                 this.ColumnsSpanned?.DeepClone(),
                                 this.Details?.DeepClone(),
                                 this.ValueFormat?.DeepClone(),
                                 format,
                                 this.HoverOver?.DeepClone(),
                                 this.Link?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="HoverOver" />.
        /// </summary>
        /// <param name="hoverOver">The new <see cref="HoverOver" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="InputCell{TValue}" /> using the specified <paramref name="hoverOver" /> for <see cref="HoverOver" /> and a deep clone of every other property.</returns>
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
        public InputCell<TValue> DeepCloneWithHoverOver(IHoverOver hoverOver)
        {
            var result = new InputCell<TValue>(
                                 this.InputAppliedToCellEvent?.DeepClone(),
                                 this.ValidationConditions?.DeepClone(),
                                 this.CellValidationEvent?.DeepClone(),
                                 this.Id?.DeepClone(),
                                 this.ColumnsSpanned?.DeepClone(),
                                 this.Details?.DeepClone(),
                                 this.ValueFormat?.DeepClone(),
                                 this.Format?.DeepClone(),
                                 hoverOver,
                                 this.Link?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Link" />.
        /// </summary>
        /// <param name="link">The new <see cref="Link" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="InputCell{TValue}" /> using the specified <paramref name="link" /> for <see cref="Link" /> and a deep clone of every other property.</returns>
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
        public InputCell<TValue> DeepCloneWithLink(ILink link)
        {
            var result = new InputCell<TValue>(
                                 this.InputAppliedToCellEvent?.DeepClone(),
                                 this.ValidationConditions?.DeepClone(),
                                 this.CellValidationEvent?.DeepClone(),
                                 this.Id?.DeepClone(),
                                 this.ColumnsSpanned?.DeepClone(),
                                 this.Details?.DeepClone(),
                                 this.ValueFormat?.DeepClone(),
                                 this.Format?.DeepClone(),
                                 this.HoverOver?.DeepClone(),
                                 link);

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override CellBase DeepCloneInternal()
        {
            var result = new InputCell<TValue>(
                                 this.InputAppliedToCellEvent?.DeepClone(),
                                 this.ValidationConditions?.DeepClone(),
                                 this.CellValidationEvent?.DeepClone(),
                                 this.Id?.DeepClone(),
                                 this.ColumnsSpanned?.DeepClone(),
                                 this.Details?.DeepClone(),
                                 this.ValueFormat?.DeepClone(),
                                 this.Format?.DeepClone(),
                                 this.HoverOver?.DeepClone(),
                                 this.Link?.DeepClone());

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"OBeautifulCode.DataStructure.{this.GetType().ToStringReadable()}: Id = {this.Id?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, ColumnsSpanned = {this.ColumnsSpanned?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, Details = {this.Details?.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, InputAppliedToCellEvent = {this.InputAppliedToCellEvent?.ToString() ?? "<null>"}, ValidationConditions = {this.ValidationConditions?.ToString() ?? "<null>"}, CellValidationEvent = {this.CellValidationEvent?.ToString() ?? "<null>"}, ValueFormat = {this.ValueFormat?.ToString() ?? "<null>"}, Format = {this.Format?.ToString() ?? "<null>"}, HoverOver = {this.HoverOver?.ToString() ?? "<null>"}, Link = {this.Link?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}