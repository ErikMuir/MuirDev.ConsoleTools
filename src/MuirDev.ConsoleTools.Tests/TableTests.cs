namespace MuirDev.ConsoleTools.Tests;

public class TableTests
{
    private static readonly TableCellConfig _redText = new() { TextColor = ConsoleColor.DarkRed };
    private static readonly TableCellConfig _blueText = new() { TextColor = ConsoleColor.DarkBlue };
    private static readonly TableCellConfig _yellowText = new() { TextColor = ConsoleColor.Yellow };
    private static readonly TableCellConfig _purpleText = new() { TextColor = ConsoleColor.DarkMagenta };
    private static readonly TableCellConfig _greenText = new() { TextColor = ConsoleColor.DarkGreen };
    private static readonly TableCellConfig _orangeText = new() { TextColor = ConsoleColor.DarkYellow };
    private static readonly TableCellConfig _alignCenter = new() { Justification = Justify.Center };
    private static readonly TableCellConfig _alignRight = new() { Justification = Justify.Right };

    [Fact]
    public void Table_Display_Test()
    {
        var tableRows = new List<TableRow>
        {
            new(
            [
                new(""),
                new("Red", _alignCenter),
                new("Blue", _alignCenter),
                new("Yellow", _alignCenter),
            ]),
            new(
            [
                new("Red", _alignRight),
                new("█ Red", _redText),
                new("█ Purple", _purpleText),
                new("█ Orange", _orangeText),
            ]),
            new(
            [
                new("Blue", _alignRight),
                new("█ Purple", _purpleText),
                new("█ Blue", _blueText),
                new("█ Green", _greenText),
            ]),
            new(
            [
                new("Yellow", _alignRight),
                new("█ Orange", _orangeText),
                new("█ Green", _greenText),
                new("█ Yellow", _yellowText),
            ]),
        };
        var table = new Table(4, new TableConfig
        {
            TableBorder = true,
            HasColumnLabels = true,
            HasRowLabels = true,
            RowBorder = true,
            ColumnBorder = true,
            BorderColor = ConsoleColor.DarkCyan,
        });
        table.SetRows(tableRows);

        table.Display();

        Assert.True(true);
    }

    [Fact]
    public void Table_AddRow_Test()
    {
        var cells = new List<TableCell>();
        for (var i = 0; i < 4; i++)
            cells.Add(new TableCell($"{i}"));
        var row = new TableRow(cells);
        var table = new Table(4);
        Assert.Empty(table.Rows);

        table.AddRow(row);

        Assert.Single(table.Rows);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    public void Table_AddRow_Throws_Test(int cellCount)
    {
        var cells = new List<TableCell>();
        for (var i = 0; i < cellCount; i++)
            cells.Add(new TableCell($"{i}"));
        var table = new Table(1);

        var exception = Assert.Throws<ArgumentException>(() => table.AddRow(new TableRow(cells)));

        Assert.Equal("Row column counts must equal table column count.", exception.Message);
    }

    [Fact]
    public void Table_AddRows_Test()
    {
        var cells = new List<TableCell> { new("foo") };
        var rows = new List<TableRow> { new(cells), new(cells) };
        var table = new Table(1);
        Assert.Empty(table.Rows);

        table.AddRows(rows);

        Assert.Equal(2, table.Rows.Count);
    }

    [Fact]
    public void Table_AddRows_Throws_Test()
    {
        var table = new Table(1);
        var row = new TableRow([]);

        var exception = Assert.Throws<ArgumentException>(() => table.AddRows([row]));

        Assert.Equal("Row column counts must equal table column count.", exception.Message);
    }

    [Fact]
    public void Table_SetRows_Test()
    {
        var originalRow = new TableRow([new("foo")]);
        var updatedRow = new TableRow([new("bar")]);
        var table = new Table(1);
        table.AddRow(originalRow);
        Assert.Single(table.Rows);
        Assert.Equal("foo", table.Rows[0].Cells[0].Content);

        table.SetRows([updatedRow]);

        Assert.Single(table.Rows);
        Assert.Equal("bar", table.Rows[0].Cells[0].Content);
    }

    [Fact]
    public void Table_SetRows_Throws_Test()
    {
        var table = new Table(1);
        var row = new TableRow([]);

        var exception = Assert.Throws<ArgumentException>(() => table.SetRows([row]));

        Assert.Equal("Row column counts must equal table column count.", exception.Message);
    }

    [Fact]
    public void Table_ClearAllRows_Test()
    {
        var cells = new List<TableCell> { new("foo") };
        var rows = new List<TableRow> { new(cells), new(cells) };
        var table = new Table(1);
        table.SetRows(rows);
        Assert.Equal(2, table.Rows.Count);

        table.ClearAllRows();

        Assert.Empty(table.Rows);
    }

    [Fact]
    public void Table_RemoveRowAt_Test()
    {
        var firstRow = new TableRow([new("1st")]);
        var secondRow = new TableRow([new("2nd")]);
        var thirdRow = new TableRow([new("3rd")]);
        var table = new Table(1);
        table.SetRows([firstRow, secondRow, thirdRow]);
        Assert.Equal(3, table.Rows.Count);

        table.RemoveRowAt(1);

        Assert.Equal(2, table.Rows.Count);
        Assert.Equal("1st", table.Rows[0].Cells[0].Content);
        Assert.Equal("3rd", table.Rows[1].Cells[0].Content);
    }
}
