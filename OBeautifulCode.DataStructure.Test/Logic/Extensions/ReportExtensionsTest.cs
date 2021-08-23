// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FakeItEasy;

    using MathNet.Numerics.Statistics;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Math.Recipes;
    using OBeautifulCode.Type;

    using Xunit;

    using NamedDecimalSet = System.Collections.Generic.IReadOnlyList<OBeautifulCode.Type.NamedValue<decimal>>;

    public static class ReportExtensionsTest
    {
        [Fact]
        public static void Recalc___Should_throw_ArgumentNullException___When_parameter_report_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ReportExtensions.Recalc(null, DateTime.UtcNow));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.Must().ContainString("report");
        }

        [Fact]
        public static async Task RecalcAsync___Should_throw_ArgumentNullException___When_parameter_report_is_null()
        {
            // Arrange, Act
            var actual = await Record.ExceptionAsync(() => ReportExtensions.RecalcAsync(null, DateTime.UtcNow));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.Must().ContainString("report");
        }

        [Fact]
        public static void Recalc___Should_throw_ArgumentException___When_parameter_timestampUtc_is_in_UTC_time()
        {
            // Arrange
            var report = A.Dummy<Report>();

            // Act
            var actual = Record.Exception(() => report.Recalc(DateTime.Now));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.Must().ContainString("timestampUtc");
        }

        [Fact]
        public static async Task RecalcAsync___Should_throw_ArgumentException___When_parameter_timestampUtc_is_in_UTC_time()
        {
            // Arrange
            var report = A.Dummy<Report>();

            // Act
            var actual = await Record.ExceptionAsync(() => report.RecalcAsync(DateTime.Now));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.Must().ContainString("timestampUtc");
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_availability_check_dependency_has_no_value()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            // Act
            report.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            var isForProfitCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            Record.Exception(() => isForProfitCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var restrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-1");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var partiallyRestrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-1");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_availability_check_dependency_has_no_value()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            // Act
            await report.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            var isForProfitCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            Record.Exception(() => isForProfitCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var restrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-1");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var partiallyRestrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-1");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_availability_check_dependency_disables_cells()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            var isForProfitCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));

            isForProfitCell.SetCellValue(true, DateTime.UtcNow);

            // Act
            report.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetCellObjectValue().Must().BeEqualTo((object)true);

            var restrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var partiallyRestrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_availability_check_dependency_disables_cells()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            var isForProfitCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));

            isForProfitCell.SetCellValue(true, DateTime.UtcNow);

            // Act
            await report.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetCellObjectValue().Must().BeEqualTo((object)true);

            var restrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var partiallyRestrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_availability_check_dependency_enables_cells()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            var isForProfitCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));

            isForProfitCell.SetCellValue(false, DateTime.UtcNow);

            // Act
            report.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetCellObjectValue().Must().BeEqualTo((object)false);

            var restrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-enabled");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var partiallyRestrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-enabled");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_availability_check_dependency_enables_cells()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            var isForProfitCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));

            isForProfitCell.SetCellValue(false, DateTime.UtcNow);

            // Act
            await report.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetCellObjectValue().Must().BeEqualTo((object)false);

            var restrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-enabled");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var partiallyRestrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-enabled");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_availability_check_dependency_disables_cells_having_values()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            var isForProfitCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));
            var restrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            var partiallyRestrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));

            isForProfitCell.SetCellValue(true, DateTime.UtcNow);
            restrictedCashCell.SetCellValue(123.45m, DateTime.UtcNow);
            partiallyRestrictedCashCell.SetCellValue(6789.10m, DateTime.UtcNow);

            // Act
            report.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetCellObjectValue().Must().BeEqualTo((object)true);

            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_availability_check_dependency_disables_cells_having_values()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            var isForProfitCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.IsForProfit));
            var restrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.RestrictedCash));
            var partiallyRestrictedCashCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.PartiallyRestrictedCash));

            isForProfitCell.SetCellValue(true, DateTime.UtcNow);
            restrictedCashCell.SetCellValue(123.45m, DateTime.UtcNow);
            partiallyRestrictedCashCell.SetCellValue(6789.10m, DateTime.UtcNow);

            // Act
            await report.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            // Assert
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            isForProfitCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            isForProfitCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            isForProfitCell.GetCellObjectValue().Must().BeEqualTo((object)true);

            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            restrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            restrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => restrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            partiallyRestrictedCashCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Disabled);
            partiallyRestrictedCashCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeEqualTo("got-disabled-2");
            Record.Exception(() => partiallyRestrictedCashCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_validation_dependency_has_no_value()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            // Act
            report.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            var salesFteCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.SalesFte));
            salesFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            salesFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("input required for sales");
            salesFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            salesFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            Record.Exception(() => salesFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var warehouseFteCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.WarehouseFte));
            warehouseFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            warehouseFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("input required for warehouse");
            warehouseFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            Record.Exception(() => warehouseFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var warehouseSupportFteCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.WarehouseSupportFte));
            warehouseSupportFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseSupportFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseSupportFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.NotApplicable);
            Record.Exception(() => warehouseSupportFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var totalFteCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.TotalFte));
            totalFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Aborted);
            totalFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            totalFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Aborted);
            Record.Exception(() => totalFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_validation_dependency_has_no_value()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            // Act
            await report.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            var salesFteCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.SalesFte));
            salesFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            salesFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("input required for sales");
            salesFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            salesFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            Record.Exception(() => salesFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var warehouseFteCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.WarehouseFte));
            warehouseFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            warehouseFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("input required for warehouse");
            warehouseFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            Record.Exception(() => warehouseFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var warehouseSupportFteCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.WarehouseSupportFte));
            warehouseSupportFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseSupportFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseSupportFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.NotApplicable);
            Record.Exception(() => warehouseSupportFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();

            var totalFteCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.TotalFte));
            totalFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Aborted);
            totalFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            totalFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Aborted);
            Record.Exception(() => totalFteCell.GetCellObjectValue()).AsArg().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_validation_dependency_has_invalid_values()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            var salesFteCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.SalesFte));
            var warehouseFteCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.WarehouseFte));

            salesFteCell.SetCellValue(-4m, DateTime.UtcNow);
            warehouseFteCell.SetCellValue(-5m, DateTime.UtcNow);

            // Act
            report.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            salesFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            salesFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("sales must be >= 0");
            salesFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            salesFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            salesFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-4m);

            warehouseFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            warehouseFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("warehouse must be >= 0");
            warehouseFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-5m);

            var warehouseSupportFteCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.WarehouseSupportFte));
            warehouseSupportFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseSupportFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseSupportFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            warehouseSupportFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-2.5m);

            var totalFteCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.TotalFte));
            totalFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            totalFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("total must be >= 0");
            totalFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            totalFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            totalFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-11.5m);
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_validation_dependency_has_invalid_values()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            var salesFteCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.SalesFte));
            var warehouseFteCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.WarehouseFte));

            salesFteCell.SetCellValue(-4m, DateTime.UtcNow);
            warehouseFteCell.SetCellValue(-5m, DateTime.UtcNow);

            // Act
            await report.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            salesFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            salesFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("sales must be >= 0");
            salesFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            salesFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            salesFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-4m);

            warehouseFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            warehouseFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("warehouse must be >= 0");
            warehouseFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-5m);

            var warehouseSupportFteCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.WarehouseSupportFte));
            warehouseSupportFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseSupportFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseSupportFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            warehouseSupportFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-2.5m);

            var totalFteCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.TotalFte));
            totalFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Invalid);
            totalFteCell.GetValidationMessageOrNull().AsTest().Must().BeEqualTo("total must be >= 0");
            totalFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            totalFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            totalFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)-11.5m);
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_validation_dependency_has_valid_values()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            var salesFteCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.SalesFte));
            var warehouseFteCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.WarehouseFte));

            salesFteCell.SetCellValue(4m, DateTime.UtcNow);
            warehouseFteCell.SetCellValue(5m, DateTime.UtcNow);

            // Act
            report.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            salesFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            salesFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            salesFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            salesFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            salesFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)4m);

            warehouseFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)5m);

            var warehouseSupportFteCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.WarehouseSupportFte));
            warehouseSupportFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseSupportFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseSupportFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            warehouseSupportFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)2.5m);

            var totalFteCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.TotalFte));
            totalFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            totalFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            totalFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            totalFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)11.5m);
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_validation_dependency_has_valid_values()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            var salesFteCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.SalesFte));
            var warehouseFteCell = reportCache.GetCell<IInputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.WarehouseFte));

            salesFteCell.SetCellValue(4m, DateTime.UtcNow);
            warehouseFteCell.SetCellValue(5m, DateTime.UtcNow);

            // Act
            await report.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            salesFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            salesFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            salesFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            salesFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            salesFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)4m);

            warehouseFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)5m);

            var warehouseSupportFteCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section2, CellIds.WarehouseSupportFte));
            warehouseSupportFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            warehouseSupportFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            warehouseSupportFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            warehouseSupportFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            warehouseSupportFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)2.5m);

            var totalFteCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section1, CellIds.TotalFte));
            totalFteCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            totalFteCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            totalFteCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();
            totalFteCell.GetCellOpExecutionOutcome().AsTest().Must().BeEqualTo(CellOpExecutionOutcome.Completed);
            totalFteCell.GetCellObjectValue().AsTest().Must().BeEqualTo((object)11.5m);
        }

        [Fact]
        public static void Recalc___Should_recalculate_report___When_specifying_proprietary_operation_and_protocol()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            NamedDecimalSet expectedValue = new List<NamedValue<decimal>>
            {
                new NamedValue<decimal>("lower-quartile", 4.41666666666667m),
                new NamedValue<decimal>("median", 6.5m),
                new NamedValue<decimal>("upper-quartile", 11.3333333333333m),
            };

            // Act
            report.Recalc(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            var quartileCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section3, CellIds.Quartiles));
            quartileCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            quartileCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            quartileCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            quartileCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();

            var actualValue = (NamedDecimalSet)quartileCell.GetCellObjectValue();
            actualValue.GetNames().AsTest().Must().BeEqualTo(expectedValue.GetNames());
            actualValue[0].Value.IsAlmostEqualTo(expectedValue[0].Value).AsTest().Must().BeTrue();
            actualValue[1].Value.IsAlmostEqualTo(expectedValue[1].Value).AsTest().Must().BeTrue();
            actualValue[2].Value.IsAlmostEqualTo(expectedValue[2].Value).AsTest().Must().BeTrue();
        }

        [Fact]
        public static async Task RecalcAsync___Should_recalculate_report___When_specifying_proprietary_operation_and_protocol()
        {
            // Arrange
            var report = BuildReport();

            var reportCache = new ReportCache(report);

            var protocolFactoryFuncs = new Func<IProtocolFactory, IProtocolFactory>[]
            {
                frameworkFactory => new ProprietaryProtocols(frameworkFactory).ToProtocolFactory(),
            };

            NamedDecimalSet expectedValue = new List<NamedValue<decimal>>
            {
                new NamedValue<decimal>("lower-quartile", 4.41666666666667m),
                new NamedValue<decimal>("median", 6.5m),
                new NamedValue<decimal>("upper-quartile", 11.3333333333333m),
            };

            // Act
            await report.RecalcAsync(DateTime.UtcNow, protocolFactoryFuncs);

            // Assert
            var quartileCell = reportCache.GetCell<IOperationOutputCell>(new ReportCellLocator(SectionIds.Section3, CellIds.Quartiles));
            quartileCell.GetValidity().AsTest().Must().BeEqualTo(Validity.Valid);
            quartileCell.GetValidationMessageOrNull().AsTest().Must().BeNull();
            quartileCell.GetAvailability().AsTest().Must().BeEqualTo(Availability.Enabled);
            quartileCell.GetAvailabilityCheckMessageOrNull().AsTest().Must().BeNull();

            var actualValue = (NamedDecimalSet)quartileCell.GetCellObjectValue();
            actualValue.GetNames().AsTest().Must().BeEqualTo(expectedValue.GetNames());
            actualValue[0].Value.IsAlmostEqualTo(expectedValue[0].Value).AsTest().Must().BeTrue();
            actualValue[1].Value.IsAlmostEqualTo(expectedValue[1].Value).AsTest().Must().BeTrue();
            actualValue[2].Value.IsAlmostEqualTo(expectedValue[2].Value).AsTest().Must().BeTrue();
        }

        private static Report BuildReport()
        {
            var isForProfitCell = Cell.CreateEnabledInput<bool>(CellIds.IsForProfit);

            var nonProfitAvailabilityCheck = Cell.CreateAvailabilityCheck(
                new[]
                {
                    new AvailabilityCheckStep(Cell.InThisSection(isForProfitCell.Id).HasValue(), Op.Const("got-disabled-1")),
                    new AvailabilityCheckStep(Op.Not(Cell.InThisSection(isForProfitCell.Id).GetValue<bool>()), Op.Const("got-disabled-2")),
                },
                endMessageOp: Op.Const("got-enabled"));

            var restrictedCash = Cell.CreateDisabledInput<decimal>(
                CellIds.RestrictedCash,
                availabilityCheck: nonProfitAvailabilityCheck);

            var partiallyRestrictedCash = Cell.CreateDisabledInput<decimal>(
                CellIds.PartiallyRestrictedCash,
                availabilityCheck: nonProfitAvailabilityCheck);

            var numberOfSalesFteCell = Cell.CreateEnabledInput<decimal>(
                id: CellIds.SalesFte,
                validation:
                    Cell.CreateValidation(
                        new[]
                        {
                            new ValidationStep(Cell.This().HasValue(), Op.Const("input required for sales")),
                            new ValidationStep(
                                Op.IsGreaterThanOrEqualTo(
                                    Cell.This().GetValue<decimal>(),
                                    Op.Const(0m)),
                                Op.Const("sales must be >= 0")),
                        }));

            var numberOfWarehouseFteCell = Cell.CreateEnabledInput<decimal>(
                id: CellIds.WarehouseFte,
                validation:
                    Cell.CreateValidation(
                        new[]
                        {
                            new ValidationStep(Cell.This().HasValue(), Op.Const("input required for warehouse")),
                            new ValidationStep(
                                Op.IsGreaterThanOrEqualTo(
                                    Cell.This().GetValue<decimal>(),
                                    Op.Const(0m)),
                                Op.Const("warehouse must be >= 0")),
                        }));

            var numberOfWarehouseSupportFteCell = Cell.CreateOp(
                id: CellIds.WarehouseSupportFte,
                operation: Op.IfThenElse(
                    Cell.InThisSection(numberOfWarehouseFteCell.Id).HasValue(),
                    Op.Divide(
                        Cell.InThisSection(numberOfWarehouseFteCell.Id).GetValue<decimal>(),
                        Op.Const(2m)),
                    Op.NotApplicable<decimal>()));

            var numberOfTotalFte = Cell.CreateOp(
                    id: CellIds.TotalFte,
                    operation:
                        Op.IfThenElse(
                            Op.AndAlso(
                                Cell.InThisReport(SectionIds.Section2, numberOfSalesFteCell.Id).HasValue(),
                                Cell.InThisReport(SectionIds.Section2, numberOfWarehouseFteCell.Id).HasValue()),
                            Op.Sum(
                                Cell.InThisReport(SectionIds.Section2, numberOfSalesFteCell.Id).GetValue<decimal>(),
                                Cell.InThisReport(SectionIds.Section2, numberOfWarehouseFteCell.Id).GetValue<decimal>(),
                                Cell.InThisReport(SectionIds.Section2, numberOfWarehouseSupportFteCell.Id).GetValue<decimal>()),
                            Op.Abort<decimal>("cannot perform sum")),
                    validation:
                        Cell.CreateValidation(
                            new[]
                            {
                                new ValidationStep(
                                    Op.IsEqualTo(
                                        Cell.This().GetOpExecutionOutcome(),
                                        Op.Const(CellOpExecutionOutcome.Completed)),
                                    falseAction: ValidationStepAction.StopToAbort),
                                new ValidationStep(
                                    Op.IsGreaterThan(
                                        Cell.This().GetValue<decimal>(),
                                        Op.Const(0m)),
                                    Op.Const("total must be >= 0")),
                            }));

            var scoresCell = Cell.CreateConst<NamedDecimalSet>(
                new[]
                {
                    new NamedValue<decimal>("bob", 5),
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
                id: CellIds.CoScores);

            var scoresCellCopy = Cell.CreateOp(
                Cell.InThisSection(scoresCell.Id).GetValue<NamedDecimalSet>(),
                id: CellIds.CoScoresCopy);

            var intConstCell = new ConstCell<int>(4, id: CellIds.NumberOfTiles);

            var quartileCell = Cell.CreateOp(
                new TileOp(
                    Cell.InThisSection(scoresCellCopy.Id).GetValue<NamedDecimalSet>(),
                    Cell.InThisSection(intConstCell.Id).GetValue<int>()),
                id: CellIds.Quartiles);

            var rows1 = new[]
            {
                new Row(new[] { isForProfitCell }),
                new Row(new[] { partiallyRestrictedCash }),
                new Row(new[] { restrictedCash }),
                new Row(new[] { numberOfTotalFte }),
            };

            var rows2 = new[]
            {
                new Row(new[] { numberOfSalesFteCell }),
                new Row(new[] { numberOfWarehouseFteCell }),
                new Row(new[] { numberOfWarehouseSupportFteCell }),
            };

            var rows3 = new[]
            {
                new Row(new[] { intConstCell }),
                new Row(new[] { scoresCellCopy }),
                new Row(new[] { scoresCell }),
                new Row(new[] { quartileCell }),
            };

            var dataRows1 = new DataRows(rows1);
            var dataRows2 = new DataRows(rows2);
            var dataRows3 = new DataRows(rows3);

            var tableRows1 = new TableRows(dataRows: dataRows1);
            var tableRows2 = new TableRows(dataRows: dataRows2);
            var tableRows3 = new TableRows(dataRows: dataRows3);

            var columns1 = new[]
            {
                new Column("the-only-column"),
            };

            var columns2 = new[]
            {
                new Column("the-only-column"),
            };

            var columns3 = new[]
            {
                new Column("the-only-column"),
            };

            var tableColumns1 = new TableColumns(columns1);
            var tableColumns2 = new TableColumns(columns2);
            var tableColumns3 = new TableColumns(columns3);

            var treeTable1 = new TreeTable(tableColumns1, tableRows1);
            var treeTable2 = new TreeTable(tableColumns2, tableRows2);
            var treeTable3 = new TreeTable(tableColumns3, tableRows3);

            var section1 = new Section(SectionIds.Section1, treeTable1);
            var section2 = new Section(SectionIds.Section2, treeTable2);
            var section3 = new Section(SectionIds.Section3, treeTable3);

            var sections = new[]
            {
                section1,
                section2,
                section3,
            };

            var result = new Report("report-id", sections);

            return result;
        }

        private static class SectionIds
        {
            public const string Section1 = "section-1";

            public const string Section2 = "section-2";

            public const string Section3 = "section-3";
        }

        private static class CellIds
        {
            public const string IsForProfit = "is-for-profit";

            public const string RestrictedCash = "restricted-cash";

            public const string PartiallyRestrictedCash = "partially-restricted-cash";

            public const string SalesFte = "sales-fte";

            public const string WarehouseFte = "warehouse-fte";

            public const string WarehouseSupportFte = "warehouse-support-fte";

            public const string TotalFte = "total-fte";

            public const string CoScores = "scores";

            public const string CoScoresCopy = "scores-copy";

            public const string NumberOfTiles = "number-of-tiles";

            public const string Quartiles = "quartiles";
        }

        private class TileOp : IReturningOperation<NamedDecimalSet>
        {
            public TileOp(
                IReturningOperation<NamedDecimalSet> setOp,
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

            public IReturningOperation<NamedDecimalSet> SetOp { get; private set; }
        }

        private class ProprietaryProtocols : ISyncReturningProtocol<TileOp, NamedDecimalSet>
        {
            private readonly ISyncReturningProtocol<GetProtocolOp, IProtocol> protocolFactory;

            public ProprietaryProtocols(
                IProtocolFactory protocolFactory)
            {
                if (protocolFactory == null)
                {
                    throw new ArgumentNullException(nameof(protocolFactory));
                }

                this.protocolFactory = protocolFactory;
            }

            public NamedDecimalSet Execute(
                TileOp operation)
            {
                var set = this.protocolFactory.GetProtocolAndExecuteViaReflection<NamedDecimalSet>(operation.SetOp);

                var tiles = this.protocolFactory.GetProtocolAndExecuteViaReflection<int>(operation.NumberOfTilesOp);

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
}
