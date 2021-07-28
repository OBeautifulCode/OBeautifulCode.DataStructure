// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellValueFormatBase{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Base implementation of <see cref="ICellValueFormat{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    public abstract partial class CellValueFormatBase<TValue> : ICellValueFormat<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellValueFormatBase{TValue}"/> class.
        /// </summary>
        protected CellValueFormatBase()
        {
        }
    }
}
