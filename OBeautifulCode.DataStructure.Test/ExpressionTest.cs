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
            /*
             * Op.IfThenElse(
             *     Op.Not(Cell.HasValue(sectionId, "sales-fte")),
             *     new ValidationResult(Invalid, Op.Value("input required")),
             *     Op.IfThenElse(
             *     Cell.HasValue(sectionId, "sales-fte"),
             *     new ValidationResult(Invalid, Op.Value("input required")),
             * ))
             */

            var isForProfitCell = Cell.CreateEnabledInput<bool>("is-for-profit");

            var restrictedCash = Cell.CreateDisabledInput<decimal>(
                "restricted-cash",
                availabilityCheck: Cell.CreateAvailabilityCheck(
                    Op.IfThenElse(
                        Op.AndAlso(
                            Cell.InThisSection(isForProfitCell.Id).HasValue(),
                            Op.Not(Cell.InThisSection(isForProfitCell.Id).GetValue<bool>())),
                        Op.Const(new AvailabilityCheckResult(Availability.Enabled)),
                        Op.Const(new AvailabilityCheckResult(Availability.Disabled)))));

            var numberOfSalesFteCell = Cell.CreateEnabledInput<decimal>(
                id: "sales-fte",
                validation: Cell.CreateValidation(Op.ValidateUsingConditions(
                    new[]
                    {
                        new ValidationCondition(Cell.This().HasValue(), Op.Const("input required")),
                        new ValidationCondition(
                            Op.IsGreaterThanOrEqualTo(
                                Cell.This().GetValue<decimal>(),
                                Op.Const(0m)),
                            Op.Const("must be >= 0")),
                    })));

            var numberOfWarehouseFteCell = Cell.CreateEnabledInput<decimal>(
                id: "warehouse-fte",
                validation: Cell.CreateValidation(Op.ValidateUsingConditions(
                    new[]
                    {
                        new ValidationCondition(Cell.This().HasValue(), Op.Const("input required")),
                        new ValidationCondition(
                            Op.IsGreaterThanOrEqualTo(
                                Cell.This().GetValue<decimal>(),
                                Op.Const(0m)),
                            Op.Const("must be >= 0")),
                    })));

            var numberOfTotalFte = Cell.CreateOp(
                    id: "total-fte",
                    operation:
                        Op.IfThenElse(
                            Op.AndAlso(
                                Cell.InThisSection(numberOfSalesFteCell.Id).HasValue(),
                                Cell.InThisSection(numberOfWarehouseFteCell.Id).HasValue()),
                            Op.Sum(
                                Cell.InThisSection(numberOfSalesFteCell.Id).GetValue<decimal>(),
                                Cell.InThisSection(numberOfWarehouseFteCell.Id).GetValue<decimal>()),
                            Op.Abort<decimal>("cannot perform sum")),
                    validation: Cell.CreateValidation(Op.ValidateUsingConditions(
                        new[]
                        {
                            new ValidationCondition(
                                Op.IfThenElse(
                                    Op.IsEqualTo(
                                        Cell.This().GetOpExecutionOutcome(),
                                        Op.Const(CellOpExecutionOutcome.Completed)),
                                    Op.Const(true),
                                    Op.Abort<bool>()),
                                Op.Const("never-hit")),
                            new ValidationCondition(
                                Op.IsGreaterThan(
                                    Cell.This().GetValue<decimal>(),
                                    Op.Const(0m)),
                                Op.Const("must be >= 0")),
                        })));

            var scoresCell = Cell.CreateConst<NamedDecimalSet>(
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
                id: "scores");

            var scoresCellCopy = Cell.CreateOp(
                Cell.InThisSection(scoresCell.Id).GetValue<NamedDecimalSet>(),
                id: "scores-copy");

            var intConstCell = new ConstCell<int>(4, id: "int-const");

            var quartileCell = Cell.CreateOp(
                new TileOp(
                    Cell.InThisSection(scoresCellCopy.Id).GetValue<NamedDecimalSet>(),
                    Cell.InThisSection(intConstCell.Id).GetValue<int>()),
                id: "quartiles");

            var rows = new[]
            {
                new Row(new[] { isForProfitCell }),
                new Row(new[] { restrictedCash }),
                new Row(new[] { numberOfSalesFteCell }),
                new Row(new[] { numberOfWarehouseFteCell }),
                new Row(new[] { numberOfTotalFte }),
                new Row(new[] { intConstCell }),
                new Row(new[] { scoresCellCopy }),
                new Row(new[] { scoresCell }),
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

            var sectionId = "section-id";

            var section = new Section(sectionId, treeTable);

            var sections = new[]
            {
                section,
            };

            var report = new Report("report-id", sections);

            report.SetInputCellValue(false, DateTime.UtcNow, sectionId, isForProfitCell.Id);

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

            // todo: code up not applicable validation (like if some cell is true, then don't validate)
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