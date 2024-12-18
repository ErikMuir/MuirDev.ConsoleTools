namespace MuirDev.ConsoleTools;

/// <summary>
/// Wraps the System.Console static class with a fluent pattern, and includes additional functionality around logging.
/// </summary>
public class FluentConsole
{
    public FluentConsole() { }

    #region -- Additional Functionality --

    /// <summary>
    /// Logs a specified <paramref>count</paramref> of newline characters to the console.
    /// </summary>
    /// <param name="count">The desired number of newlines to output to the console. Defaults to 1.</param>
    public FluentConsole LineFeed(int count = 1)
    {
        while (count-- > 0) WriteLine();
        return this;
    }

    /// <summary>
    /// Outputs a horizontal line to be used as a separator.
    /// </summary>
    /// <param name="type">The desired log type.</param>
    /// <param name="options">The log options to be used.</param>
    public FluentConsole LogSeparator(LogType type = LogType.Info, LogOptions options = null)
        => Log(new string('-', Math.Min(BufferWidth - 1, 80)), type, options);

    /// <summary>
    /// Outputs a horizontal line to be used as a separator.
    /// </summary>
    /// <param name="type">The desired log type.</param>
    /// <param name="options">The log options to be used.</param>
    public FluentConsole LogSeparatorFullWidth(LogType type = LogType.Info, LogOptions options = null)
        => Log(new string('-', BufferWidth - 1), type, options);

    /// <summary>
    /// Sets the output encoding.
    /// </summary>
    /// <param name="encoding">The desired output encoding.</param>
    public FluentConsole SetEncoding(Encoding encoding)
    {
        OutputEncoding = encoding;
        return this;
    }

    /// <summary>
    /// Sets the colors of the console output based on the provided colors.
    /// </summary>
    public FluentConsole SetColor(ConsoleColor foreground, ConsoleColor? background = null)
    {
        ForegroundColor = foreground;
        if (background.HasValue)
            BackgroundColor = background.Value;
        return this;
    }

    /// <summary>
    /// Sets the colors of the console output based on the desired log type.
    /// </summary>
    /// <param name="type">The desired log type.</param>
    public FluentConsole SetColor(LogType type) => SetColor(type, new LogOptions());

    /// <summary>
    /// Sets the colors of the console output based on the desired log type and log options.
    /// </summary>
    /// <param name="type">The desired log type.</param>
    /// <param name="options">The log options to be used.</param>
    public FluentConsole SetColor(LogType type, LogOptions options)
    {
        ForegroundColor = options?.ForegroundColor ?? GetTypeColor(type);
        BackgroundColor = options?.BackgroundColor ?? Console.BackgroundColor;
        return this;
    }

    /// <summary>
    /// Returns the text color associated with the provided <paramref>type</paramref>.
    /// </summary>
    /// <param name="type">The desired log type.</param>
    public ConsoleColor GetTypeColor(LogType type) => type switch
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
    public FluentConsole Info(string message) => Log(message, LogType.Info, new LogOptions());

    /// <summary>
    /// Logs an Info <paramref>message</paramref> to the console using provided <paramref>options</paramref>.
    /// </summary>
    /// <param name="message">The message to be output to the console.</param>
    /// <param name="options">Log options to override default logging behavior.</param>
    public FluentConsole Info(string message, LogOptions options) => Log(message, LogType.Info, options);

    /// <summary>
    /// Logs a Success <paramref>message</paramref> to the console.
    /// </summary>
    /// <param name="message">The message to be output to the console.</param>
    public FluentConsole Success(string message) => Log(message, LogType.Success, new LogOptions());

    /// <summary>
    /// Logs a Success <paramref>message</paramref> to the console using provided <paramref>options</paramref>.
    /// </summary>
    /// <param name="message">The message to be output to the console.</param>
    /// <param name="options">Log options to override default logging behavior.</param>
    public FluentConsole Success(string message, LogOptions options) => Log(message, LogType.Success, options);

    /// <summary>
    /// Logs a Warning <paramref>message</paramref> to the console.
    /// </summary>
    /// <param name="message">The message to be output to the console.</param>
    public FluentConsole Warning(string message) => Log(message, LogType.Warning, new LogOptions());

    /// <summary>
    /// Logs a Warning <paramref>message</paramref> to the console using provided <paramref>options</paramref>.
    /// </summary>
    /// <param name="message">The message to be output to the console.</param>
    /// <param name="options">Log options to override default logging behavior.</param>
    public FluentConsole Warning(string message, LogOptions options) => Log(message, LogType.Warning, options);

    /// <summary>
    /// Logs a Failure <paramref>message</paramref> to the console.
    /// </summary>
    /// <param name="message">The message to be output to the console.</param>
    public FluentConsole Failure(string message) => Log(message, LogType.Failure, new LogOptions());

    /// <summary>
    /// Logs a Failure <paramref>message</paramref> to the console using provided <paramref>options</paramref>.
    /// </summary>
    /// <param name="message">The message to be output to the console.</param>
    /// <param name="options">Log options to override default logging behavior.</param>
    public FluentConsole Failure(string message, LogOptions options) => Log(message, LogType.Failure, options);

    /// <summary>
    /// Logs a <paramref>message</paramref> to the console.
    /// </summary>
    /// <param name="message">The message to be output to the console.</param>
    public FluentConsole Log(string message) => Log(message, LogType.Info, new LogOptions());

    /// <summary>
    /// Logs a <paramref>message</paramref> of the specified <paramref>type</paramref> to the console.
    /// </summary>
    /// <param name="message">The message to be output to the console.</param>
    /// <param name="type">The log type of the message.</param>
    public FluentConsole Log(string message, LogType type) => Log(message, type, new LogOptions());

    /// <summary>
    /// Logs a <paramref>message</paramref> of the specified <paramref>type</paramref> to the console using provided <paramref>options</paramref>.
    /// </summary>
    /// <param name="message">The message to be output to the console.</param>
    /// <param name="options">Log options to override default logging behavior.</param>
    public FluentConsole Log(string message, LogOptions options) => Log(message, LogType.Info, options);

    /// <summary>
    /// Logs a <paramref>message</paramref> of the specified <paramref>type</paramref> to the console using provided <paramref>options</paramref>.
    /// </summary>
    /// <param name="message">The message to be output to the console.</param>
    /// <param name="type">The log type of the message.</param>
    /// <param name="options">Log options to override default logging behavior.</param>
    public FluentConsole Log(string message, LogType type, LogOptions options)
    {
        SetColor(type, options).Write(message);
        if (options?.IsEndOfLine ?? true)
            WriteLine();
        ResetColor();
        return this;
    }

    /// <summary>
    /// Returns the Console's current <c>CursorPosition</c>.
    /// </summary>
    public static CursorPosition GetCursorPosition() => new(Console.CursorTop, Console.CursorLeft);

    /// <summary>
    /// Sets the Console's cursor position.
    /// </summary>
    /// <param name="position">The <c>CursorPosition</c> to set on the Console.</param>
    public FluentConsole SetCursorPosition(CursorPosition position)
    {
        CursorTop = position.Top;
        CursorLeft = position.Left;
        return this;
    }

    #endregion

    #region -- Console Wrappers --

    public bool IsInputRedirected => Console.IsInputRedirected;
    public int BufferHeight { get => Console.BufferHeight; }
    public int BufferWidth { get => Console.BufferWidth; }
    public int CursorLeft { get => Console.CursorLeft; set => Console.CursorLeft = value; }
    public int CursorSize { get => Console.CursorSize; }
    public int CursorTop { get => Console.CursorTop; set => Console.CursorTop = value; }
    public bool CursorVisible { set => Console.CursorVisible = value; }
    public TextWriter Error => Console.Error;
    public ConsoleColor ForegroundColor { get => Console.ForegroundColor; set => Console.ForegroundColor = value; }
    public TextReader In => Console.In;
    public Encoding InputEncoding { get => Console.InputEncoding; set => Console.InputEncoding = value; }
    public bool IsErrorRedirected => Console.IsErrorRedirected;
    public int WindowWidth { get => Console.WindowWidth; }
    public bool IsOutputRedirected => Console.IsOutputRedirected;
    public bool KeyAvailable => Console.KeyAvailable;
    public int LargestWindowHeight => Console.LargestWindowHeight;
    public int LargestWindowWidth => Console.LargestWindowWidth;
    public TextWriter Out => Console.Out;
    public Encoding OutputEncoding { get => Console.OutputEncoding; set => Console.OutputEncoding = value; }
    public string Title { set => Console.Title = value; }
    public bool TreatControlCAsInput { get => Console.TreatControlCAsInput; set => Console.TreatControlCAsInput = value; }
    public int WindowHeight { get => Console.WindowHeight; }
    public int WindowLeft { get => Console.WindowLeft; }
    public int WindowTop { get => Console.WindowTop; }
    public ConsoleColor BackgroundColor { get => Console.BackgroundColor; set => Console.BackgroundColor = value; }

    public FluentConsole Beep()
    {
        Console.Beep();
        return this;
    }
    public FluentConsole Clear()
    {
        Console.Clear();
        return this;
    }
    public Stream OpenStandardError(int bufferSize) => Console.OpenStandardError(bufferSize);
    public Stream OpenStandardError() => Console.OpenStandardError();
    public Stream OpenStandardInput(int bufferSize) => Console.OpenStandardInput(bufferSize);
    public Stream OpenStandardInput() => Console.OpenStandardInput();
    public Stream OpenStandardOutput(int bufferSize) => Console.OpenStandardOutput(bufferSize);
    public Stream OpenStandardOutput() => Console.OpenStandardOutput();
    public int Read() => Console.Read();
    public ConsoleKeyInfo ReadKey(bool intercept) => Console.ReadKey(intercept);
    public ConsoleKeyInfo ReadKey() => Console.ReadKey();
    public string ReadLine() => Console.ReadLine();
    public FluentConsole WaitForKeyPress()
    {
        Console.ReadKey(true);
        return this;
    }
    public FluentConsole WaitForEnter()
    {
        Console.ReadLine();
        return this;
    }
    public FluentConsole ResetColor()
    {
        Console.ResetColor();
        return this;
    }
    public FluentConsole SetCursorPosition(int left, int top)
    {
        Console.SetCursorPosition(left, top);
        return this;
    }
    public FluentConsole SetError(TextWriter newError)
    {
        Console.SetError(newError);
        return this;
    }
    public FluentConsole SetIn(TextReader newIn)
    {
        Console.SetIn(newIn);
        return this;
    }
    public FluentConsole SetOut(TextWriter newOut)
    {
        Console.SetOut(newOut);
        return this;
    }
    public FluentConsole Write(ulong value)
    {
        Console.Write(value);
        return this;
    }
    public FluentConsole Write(bool value)
    {
        Console.Write(value);
        return this;
    }
    public FluentConsole Write(char value)
    {
        Console.Write(value);
        return this;
    }
    public FluentConsole Write(char[] buffer)
    {
        Console.Write(buffer);
        return this;
    }
    public FluentConsole Write(char[] buffer, int index, int count)
    {
        Console.Write(buffer, index, count);
        return this;
    }
    public FluentConsole Write(double value)
    {
        Console.Write(value);
        return this;
    }
    public FluentConsole Write(long value)
    {
        Console.Write(value);
        return this;
    }
    public FluentConsole Write(object value)
    {
        Console.Write(value);
        return this;
    }
    public FluentConsole Write(float value)
    {
        Console.Write(value);
        return this;
    }
    public FluentConsole Write(string value)
    {
        Console.Write(value);
        return this;
    }
    public FluentConsole Write(string format, object arg0)
    {
        Console.Write(format, arg0);
        return this;
    }
    public FluentConsole Write(string format, object arg0, object arg1)
    {
        Console.Write(format, arg0, arg1);
        return this;
    }
    public FluentConsole Write(string format, object arg0, object arg1, object arg2)
    {
        Console.Write(format, arg0, arg1, arg2);
        return this;
    }
    public FluentConsole Write(string format, params object[] arg)
    {
        Console.Write(format, arg);
        return this;
    }
    public FluentConsole Write(uint value)
    {
        Console.Write(value);
        return this;
    }
    public FluentConsole Write(decimal value)
    {
        Console.Write(value);
        return this;
    }
    public FluentConsole Write(int value)
    {
        Console.Write(value);
        return this;
    }
    public FluentConsole WriteLine(ulong value)
    {
        Console.WriteLine(value);
        return this;
    }
    public FluentConsole WriteLine(string format, params object[] arg)
    {
        Console.WriteLine(format, arg);
        return this;
    }
    public FluentConsole WriteLine()
    {
        Console.WriteLine();
        return this;
    }
    public FluentConsole WriteLine(bool value)
    {
        Console.WriteLine(value);
        return this;
    }
    public FluentConsole WriteLine(char[] buffer)
    {
        Console.WriteLine(buffer);
        return this;
    }
    public FluentConsole WriteLine(char[] buffer, int index, int count)
    {
        Console.WriteLine(buffer, index, count);
        return this;
    }
    public FluentConsole WriteLine(decimal value)
    {
        Console.WriteLine(value);
        return this;
    }
    public FluentConsole WriteLine(double value)
    {
        Console.WriteLine(value);
        return this;
    }
    public FluentConsole WriteLine(uint value)
    {
        Console.WriteLine(value);
        return this;
    }
    public FluentConsole WriteLine(int value)
    {
        Console.WriteLine(value);
        return this;
    }
    public FluentConsole WriteLine(object value)
    {
        Console.WriteLine(value);
        return this;
    }
    public FluentConsole WriteLine(float value)
    {
        Console.WriteLine(value);
        return this;
    }
    public FluentConsole WriteLine(string value)
    {
        Console.WriteLine(value);
        return this;
    }
    public FluentConsole WriteLine(string format, object arg0)
    {
        Console.WriteLine(format, arg0);
        return this;
    }
    public FluentConsole WriteLine(string format, object arg0, object arg1)
    {
        Console.WriteLine(format, arg0, arg1);
        return this;
    }
    public FluentConsole WriteLine(string format, object arg0, object arg1, object arg2)
    {
        Console.WriteLine(format, arg0, arg1, arg2);
        return this;
    }
    public FluentConsole WriteLine(long value)
    {
        Console.WriteLine(value);
        return this;
    }
    public FluentConsole WriteLine(char value)
    {
        Console.WriteLine(value);
        return this;
    }

    #endregion
}
