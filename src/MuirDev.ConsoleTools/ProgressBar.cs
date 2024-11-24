namespace MuirDev.ConsoleTools;

/*
┌ Download ────────────────────────────────────────────────────────────────────┐
│ █████████████████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ │
└ 33% ─────────────────────────────────────────────────────────────────────────┘
*/

/// <summary>
/// Used to display the progress of a potentially long-running, multi-step process.
/// </summary>
public class ProgressBar(double whole, ProgressBarConfig config)
{
    public ProgressBar(double whole) : this(whole, new ProgressBarConfig()) { }

    private static readonly FluentConsole _console = new();
    private bool InitialRender { get; set; } = true;
    private LogOptions BorderOptions => new(Config.BorderColor, isEndOfLine: false);
    private LogOptions TextOptions => new(Config.TextColor, isEndOfLine: false);
    private LogOptions FillOptions => new(Config.FillColor, isEndOfLine: false);
    private string Percent => Config.DisplayBorder ? $" {Part / Whole:P0} " : (Part / Whole).ToString("P0");
    private string Label => string.IsNullOrWhiteSpace(Config.Label) ? "" : Config.DisplayBorder ? $" {Config.Label} " : Config.Label;

    /// <summary>
    /// The configuration for the progress bar.
    /// </summary>
    public ProgressBarConfig Config { get; } = config;

    /// <summary>
    /// The time at which the first update occurs.
    /// </summary>
    public DateTime? StartTime { get; private set; } = null;

    /// <summary>
    /// The time at which the progress reached 100%.
    /// </summary>
    public DateTime? EndTime { get; private set; } = null;

    /// <summary>
    /// The current progress.
    /// </summary>
    public double Part { get; private set; } = 0;

    /// <summary>
    /// The value for which, when reached, means that the progress is complete.
    /// </summary>
    public double Whole { get; private set; } = whole;

    /// <summary>
    /// The duration of time from when progress started until progress completed, or now if not yet complete.
    /// </summary>
    public TimeSpan Duration => StartTime.HasValue ? (EndTime ?? DateTime.Now) - StartTime.Value : TimeSpan.Zero;

    /// <summary>
    /// A description of the current error state, if there is one.
    /// </summary>
    public string ErrorMessage { get; private set; } = null;

    /// <summary>
    /// Start the progress.
    /// </summary>
    public ProgressBar Start()
    {
        InitialRender = true;
        ErrorMessage = null;
        StartTime = DateTime.Now;
        EndTime = null;
        _console.CursorVisible = false;
        return this;
    }

    /// <summary>
    /// Update the progress.
    /// </summary>
    public ProgressBar Update(double value)
    {
        if (EndTime.HasValue)
        {
            ErrorMessage = "Progress complete.";
            return this;
        }

        if (value < 0)
        {
            ErrorMessage = "Value cannot be negative.";
            return this;
        }

        if (!StartTime.HasValue)
        {
            Start();
        }

        Part = value;

        if (Config.Display)
        {
            Display();
        }

        if (Part >= Whole)
        {
            End();
        }

        return this;
    }

    /// <summary>
    /// End the progress.
    /// </summary>
    public ProgressBar End()
    {
        EndTime = DateTime.Now;
        _console.CursorVisible = true;
        return this;
    }

    /// <summary>
    /// Reset the progress.
    /// </summary>
    public ProgressBar Reset() => Reset(Whole);

    /// <summary>
    /// Reset the progress.
    /// </summary>
    public ProgressBar Reset(double whole)
    {
        StartTime = null;
        EndTime = null;
        Part = 0;
        Whole = whole;
        InitialRender = true;
        ErrorMessage = null;
        return this;
    }

    private void Display()
    {
        if (!string.IsNullOrWhiteSpace(ErrorMessage))
        {
            _console.LineFeed().Failure(ErrorMessage);
            return;
        }
        if (!InitialRender) SetCursorPosition();
        InitialRender = false;
        DisplayLineAbove();
        DisplayBar();
        DisplayLineBelow();
        _console.LineFeed();
    }

    private void SetCursorPosition()
    {
        var lines = 1;
        if (Config.DisplayBorder || Label.Length > 0) lines++;
        if (Config.DisplayBorder || Config.DisplayPercentage) lines++;
        _console.SetCursorPosition(0, Math.Max(0, _console.CursorTop - lines));
    }

    private void DisplayLineAbove()
    {
        if (!Config.DisplayBorder && Label.Length == 0) return;
        if (Config.DisplayBorder) _console.Log(Constants.SingleDownAndRight.ToString(), BorderOptions);
        if (Label.Length > 0) _console.Log(Label, TextOptions);
        if (Config.DisplayBorder)
        {
            _console.Log(new string(Constants.SingleHorizontal, Config.Length - Label.Length - 2), BorderOptions);
            _console.Log(Constants.SingleDownAndLeft.ToString(), BorderOptions);
        }
        _console.LineFeed();
    }

    private void DisplayBar()
    {
        if (Config.DisplayBorder) _console.Log($"{Constants.SingleVertical} ", BorderOptions);
        var barLength = Config.DisplayBorder ? Config.Length - 4 : Config.Length;
        var percent = Part / Whole;
        var progressLength = (int)Math.Floor(percent * barLength);
        var progress = new string(Constants.FullBlock, progressLength);
        var remaining = new string(Constants.LightShade, barLength - progressLength);
        _console.Log($"{progress}{remaining}", FillOptions);
        if (Config.DisplayBorder) _console.Log($" {Constants.SingleVertical}", BorderOptions);
    }

    private void DisplayLineBelow()
    {
        _console.LineFeed();
        if (!Config.DisplayBorder && !Config.DisplayPercentage) return;
        if (Config.DisplayBorder) _console.Log(Constants.SingleUpAndRight.ToString(), BorderOptions);
        if (Config.DisplayPercentage) _console.Log(Percent, TextOptions);
        if (Config.DisplayBorder)
        {
            _console.Log(new string(Constants.SingleHorizontal, Config.Length - Percent.Length - 2), BorderOptions);
            _console.Log(Constants.SingleUpAndLeft.ToString(), BorderOptions);
        }
    }
}
