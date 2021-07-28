// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveStandardCellFeatures{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// Specifies a cell having a standardized set of features.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    public interface IHaveStandardCellFeatures<TValue> : IHaveStandardCellFeatures, IHaveCellValueFormat<TValue>
    {
    }
}
