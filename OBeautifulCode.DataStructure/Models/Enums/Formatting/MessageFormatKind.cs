// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageFormatKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies how to format a string message.
    /// </summary>
    public enum MessageFormatKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Format the message as plaintext.
        /// </summary>
        Plaintext,

        /// <summary>
        /// Format the message as HTML.
        /// </summary>
        Html,
    }
}