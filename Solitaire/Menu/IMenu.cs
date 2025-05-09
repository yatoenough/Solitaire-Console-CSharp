using Solitaire.Menu.Options;

namespace Solitaire.Menu;

public interface IMenu
{
    List<IMenuOption> Options { get; }

    public void Display(int pickedOption);

    public void Confirm(int pickedOption)
    {
        Options[pickedOption].Execute();
    }

    public static void HandleSubMenuInteraction(IMenu menu, MenuOptionPicker picker, Action? onConfirm = null, bool infinite = false)
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
    