using System;
using System.Text;
using System.Diagnostics;

namespace jasondel.Tools
{
    public static class Logger
    {
        public static void Log(string message)
        {
            InternalLog(message);
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

        private static void InternalLog(string message, int stackPosition = 2)
        {
            const string DateFormat = "MM/dd/yyyy hh:mm:ss.ffff";
            try
            {
                StackTrace stackTrace = new StackTrace();

                string method = stackTrace.GetFrame(stackPosition).GetMethod().Name;
                string type = stackTrace.GetFrame(stackPosition).GetMethod().ReflectedType.Name;
                Console.WriteLine($"[{DateTime.Now.ToString(DateFormat)}, {type}:{method}] {message}");            
            }
            catch (Exception e)
            {
                Console.WriteLine($"[{DateTime.Now.ToString(DateFormat)} (log failure)] {message}");
                Console.WriteLine($"\t{e.Message}");
            }
        }
    }
 }
