// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiActionButtonItem.cs" company="OBeautifulCode">
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
    /// An action item in a <see cref="MultiActionButton"/>.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
    public partial class MultiActionButtonItem : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiActionButtonItem"/> class.
        /// </summary>
        /// <param name="text">The text to link.</param>
        /// <param name="link">The link.</param>
        /// <param name="availability">OPTIONAL availability of the action item.  DEFAULT is an enabled action.</param>
        /// <param name="media">OPTIONAL media to display alongside the linked text.  DEFAULT is no media.</param>
        /// <param name="hoverOverText">OPTIONAL hover-over text.  DEFAULT is no hover-over text.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = ObcSuppressBecause.CA1054_UriParametersShouldNotBeStrings_PreferToRepresentUrlAsString)]
        public MultiActionButtonItem(
            string text,
            ILink link,
            Availability availability = Availability.Enabled,
            IMedia media = null,
            string hoverOverText = null)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException(Invariant($"{nameof(text)} is white space."), nameof(text));
            }

            if (link == null)
            {
                throw new ArgumentNullException(nameof(link));
            }

            if (availability == Availability.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(availability), Invariant($"{nameof(availability)} is {nameof(DataStructure.Availability)}.{nameof(Availability.Unknown)}."));
            }

            this.Text = text;
            this.Link = link;
            this.Availability = availability;
            this.Media = media;
            this.HoverOverText = hoverOverText;
        }

        /// <summary>
        /// Gets the text to link.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the link.
        /// </summary>
        public ILink Link { get; private set; }

        /// <summary>
        /// Gets the availability of the action item.
        /// </summary>
        public Availability Availability { get; private set; }

        /// <summary>
        /// Gets the media to display alongside the linked text.
        /// </summary>
        public IMedia Media { get; private set; }

        /// <summary>
        /// Gets the hover-over text.
        /// </summary>
        public string HoverOverText { get; private set; }
    }
}