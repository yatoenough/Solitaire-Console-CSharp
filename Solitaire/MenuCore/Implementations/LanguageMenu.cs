using Figgle;
using Solitaire.I18n;
using Solitaire.MenuCore.Options;
using Solitaire.MenuCore.Options.LanguageOptions;

namespace Solitaire.MenuCore.Implementations;

public class LanguageMenu : Menu
{
    protected override List<MenuOption> Options { get; } = [
        new EnglishMenuOption(),
        new PolishMenuOption()
    ];

    protected override void DisplayMenu()
    {
        Console.WriteLine(FiggleFonts.Standard.Render(GameStrings.language_label));
    }
}