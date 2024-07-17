namespace MuirDev.ConsoleTools;

/// <summary>
/// Collection of properties for ProgressBar configuration.
/// </summary>
public class ProgressBarConfig
{
    /// <summary>
    /// An optional label to appear directly above the progress bar.
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// Whether or not to display the progress bar. Defaults to true.
    /// *** This is a hack for unit testing ***
    /// </summary>
    public bool Display { get; set; } = true;

    /// <summary>
    /// Whether or not to display the percentage of progress completion. Defaults to false.
    /// </summary>
    public bool DisplayPercentage { get; set; } = false;

    /// <summary>
    /// Whether or not to display a border around the progress bar. Defaults to false.
    /// </summary>
    public bool DisplayBorder { get; set; } = false;

    /// <summary>
    /// The color to use for label and percentage text. Defaults to current foreground color.
    /// </summary>
    public ConsoleColor TextColor { get; set; } = Console.ForegroundColor;

    /// <summary>
    /// The color to use for the border. Defaults to current foreground color.
    /// </summary>
    public ConsoleColor BorderColor { get; set; } = Console.ForegroundColor;

    /// <summary>
    /// The color to use for the progress bar itself. Defaults to current foreground color.
    /// </summary>
    public ConsoleColor FillColor { get; set; } = Console.ForegroundColor;

    /// <summary>
    /// The length of the progress bar, including border if applicable. Defaults to 80.
    /// </summary>
    public int Length { get; set; } = 80;
}
