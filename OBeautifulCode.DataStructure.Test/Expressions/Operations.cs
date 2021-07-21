namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    public class TileOp : IReturningOperation<IReadOnlyList<NamedValue<decimal>>>
    {
        public TileOp(
            IReturningOperation<IReadOnlyList<NamedValue<decimal>>> set,
            IReturningOperation<int> numberOfTiles)
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            if (numberOfTiles == null)
            {
                throw new ArgumentNullException(nameof(numberOfTiles));
            }

            this.Set = set;
            this.NumberOfTiles = numberOfTiles;
        }

        public IReturningOperation<int> NumberOfTiles { get; private set; }

        public IReturningOperation<IReadOnlyList<NamedValue<decimal>>> Set { get; private set; }
    }
}