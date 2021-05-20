using System;
using System.Text;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Google.Cloud.Logging.Console;

namespace jasondel.Tools
{
    public class Logger
    {
        static ILogger<Logger> _logger;

        static Logger()
        {
            using ILoggerFactory loggerFactory =
                LoggerFactory.Create(builder =>
                {
                    builder.AddConsoleFormatter<GoogleCloudConsoleFormatter, GoogleCloudConsoleFormatterOptions>(options => 
                        options.IncludeScopes = true);
                    builder.AddConsole(options => 
                        options.FormatterName = nameof(GoogleCloudConsoleFormatter));
                });

            _logger = loggerFactory.CreateLogger<Logger>();         
        }

        public static void Log(string message,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            InternalLog(message, memberName: memberName);
        }

        public static void Log(string message, Exception e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message);
            if (e != null)
                sb.Append("\tERROR: " + e.Message);
            if (e.InnerException != null)
                sb.Append("\n\tINNER ERROR: " + e.InnerException.Message);
            
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            InternalLog(sb.ToString());
            Console.ForegroundColor = defaultColor;
        }

        private static void InternalLog(string message, int stackPosition = 2, string memberName = null)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();

                string method = memberName ?? stackTrace.GetFrame(stackPosition).GetMethod().Name;
                string type = stackTrace.GetFrame(stackPosition).GetMethod().ReflectedType.Name;

                _logger.LogInformation("{type}:{method} {message}", type, method, message);
            }
            catch (Exception e)
            {
                _logger.LogCritical("(Failure trying to log:) {message}\n\tInner Exception:{exception}", 
                    message, e.Message);
            }
        }
    }
 }
