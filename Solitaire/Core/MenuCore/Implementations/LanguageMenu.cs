using Figgle;
using Solitaire.Core.MenuCore.Options;
using Solitaire.Core.MenuCore.Options.LanguageOptions;
using Solitaire.I18n;

namespace Solitaire.Core.MenuCore.Implementations;

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