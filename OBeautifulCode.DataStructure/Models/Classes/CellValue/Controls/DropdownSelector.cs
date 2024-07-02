// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DropdownSelector.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Represents a drop-down selector, where only a single value may be selected.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class DropdownSelector : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropdownSelector"/> class.
        /// </summary>
        /// <param name="items">
        /// The items in the selector.  When rendered, the names should be displayed as items that can be selected.
        /// When the selection changes, the system should navigate the user to resource specified by the item's link.
        /// If the selector should default to a blank item that is selected on load, then the caller should insert a
        /// new NamedValue{ILink}(new StandardLink(new NullLinkedResource)) as the first item in the list and leave
        /// <paramref name="selectedItemName"/> null.
        /// </param>
        /// <param name="selectedItemName">
        /// OPTIONAL name of the item to select on load.  DEFAULT is to select the first item in <paramref name="items"/>.
        /// </param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = ObcSuppressBecause.CA1054_UriParametersShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public DropdownSelector(
            IReadOnlyList<NamedValue<ILink>> items,
            string selectedItemName = null)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (!items.Any())
            {
                throw new ArgumentException(Invariant($"{nameof(items)} is an empty enumerable."));
            }

            if (items.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(items)} contains at least one null element."));
            }

            // ReSharper disable once SimplifyLinqExpressionUseAll - prefer !Any for readability
            if ((selectedItemName != null) && (!items.Any(_ => _.Name == selectedItemName)))
            {
                throw new ArgumentException(Invariant($"{nameof(selectedItemName)} '{selectedItemName}' does not exist in {nameof(items)}."));
            }

            this.Items = items;
            this.SelectedItemName = selectedItemName;
        }

        /// <summary>
        /// Gets the items in the selector.  When rendered, the names should be displayed as items that can be selected.
        /// When the selection changes, the system should navigate the user to resource specified by the item's link.
        /// </summary>
        public IReadOnlyList<NamedValue<ILink>> Items { get; private set; }

        /// <summary>
        /// Gets the name of the item to select on load.
        /// If null, the first item will be selected by default.
        /// </summary>
        public string SelectedItemName { get; private set; }
    }
}