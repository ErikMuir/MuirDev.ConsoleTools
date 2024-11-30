namespace MuirDev.ConsoleTools;

/// <summary>
/// Represents a menu item to be used with a Menu.
/// </summary>
public class MenuItem(char @char, string name, Action action = null)
{
    /// <summary>
    /// A single character that the user will type to choose this menu item.
    /// </summary>
    public char Char { get; set; } = @char;

    /// <summary>
    /// The name of this menu item.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// The action to take when the user chooses this menu item. Defaults to an empty action.
    /// </summary>
    public Action Action { get; set; } = action;
}
