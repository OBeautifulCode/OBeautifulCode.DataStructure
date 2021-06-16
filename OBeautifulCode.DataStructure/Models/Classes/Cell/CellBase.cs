// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Base implementation of <see cref="ICell"/>.
    /// </summary>
    public abstract partial class CellBase : ICell, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellBase"/> class.
        /// </summary>
        /// <param name="id">The cell's unique identifier.</param>
        protected CellBase(
            string id)
        {
            this.Id = id;
        }

        /// <inheritdoc />
        public string Id { get; private set; }
    }
}