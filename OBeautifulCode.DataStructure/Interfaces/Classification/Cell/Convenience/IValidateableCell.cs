// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidateableCell.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    /// <summary>
    /// A cell in a tree table.
    /// </summary>
    /// <remarks>
    /// This interface is a convenience for addressing the properties and methods required to
    /// validate <see cref="IOperationOutputCell{TValue}"/> and <see cref="IInputCell{TValue}"/>.
    /// If authoring your own <see cref="ICell"/> types, DO NOT implement this interface because
    /// it may not be honored when performing validation. For example, if you author an
    /// <see cref="IConstOutputCell{TValue}"/> and mark it with this interface, the cell WILL NOT
    /// get validated.
    /// </remarks>
    public interface IValidateableCell : ICell, IHaveCellValidationConditions, IValidateCellValue
    {
    }
}
