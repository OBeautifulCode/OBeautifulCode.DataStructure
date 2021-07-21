// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConstOutputCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// A cell who's output is an immutable value.
    /// </summary>
    /// <typeparam name="TValue">The type of output value.</typeparam>
    public interface IConstOutputCell<TValue> : IOutputCell<TValue>
    {
    }
}
