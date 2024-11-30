using System.Linq;

namespace MuirDev.ConsoleTools;

/// <summary>
/// Represents a list of options to be presented to the user.
/// </summary>
public class Menu
{
    private static readonly FluentConsole _console = new();
    private readonly string _title;
    private readonly string _detail;
    private readonly List<MenuItem> _items;

    /// <summary>
    /// Initializes a new instance of the <c>Menu</c> class.
    /// </summary>
    /// <param name="items">
    /// A list of menu items that are displayed to the user.
    /// </param>
    public Menu(List<MenuItem> items) : this(items, null, null) { }

    /// <summary>
    /// Initializes a new instance of the <c>Menu</c> class.
    /// </summary>
    /// <param name="items">
    /// A list of menu items that are displayed to the user.
    /// </param>
    /// <param name="title">The menu's title that is displayed at the top of the menu.</param>
    public Menu(List<MenuItem> items, string title) : this(items, title, null) { }

    /// <summary>
    /// Initializes a new instance of the <c>Menu</c> class.
    /// </summary>
    /// <param name="items">
    /// A list of menu items that are displayed to the user.
    /// </param>
    /// <param name="title">The menu's title that is displayed at the top of the menu.</param>
    /// <param name="detail">Any details related to the menu that are displayed between the title and items.</param>
    public Menu(List<MenuItem> items, string title, string detail)
    {
        if (items.Count < 2)
            throw new ArgumentException("Menus must contain at least 2 options.");

        _items = items;
        _title = title;
        _detail = detail;
    }


    /// <summary>
    /// Displays the menu, prompts the user for a response, and returns the response.
    /// </summary>
    public char Run() => Run(new LogOptions());

    /// <summary>
    /// Displays the menu, prompts the user for a response, and returns the response.
    /// </summary>
    /// <param name="options">Log options to customize logging behavior.</param>
    public char Run(LogOptions options)
    {
        var type = LogType.Info;
        options ??= new LogOptions();
        options.IsEndOfLine = true;

        _console.LogSeparator(type, options);
        if (!string.IsNullOrWhiteSpace(_title))
        {
            _console.Log($" {_title}", type, options).LogSeparator(type, options);
        }
        if (!string.IsNullOrWhiteSpace(_detail))
        {
            _console.Log(_detail, type, options).WriteLine();
        }
        foreach (var option in _items)
        {
            _console.Log($"  {option.Char} - {option.Name}", type, options);
        }
        _console.LogSeparator(type, options).WriteLine();

        var left = _console.CursorLeft;
        var top = _console.CursorTop;
        options.IsEndOfLine = false;
        while (true)
        {
            var response = _console
                .SetCursorPosition(left, top)
                .Log("Make your selection:  \b", type, options)
                .ReadKey()
                .KeyChar;
            var chosenOption = _items.SingleOrDefault(o => o.Char == response);
            if (chosenOption is null)
            {
                _console.Beep();
            }
            else
            {
                _console.LineFeed();
                chosenOption.Action();
                return response;
            }
        }
    }
}
