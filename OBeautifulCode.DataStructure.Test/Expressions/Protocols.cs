// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Protocols.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MathNet.Numerics.Statistics;

    using OBeautifulCode.Type;

    public partial class MyProprietaryProtocols
    {
        public MyProprietaryProtocols(
            IProtocolFactory protocolFactory)
        {
            this.ProtocolFactory = protocolFactory;
        }

        public ISyncReturningProtocol<GetProtocolOp, IProtocol> ProtocolFactory { get; private set; }
    }

    public class TileOp : IReturningOperation<IReadOnlyList<NamedValue<decimal>>>
    {
        public TileOp(
            IReturningOperation<IReadOnlyList<NamedValue<decimal>>> setOp,
            IReturningOperation<int> numberOfTilesOp)
        {
            if (setOp == null)
            {
                throw new ArgumentNullException(nameof(setOp));
            }

            if (numberOfTilesOp == null)
            {
                throw new ArgumentNullException(nameof(numberOfTilesOp));
            }

            this.SetOp = setOp;
            this.NumberOfTilesOp = numberOfTilesOp;
        }

        public IReturningOperation<int> NumberOfTilesOp { get; private set; }

        public IReturningOperation<IReadOnlyList<NamedValue<decimal>>> SetOp { get; private set; }
    }

    public partial class MyProprietaryProtocols : ISyncReturningProtocol<TileOp, IReadOnlyList<NamedValue<decimal>>>
    {
        public IReadOnlyList<NamedValue<decimal>> Execute(
            TileOp operation)
        {
            var set = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<IReadOnlyList<NamedValue<decimal>>>(operation.SetOp);

            var tiles = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<int>(operation.NumberOfTilesOp);

            if (tiles != 4)
            {
                throw new NotSupportedException();
            }

            var doubleSet = set.GetValues().Select(Convert.ToDouble).ToArray();

            var lowerQuartile = doubleSet.LowerQuartile();

            var upperQuartile = doubleSet.UpperQuartile();

            var median = doubleSet.Median();

            var result = new[]
            {
                new NamedValue<decimal>("lower-quartile", Convert.ToDecimal(lowerQuartile)),
                new NamedValue<decimal>("median", Convert.ToDecimal(median)),
                new NamedValue<decimal>("upper-quartile", Convert.ToDecimal(upperQuartile)),
            };

            return result;
        }
    }
}