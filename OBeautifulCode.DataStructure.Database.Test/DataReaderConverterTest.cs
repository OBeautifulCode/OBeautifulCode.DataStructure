// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataReaderConverterTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Database.Test
{
    using OBeautifulCode.Database.Recipes;
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

            var commandText = "COMMAND_TEXT_HERE";

            var connectionString = ConnectionStringHelper.BuildConnectionString(serverName, userName: userName, clearTextPassword: password);

            var reader = connectionString.ExecuteReader(commandText);

            // Act
            var actual = reader.ToTreeTable();

            // Assert
        }
    }
}