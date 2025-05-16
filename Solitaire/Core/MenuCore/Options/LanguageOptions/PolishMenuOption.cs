using System.Globalization;
using Solitaire.Config;

namespace Solitaire.Core.MenuCore.Options.LanguageOptions;

public class PolishMenuOption : MenuOption
{
    public override string GetLabel()
    {
        return "Polski";
    }

    public override void Execute()
    {
        Settings.CultureCode = "pl";
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.CultureCode);
    }
}