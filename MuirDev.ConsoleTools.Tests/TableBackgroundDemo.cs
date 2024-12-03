namespace MuirDev.ConsoleTools.Tests;

public static class TableBackgroundDemo
{
    public static void Run()
    {
        var stars = new TableCellConfig { BackgroundColor = ConsoleColor.DarkBlue };
        var redStripe = new TableCellConfig { BackgroundColor = ConsoleColor.DarkRed };
        var whiteStripe = new TableCellConfig { BackgroundColor = ConsoleColor.White };
        var rows = new List<TableRow>
        {
            new([new("* * * * * * *", stars), new("                          ", redStripe)]),
            new([new(" * * * * * * ", stars), new("                          ", whiteStripe)]),
            new([new("* * * * * * *", stars), new("                          ", redStripe)]),
            new([new(" * * * * * * ", stars), new("                          ", whiteStripe)]),
            new([new("* * * * * * *", stars), new("                          ", redStripe)]),
            new([new(" * * * * * * ", stars), new("                          ", whiteStripe)]),
            new([new("* * * * * * *", stars), new("                          ", redStripe)]),
            new([new("             ", whiteStripe), new("                          ", whiteStripe)]),
            new([new("             ", redStripe), new("                          ", redStripe)]),
            new([new("             ", whiteStripe), new("                          ", whiteStripe)]),
            new([new("             ", redStripe), new("                          ", redStripe)]),
            new([new("             ", whiteStripe), new("                          ", whiteStripe)]),
            new([new("             ", redStripe), new("                          ", redStripe)]),
        };
        var config = new TableConfig { TextColor = ConsoleColor.White };
        var table = new Table(rows, config);
        table.Display();
    }
}
