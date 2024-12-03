## Table

The Table tool presents tabular data to the user.

#### TableConfig

|Property|DataType|Notes|
|---|---|---|
|HasColumnLabels|Boolean|Whether or not the first row contains column labels.|
|HasRowLabels|Boolean|Whether or not the first column contains row labels.|
|TableBorder|Boolean|Whether or not the table should have an exterior border.|
|RowBorder|Boolean|Whether or not table rows are separated by lines.|
|ColumnBorder|Boolean|Whether or not table columns are separated by lines.|
|EnforceEqualColumnCounts|Boolean|Whether or not to enforce rows with equal column counts. Defaults to true.|
|TextColor|ConsoleColor|The color to be used for all cell content, unless everriden by the cell's config. Defaults to current Console.ForegroundColor.|
|BackgroundColor|ConsoleColor|The background color to be used for all cells, unless overriden by the cell's config. Defaults to current Console.BackgroundColor.|
|BorderColor|ConsoleColor|The color to be used for borders. Defaults to current Console.ForegroundColor.|
|Justification|Justify|The justification to be used for all cell content, unless overriden by the cell's config. Defaults to Left.|

#### TableCellConfig

|Property|DataType|Notes|
|---|---|---|
|TextColor|ConsoleColor|The color to be used for cell content. Defaults to table configuration.|
|BackgroundColor|ConsoleColor|The background color to be used for the cell. Defaults to table configuration.|
|Justification|Justify|The justification to be used for cell content. Defaults to table configuration.|

### Example:

```csharp
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
```

Output:
![Table example](/docs/Table.png)
