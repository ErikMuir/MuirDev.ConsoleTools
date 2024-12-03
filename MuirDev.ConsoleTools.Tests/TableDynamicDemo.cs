namespace MuirDev.ConsoleTools.Tests;

public static class TableDynamicDemo
{
    public static void Run()
    {
        var rows = new List<TableRow>
        {
            new([new("red"), new("orange"), new("yellow"), new("green"), new("blue")]),
            new([new("left"), new("center"), new("right")]),
            new([new("int"), new("decimal"), new("long"), new("double")]),
        };
        var config = new TableConfig()
        {
            TableBorder = true,
            RowBorder = true,
            ColumnBorder = true,
            BorderColor = ConsoleColor.DarkGray,
        };
        var table = new Table(rows, config);
        table.Display();
    }
}
