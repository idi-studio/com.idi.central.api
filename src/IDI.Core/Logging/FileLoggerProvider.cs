using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace IDI.Core.Logging
{
    public class FileLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger();
        }

        public void Dispose()
        {

        }

        private class FileLogger : ILogger
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                File.AppendAllText(@"d:\test\log.txt", formatter(state, exception));
                Console.WriteLine(formatter(state, exception));

                //var path = GetFilePath();

                //File.AppendAllText(path, formatter(state, exception));
            }

            private string GetFilePath()
            {
                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), @"logs");

                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.log";

                string filePath = Path.Combine(directoryPath, fileName);

                if (!File.Exists(filePath))
                    File.Create(filePath);

                return filePath;
            }
        }
    }
}
