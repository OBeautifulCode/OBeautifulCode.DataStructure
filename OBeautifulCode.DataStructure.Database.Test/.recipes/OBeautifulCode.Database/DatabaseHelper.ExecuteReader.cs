// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseHelper.ExecuteReader.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Database.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Database.Recipes
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Threading.Tasks;

    using OBeautifulCode.CodeAnalysis.Recipes;

    using static global::System.FormattableString;

#if !OBeautifulCodeDatabaseSolution
    internal
#else
    public
#endif
    static partial class DatabaseHelper
    {
        /// <summary>
        /// Opens a connection to the database, executes the <paramref name="commandText"/>, and builds an <see cref="SqlDataReader"/>.
        /// </summary>
        /// <param name="connectionString">String used to open a connection to the database.</param>
        /// <param name="commandText">The SQL statement, table name, or stored procedure to execute at the data source.</param>
        /// <param name="commandTimeoutInSeconds">OPTIONAL value with the wait time, in seconds, before terminating an attempt to execute the command and generating an error.  DEFAULT is 30 seconds.  A value of 0 indicates no limit (an attempt to execute a command will wait indefinitely).</param>
        /// <param name="commandParameters">OPTIONAL set of parameters to associate with the command.  DEFAULT is null (no parameters).</param>
        /// <param name="commandType">OPTIONAL value that determines how the command text is to be interpreted.  DEFAULT is <see cref="CommandType.Text"/>; a SQL text command.</param>
        /// <param name="commandBehavior">OPTIONAL value providing a description of the results of the query and its effect on the database.  DEFAULT is <see cref="CommandBehavior.Default"/>; the query may return multiple result sets and execution of the query may affect the database state.  This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</param>
        /// <param name="prepareCommand">OPTIONAL value indicating whether to prepared (or compile) the command on the data source.</param>
        /// <param name="sqlInfoMessageEventHandler">OPTIONAL method that will handle the <see cref="SqlConnection.InfoMessage"/> event.</param>
        /// <remarks>
        /// If an expected parameter type does not match an actual parameter value's type, ExecuteReader() does not throw <see cref="SqlException"/>.
        /// Instead, a reader with no rows is returned.  Any attempt to Read() will throw an exception.
        /// </remarks>
        /// <returns>
        /// A constructed <see cref="SqlDataReader"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = ObcSuppressBecause.CA2000_DisposeObjectsBeforeLosingScope_MethodCreatesDisposableObjectButItCannotBeDisposedBecauseReturnObjectRequiresDisposableObjectToBeFullyIntact)]
        public static SqlDataReader ExecuteReader(
            this string connectionString,
            string commandText,
            int commandTimeoutInSeconds = 30,
            IReadOnlyList<SqlParameter> commandParameters = null,
            CommandType commandType = CommandType.Text,
            CommandBehavior commandBehavior = CommandBehavior.CloseConnection,
            bool prepareCommand = false,
            SqlInfoMessageEventHandler sqlInfoMessageEventHandler = null)
        {
            if (!commandBehavior.HasFlag(CommandBehavior.CloseConnection))
            {
                throw new ArgumentException(Invariant($"{nameof(commandBehavior)} does not set the flag {CommandBehavior.CloseConnection}.  This will result in an open connection with the caller having no means of closing it."));
            }

            var connection = OpenSqlConnection(connectionString, sqlInfoMessageEventHandler);

            var result = connection.ExecuteReader(commandText, commandTimeoutInSeconds, commandParameters, commandType, null, commandBehavior, prepareCommand);

            return result;
        }

        /// <summary>
        /// Executes the <paramref name="commandText"/> against the <paramref name="connection"/> and builds an <see cref="SqlDataReader"/>.
        /// </summary>
        /// <param name="connection">An open connection to the database.</param>
        /// <param name="commandText">The SQL statement, table name, or stored procedure to execute at the data source.</param>
        /// <param name="commandTimeoutInSeconds">OPTIONAL value with the wait time, in seconds, before terminating an attempt to execute the command and generating an error.  DEFAULT is 30 seconds.  A value of 0 indicates no limit (an attempt to execute a command will wait indefinitely).</param>
        /// <param name="commandParameters">OPTIONAL set of parameters to associate with the command.  DEFAULT is null (no parameters).</param>
        /// <param name="commandType">OPTIONAL value that determines how the command text is to be interpreted.  DEFAULT is <see cref="CommandType.Text"/>; a SQL text command.</param>
        /// <param name="transaction">OPTIONAL transaction within which the command will execute.  DEFAULT is null (no transaction).</param>
        /// <param name="commandBehavior">OPTIONAL value providing a description of the results of the query and its effect on the database.  DEFAULT is <see cref="CommandBehavior.Default"/>; the query may return multiple result sets and execution of the query may affect the database state.  This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</param>
        /// <param name="prepareCommand">OPTIONAL value indicating whether to prepared (or compile) the command on the data source.</param>
        /// <remarks>
        /// If an expected parameter type does not match an actual parameter value's type, ExecuteReader() does not throw <see cref="SqlException"/>.
        /// Instead, a reader with no rows is returned.  Any attempt to Read() will throw an exception.
        /// </remarks>
        /// <returns>
        /// A constructed <see cref="SqlDataReader"/>.
        /// </returns>
        public static SqlDataReader ExecuteReader(
            this SqlConnection connection,
            string commandText,
            int commandTimeoutInSeconds = 30,
            IReadOnlyList<SqlParameter> commandParameters = null,
            CommandType commandType = CommandType.Text,
            SqlTransaction transaction = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            bool prepareCommand = false)
        {
            using (var command = connection.BuildSqlCommand(commandText, commandTimeoutInSeconds, commandParameters, commandType, transaction, prepareCommand))
            {
                var result = command.ExecuteReader(commandBehavior);  // can throw SqlException

                return result;
            }
        }

        /// <summary>
        /// Opens a connection to the database, executes the <paramref name="commandText"/>, and builds an <see cref="SqlDataReader"/>.
        /// </summary>
        /// <param name="connectionString">String used to open a connection to the database.</param>
        /// <param name="commandText">The SQL statement, table name, or stored procedure to execute at the data source.</param>
        /// <param name="commandTimeoutInSeconds">OPTIONAL value with the wait time, in seconds, before terminating an attempt to execute the command and generating an error.  DEFAULT is 30 seconds.  A value of 0 indicates no limit (an attempt to execute a command will wait indefinitely).</param>
        /// <param name="commandParameters">OPTIONAL set of parameters to associate with the command.  DEFAULT is null (no parameters).</param>
        /// <param name="commandType">OPTIONAL value that determines how the command text is to be interpreted.  DEFAULT is <see cref="CommandType.Text"/>; a SQL text command.</param>
        /// <param name="commandBehavior">OPTIONAL value providing a description of the results of the query and its effect on the database.  DEFAULT is <see cref="CommandBehavior.Default"/>; the query may return multiple result sets and execution of the query may affect the database state.  This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</param>
        /// <param name="prepareCommand">OPTIONAL value indicating whether to prepared (or compile) the command on the data source.</param>
        /// <param name="sqlInfoMessageEventHandler">OPTIONAL method that will handle the <see cref="SqlConnection.InfoMessage"/> event.</param>
        /// <remarks>
        /// If an expected parameter type does not match an actual parameter value's type, ExecuteReader() does not throw <see cref="SqlException"/>.
        /// Instead, a reader with no rows is returned.  Any attempt to Read() will throw an exception.
        /// </remarks>
        /// <returns>
        /// A constructed <see cref="SqlDataReader"/>.
        /// </returns>
        public static async Task<SqlDataReader> ExecuteReaderAsync(
            this string connectionString,
            string commandText,
            int commandTimeoutInSeconds = 30,
            IReadOnlyList<SqlParameter> commandParameters = null,
            CommandType commandType = CommandType.Text,
            CommandBehavior commandBehavior = CommandBehavior.CloseConnection,
            bool prepareCommand = false,
            SqlInfoMessageEventHandler sqlInfoMessageEventHandler = null)
        {
            if (!commandBehavior.HasFlag(CommandBehavior.CloseConnection))
            {
                throw new ArgumentException(Invariant($"{nameof(commandBehavior)} does not set the flag {CommandBehavior.CloseConnection}.  This will result in an open connection with the caller having no means of closing it."));
            }

            var connection = await OpenSqlConnectionAsync(connectionString, sqlInfoMessageEventHandler);

            var result = await connection.ExecuteReaderAsync(commandText, commandTimeoutInSeconds, commandParameters, commandType, null, commandBehavior, prepareCommand);

            return result;
        }

        /// <summary>
        /// Executes the <paramref name="commandText"/> against the <paramref name="connection"/> and builds an <see cref="SqlDataReader"/>.
        /// </summary>
        /// <param name="connection">An open connection to the database.</param>
        /// <param name="commandText">The SQL statement, table name, or stored procedure to execute at the data source.</param>
        /// <param name="commandTimeoutInSeconds">OPTIONAL value with the wait time, in seconds, before terminating an attempt to execute the command and generating an error.  DEFAULT is 30 seconds.  A value of 0 indicates no limit (an attempt to execute a command will wait indefinitely).</param>
        /// <param name="commandParameters">OPTIONAL set of parameters to associate with the command.  DEFAULT is null (no parameters).</param>
        /// <param name="commandType">OPTIONAL value that determines how the command text is to be interpreted.  DEFAULT is <see cref="CommandType.Text"/>; a SQL text command.</param>
        /// <param name="transaction">OPTIONAL transaction within which the command will execute.  DEFAULT is null (no transaction).</param>
        /// <param name="commandBehavior">OPTIONAL value providing a description of the results of the query and its effect on the database.  DEFAULT is <see cref="CommandBehavior.Default"/>; the query may return multiple result sets and execution of the query may affect the database state.  This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</param>
        /// <param name="prepareCommand">OPTIONAL value indicating whether to prepared (or compile) the command on the data source.</param>
        /// <remarks>
        /// If an expected parameter type does not match an actual parameter value's type, ExecuteReader() does not throw <see cref="SqlException"/>.
        /// Instead, a reader with no rows is returned.  Any attempt to Read() will throw an exception.
        /// </remarks>
        /// <returns>
        /// A constructed <see cref="SqlDataReader"/>.
        /// </returns>
        public static async Task<SqlDataReader> ExecuteReaderAsync(
            this SqlConnection connection,
            string commandText,
            int commandTimeoutInSeconds = 30,
            IReadOnlyList<SqlParameter> commandParameters = null,
            CommandType commandType = CommandType.Text,
            SqlTransaction transaction = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            bool prepareCommand = false)
        {
            using (var command = connection.BuildSqlCommand(commandText, commandTimeoutInSeconds, commandParameters, commandType, transaction, prepareCommand))
            {
                var result = await command.ExecuteReaderAsync(commandBehavior);  // can throw SqlException

                return result;
            }
        }
    }
}