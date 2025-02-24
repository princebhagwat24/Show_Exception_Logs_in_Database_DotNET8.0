using Microsoft.Extensions.Logging;
using ExceptionLogsTask.Data;

namespace ExceptionLogsTask.Logging
{
    public class DatabaseLoggerProvider : ILoggerProvider
    {
        private readonly string _connectionString;

        public DatabaseLoggerProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLoggerService(_connectionString);
        }

        public void Dispose() { }
    }

    public class DatabaseLoggerService : ILogger
    {
        private readonly string _connectionString;

        public DatabaseLoggerService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel == LogLevel.Error;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel == LogLevel.Error && exception != null)
            {
                var controllerName = state?.ToString();
                var actionName = "SampleAction"; // Use dynamic values as needed

                var logger = new DatabaseLogger(_connectionString);
                logger.LogError(controllerName, actionName, exception);
            }
        }
    }
}
