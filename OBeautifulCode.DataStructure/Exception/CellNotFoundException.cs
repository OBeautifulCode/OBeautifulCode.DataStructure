﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellNotFoundException.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Exception thrown when a cell cannot be found.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = ObcSuppressBecause.CA1032_ImplementStandardExceptionConstructors_ExceptionUsedInternallyAndConstructorsEnsureRequiredInfoAvailableWhenCaught)]
    public class CellNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellNotFoundException"/> class.
        /// </summary>
        /// <param name="cellLocator">The cell locator.</param>
        public CellNotFoundException(
            CellLocator cellLocator)
            : base()
        {
            this.CellLocator = cellLocator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="cellLocator">The cell locator.</param>
        public CellNotFoundException(
            string message,
            CellLocator cellLocator)
            : base(message)
        {
            this.CellLocator = cellLocator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="cellLocator">The cell locator.</param>
        public CellNotFoundException(
            string message,
            Exception innerException,
            CellLocator cellLocator)
            : base(message, innerException)
        {
            this.CellLocator = cellLocator;
        }

        /// <summary>
        /// Gets the cell locator.
        /// </summary>
        public CellLocator CellLocator { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = string.Empty;

            if (this.CellLocator != null)
            {
                result = result + "Could not find a cell using this locator: " + this.CellLocator + Environment.NewLine;
            }

            result = result + base.ToString();

            return result;
        }
    }
}