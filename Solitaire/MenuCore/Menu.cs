using Solitaire.MenuCore.Options;

namespace Solitaire.MenuCore;

public abstract class Menu
{
    public abstract List<MenuOption> Options { get; }

    public abstract void Display(int pickedOption);

    public void Confirm(int pickedOption)
    {
        Options[pickedOption].Execute();
    }

    public static void HandleSubMenuInteraction(Menu menu, MenuOptionPicker picker, Action? onConfirm = null, bool infinite = false)
    {
        while (true)
        {
            Console.Clear();
            menu.Display(picker.PickedOption);

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                {
                    picker.OnUpArrowPressed();
                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    picker.OnDownArrowPressed();
                    break;
                }
                case ConsoleKey.Enter:
                {
                    menu.Confirm(picker.PickedOption);
                    onConfirm?.Invoke();
                    if (infinite)
                    {
                        break;
                    }
                    return;
                }
            }
        }
    }
}
    