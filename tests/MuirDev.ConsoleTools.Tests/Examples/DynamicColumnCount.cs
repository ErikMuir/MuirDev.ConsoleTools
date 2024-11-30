namespace MuirDev.ConsoleTools.Tests;

public static partial class Examples
{
    public static void DynamicColumnCount()
    {
        var tableRows = new List<TableRow>
        {
            new([new("red"), new("orange"), new("yellow"), new("green"), new("blue")]),
            new([new("left"), new("center"), new("right")]),
            new([new("int"), new("decimal"), new("long"), new("double")]),
        };
        var tableConfig = new TableConfig
        {
            TableBorder = true,
            RowBorder = true,
            ColumnBorder = true,
            BorderColor = ConsoleColor.DarkGray,
        };
        var table = new Table(tableRows, tableConfig);
        table.Display();
        Console.WriteLine();
    }
}
