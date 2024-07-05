using System.Linq;

namespace MuirDev.ConsoleTools;

/// <summary>
/// Represents tabular data to be presented to the user.
/// </summary>
public class Table
{
    public Table(List<TableRow> rows) : this(rows, new TableConfig()) { }

    public Table(List<TableRow> rows, TableConfig config)
    {
        var columnCount = rows.First().ColumnCount;
        if (rows.Any(row => row.ColumnCount != columnCount))
            throw new Exception("All rows must be the same length.");
        if (config is not null && config.ColumnLabels?.Count != columnCount)
            throw new Exception("The column label count must equal the table's column count.");
        Rows = rows;
    }

    private readonly List<TableRow> Rows;

    public void Display()
    {
        var console = new FluentConsole();

    }
}
