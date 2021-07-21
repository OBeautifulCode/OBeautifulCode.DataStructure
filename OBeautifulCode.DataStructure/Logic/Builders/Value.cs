// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Value.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using NamedDecimalSet = System.Collections.Generic.IReadOnlyList<OBeautifulCode.Type.NamedValue<decimal>>;

    /// <summary>
    /// Builder methods related to values.
    /// </summary>
    public static class Value
    {
        /// <summary>
        /// Builder methods related to getting values.
        /// </summary>
        public static class Get
        {
            /// <summary>
            /// Builds an operation that gets a specified named decimal set value.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns>
            /// An operation that gets the specified value.
            /// </returns>
            public static GetConstValueOp<NamedDecimalSet> NamedDecimalSet(
                NamedDecimalSet value)
            {
                var result = new GetConstValueOp<NamedDecimalSet>(value);

                return result;
            }

            /// <summary>
            /// Builds an operation that gets a specified integer value.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns>
            /// An operation that gets the specified value.
            /// </returns>
            public static GetConstValueOp<int> Int(
                int value)
            {
                var result = new GetConstValueOp<int>(value);

                return result;
            }

            /// <summary>
            /// Builds an operation that gets a specified decimal value.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns>
            /// An operation that gets the specified value.
            /// </returns>
            public static GetConstValueOp<decimal> Decimal(
                decimal value)
            {
                var result = new GetConstValueOp<decimal>(value);

                return result;
            }

            /// <summary>
            /// Builds an operation that gets a specified boolean value.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns>
            /// An operation that gets the specified value.
            /// </returns>
            public static GetConstValueOp<bool> Bool(
                bool value)
            {
                var result = new GetConstValueOp<bool>(value);

                return result;
            }
        }
    }
}
