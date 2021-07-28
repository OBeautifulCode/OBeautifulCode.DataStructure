// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionTest.cs" company="OBeautifulCode">
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

    using Xunit;

    using NamedDecimalSet = System.Collections.Generic.IReadOnlyList<OBeautifulCode.Type.NamedValue<decimal>>;

    public partial class ExpressionTestOp
    {
        [Fact]
        public static void Test()
        {
            const string sectionId = "section-id";

            var numberOfSalesFteCell = Cell.CreateInput<decimal>(
                id: "sales-fte",
                validationConditions: new ValidationConditions(
                    new[]
                    {
                        new ValidationCondition(Cell.HasValue(sectionId, "sales-fte"), Do.Value("input required")),
                        new ValidationCondition(
                            Do.IsGreaterThanOrEqualTo(
                                Cell.GetValue<decimal>(sectionId, "sales-fte"),
                                Do.Value(0m)),
                            Do.Value("must be >= 0")),
                    }));

            var numberOfWarehouseFteCell = Cell.CreateInput<decimal>(
                id: "warehouse-fte",
                validationConditions: new ValidationConditions(
                    new[]
                    {
                        new ValidationCondition(Cell.HasValue(sectionId, "warehouse-fte"), Do.Value("input required")),
                        new ValidationCondition(
                            Do.IsGreaterThanOrEqualTo(
                                Cell.GetValue<decimal>(sectionId, "warehouse-fte"),
                                Do.Value(0m)),
                            Do.Value("must be >= 0")),
                    }));

            var numberOfTotalFte = Cell.CreateOp(
                    id: "total-fte",
                    operation:
                        Do.IfThenElse(
                            Do.AndAlso(
                                Cell.HasValue(sectionId, numberOfSalesFteCell.Id),
                                Cell.HasValue(sectionId, numberOfWarehouseFteCell.Id)),
                            Do.Sum(
                                Cell.GetValue<decimal>(sectionId, numberOfSalesFteCell.Id),
                                Cell.GetValue<decimal>(sectionId, numberOfWarehouseFteCell.Id)),
                            Do.Abort<decimal>("cannot perform sum")),
                    validationConditions: new ValidationConditions(
                        new[]
                        {
                            new ValidationCondition(
                                Do.IfThenElse(
                                    Do.AndAlso(
                                        Cell.HasValue(sectionId, numberOfSalesFteCell.Id),
                                        Cell.HasValue(sectionId, numberOfWarehouseFteCell.Id)),
                                    Do.Value(true),
                                    Do.Abort<bool>()),
                                Do.Value("never-hit")),
                            new ValidationCondition(
                                Do.IsGreaterThan(
                                    Cell.GetValue<decimal>(sectionId, "total-fte"),
                                    Do.Value(0m)),
                                Do.Value("must be >= 0")),
                        }));

            var coscoresCell = Cell.CreateConst<NamedDecimalSet>(
                new[]
                {
                    new NamedValue<decimal>("bob", 1),
                    new NamedValue<decimal>("joe", 4),
                    new NamedValue<decimal>("sally", 9),
                    new NamedValue<decimal>("jane", 15),
                    new NamedValue<decimal>("john", 1),
                    new NamedValue<decimal>("ed", 2),
                    new NamedValue<decimal>("lynn", 9),
                    new NamedValue<decimal>("luke", 20),
                    new NamedValue<decimal>("mark", 7),
                    new NamedValue<decimal>("may", 6),
                    new NamedValue<decimal>("april", 13),
                    new NamedValue<decimal>("wally", 5),
                },
                id: "coscores");

            var coscoreCellsCopy = Cell.CreateOp(
                Cell.GetValue<NamedDecimalSet>(sectionId, "coscores"),
                id: "coscores-copy");

            var intConstCell = new ConstCell<int>(4, id: "int-const");

            var quartileCell = Cell.CreateOp(
                new TileOp(
                    Cell.GetValue<NamedDecimalSet>(sectionId, "coscores-copy"),
                    Cell.GetValue<int>(sectionId, "int-const")),
                null,
                id: "quartiles");

            var rows = new[]
            {
                new Row(new[] { numberOfSalesFteCell }),
                new Row(new[] { numberOfWarehouseFteCell }),
                new Row(new[] { numberOfTotalFte }),
                new Row(new[] { intConstCell }),
                new Row(new[] { coscoreCellsCopy }),
                new Row(new[] { coscoresCell }),
                new Row(new[] { quartileCell }),
            };

            var dataRows = new DataRows(rows);

            var tableRows = new TableRows(dataRows: dataRows);

            var columns = new[]
            {
                new Column("the-only-column"),
            };

            var tableColumns = new TableColumns(columns);

            var treeTable = new TreeTable(tableColumns, tableRows);

            var section = new Section(sectionId, treeTable);

            var sections = new[]
            {
                section,
            };

            var report = new Report("report-id", sections);

            report.ReCalc(
                DateTime.UtcNow,
                new Func<IProtocolFactory, IProtocolFactory>[]
                {
                    frameworkFactory => new MyProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
                });

            report.SetInputCellValue(2.2m, DateTime.UtcNow, sectionId, numberOfSalesFteCell.Id);
            report.SetInputCellValue(1.1m, DateTime.UtcNow, sectionId, numberOfWarehouseFteCell.Id);

            report.ReCalc(
                DateTime.UtcNow,
                new Func<IProtocolFactory, IProtocolFactory>[]
                {
                    frameworkFactory => new MyProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
                });

            // todo: code up not applicable validaition (like if some cell is true, then don't validate)
        }
    }

    public partial class MyProprietaryProtocols
    {
        public MyProprietaryProtocols(
            IProtocolFactory protocolFactory)
        {
            this.ProtocolFactory = protocolFactory;
        }

        public ISyncReturningProtocol<GetProtocolOp, IProtocol> ProtocolFactory { get; private set; }
    }

    public class TileOp : IReturningOperation<NamedDecimalSet>
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