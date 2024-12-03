namespace MuirDev.ConsoleTools.Tests;

public static class FluentConsoleDemo
{
    public static void Run()
    {
        var console = new FluentConsole();

        console
            .LineFeed(2)
            .Info("This text is white.")
            .LogSeparator()
            .Success("This text is dark green.")
            .Warning("This text is dark yellow.")
            .Failure("This text is dark red.");
    }
}
