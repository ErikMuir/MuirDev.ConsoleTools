namespace MuirDev.ConsoleTools;

public class TableRow
{
    public TableRow(List<TableCell> tableCells)
    {
        TableCells = tableCells;
    }

    private readonly List<TableCell> TableCells;

    public int ColumnCount => TableCells.Count;
}
