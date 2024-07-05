namespace MuirDev.ConsoleTools;

public class TableCell(string content, TableCellConfig config)
{
    public TableCell(string content) : this(content, new TableCellConfig()) { }

    public string Content { get; } = content;

    public TableCellConfig Config { get; } = config;
}
