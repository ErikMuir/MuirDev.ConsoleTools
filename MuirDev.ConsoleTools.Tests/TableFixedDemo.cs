namespace MuirDev.ConsoleTools.Tests;

public static class TableFixedDemo
{
    public static void Run()
    {
        var redText = new TableCellConfig { TextColor = ConsoleColor.DarkRed };
        var blueText = new TableCellConfig { TextColor = ConsoleColor.DarkBlue };
        var yellowText = new TableCellConfig { TextColor = ConsoleColor.Yellow };
        var purpleText = new TableCellConfig { TextColor = ConsoleColor.DarkMagenta };
        var greenText = new TableCellConfig { TextColor = ConsoleColor.DarkGreen };
        var orangeText = new TableCellConfig { TextColor = ConsoleColor.DarkYellow };
        var alignCenter = new TableCellConfig { Justification = Justify.Center };
        var alignRight = new TableCellConfig { Justification = Justify.Right };
        var rows = new List<TableRow>
        {
            new(
            [
                new(""),
                new("Red", alignCenter),
                new("Blue", alignCenter),
                new("Yellow", alignCenter),
            ]),
            new(
            [
                new("Red", alignRight),
                new("█ Red", redText),
                new("█ Purple", purpleText),
                new("█ Orange", orangeText),
            ]),
            new(
            [
                new("Blue", alignRight),
                new("█ Purple", purpleText),
                new("█ Blue", blueText),
                new("█ Green", greenText),
            ]),
            new(
            [
                new("Yellow", alignRight),
                new("█ Orange", orangeText),
                new("█ Green", greenText),
                new("█ Yellow", yellowText),
            ]),
        };
        var config = new TableConfig
        {
            TableBorder = true,
            RowBorder = true,
            ColumnBorder = true,
            BorderColor = ConsoleColor.DarkCyan,
        };
        var table = new Table(rows, config);
        table.Display();
    }
}
