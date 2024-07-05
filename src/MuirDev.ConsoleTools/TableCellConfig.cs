namespace MuirDev.ConsoleTools;

public class TableCellConfig
{
    public ConsoleColor? TextColor { get; set; }
    public ConsoleColor? BackgroundColor { get; set; }
    public Justify Justification { get; set; } = Justify.Left;
}
