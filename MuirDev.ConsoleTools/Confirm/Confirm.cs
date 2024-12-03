namespace MuirDev.ConsoleTools;

/// <summary>
/// Represents a yes or no question to be presented to the user.
/// </summary>
public class Confirm
{
    private static readonly FluentConsole _console = new();
    private readonly string _question;
    private readonly Dictionary<ConsoleKey, bool> _allowedResponses;


    /// <summary>
    /// Initializes a new instance of the <c>Confirm</c> class.
    /// </summary>
    /// <param name="question">The question to be output to the console.</param>
    /// <exception cref="System.ArgumentException">Throws when <c>question</c> is null or whitespace.</exception>
    public Confirm(string question)
    {
        if (string.IsNullOrWhiteSpace(question))
            throw new ArgumentException("Question is required.");

        if (!question.Contains('?'))
            question += "?";
        question += " [y/n] ";

        _question = question;
        _allowedResponses = new Dictionary<ConsoleKey, bool>
            {
                { ConsoleKey.Y, true },
                { ConsoleKey.N, false },
            };
    }

    /// <summary>
    /// Initializes a new instance of the <c>Menu</c> class.
    /// </summary>
    /// <param name="question">The question to be output to the console.</param>
    /// <param name="defaultResponse">The desired return value when the Enter key is pressed.</param>
    /// <exception cref="System.ArgumentException">Throws when <c>question</c> is null or whitespace.</exception>
    public Confirm(string question, bool defaultResponse)
    {
        if (string.IsNullOrWhiteSpace(question))
            throw new ArgumentException("Question is required.");

        if (!question.Contains('?'))
            question += "?";
        question += defaultResponse ? " [Y/n] " : " [y/N] ";

        _question = question;
        _allowedResponses = new Dictionary<ConsoleKey, bool>
            {
                { ConsoleKey.Y, true },
                { ConsoleKey.N, false },
                { ConsoleKey.Enter, defaultResponse },
                { ConsoleKey.Escape, !defaultResponse },
            };
    }


    /// <summary>
    /// Prompts the user to answer a yes or no question.
    /// </summary>
    public bool Run() => Run(LogType.Info, new LogOptions());

    /// <summary>
    /// Prompts the user to answer a yes or no question.
    /// </summary>
    /// <param name="type">The log type of the message.</param>
    public bool Run(LogType type) => Run(type, new LogOptions());

    /// <summary>
    /// Prompts the user to answer a yes or no question.
    /// </summary>
    /// <param name="options">Log options to override default logging behavior.</param>
    public bool Run(LogOptions options) => Run(LogType.Info, options);

    /// <summary>
    /// Prompts the user to answer a yes or no question.
    /// </summary>
    /// <param name="type">The log type of the message.</param>
    /// <param name="options">Log options to override default logging behavior.</param>
    public bool Run(LogType type, LogOptions options)
    {
        options ??= new LogOptions();
        options.IsEndOfLine = false;
        ConsoleKey response;
        do
        {
            _console.Log(_question, type, options);
            response = _console.ReadKey(false).Key;
            _console.LineFeed();
        } while (!_allowedResponses.ContainsKey(response));
        return _allowedResponses[response];
    }
}
