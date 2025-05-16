using Solitaire.Core.MenuCore.Options;

namespace Solitaire.Core.MenuCore;

public abstract class Menu
{
    protected abstract List<MenuOption> Options { get; }

    private MenuOptionPicker optionPicker;

    protected Menu()
    {
        optionPicker = new MenuOptionPicker(Options.Count);
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
            if(optionPicker.PickedOption == i) Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{i + 1}. {Options[i].GetLabel()}");
            Console.ResetColor();
        }
    }

    private void Confirm()
    {
        Options[optionPicker.PickedOption].Execute();
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
                    menu.optionPicker.OnUpArrowPressed();
                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    menu.optionPicker.OnDownArrowPressed();
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
    