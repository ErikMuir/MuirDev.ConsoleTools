## Menu

The Menu tool presents the user with 2 or more options, keyed by distinct characters, with optional actions to tie to the options.

#### Example:

```csharp
var console = new FluentConsole();

void Reply(int choice)
{
    console.Info($"You chose option {choice}.");
}

var items = new Dictionary<char, MenuItem>
{
    ['1'] = new("Option 1", Reply(1)),
    ['2'] = new("Option 2", Reply(2)),
    ['3'] = new("Option 3", Reply(3)),
};

new Menu(items, "Menu").Run();
```

Output:
![Menu example](/docs/Menu.png)
