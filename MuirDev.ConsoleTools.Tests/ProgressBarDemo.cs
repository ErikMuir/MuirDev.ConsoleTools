namespace MuirDev.ConsoleTools.Tests;

public static class ProgressBarDemo
{
    public static void Run()
    {
        bool addRandomSleepsToSimulateRealLifeScenario = true;
        var whole = 45;
        var config = new ProgressBarConfig
        {
            Label = "Download",
            DisplayBorder = true,
            DisplayPercentage = true,
            TextColor = ConsoleColor.DarkYellow,
            BorderColor = ConsoleColor.DarkCyan,
            FillColor = ConsoleColor.DarkGreen,
        };
        var downloadStatus = new ConsoleTools.ProgressBar(whole, config);
        var part = 0;
        var rnd = new Random();

        do
        {
            downloadStatus.Update(part);
            if (addRandomSleepsToSimulateRealLifeScenario)
                Thread.Sleep(rnd.Next(250));
            part++;
        } while (part <= whole);
    }
}
