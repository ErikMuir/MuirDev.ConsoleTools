namespace MuirDev.ConsoleTools.Tests;

[Collection("Sequential")]
public class TableTests
{
    private static readonly TableRow _row1 = new([new TableCell("1-1"), new TableCell("1-2"), new TableCell("1-3")]);
    private static readonly TableRow _row2 = new([new TableCell("2-1"), new TableCell("2-2"), new TableCell("2-3")]);
    private static readonly TableRow _rowWithOnlyTwoColumns = new([new TableCell("3-1"), new TableCell("3-2")]);


    #region Constructors

    [Fact]
    public void EmptyConstructor_Test()
    {
        // Arrange

        // Act
        var table = new Table();

        // Assert
        Assert.Empty(table.Rows);
        var config = table.Config;
        Assert.False(config.HasColumnLabels);
        Assert.False(config.HasRowLabels);
        Assert.False(config.TableBorder);
        Assert.False(config.RowBorder);
        Assert.False(config.ColumnBorder);
        Assert.False(config.EnforceEqualColumnCounts);
        Assert.Null(config.TextColor);
        Assert.Null(config.BackgroundColor);
        Assert.Null(config.BorderColor);
        Assert.Null(config.Justification);
    }

    [Fact]
    public void RowsConstructor_Test()
    {
        // Arrange

        // Act
        var table = new Table([_row1, _row2]);

        // Assert
        Assert.Equal(2, table.Rows.Count);
        Assert.Contains(_row1, table.Rows);
        Assert.Contains(_row2, table.Rows);
        var config = table.Config;
        Assert.False(config.HasColumnLabels);
        Assert.False(config.HasRowLabels);
        Assert.False(config.TableBorder);
        Assert.False(config.RowBorder);
        Assert.False(config.ColumnBorder);
        Assert.False(config.EnforceEqualColumnCounts);
        Assert.Null(config.TextColor);
        Assert.Null(config.BackgroundColor);
        Assert.Null(config.BorderColor);
        Assert.Null(config.Justification);
    }

    [Fact]
    public void ConfigConstructor_Test()
    {
        // Arrange
        var textColor = ConsoleColor.Blue;
        var backgroundColor = ConsoleColor.DarkGreen;
        var borderColor = ConsoleColor.Red;
        var justification = Justify.Center;
        var config = new TableConfig
        {
            HasColumnLabels = true,
            HasRowLabels = true,
            TableBorder = true,
            RowBorder = true,
            ColumnBorder = true,
            EnforceEqualColumnCounts = true,
            TextColor = textColor,
            BackgroundColor = backgroundColor,
            BorderColor = borderColor,
            Justification = justification,
        };

        // Act
        var table = new Table(config);

        // Assert
        Assert.Empty(table.Rows);
        Assert.True(table.Config.HasColumnLabels);
        Assert.True(table.Config.HasRowLabels);
        Assert.True(table.Config.TableBorder);
        Assert.True(table.Config.RowBorder);
        Assert.True(table.Config.ColumnBorder);
        Assert.True(table.Config.EnforceEqualColumnCounts);
        Assert.Equal(textColor, table.Config.TextColor);
        Assert.Equal(backgroundColor, table.Config.BackgroundColor);
        Assert.Equal(borderColor, table.Config.BorderColor);
        Assert.Equal(justification, table.Config.Justification);
    }

    [Fact]
    public void RowsConfigConstructor_Test()
    {
        // Arrange
        var textColor = ConsoleColor.Blue;
        var backgroundColor = ConsoleColor.DarkGreen;
        var borderColor = ConsoleColor.Red;
        var justification = Justify.Center;
        var config = new TableConfig
        {
            HasColumnLabels = true,
            HasRowLabels = true,
            TableBorder = true,
            RowBorder = true,
            ColumnBorder = true,
            EnforceEqualColumnCounts = true,
            TextColor = textColor,
            BackgroundColor = backgroundColor,
            BorderColor = borderColor,
            Justification = justification,
        };
        var rows = new List<TableRow> { _row1, _row2};

        // Act
        var table = new Table(rows, config);

        // Assert
        Assert.Equal(2, table.Rows.Count);
        Assert.Contains(_row1, table.Rows);
        Assert.Contains(_row2, table.Rows);
        Assert.True(table.Config.HasColumnLabels);
        Assert.True(table.Config.HasRowLabels);
        Assert.True(table.Config.TableBorder);
        Assert.True(table.Config.RowBorder);
        Assert.True(table.Config.ColumnBorder);
        Assert.True(table.Config.EnforceEqualColumnCounts);
        Assert.Equal(textColor, table.Config.TextColor);
        Assert.Equal(backgroundColor, table.Config.BackgroundColor);
        Assert.Equal(borderColor, table.Config.BorderColor);
        Assert.Equal(justification, table.Config.Justification);
    }

    [Fact]
    public void RowsConfigConstructor_Throws_Test()
    {
        // Arrange
        var rows = new List<TableRow> { _row1, _row2, _rowWithOnlyTwoColumns };
        var config = new TableConfig { EnforceEqualColumnCounts = true };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Table(rows, config));
    }

    #endregion

    #region AddRow

    [Fact]
    public void AddRow_Test()
    {
        // Arrange
        var table = new Table();

        // Act
        table.AddRow(_row1);

        // Assert
        Assert.Equal(_row1, table.Rows.Single());
    }

    [Fact]
    public void AddRow_Throws_Test()
    {
        // Arrange
        var config = new TableConfig { EnforceEqualColumnCounts = true };
        var table = new Table([_row1], config);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => table.AddRow(_rowWithOnlyTwoColumns));
        Assert.Equal(_row1, table.Rows.Single());
    }

    #endregion

    #region AddRows

    [Fact]
    public void AddRows_Test()
    {
        // Arrange
        var table = new Table();

        // Act
        table.AddRows([_row1, _row2]);

        // Assert
        Assert.Equal(2, table.Rows.Count);
        Assert.Contains(_row1, table.Rows);
        Assert.Contains(_row2, table.Rows);
    }

    [Fact]
    public void AddRows_WithDifferentColumnCounts_Test()
    {
        // Arrange
        var table = new Table();

        // Act
        table.AddRows([_row1, _row2, _rowWithOnlyTwoColumns]);

        // Assert
        Assert.Equal(3, table.Rows.Count);
        Assert.Contains(_row1, table.Rows);
        Assert.Contains(_row2, table.Rows);
        Assert.Contains(_rowWithOnlyTwoColumns, table.Rows);
    }

    [Fact]
    public void AddRows_WithDifferentColumnCounts_Throws_Test()
    {
        // Arrange
        var config = new TableConfig { EnforceEqualColumnCounts = true };
        var table = new Table([_row1], config);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => table.AddRows([_row2, _rowWithOnlyTwoColumns]));
        Assert.Equal(_row1, table.Rows.Single());
    }

    #endregion

    #region RemoveRowAt

    [Fact]
    public void RemoveRowAt_Test()
    {
        // Arrange
        var table = new Table([_row1, _row2]);

        // Act
        table.RemoveRowAt(0);

        // Assert
        Assert.Equal(_row2, table.Rows.Single());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(2)]
    public void RemoveRowAt_Throws_Test(int index)
    {
        // Arrange
        var table = new Table([_row1, _row2]);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => table.RemoveRowAt(index));
        Assert.Equal(2, table.Rows.Count);
    }

    #endregion

    #region ClearAllRows

    [Fact]
    public void ClearAllRows_Test()
    {
        // Arrange
        var table = new Table([_row1, _row2]);

        // Act
        table.ClearAllRows();

        // Assert
        Assert.Empty(table.Rows);
    }

    #endregion

    #region SetRows

    [Fact]
    public void SetRows_Test()
    {
        // Arrange
        var table = new Table([_rowWithOnlyTwoColumns]);

        // Act
        table.SetRows([_row1, _row2]);

        // Assert
        Assert.Equal(2, table.Rows.Count);
        Assert.DoesNotContain(_rowWithOnlyTwoColumns, table.Rows);
        Assert.Contains(_row1, table.Rows);
        Assert.Contains(_row2, table.Rows);
    }

    [Fact]
    public void SetRows_WithDifferentColumnCounts_Tests()
    {
        // Arrange
        var table = new Table([_row1]);

        // Act
        table.SetRows([_row2, _rowWithOnlyTwoColumns]);

        // Assert
        Assert.Equal(2, table.Rows.Count);
        Assert.DoesNotContain(_row1, table.Rows);
        Assert.Contains(_row2, table.Rows);
        Assert.Contains(_rowWithOnlyTwoColumns, table.Rows);
    }

    [Fact]
    public void SetRows_WithDifferentColumnCounts_Throws_Test()
    {
        // Arrange
        var config = new TableConfig { EnforceEqualColumnCounts = true };
        var table = new Table([_row1], config);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => table.SetRows([_row2, _rowWithOnlyTwoColumns]));
        Assert.Equal(_row1, table.Rows.Single());
    }

    #endregion
}
