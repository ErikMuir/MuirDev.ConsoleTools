using System;

namespace MuirDev.ConsoleTools
{
    /// <summary>
    /// Collection of properties controlling Log behavior.
    /// </summary>
    public class LogOptions
    {
        public LogOptions() { }

        public LogOptions(ConsoleColor foregroundColor, bool isEndOfLine = true)
        {
            ForegroundColor = foregroundColor;
            IsEndOfLine = isEndOfLine;
        }

        public LogOptions(ConsoleColor foregroundColor, ConsoleColor backgroundColor, bool isEndOfLine = true)
        {
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            IsEndOfLine = isEndOfLine;
        }

        /// <summary>
        /// Determines whether or not a newline character is appended to the message. Defaults to true.
        /// </summary>
        public bool IsEndOfLine { get; set; } = true;

        /// <summary>
        /// Overrides the foreground color to be used for the log.
        /// </summary>
        public ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// Overrides the background color to be used for the log.
        /// </summary>
        public ConsoleColor? BackgroundColor { get; set; }
    }
}
