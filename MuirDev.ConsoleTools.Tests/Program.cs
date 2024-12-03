using MuirDev.ConsoleTools;
using MuirDev.ConsoleTools.Tests;

static void Run(Action demo)
{
    Console.WriteLine();
    demo();
    Console.WriteLine();
}

var items = new Dictionary<char, MenuItem>
{
    ['0'] = new MenuItem(">>> Exit <<<", () => { }),
    ['1'] = new MenuItem("FluentConsole", () => Run(FluentConsoleDemo.Run)),
    ['2'] = new MenuItem("Confirm", () => Run(ConfirmDemo.Run)),
    ['3'] = new MenuItem("ProgressBar", () => Run(ProgressBarDemo.Run)),
    ['4'] = new MenuItem("Table: Fixed", () => Run(TableFixedDemo.Run)),
    ['5'] = new MenuItem("Table: Dynamic", () => Run(TableDynamicDemo.Run)),
    ['6'] = new MenuItem("Table: Backgrounds", () => Run(TableBackgroundDemo.Run)),
};

var menu = new Menu(items, "Demos");

while (menu.Run() != '0') { }
