namespace MuirDev.ConsoleTools;

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
    /// <param name="options">Log options to override default logging behavior.</param>
    public char Run(LogOptions options) => Run(LogType.Info, options);

    /// <summary>
    /// Displays the menu, prompts the user for a response, and returns the response.
    /// </summary>
    /// <param name="type">The log type of the menu.</param>
    /// <param name="options">Log options to override default logging behavior.</param>
    public char Run(LogType type, LogOptions options)
    {
        var console = new FluentConsole();

        options ??= new LogOptions();
        options.IsEndOfLine = true;

        console.LogSeparator(type, options);
        if (!string.IsNullOrWhiteSpace(_title))
        {
            console.Log($" {_title}", type, options).LogSeparator(type, options);
        }
        if (!string.IsNullOrWhiteSpace(_detail))
        {
            console.Log(_detail, type, options).WriteLine();
        }
        foreach (KeyValuePair<char, string> option in _options)
        {
            console.Log($"  {option.Key} - {option.Value}", type, options);
        }
        console.LogSeparator(type, options).WriteLine();

        var left = console.CursorLeft;
        var top = console.CursorTop;
        options.IsEndOfLine = false;
        char response;
        do
        {
            console.SetCursorPosition(left, top).Log("Make your selection:  \b", type, options);
            response = console.ReadKey().KeyChar;
        } while (!_options.ContainsKey(response));
        console.WriteLine();

        return response;
    }
}
