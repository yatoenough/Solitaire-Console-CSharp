using System.Globalization;
using Solitaire.Config;

namespace Solitaire.Core.MenuCore.Options.LanguageOptions;

public class EnglishMenuOption : MenuOption
{
    public override string GetLabel()
    {
        return "English";
    }

    public override void Execute()
    {
        Settings.CultureCode = "en";
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.CultureCode);
    }
}