using System;
using System.Collections.Generic;

namespace MuirDev.ConsoleTools
{
    /// <summary>
	/// Represents a list of options to be presented to the user.
	/// </summary>
    public class Menu
    {
        private readonly string _title;
        private readonly string _detail;
        private readonly IDictionary<char, string> _options;


        /// <summary>
        /// Initializes a new instance of the <c>Menu</c> class.
        /// </summary>
        /// <param name="options">
        /// A dictionary of options that are displayed to the user.
        /// Each option consists of the <c>char</c> used to select it and its <c>string</c> description.
        /// </param>
        public Menu(IDictionary<char, string> options)
        {
            if (options.Count <= 1)
                throw new ArgumentException("Menus must contain at least 2 options.");

            _options = options;
        }

        /// <summary>
        /// Initializes a new instance of the <c>Menu</c> class.
        /// </summary>
        /// <param name="options">
        /// A dictionary of options that are displayed to the user.
		/// Each option consists of the <c>char</c> used to select it and its <c>string</c> description.
        /// </param>
        /// <param name="title">The menu's title that is displayed at the top of the menu.</param>
        public Menu(IDictionary<char, string> options, string title)
        {
            if (options.Count < 2)
                throw new ArgumentException("Menus must contain at least 2 options.");

            _options = options;
            _title = title;
        }

        /// <summary>
        /// Initializes a new instance of the <c>Menu</c> class.
        /// </summary>
        /// <param name="options">
        /// A dictionary of options that are displayed to the user.
		/// Each option consists of the <c>char</c> used to select it and its <c>string</c> description.
        /// </param>
        /// <param name="title">The menu's title that is displayed at the top of the menu.</param>
        /// <param name="detail">Any details related to the menu that are displayed between the title and options.</param>
        public Menu(IDictionary<char, string> options, string title, string detail)
        {
            if (options.Count < 2)
                throw new ArgumentException("Menus must contain at least 2 options.");

            _options = options;
            _title = title;
            _detail = detail;
        }


        /// <summary>
        /// Displays the menu, prompts the user for a response, and returns the response.
        /// </summary>
        public char Run() => Run(LogType.Info, new LogOptions());

        /// <summary>
        /// Displays the menu, prompts the user for a response, and returns the response.
        /// </summary>
        /// <param name="type">The log type of the menu.</param>
        public char Run(LogType type) => Run(type, new LogOptions());

        /// <summary>
        /// Displays the menu, prompts the user for a response, and returns the response.
        /// </summary>
        /// <param name="type">The log type of the menu.</param>
        /// <param name="options">Log options to override default logging behavior.</param>
        public char Run(LogType type, LogOptions options)
        {
            options = options ?? new LogOptions();
            options.IsEndOfLine = true;

            ConsoleTools.LogSeparator(type, options);
            if (!string.IsNullOrWhiteSpace(_title))
            {
                ConsoleTools.Log($" {_title}", type, options);
                ConsoleTools.LogSeparator(type, options);
            }
            if (!string.IsNullOrWhiteSpace(_detail))
            {
                ConsoleTools.Log(_detail, type, options);
                Console.WriteLine();
            }
            foreach (KeyValuePair<char, string> option in _options)
            {
                ConsoleTools.Log($"  {option.Key} - {option.Value}", type, options);
            }
            ConsoleTools.LogSeparator(type, options);
            Console.WriteLine();

            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            options.IsEndOfLine = false;
            var response = char.MinValue;
            do
            {
                Console.SetCursorPosition(left, top);
                ConsoleTools.Log("Make your selection:  \b", type, options);
                response = Console.ReadKey().KeyChar;
            } while (!_options.ContainsKey(response));
            Console.WriteLine();

            return response;
        }
    }
}
