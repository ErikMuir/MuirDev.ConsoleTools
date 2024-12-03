namespace MuirDev.ConsoleTools.Tests;

public static class ConfirmDemo
{
    public static void Run()
    {
        var question = "Are you sure you want to delete all logs?";
        var confirm = new Confirm(question, false).Run(LogType.Warning);
        new FluentConsole().Info($"Your answer: {(confirm ? "Yes" : "No")}");
    }
}
