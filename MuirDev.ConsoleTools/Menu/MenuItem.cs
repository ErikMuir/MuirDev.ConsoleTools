namespace MuirDev.ConsoleTools;

/// <summary>
/// Represents a menu item to be used with a Menu.
/// </summary>
public class MenuItem(string label, Action action = null)
{
    /// <summary>
    /// The label for this menu item.
    /// </summary>
    public string Label { get; set; } = label;

    /// <summary>
    /// The action to take when the user chooses this menu item. Defaults to null.
    /// </summary>
    public Action Action { get; set; } = action;
}
