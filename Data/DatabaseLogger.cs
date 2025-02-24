using ExceptionLogsTask.Models;
using System.Data.SqlClient;

namespace ExceptionLogsTask.Data
{
    public class DatabaseLogger
    {
        private readonly string _connectionString;

        public DatabaseLogger(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void LogError(string controllerName, string actionName, Exception exception)
        {
            var errorLog = new ErrorLog
            {
                ControllerName = controllerName,
                ActionName = actionName,
                ExceptionMessage = exception.Message,
                StackTrace = exception.StackTrace,
                LogDate = DateTime.UtcNow
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO ErrorLogs (ControllerName, ActionName, ExceptionMessage, StackTrace, LogDate) VALUES (@Controller, @Action, @Message, @StackTrace, @LogDate)", connection);
                command.Parameters.AddWithValue("@Controller", errorLog.ControllerName);
                command.Parameters.AddWithValue("@Action", errorLog.ActionName);
                command.Parameters.AddWithValue("@Message", errorLog.ExceptionMessage);
                command.Parameters.AddWithValue("@StackTrace", errorLog.StackTrace);
                command.Parameters.AddWithValue("@LogDate", errorLog.LogDate);
                command.ExecuteNonQuery();
            }
        }
    }
}
