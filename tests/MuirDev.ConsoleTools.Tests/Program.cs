using MuirDev.ConsoleTools;
using MuirDev.ConsoleTools.Tests;

var items = new List<MenuItem>
{
    new('0', "Exit", () => { }),
    new('1', "Progress Bar", Examples.ProgressBar),
    new('2', "Fixed Table", Examples.FixedColumnCount),
    new('3', "Dynamic Table", Examples.DynamicColumnCount),
    new('4', "Background Table", Examples.AmericanFlag),
};

var menu = new Menu(items, "Examples");

while (menu.Run() != '0') { }
