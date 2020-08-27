using System;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace jasondel.Tools
{
    public static class Logger
    {
        /// <summary>
        /// Logs the message to stdout unless an exception is provided and it
        /// will log to stderr. 
        /// </summary>
        public static void Log(string message, Exception e = null)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            StringBuilder sb = new StringBuilder();
            TextWriter writer = null;
            
            sb.AppendLine(message);
            if (e != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                writer = Console.Error;

                sb.AppendLine("\tERROR: " + e.Message);
                if (e.InnerException != null)
                    sb.AppendLine("\tINNER ERROR: " + e.InnerException.Message);
            }
            else
            {
                writer = Console.Out;                
            }

            InternalLog(writer, sb.ToString());
            
            // Assign color back.
            if (Console.ForegroundColor != defaultColor)
                Console.ForegroundColor = defaultColor;
        }

        private static void InternalLog(TextWriter writer, string message, int stackPosition = 2)
        {
            const string DateFormat = "MM/dd/yyyy hh:mm:ss.ff";
            try
            {
                StackTrace stackTrace = new StackTrace();

                string method = stackTrace.GetFrame(stackPosition).GetMethod().Name;
                string type = stackTrace.GetFrame(stackPosition).GetMethod().ReflectedType.Name;
                writer.Write($"[{DateTime.Now.ToString(DateFormat)}, {type}:{method}] {message}");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[{DateTime.Now.ToString(DateFormat)} (log failure)] {message}");
                Console.Error.WriteLine($"\t{e.Message}");
            }
        }
    }
 }
