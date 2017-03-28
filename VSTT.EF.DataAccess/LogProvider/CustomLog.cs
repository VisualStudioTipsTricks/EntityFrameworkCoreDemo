using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSTT.EF.DataAccess.LogProvider
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.IO;

    namespace EFLogging
    {
        public class CustomLogProvider : ILoggerProvider
        {
            public ILogger CreateLogger(string categoryName)
            {
                return new MyLogger();
            }

            public void Dispose()
            { }

            private class MyLogger : ILogger
            {
                public bool IsEnabled(LogLevel logLevel)
                {
                    return true;
                }

                public void Log<TState>(
                    LogLevel logLevel, 
                    EventId eventId, 
                    TState state, 
                    Exception exception, 
                    Func<TState, Exception, string> formatter)
                {
                    //File.AppendAllText(@"C:\temp\log.txt", formatter(state, exception));
                    Console.WriteLine(formatter(state, exception));
                }

                public IDisposable BeginScope<TState>(TState state)
                {
                    return null;
                }
            }
        }
    }

}
