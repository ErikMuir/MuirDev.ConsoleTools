namespace MuirDev.ConsoleTools;

/// <summary>
/// Collection of properties for Table configuration.
/// </summary>
public class TableConfig
{
    /// <summary>
    /// Whether or not the first row contains column labels.
    /// </summary>
    public bool HasColumnLabels { get; set; }

    /// <summary>
    /// Whether or not the first column contains row labels.
    /// </summary>
    public bool HasRowLabels { get; set; }

    /// <summary>
    /// Whether or not the table should have an exterior border.
    /// </summary>
    public bool TableBorder { get; set; }

    /// <summary>
    /// Whether or not table rows are separated by lines.
    /// </summary>
    public bool RowBorder { get; set; }

    /// <summary>
    /// Whether or not table columns are separated by lines.
    /// </summary>
    public bool ColumnBorder { get; set; }

    /// <summary>
    /// The color to be used for borders. Defaults to current Console.ForegroundColor.
    /// </summary>
    public ConsoleColor? BorderColor { get; set; }

    /// <summary>
    /// The color to be used for all cell content, unless everriden by the cell's config. Defaults to current Console.ForegroundColor.
    /// </summary>
    public ConsoleColor? TextColor { get; set; }

    /// <summary>
    /// The justification to be used for all cell content, unless overriden by the cell's config. Defaults to Left.
    /// </summary>
    public Justify? Justification { get; set; }
}
