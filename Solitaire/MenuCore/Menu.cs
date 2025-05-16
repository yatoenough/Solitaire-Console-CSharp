using Solitaire.MenuCore.Options;

namespace Solitaire.MenuCore;

public abstract class Menu
{
    protected abstract List<MenuOption> Options { get; }

    private MenuOptionPicker OptionPicker;

    protected Menu()
    {
        OptionPicker = new MenuOptionPicker(Options.Count);
    }

    protected abstract void DisplayMenu();

    private void Display()
    {
        DisplayMenu();
        DisplayOptions();
    }

    private void DisplayOptions()
    {
        for (int i = 0; i < Options.Count; i++)
        {
            if(OptionPicker.PickedOption == i) Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{i + 1}. {Options[i].GetLabel()}");
            Console.ResetColor();
        }
    }

    private void Confirm()
    {
        Options[OptionPicker.PickedOption].Execute();
    }

    public static void Handle(Menu menu, Action? onConfirm = null, bool infinite = false)
    {
        while (true)
        {
            Console.Clear();
            menu.Display();

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                {
                    menu.OptionPicker.OnUpArrowPressed();
                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    menu.OptionPicker.OnDownArrowPressed();
                    break;
                }
                case ConsoleKey.Enter:
                {
                    menu.Confirm();
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
    