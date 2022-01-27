// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportProjectorTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Excel.Test
{
    using System.IO;
    using System.Text;
    using Aspose.Cells;
    using OBeautifulCode.DataStructure.Serialization.Json;
    using OBeautifulCode.Serialization.Json;
    using Xunit;

    public static partial class ReportProjectorTest
    {
        [Fact(Skip = "For local testing only.")]
        public static void ToExcelWorkbook___Should_project_report_into_Excel_Workbook___When_called()
        {
            // Arrange
            var reportSerializedJsonFile = "FILE_PATH_HERE";

            var excelWorkbookFilePath = "FILE_PATH_HERE";

            var asposeLicense = @"PASTE_LICENSE_HERE";

            using (var licenseStream = new MemoryStream(Encoding.ASCII.GetBytes(asposeLicense)))
            {
                new License().SetLicense(licenseStream);
            }

            var serializer = new ObcJsonSerializer<DependencyOnlyJsonSerializationConfiguration<DataStructureJsonSerializationConfiguration>>();

            var report = serializer.Deserialize<Report>(File.ReadAllText(reportSerializedJsonFile));

            if (report.AdditionalInfo == null)
            {
                report = report.DeepCloneWithAdditionalInfo(new AdditionalReportInfo("Copyright (c) 2021 The Company. All rights reserved.", "This file is for authorized users only.  All data is private and confidential to its owner and is only viewable by the owner's authorized parties."));
            }

            File.Delete(excelWorkbookFilePath);

            // Act
            var workbook = report.ToExcelWorkbook();

            // Assert
            workbook.Save(excelWorkbookFilePath);

            // Cleanup
            workbook.Dispose();
        }
    }
}