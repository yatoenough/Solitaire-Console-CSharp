using System.Globalization;
using Solitaire.Config;

namespace Solitaire.Menu.Options.LanguageOptions;

public class EnglishMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return "English";
    }

    public void Execute()
    {
        Settings.CultureCode = "en";
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.CultureCode);
    }
}