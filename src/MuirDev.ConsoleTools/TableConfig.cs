namespace MuirDev.ConsoleTools;

public class TableConfig
{
    public List<string> ColumnLabels { get; set; }
    public bool FirstCellOfRowIsLabel { get; set; }
    public bool TableBorder { get; set; }
    public bool RowBorder { get; set; }
    public bool ColumnBorder { get; set; }
    public ConsoleColor? BorderColor { get; set; }
}
