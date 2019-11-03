using System;

namespace MuirDev.ConsoleTools
{
    /// <summary>
    /// Collection of properties controlling Log behavior.
    /// </summary>
    public class LogOptions
    {
        /// <summary>
        /// Determines whether or not a newline character is appended to the message.
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
