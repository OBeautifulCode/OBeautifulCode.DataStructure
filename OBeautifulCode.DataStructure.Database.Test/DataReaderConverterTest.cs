// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataReaderConverterTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Database.Test
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using OBeautifulCode.Database.Recipes;
    using OBeautifulCode.DataStructure.Excel;
    using OBeautifulCode.Excel.AsposeCells;
    using Xunit;

    public static partial class DataReaderConverterTest
    {
        [Fact(Skip = "For local testing only.")]
        public static void ToTreeTable___Should_convert_IDataReader_to_TreeTable___When_called()
        {
            // Arrange
            var serverName = "SERVER_NAME_HERE";

            var userName = "USER_NAME_HERE";

            var password = "PASSWORD_HERE";

            var commandText = @"COMMAND_TEXT_HERE";

            var excelWorkbookFilePath = "FILE_PATH_HERE";

            var asposeLicense = @"PASTE_LICENSE_HERE";

            var context = new DataReaderToTreeTableConversionContext
            {
                ConvertValuesToPreferredInvariantString = true,
                ColumnsFormat = new ColumnFormat(autofitColumnWidth: true, options: ColumnFormatOptions.Filterable | ColumnFormatOptions.Sortable),
                HeaderRowFormat = new RowFormat(cellsFormat: new CellFormat(fontFormat: new FontFormat(options: FontFormatOptions.Bold))),
                ColumnNameToCellFormatForValueMap = new Dictionary<string, IReadOnlyCollection<CellFormatForValue>>
                {
                    {
                        "Status",
                        new List<CellFormatForValue>
                        {
                            new CellFormatForValue(null, new CellFormat(backgroundColor: Color.LightYellow)),
                            new CellFormatForValue("Running", new CellFormat(backgroundColor: Color.LightGreen)),
                            new CellFormatForValue("Failed", new CellFormat(backgroundColor: Color.PaleVioletRed)),
                        }
                    },
                },
            };

            new AsposeCellsLicense(asposeLicense).Register();

            var connectionString = ConnectionStringHelper.BuildConnectionString(serverName, userName: userName, clearTextPassword: password);

            var reader = connectionString.ExecuteReader(commandText);

            // Act
            var actual = reader.ToTreeTable(context);

            // Assert
            var report = actual.ToReport();

            var reportToWorkbookProjectionContext = new ReportToWorkbookProjectionContext
            {
                SectionIdToWorksheetNameOverrideMap = new Dictionary<string, string>
                {
                    { report.Sections.First().Id, "export" },
                },
            };

            using (var workbook = report.ToExcelWorkbook(reportToWorkbookProjectionContext))
            {
                workbook.Save(excelWorkbookFilePath);
            }
        }
    }
}