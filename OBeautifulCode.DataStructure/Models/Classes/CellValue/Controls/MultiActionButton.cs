// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiActionButton.cs" company="OBeautifulCode">
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
    /// A button that supports multiple actions.
    /// </summary>
    /// <remarks>
    /// A nice user experience for this is a button that, when pressed, reveals a set of options
    /// in a dropdown selector, and when the desired option is selected, the action is executed.
    /// </remarks>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MultiActionButton : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiActionButton"/> class.
        /// </summary>
        /// <param name="buttonText">The text to display on the button.</param>
        /// <param name="actionItems">The action items in the order they should be displayed.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = ObcSuppressBecause.CA1054_UriParametersShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public MultiActionButton(
            string buttonText,
            IReadOnlyList<MultiActionButtonItem> actionItems)
        {
            if (buttonText == null)
            {
                throw new ArgumentNullException(nameof(buttonText));
            }

            if (string.IsNullOrWhiteSpace(buttonText))
            {
                throw new ArgumentException(Invariant($"{nameof(buttonText)} is white space."), nameof(buttonText));
            }

            if (actionItems == null)
            {
                throw new ArgumentNullException(nameof(actionItems));
            }

            if (!actionItems.Any())
            {
                throw new ArgumentException(Invariant($"{nameof(actionItems)} is an empty enumerable."));
            }

            if (actionItems.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(actionItems)} contains at least one null element."));
            }

            this.ButtonText = buttonText;
            this.ActionItems = actionItems;
        }

        /// <summary>
        /// Gets the text to display on the button.
        /// </summary>
        public string ButtonText { get; private set; }

        /// <summary>
        /// Gets the action items in the order they should be displayed.
        /// </summary>
        public IReadOnlyList<MultiActionButtonItem> ActionItems { get; private set; }
    }
}