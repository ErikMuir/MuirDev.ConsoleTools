using System;

namespace MuirDev.ConsoleTools
{
    public static class ConsoleTools
    {
        /// <summary>
        /// Logs a specified <paramref>count</paramref> of newline characters to the console.
        /// </summary>
        /// <param name="count">The desired number of newlines to output to the console. Defaults to 1.</param>
        public static void LineFeed(int count = 1)
        {
            while (count-- > 0) Console.WriteLine();
        }

        /// <summary>
        /// Returns a horizontal line to be used as a separator.
        /// </summary>
        public static string Separator => new string('-', Console.BufferWidth - 1);

        /// <summary>
        /// Outputs a horizontal line to be used as a separator.
        /// </summary>
        public static void LogSeparator() => Log(Separator, LogType.Info, new LogOptions());

        /// <summary>
        /// Outputs a horizontal line to be used as a separator.
        /// </summary>
        /// <param name="type">The desired log type.</param>
        public static void LogSeparator(LogType type) => Log(Separator, type, new LogOptions());

        /// <summary>
        /// Outputs a horizontal line to be used as a separator.
        /// </summary>
        /// <param name="type">The desired log type.</param>
        /// <param name="options">The log options to be used.</param>
        public static void LogSeparator(LogType type, LogOptions options) => Log(Separator, type, options);

        /// <summary>
        /// Sets the colors of the console output based on the desired log type.
        /// </summary>
        public static void SetColor() => SetColor(LogType.Info, new LogOptions());

        /// <summary>
        /// Sets the colors of the console output based on the desired log type.
        /// </summary>
        /// <param name="type">The desired log type.</param>
        public static void SetColor(LogType type) => SetColor(type, new LogOptions());

        /// <summary>
        /// Sets the colors of the console output based on the desired log type and log options.
        /// </summary>
        /// <param name="type">The desired log type.</param>
        /// <param name="options">The log options to be used.</param>
        public static void SetColor(LogType type, LogOptions options)
        {
            Console.ForegroundColor = options?.ForegroundColor ?? GetTypeColor(type);
            Console.BackgroundColor = options?.BackgroundColor ?? ConsoleColor.Black;
        }

        /// <summary>
        /// Returns the text color associated with the provided <paramref>type</paramref>.
        /// </summary>
        /// <param name="type">The desired log type.</param>
        public static ConsoleColor GetTypeColor(LogType type) => type switch
        {
            LogType.Info => ConsoleColor.Gray,
            LogType.Success => ConsoleColor.DarkGreen,
            LogType.Warning => ConsoleColor.DarkYellow,
            LogType.Failure => ConsoleColor.DarkRed,
            _ => throw new NotImplementedException(),
        };

        /// <summary>
        /// Logs an Info <paramref>message</paramref> to the console.
        /// </summary>
        /// <param name="message">The message to be output to the console.</param>
        public static void Info(string message) => Log(message, LogType.Info, new LogOptions());

        /// <summary>
        /// Logs an Info <paramref>message</paramref> to the console using provided <paramref>options</paramref>.
        /// </summary>
        /// <param name="message">The message to be output to the console.</param>
        /// <param name="options">Log options to override default logging behavior.</param>
        public static void Info(string message, LogOptions options) => Log(message, LogType.Info, options);

        /// <summary>
        /// Logs a Success <paramref>message</paramref> to the console.
        /// </summary>
        /// <param name="message">The message to be output to the console.</param>
        public static void Success(string message) => Log(message, LogType.Success, new LogOptions());

        /// <summary>
        /// Logs a Success <paramref>message</paramref> to the console using provided <paramref>options</paramref>.
        /// </summary>
        /// <param name="message">The message to be output to the console.</param>
        /// <param name="options">Log options to override default logging behavior.</param>
        public static void Success(string message, LogOptions options) => Log(message, LogType.Success, options);

        /// <summary>
        /// Logs a Warning <paramref>message</paramref> to the console.
        /// </summary>
        /// <param name="message">The message to be output to the console.</param>
        public static void Warning(string message) => Log(message, LogType.Warning, new LogOptions());

        /// <summary>
        /// Logs a Warning <paramref>message</paramref> to the console using provided <paramref>options</paramref>.
        /// </summary>
        /// <param name="message">The message to be output to the console.</param>
        /// <param name="options">Log options to override default logging behavior.</param>
        public static void Warning(string message, LogOptions options) => Log(message, LogType.Warning, options);

        /// <summary>
        /// Logs a Failure <paramref>message</paramref> to the console.
        /// </summary>
        /// <param name="message">The message to be output to the console.</param>
        public static void Failure(string message) => Log(message, LogType.Failure, new LogOptions());

        /// <summary>
        /// Logs a Failure <paramref>message</paramref> to the console using provided <paramref>options</paramref>.
        /// </summary>
        /// <param name="message">The message to be output to the console.</param>
        /// <param name="options">Log options to override default logging behavior.</param>
        public static void Failure(string message, LogOptions options) => Log(message, LogType.Failure, options);

        /// <summary>
        /// Logs a <paramref>message</paramref> to the console.
        /// </summary>
        /// <param name="message">The message to be output to the console.</param>
        public static void Log(string message) => Log(message, LogType.Info, new LogOptions());

        /// <summary>
        /// Logs a <paramref>message</paramref> of the specified <paramref>type</paramref> to the console.
        /// </summary>
        /// <param name="message">The message to be output to the console.</param>
        /// <param name="type">The log type of the message.</param>
        public static void Log(string message, LogType type) => Log(message, type, new LogOptions());

        /// <summary>
        /// Logs a <paramref>message</paramref> of the specified <paramref>type</paramref> to the console using provided <paramref>options</paramref>.
        /// </summary>
        /// <param name="message">The message to be output to the console.</param>
        /// <param name="type">The log type of the message.</param>
        /// <param name="options">Log options to override default logging behavior.</param>
        public static void Log(string message, LogType type, LogOptions options)
        {
            SetColor(type, options);
            Console.Write(message);
            if (options?.IsEndOfLine ?? true)
                Console.WriteLine();
            Console.ResetColor();
        }
    }
}
