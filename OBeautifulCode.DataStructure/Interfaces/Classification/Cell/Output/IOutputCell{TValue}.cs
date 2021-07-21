// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOutputCell{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// A cell that can output a specified type of value.
    /// </summary>
    /// <typeparam name="TValue">The type of output value.</typeparam>
    public interface IOutputCell<TValue> : INotSlottedCell, IGetCellValue<TValue>
    {
    }
}
