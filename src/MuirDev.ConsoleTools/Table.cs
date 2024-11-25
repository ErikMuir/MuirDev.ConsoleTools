using System.Linq;

namespace MuirDev.ConsoleTools;

/// <summary>
/// Used to display tabular data.
/// </summary>
public class Table
{
    private static readonly FluentConsole _console = new();

    private List<TableRow> _rows { get; } = [];

    /// <summary>
    /// Instantiates a Table with provided rows and provided configuration.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if all rows don't have the same number of columns.</exception>
    public Table(IEnumerable<TableRow> rows, TableConfig config)
    {
        _rows = rows.ToList();
        Config = config;
        EnforceEqualColumnCounts(rows);
    }

    /// <summary>
    /// Instantiates an empty Table with default configuration.
    /// </summary>
    public Table() : this([], new TableConfig()) { }

    /// <summary>
    /// Instantiates a Table with provided rows and default configuration.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if all rows don't have the same number of columns.</exception>
    public Table(IEnumerable<TableRow> rows) : this(rows, new TableConfig()) { }

    /// <summary>
    /// Instantiates an empty Table with provided configuration.
    /// </summary>
    public Table(TableConfig config) : this([], config) { }



    /// <summary>
    /// The configuration for the table.
    /// </summary>
    public TableConfig Config;

    /// <summary>
    /// The list of rows in the table.
    /// </summary>
    public List<TableRow> Rows => _rows;

    /// <summary>
    /// Returns the number of columns in the table.
    /// </summary>
    public int MaxColumnCount => _rows.Count > 0 ? _rows.Select(row => row.ColumnCount).Max() : 0;

    /// <summary>
    /// Add a single row to the table.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if all rows don't have the same number of columns.</exception>
    public void AddRow(TableRow row) => AddRows([row]);

    /// <summary>
    /// Add multiple rows to the table.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if all rows don't have the same number of columns.</exception>
    public void AddRows(IEnumerable<TableRow> rows)
    {
        EnforceEqualColumnCounts(rows);
        _rows.AddRange(rows);
    }

    /// <summary>
    /// Remove a row from the table by index.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if index is less than 0 or greater than the row count minus 1.</exception> 
    public void RemoveRowAt(int index) => _rows.RemoveAt(index);

    /// <summary>
    /// Remove all rows from the table.
    /// </summary>
    public void ClearAllRows() => _rows.Clear();

    /// <summary>
    /// Clear all rows and add the provided rows to the table.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if all rows don't have the same number of columns.</exception>
    public void SetRows(IEnumerable<TableRow> rows)
    {
        if (Config.EnforceEqualColumnCounts && rows.Select(row => row.ColumnCount).Distinct().Count() > 1)
            throw new ArgumentException("Rows must all have equal column counts.");
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

    private void EnforceEqualColumnCounts(IEnumerable<TableRow> rows = null)
    {
        if (!Config.EnforceEqualColumnCounts)
            return;
        if (_rows.Concat(rows).Select(row => row.ColumnCount).Distinct().Count() > 1)
            throw new ArgumentException("Rows must all have equal column counts.");
    }

    private int TableWidth
    {
        get
        {
            var tableWidth = ColumnWidths.Sum();
            tableWidth += MaxColumnCount * 2;
            if (Config.TableBorder)
                tableWidth += 2;
            if (Config.ColumnBorder)
                tableWidth += MaxColumnCount - 1;
            return tableWidth;
        }
    }

    private List<int> ColumnWidths
    {
        get
        {
            var columnWidths = new List<int>();
            for (var iCol = 0; iCol < MaxColumnCount; iCol++)
            {
                var maxContentLength = 0;
                for (var iRow = 0; iRow < _rows.Count; iRow++)
                {
                    var row = _rows[iRow];
                    if (row.ColumnCount <= iCol) continue;
                    var cell = row.Cells[iCol];
                    var contentLength = cell.Content?.Length ?? 0;
                    maxContentLength = Math.Max(contentLength, maxContentLength);
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

    private ConsoleColor BorderColor => Config.BorderColor ?? _console.ForegroundColor;
    private ConsoleColor BorderBackgroundColor => Config.BorderBackgroundColor ?? Config.BackgroundColor ?? _console.BackgroundColor;
    private ConsoleColor TextColor => Config.TextColor ?? _console.ForegroundColor;
    private ConsoleColor BackgroundColor => Config.BackgroundColor ?? _console.BackgroundColor;

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
        _console.Log(sb.ToString(), new LogOptions(BorderColor, BorderBackgroundColor));
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
        _console.Log(sb.ToString(), new LogOptions(BorderColor, BorderBackgroundColor));
    }

    private void DisplayCell(int iRow, int iCol, int width)
    {
        if (Config.ColumnBorder && iCol > 0)
        {
            var isDouble = Config.HasRowLabels && iCol == 1;
            var columnBorder = $"{(isDouble ? Constants.DoubleVertical : Constants.SingleVertical)}";
            _console.Log(columnBorder, new LogOptions(BorderColor, BorderBackgroundColor, false));
        }
        var row = _rows[iRow];
        var cell = row.ColumnCount > iCol ? row.Cells[iCol] : new TableCell("");
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
        var color = cell.Config.TextColor ?? TextColor;
        var background = cell.Config.BackgroundColor ?? BackgroundColor;
        _console.Log(sb.ToString(), new LogOptions(color, background, false));
    }

    private void DisplayRow(int iRow)
    {
        DisplayRowBorder(iRow);
        var borderOptions = new LogOptions
        {
            ForegroundColor = BorderColor,
            BackgroundColor = BorderBackgroundColor,
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
