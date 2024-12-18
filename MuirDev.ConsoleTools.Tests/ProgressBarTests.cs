namespace MuirDev.ConsoleTools.Tests;

[Collection("Sequential")]
public class ProgressBarTests
{
    private static readonly ProgressBarConfig _noDisplay = new() { Display = false };

    [Fact]
    public void ProgressBar_Update_With_Negative_Value_Sets_Error_Message_Test()
    {
        var testObject = new ProgressBar(100, _noDisplay);
        Assert.Null(testObject.ErrorMessage);
        testObject.Update(-1);
        Assert.Equal("Value cannot be negative.", testObject.ErrorMessage);
    }

    [Fact]
    public void ProgressBar_Update_After_Ended_Sets_Error_Message_Test()
    {
        var testObject = new ProgressBar(100, _noDisplay);
        testObject.End();
        Assert.Null(testObject.ErrorMessage);
        testObject.Update(1);
        Assert.Equal("Progress complete.", testObject.ErrorMessage);
    }

    [Fact]
    public void ProgressBar_Update_Sets_StartTime_Test()
    {
        var testObject = new ProgressBar(100, _noDisplay);
        Assert.Null(testObject.StartTime);
        testObject.Update(1);
        Assert.NotNull(testObject.StartTime);
    }

    [Fact]
    public void ProgressBar_Update_Sets_EndTime_Test()
    {
        var testObject = new ProgressBar(100, _noDisplay);
        Assert.Null(testObject.EndTime);
        testObject.Update(100);
        Assert.NotNull(testObject.EndTime);
    }

    [Fact]
    public void ProgressBar_Update_Sets_Part_Test()
    {
        var testObject = new ProgressBar(100, _noDisplay);
        Assert.Equal(0, testObject.Part);
        testObject.Update(3);
        Assert.Equal(3, testObject.Part);
    }
}
