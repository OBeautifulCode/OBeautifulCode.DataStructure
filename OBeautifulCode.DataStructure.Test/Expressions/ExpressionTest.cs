// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionTestOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Collections.Generic;

    using OBeautifulCode.Type;

    using Xunit;

    public partial class ExpressionTestOp
    {
        [Fact]
        public static void Test()
        {
            // Scenario 1:  Input is NamedElementList of coscores and output is NamedElementList of set of typical entities
            // Scenario 2: Input is NamedElementList of Sales Growth and output is the median Sales Growth
            // Scenario 3: If has sales growth then sales growth over time
            const string sectionId = "section-id";

            var coscoresCell = new ConstCell<IReadOnlyList<NamedValue<decimal>>>(
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

            var intConstCell = new ConstCell<int>(4, id: "int-const");

            var quartileCell = Cell.Make.ForOperationOf.NamedDecimalSet(
                new TileOp(
                    Cell.Get.ValueOf.NamedDecimalSet(sectionId, "coscores"),
                    Cell.Get.ValueOf.Int(sectionId, "int-const")),
                null,
                id: "quartiles");

            var rows = new[]
            {
                new Row(new[] { intConstCell }),
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

            var protocolFactory = new ProtocolFactory();

            var dataStructureCalculationProtocols = new DataStructureCalculationProtocols(report, protocolFactory);

            protocolFactory.RegisterProtocol(typeof(DataStructureCalculationProtocols), () => dataStructureCalculationProtocols);

            var quartiles = protocolFactory.GetProtocolAndExecuteViaReflection<IReadOnlyList<NamedValue<decimal>>>(quartileCell.Operation);

            // todo: layer-in recalc-all
            quartileCell.RecordExecution(new CellOpExecutedEvent<IReadOnlyList<NamedValue<decimal>>>(DateTime.UtcNow, quartiles));
        }
    }
}