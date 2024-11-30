using MuirDev.ConsoleTools;
using MuirDev.ConsoleTools.Tests;

var options = new Dictionary<char, string>
{
    ['0'] = "Exit",
    ['1'] = "Progress Bar",
    ['2'] = "Fixed Table",
    ['3'] = "Dynamic Table",
    ['4'] = "Background Table",
};

var actions = new Dictionary<char, Action>
{
    ['0'] = () => {},
    ['1'] = Examples.ProgressBar,
    ['2'] = Examples.FixedColumnCount,
    ['3'] = Examples.DynamicColumnCount,
    ['4'] = Examples.AmericanFlag,
};

var menu = new Menu(options, "Examples");

while (true)
{
    var result = menu.Run();
    if (result == '0') return;
    actions[result]();
}
