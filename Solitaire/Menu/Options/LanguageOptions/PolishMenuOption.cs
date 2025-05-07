using System.Globalization;
using Solitaire.Config;

namespace Solitaire.Menu.Options.LanguageOptions;

public class PolishMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return "Polski";
    }

    public void Execute()
    {
        Settings.CultureCode = "pl";
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.CultureCode);
    }
}