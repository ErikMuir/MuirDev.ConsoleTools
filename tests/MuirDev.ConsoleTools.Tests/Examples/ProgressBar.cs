namespace MuirDev.ConsoleTools.Tests;

public static partial class Examples
{
    public static void ProgressBar()
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
        var testObject = new ProgressBar(whole, config);
        var part = 0;
        var rnd = new Random();
        do
        {
            testObject.Update(part);
            if (addRandomSleepsToSimulateRealLifeScenario)
                Thread.Sleep(rnd.Next(250));
            part++;
        } while (part <= whole);
        Console.WriteLine();
    }
}
