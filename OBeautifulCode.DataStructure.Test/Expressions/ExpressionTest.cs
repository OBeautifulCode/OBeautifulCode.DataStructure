// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;

    using OBeautifulCode.Type;

    using Xunit;

    using NamedDecimalSet = System.Collections.Generic.IReadOnlyList<OBeautifulCode.Type.NamedValue<decimal>>;

    public partial class ExpressionTestOp
    {
        [Fact]
        public static void Test()
        {
            const string sectionId = "section-id";

            var numberOfSalesFteCell = Cell.CreateInput<decimal>(id: "sales-fte");

            var numberOfWarehouseFteCell = Cell.CreateInput<decimal>(id: "warehouse-fte");

            var numberOfTotalFte = Cell.CreateOp(
                Do.IfThenElse(
                    Do.AndAlso(
                        Cell.HasValue(sectionId, numberOfSalesFteCell.Id),
                        Cell.HasValue(sectionId, numberOfWarehouseFteCell.Id)),
                    Do.Sum(
                        Cell.GetValue<decimal>(sectionId, numberOfSalesFteCell.Id),
                        Cell.GetValue<decimal>(sectionId, numberOfWarehouseFteCell.Id)),
                    Do.Stop<decimal>()));

            var coscoresCell = new ConstCell<NamedDecimalSet>(
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

            report.SetInputCellValue(2.2m, DateTime.UtcNow, sectionId, numberOfSalesFteCell.Id);
            report.SetInputCellValue(1.1m, DateTime.UtcNow, sectionId, numberOfWarehouseFteCell.Id);

            report.ExecuteAllOperationsAndRecordResults(
                DateTime.UtcNow,
                new Func<IProtocolFactory, IProtocolFactory>[]
                {
                    frameworkFactory => new MyProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
                });

            var quartiles = quartileCell.GetCellValue();

            var fte = numberOfTotalFte.GetCellValue();

            // var quartiles = protocolFactory.GetProtocolAndExecuteViaReflection<IReadOnlyList<NamedValue<decimal>>>(quartileCell.Operation);
            // quartileCell.RecordExecution(new CellOpExecutedEvent<IReadOnlyList<NamedValue<decimal>>>(DateTime.UtcNow, quartiles));
        }
    }
}