// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellValueMissingException.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Exception thrown when attempting to get the value of a cell that does not yet have a value.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = ObcSuppressBecause.CA1032_ImplementStandardExceptionConstructors_ExceptionUsedInternallyAndConstructorsEnsureRequiredInfoAvailableWhenCaught)]
    public class CellValueMissingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValueMissingException"/> class.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        public CellValueMissingException(
            CellLocatorBase cellLocator)
            : base()
        {
            this.CellLocator = cellLocator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellValueMissingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="cellLocator">The cell locator.</param>
        public CellValueMissingException(
            string message,
            CellLocatorBase cellLocator)
            : base(message)
        {
            this.CellLocator = cellLocator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellValueMissingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="cellLocator">The cell locator.</param>
        public CellValueMissingException(
            string message,
            Exception innerException,
            CellLocatorBase cellLocator)
            : base(message, innerException)
        {
            this.CellLocator = cellLocator;
        }

        /// <summary>
        /// Gets the cell locator.
        /// </summary>
        public CellLocatorBase CellLocator { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = string.Empty;

            if (this.CellLocator != null)
            {
                result = result + "The cell that was found using the following locator does not yet have a value: " + this.CellLocator + Environment.NewLine;
            }

            result = result + base.ToString();

            return result;
        }
    }
}
