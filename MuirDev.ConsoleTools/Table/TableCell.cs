namespace MuirDev.ConsoleTools;

/// <summary>
/// Represents a single cell of a table.
/// </summary>
public class TableCell(string content, TableCellConfig config)
{
    public TableCell(string content) : this(content, new TableCellConfig()) { }

    /// <summary>
    /// The content of the cell.
    /// </summary>
    public string Content { get; } = content;

    /// <summary>
    /// The configuration for the cell.
    /// </summary>
    public TableCellConfig Config { get; } = config;
}
