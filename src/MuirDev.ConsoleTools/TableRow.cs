namespace MuirDev.ConsoleTools;

/// <summary>
/// Represents a row of table cells.
/// </summary>
public class TableRow(List<TableCell> cells)
{
    /// <summary>
    /// The list of cells in the row.
    /// </summary>
    public readonly List<TableCell> Cells = cells;

    /// <summary>
    /// The count of columns in the row.
    /// </summary>
    public int ColumnCount => Cells.Count;
}
