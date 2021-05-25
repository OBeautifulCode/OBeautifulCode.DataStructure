// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkTarget.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies where/how a linked resource is displayed/experienced.
    /// </summary>
    public enum LinkTarget
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Target the current pane.
        /// </summary>
        CurrentPane,

        /// <summary>
        /// Target the left pane.
        /// </summary>
        LeftPane,

        /// <summary>
        /// Target the center pane.
        /// </summary>
        CenterPane,

        /// <summary>
        /// Target the right pane.
        /// </summary>
        RightPane,

        /// <summary>
        /// Target the current window.
        /// </summary>
        CurrentWindow,

        /// <summary>
        /// Target a new window.
        /// </summary>
        NewWindow,

        /// <summary>
        /// Target a download.
        /// </summary>
        Download,

        /// <summary>
        /// Target a modal.
        /// </summary>
        Modal,
    }
}