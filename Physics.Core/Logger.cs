using System.Runtime.CompilerServices;

namespace Physics.Core
{
    public class Logger
    {
        private readonly string? _logFile;
        private readonly bool _useConsole;
        private static readonly object _lock = new();

        public Logger(string? logFile = null, bool useConsole = true)
        {
            _logFile = logFile;
            _useConsole = useConsole;
        }

        public enum LogLevel
        {
            DEBUG,
            INFO,
            WARNING,
            ERROR,
            CRITICAL
        }

        private static ConsoleColor GetColorForLevel(LogLevel level) => level switch
        {
            LogLevel.DEBUG => ConsoleColor.Gray,
            LogLevel.INFO => ConsoleColor.Green,
            LogLevel.WARNING => ConsoleColor.Yellow,
            LogLevel.ERROR => ConsoleColor.Red,
            LogLevel.CRITICAL => ConsoleColor.DarkRed,
            _ => ConsoleColor.White
        };

        public void Log(LogLevel level, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var logMessage = $"[{timestamp}] [{level}] [{Path.GetFileName(sourceFilePath)}:{lineNumber}] [{memberName}] {message}";

            lock (_lock)
            {
                if (_useConsole)
                {
                    var originalColor = Console.ForegroundColor;
                    Console.ForegroundColor = GetColorForLevel(level);
                    Console.Write($"[{level}] ");
                    Console.ForegroundColor = originalColor;
                    Console.WriteLine(message);
                }

                if (_logFile != null)
                {
                    var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _logFile);
                    File.AppendAllText(logPath, logMessage + Environment.NewLine);
                }
            }
        }

        // Convenience methods
        public void Debug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int lineNumber = 0)
            => Log(LogLevel.DEBUG, message, memberName, sourceFilePath, lineNumber);

        public void Info(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int lineNumber = 0)
            => Log(LogLevel.INFO, message, memberName, sourceFilePath, lineNumber);

        public void Warning(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int lineNumber = 0)
            => Log(LogLevel.WARNING, message, memberName, sourceFilePath, lineNumber);

        public void Error(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int lineNumber = 0)
            => Log(LogLevel.ERROR, message, memberName, sourceFilePath, lineNumber);

        public void Critical(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int lineNumber = 0)
            => Log(LogLevel.CRITICAL, message, memberName, sourceFilePath, lineNumber);
    }
}