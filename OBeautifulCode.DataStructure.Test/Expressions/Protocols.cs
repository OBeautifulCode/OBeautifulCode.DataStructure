namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MathNet.Numerics.Statistics;

    using OBeautifulCode.Type;

    public partial class DataStructureCalculationProtocols : ISyncReturningProtocol<TileOp, IReadOnlyList<NamedValue<decimal>>>
    {
        public IReadOnlyList<NamedValue<decimal>> Execute(
            TileOp operation)
        {
            var set = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<IReadOnlyList<NamedValue<decimal>>>(operation.Set);

            var tiles = this.ProtocolFactory.GetProtocolAndExecuteViaReflection<int>(operation.NumberOfTiles);

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

    public partial class DataStructureCalculationProtocols : ISyncReturningProtocol<GetIntOp, int>
    {
        public int Execute(GetIntOp operation)
        {
            return operation.Value;
        }
    }

    public partial class DataStructureCalculationProtocols : ISyncReturningProtocol<GetCellValueOp<IReadOnlyList<NamedValue<decimal>>>, IReadOnlyList<NamedValue<decimal>>>
    {
        public DataStructureCalculationProtocols(
            Report report,
            IProtocolFactory protocolFactory)
        {
            this.Report = report;
            this.ProtocolFactory = protocolFactory;
        }

        public IProtocolFactory ProtocolFactory { get; private set; }

        public Report Report { get; set; }

        public IReadOnlyList<NamedValue<decimal>> Execute(
            GetCellValueOp<IReadOnlyList<NamedValue<decimal>>> operation)
        {
            var result = this.Report.GetCellValue<IReadOnlyList<NamedValue<decimal>>>(operation.SectionId ?? this.Report.Sections.Single().Id, operation.CellId);

            return result;
        }
    }

    public partial class DataStructureCalculationProtocols : ISyncReturningProtocol<GetCellValueOp<int>, int>
    {
        public int Execute(
            GetCellValueOp<int> operation)
        {
            var result = this.Report.GetCellValue<int>(operation.SectionId ?? this.Report.Sections.Single().Id, operation.CellId);

            return result;
        }
    }
}