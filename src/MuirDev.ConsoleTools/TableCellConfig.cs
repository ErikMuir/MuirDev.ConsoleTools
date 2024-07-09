namespace MuirDev.ConsoleTools;

/// <summary>
/// Collection of properties for TableCell configuration.
/// </summary>
public class TableCellConfig
{
    /// <summary>
    /// The color to be used for cell content. Defaults to table configuration.
    /// </summary>
    public ConsoleColor? TextColor { get; set; }

    /// <summary>
    /// The justification to be used for cell content. Defaults to table configuration.
    /// </summary>
    public Justify? Justification { get; set; }
}
