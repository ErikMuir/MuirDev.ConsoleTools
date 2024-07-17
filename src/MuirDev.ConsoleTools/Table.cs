using System.Linq;

namespace MuirDev.ConsoleTools;

/// <summary>
/// Used to display tabular data.
/// </summary>
public class Table(int columnCount, TableConfig config)
{
    private static readonly FluentConsole _console = new();

    private List<TableRow> _rows { get; } = [];

    public Table(int columnCount) : this(columnCount, new TableConfig()) { }

    /// <summary>
    /// The number of columns this table has.
    /// </summary>
    public readonly int ColumnCount = columnCount;

    /// <summary>
    /// The configuration for the table.
    /// </summary>
    public TableConfig Config = config;

    /// <summary>
    /// The list of rows in the table.
    /// </summary>
    public List<TableRow> Rows => _rows;

    /// <summary>
    /// Add a single row to the table.
    /// </summary>
    public void AddRow(TableRow row) => AddRows([row]);

    /// <summary>
    /// Add multiple rows to the table.
    /// </summary>
    public void AddRows(IEnumerable<TableRow> rows)
    {
        if (rows.Any(row => row.ColumnCount != ColumnCount))
            throw new ArgumentException("Row column counts must equal table column count.");
        _rows.AddRange(rows);
    }

    /// <summary>
    /// Remove a row from the table by index.
    /// </summary>
    public void RemoveRowAt(int index) => _rows.RemoveAt(index);

    /// <summary>
    /// Remove all rows from the table.
    /// </summary>
    public void ClearAllRows() => _rows.Clear();

    /// <summary>
    /// Clear all rows and add the provided rows to the table.
    /// </summary>
    public void SetRows(IEnumerable<TableRow> rows)
    {
        ClearAllRows();
        AddRows(rows);
    }

    /// <summary>
    /// Display the table.
    /// </summary>
    public void Display()
    {
        HandleDisplayOverflow(() =>
        {
            DisplayTableBorder(isTop: true);
            for (var iRow = 0; iRow < _rows.Count; iRow++)
            {
                DisplayRow(iRow);
            }
            DisplayTableBorder(isTop: false);
        });
    }

    private int TableWidth
    {
        get
        {
            var tableWidth = ColumnWidths.Sum();
            tableWidth += ColumnCount * 2;
            if (Config.TableBorder)
                tableWidth += 2;
            if (Config.ColumnBorder)
                tableWidth += ColumnCount - 1;
            return tableWidth;
        }
    }

    private List<int> ColumnWidths
    {
        get
        {
            var columnWidths = new List<int>();
            for (var col = 0; col < ColumnCount; col++)
            {
                var maxContentLength = 0;
                for (var row = 0; row < _rows.Count; row++)
                {
                    var contentLength = _rows[row].Cells[col].Content?.Length ?? 0;
                    if (contentLength > maxContentLength)
                        maxContentLength = contentLength;
                }
                columnWidths.Add(maxContentLength);
            }
            return columnWidths;
        }
    }

    private void HandleDisplayOverflow(Action action)
    {
        const string warningMessage = "** Content overflow **";
        var isOverflow = TableWidth > _console.WindowWidth;
        if (isOverflow) _console.Warning(warningMessage);
        action();
        if (isOverflow) _console.Warning(warningMessage);
    }

    private void DisplayTableBorder(bool isTop)
    {
        if (!Config.TableBorder) return;
        var sb = new StringBuilder();
        sb.Append(isTop ? Constants.SingleDownAndRight : Constants.SingleUpAndRight);
        var columnWidths = ColumnWidths;
        for (var iCol = 0; iCol < columnWidths.Count; iCol++)
        {
            if (Config.ColumnBorder && iCol > 0)
            {
                if (Config.HasRowLabels && iCol == 1)
                    sb.Append(isTop ? Constants.DoubleDownAndSingleHorizontal : Constants.DoubleUpAndSingleHorizontal);
                else
                    sb.Append(isTop ? Constants.SingleDownAndHorizontal : Constants.SingleUpAndHorizontal);
            }
            sb.Append(Constants.SingleHorizontal, columnWidths[iCol] + 2);
        }
        sb.Append(isTop ? Constants.SingleDownAndLeft : Constants.SingleUpAndLeft);
        _console.Log(sb.ToString(), new LogOptions(Config.BorderColor ?? _console.ForegroundColor));
    }

    private void DisplayRowBorder(int iRow)
    {
        if (iRow == 0) return;
        if (!Config.RowBorder && (iRow != 1 || !Config.HasColumnLabels)) return;
        var sb = new StringBuilder();
        var isDouble = Config.HasColumnLabels && iRow == 1;
        if (Config.TableBorder)
            sb.Append(isDouble ? Constants.SingleVerticalAndDoubleRight : Constants.SingleVerticalAndRight);
        var columnWidths = ColumnWidths;
        for (var iCol = 0; iCol < columnWidths.Count; iCol++)
        {
            if (Config.ColumnBorder && iCol > 0)
            {
                if (Config.HasColumnLabels && iCol == 1)
                    sb.Append(isDouble ? Constants.DoubleVerticalAndHorizontal : Constants.DoubleVerticalAndSingleHorizontal);
                else
                    sb.Append(isDouble ? Constants.SingleVerticalAndDoubleHorizontal : Constants.SingleVerticalAndHorizontal);
            }
            sb.Append(isDouble ? Constants.DoubleHorizontal : Constants.SingleHorizontal, columnWidths[iCol] + 2);
        }
        if (Config.TableBorder)
            sb.Append(isDouble ? Constants.SingleVerticalAndDoubleLeft : Constants.SingleVerticalAndLeft);
        _console.Log(sb.ToString(), new LogOptions(Config.BorderColor ?? _console.ForegroundColor));
    }

    private void DisplayCell(int iRow, int iCol, int width)
    {
        if (Config.ColumnBorder && iCol > 0)
        {
            var isDouble = Config.HasRowLabels && iCol == 1;
            var columnBorder = $"{(isDouble ? Constants.DoubleVertical : Constants.SingleVertical)}";
            _console.Log(columnBorder, new LogOptions(Config.BorderColor ?? _console.ForegroundColor, false));
        }
        var cell = _rows[iRow].Cells[iCol];
        var extraPadding = width - cell.Content.Length;
        var sb = new StringBuilder();
        sb.Append(Constants.Space);
        switch (cell.Config.Justification ?? Config.Justification ?? Justify.Left)
        {
            case Justify.Left:
                sb.Append(cell.Content);
                sb.Append(Constants.Space, extraPadding);
                break;
            case Justify.Center:
                sb.Append(Constants.Space, (int)Math.Floor(extraPadding / 2.0));
                sb.Append(cell.Content);
                sb.Append(Constants.Space, (int)Math.Ceiling(extraPadding / 2.0));
                break;
            case Justify.Right:
                sb.Append(Constants.Space, extraPadding);
                sb.Append(cell.Content);
                break;
            default:
                throw new ArgumentException("Unsupported justification.");
        }
        sb.Append(Constants.Space);
        _console.Log(sb.ToString(), new LogOptions(cell.Config.TextColor ?? Config.TextColor ?? _console.ForegroundColor, false));
    }

    private void DisplayRow(int iRow)
    {
        DisplayRowBorder(iRow);
        var borderOptions = new LogOptions
        {
            ForegroundColor = Config.BorderColor ?? _console.ForegroundColor,
            IsEndOfLine = false,
        };
        if (Config.TableBorder)
            _console.Log(Constants.SingleVertical.ToString(), borderOptions);
        var columnWidths = ColumnWidths;
        for (var iCol = 0; iCol < columnWidths.Count; iCol++)
        {
            DisplayCell(iRow, iCol, columnWidths[iCol]);
        }
        if (Config.TableBorder)
            _console.Log(Constants.SingleVertical.ToString(), borderOptions);
        _console.Log("");
    }
}
