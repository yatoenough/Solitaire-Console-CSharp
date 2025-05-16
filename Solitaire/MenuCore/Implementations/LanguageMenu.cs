using Figgle;
using Solitaire.I18n;
using Solitaire.MenuCore.Options;
using Solitaire.MenuCore.Options.LanguageOptions;

namespace Solitaire.MenuCore.Implementations;

public class LanguageMenu : Menu
{
    public override List<MenuOption> Options { get; } = [
        new EnglishMenuOption(),
        new PolishMenuOption()
    ];

    public override void Display()
    {
        Console.WriteLine(FiggleFonts.Standard.Render(GameStrings.language_label));
        
        DisplayOptions();
    }
}